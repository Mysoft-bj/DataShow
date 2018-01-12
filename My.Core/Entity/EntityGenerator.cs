using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using My.Core.Sql;
using System.Data.Common;
namespace My.Core
{
    public class EntityGenerator
    {
        public string ConnectionString { get; set; }
        public string NameSpace { get; set; }
        public string TableName { get; set; }
        Database _db;
        public EntityGenerator(string connectionString, string ns, string tableName)
        {
            ConnectionString = connectionString;
            NameSpace = ns;
            TableName = tableName;

        }
        StringBuilder _sb = new StringBuilder();      
        int _indent = 0;
        class ColumnInfo {
            public string ColumnName { get; set; }
            public string DbType { get; set; }
            public string DefaultValue { get; set; }
            public int? MaxLength { get; set; }
            public Type DataType { get; set; }
            public int IsNullable { get; set; }
        }
        /// <summary>
        /// 获取大小写敏感的数据库表名
        /// </summary>
        /// <returns></returns>
        string GetTableName() {
          return  _db.ExecuteScalar<string>("SELECT * FROM  sys.objects WHERE object_id=object_id('" + TableName + "')");
        }
        /// <summary>
        /// 获取表的主键
        /// </summary>
        /// <returns></returns>
        string GetPKColumn()
        {
            var pkColumnSql = @"SELECT a.name 
  FROM   syscolumns a 
  inner  join sysobjects d on a.id=d.id       
  where  d.name=@0 and exists(SELECT 1 FROM sysobjects where xtype='PK' and  parent_obj=a.id and name in (  
  SELECT name  FROM sysindexes   WHERE indid in(  
  SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid  
)))";
            return _db.ExecuteScalar<string>(pkColumnSql,TableName);
        }
        public DataTable GetDataTable(string sql){
            _db.OpenSharedConnection();
            IDbCommand cmd =_db.Connection.CreateCommand();
            cmd.Connection = _db.Connection;
            cmd.CommandText = sql;
            DbDataAdapter da = _db.DbProviderFactory.CreateDataAdapter();
            da.SelectCommand = cmd as DbCommand;
            cmd.CommandTimeout = 300;
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
          
            return ds.Tables[0];
        }
         
        Dictionary<string, string> GetTableDesc() { 
             Dictionary<string, string> descDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
             var sql = "SELECT table_name_c,field_name,field_name_c,b_pk FROM dbo.data_dict WHERE table_name='" + TableName + "'";
             var tabel = GetDataTable(sql);
             foreach (DataRow row in tabel.Rows)
             {
                 descDict[TableName] = row["table_name_c"].ToString();
                 descDict[row["field_name"].ToString()] = row["field_name_c"].ToString();
             }
             return descDict;
        }
        /// <summary>
        /// 生成实体类方法
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            _db = new Database(ConnectionString, "System.Data.SqlClient");
           
            var tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName)) return "";          
             var    pkColumn =GetPKColumn();          

            var descDict = GetTableDesc();
            string tabledesc = "";
            descDict.TryGetValue(tableName, out tabledesc);

            //namespace
            AddImport();
            AddLine("namespace " + NameSpace);
            AddLine("{");

            //class
            _indent++;
            AddSummary(tabledesc);

            AddLine("public partial class " + tableName +":Entity");
            AddLine("{");
            _indent++;
            #region 构造函数

            string coloinfoSql = @"SELECT  COLUMN_NAME ColumnName ,
        COLUMN_DEFAULT DefaultValue ,
        DATA_TYPE DbType ,
        CAST(CHARACTER_MAXIMUM_LENGTH AS INT) MaxLength ,
        CASE WHEN is_nullable = 'yes' THEN 1
             ELSE 0
        END isnullable
FROM    information_schema.COLUMNS
WHERE   TABLE_NAME = @0 ";
            var columns = _db.Query<ColumnInfo>(coloinfoSql, tableName).ToList();
            AddLine("public " + tableName + "()");
            AddLine("{");
            _indent++;

            foreach (var col in columns)
            {
                col.DataType = SqlServerType.SqlType2CsharpType(SqlServerType.SqlTypeString2SqlType(col.DbType));
                string columnName = col.ColumnName;

                //if (!string.IsNullOrEmpty(col.DefaultValue)) {
                //    var defVal = col.DefaultValue.Replace("(", "").Replace(")", "").Replace("'","");
                //    if (col.DataType.IsPrimitive || col.DataType == typeof(decimal))
                //    {

                //        AddLine("this." + columnName + " = " + defVal + " ;");
                //    }
                //    else if (col.DataType == typeof(string))
                //    {

                //        AddLine("this." + columnName + " = \"" + defVal + "\";");
                //    }
                //}
               
            }


            _indent--;
            AddLine("}");

            #endregion
            AddLine("");
            #region 属性

            foreach (var col in columns)
            {
                if (col.DbType.Equals("timestamp", StringComparison.OrdinalIgnoreCase))
                    continue;
                string allowDBNull = ((col.DataType.IsPrimitive || col.DataType == typeof(DateTime)) && col.IsNullable == 1 && string.IsNullOrEmpty(col.DefaultValue)) ? "?" : "";
                string desc;
                descDict.TryGetValue(col.ColumnName, out desc);
                AddSummary(desc);
                if (col.DataType == typeof(string) && col.MaxLength.HasValue && col.MaxLength > 0)
                {
                    AddLine("[StringLength(" + col.MaxLength.Value + ")]");
                }
                if (pkColumn == col.ColumnName)
                {
                    AddLine("[Key]");
                    AddLine("[Column(\"" + col.ColumnName + "\")]");

                    AddLine("public " + GetFiledType(col.DataType) + allowDBNull + " Id" + " { get; set; }");

                }
                else
                    AddLine("public " + GetFiledType(col.DataType) + allowDBNull + " " + col.ColumnName + " { get; set; }");


                //AddLine("[DbType(SqlDbType." + SqlServerType.SqlTypeString2SqlType(col.DbType) + ")]");

                AddLine("");
            }

            #endregion

            //end
            _indent--;
            AddLine("}");
            _indent--;
            AddLine("}");

            return _sb.ToString();
        }

        /// <summary>
        /// 引入名字空间
        /// </summary>
        void AddImport()
        {
            AddLine("using System;");
            AddLine("using System.Data;");
            AddLine("using My.Core;");
            AddLine("using System.ComponentModel.DataAnnotations;");
            AddLine("  using System.ComponentModel.DataAnnotations.Schema;");


        }

        void AddLine(string text)
        {
            text = text ?? "";
            var whitespace = "";
            for (int i = 0; i < _indent; i++)
            {
                whitespace += "\t";
            }
            _sb.AppendLine(whitespace + text);
        }

        void AddSummary(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            AddLine("/// <summary>");
            AddLine("///" + text);
            AddLine("/// </summary>");
        }
        string GetFiledType(Type type)
        {
            
            return type.Name;
        }

    }

}

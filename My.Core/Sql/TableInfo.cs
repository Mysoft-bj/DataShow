using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace My.Core.Sql
{
    public class TableInfo
    {
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }
        public string SequenceName { get; set; }
        public string AutoAlias { get; set; }

        public static TableInfo FromPoco(Type t)
        {
            var tableInfo = new TableInfo();

          
            var a = t.GetCustomAttributes(typeof(TableAttribute), true);
            tableInfo.TableName = a.Length == 0 ? t.Name : (a[0] as TableAttribute).Name;
             tableInfo.PrimaryKey="Id";
            tableInfo.SequenceName="Id";
            foreach (var pi in t.GetProperties())
            {
                a = pi.GetCustomAttributes(typeof(KeyAttribute), true);
                if (a.Length > 0)
                {
                    KeyAttribute idAttri = a[0] as KeyAttribute;
                    tableInfo.PrimaryKey =  pi.Name;
                    tableInfo.SequenceName =  pi.Name;
                    tableInfo.AutoIncrement = false;
                    break;
                }
            } 
            return tableInfo;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.Core.Helper;


namespace My.Core
{
    public class IDProperty : PropertyEx
    {
        public IDProperty(PropertyDescriptor property, KeyAttribute idAttribute)
            : base(property)
        {
            IsKeyColumn = true;
            KeyAttribute = idAttribute;
        }

        public KeyAttribute KeyAttribute { get; set; }
    }
    public class PropertyEx
    {
        public static PropertyEx Create(PropertyDescriptor property)
        {
            var key = property.Attributes.OfType<KeyAttribute>().FirstOrDefault();
            if (key != null)
            {
                return new IDProperty(property, key);
            }
            else
                return new PropertyEx(property);
        }

        protected PropertyEx(PropertyDescriptor property)
        {
            PropertyDescriptor = new MyPropertyDescriptor(property);
            Name = property.Name;
            var prop = PropertyInfo = property.ComponentType.GetProperty(property.Name);
            GetValue = prop.ComplieGetMethod();
            SetValue = prop.ComplieSetMethod();
            Attributes = PropertyDescriptor.Attributes;
            var display = Attributes.OfType<DisplayAttribute>().FirstOrDefault();
            DisplayName = property.Name;
            if (display != null)
                DisplayName = display.Name;
            SortableAttribute = Attributes.OfType<SortableAttribute>().FirstOrDefault();
           
                RequiredAttribute = Attributes.OfType<RequiredAttribute>().FirstOrDefault();
           
            IndexAttribute = Attributes.OfType<IndexAttribute>().FirstOrDefault();
            StringLengthAttribute = Attributes.OfType<StringLengthAttribute>().FirstOrDefault();

            
            if (Attributes.OfType<NotMappedAttribute>().FirstOrDefault() != null)
            {
                IsNotMapped = true;
                return;
            }
            if (Attributes.OfType<HasOneAttribute>().FirstOrDefault() != null)
            {
                HasOneAttribute = Attributes.OfType<HasOneAttribute>().FirstOrDefault();
                HasOneAttribute.ItemType = PropertyDescriptor.PropertyType;
                if (HasOneAttribute.ReferenceID.IsNullOrWhiteSpace())
                    HasOneAttribute.ReferenceID = GetReferenceID(PropertyDescriptor.PropertyType);               
            }

            if (Attributes.OfType<HasManyAttribute>().FirstOrDefault() != null)
            {
                HasManyAttribute = Attributes.OfType<HasManyAttribute>().FirstOrDefault();
                HasManyAttribute.ItemType = TypeHelper.GetElementType(PropertyDescriptor.PropertyType);

                if (HasManyAttribute.ParentKey.IsNullOrWhiteSpace()) {
                    HasManyAttribute.ParentKey = GetReferenceID(prop.DeclaringType);    
                }
            }
            if (Attributes.OfType<HasMany2ManyAttribute>().FirstOrDefault() != null)
            {

                HasMany2ManyAttribute = Attributes.OfType<HasMany2ManyAttribute>().FirstOrDefault();
                HasMany2ManyAttribute.ItemType = TypeHelper.GetElementType(PropertyDescriptor.PropertyType);
                if (HasMany2ManyAttribute.MidTable.IsNullOrWhiteSpace())
                    HasMany2ManyAttribute.MidTable = GetRelationTable(prop.DeclaringType, HasMany2ManyAttribute.ItemType);
                if (HasMany2ManyAttribute.ParentKey.IsNullOrWhiteSpace())
                {
                    HasMany2ManyAttribute.ParentKey = GetReferenceID(prop.DeclaringType);
                }
                if (HasMany2ManyAttribute.ChildKey.IsNullOrWhiteSpace())
                {
                    HasMany2ManyAttribute.ChildKey = GetReferenceID(HasMany2ManyAttribute.ItemType);
                }


            }           

            var column = Attributes.OfType<ColumnAttribute>().FirstOrDefault() ?? new ColumnAttribute();
            ColumnName = column.Name ?? Name;

        }
      
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public MyPropertyDescriptor PropertyDescriptor { get; private set; }
        public string Name { get; set; }
        public Action<object, object> SetValue { get; private set; }
        public Func<object, object> GetValue { get; private set; }
        public AttributeCollection Attributes { get; private set; }
        public HasOneAttribute HasOneAttribute { get; set; }
        public HasMany2ManyAttribute HasMany2ManyAttribute { get; set; }
        public HasManyAttribute HasManyAttribute { get; set; }
        public SortableAttribute SortableAttribute { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public bool IsNotMapped { get; set; }
        public RequiredAttribute RequiredAttribute { get; set; }
        public bool IsKeyColumn { get; set; }
        public StringLengthAttribute StringLengthAttribute { get; set; }
        public IndexAttribute IndexAttribute { get; set; }
        private string GetReferenceID(Type type) {
            return MetaDataHelper.GetTableName(type) + "Id";
           
        }

        private string GetRelationTable(Type type1, Type type2)
        {
            string table1 = MetaDataHelper.GetTableName(type1);
            string table2 = MetaDataHelper.GetTableName(type2);
            return table1 + '_' + table2;
        }
       
    }

    public class MetaData
    {       
        public Dictionary<string, PropertyEx> RelationColumns { get; private set; }
        public string TableName { get; set; }
        public IDProperty KeyColumn { get; set; }
        public Dictionary<string, PropertyEx> Properties { get; private set; }
        internal MetaData(Type type)
        {
            RelationColumns = new Dictionary<string, PropertyEx>(StringComparer.OrdinalIgnoreCase);
            Properties = new Dictionary<string, PropertyEx>(StringComparer.OrdinalIgnoreCase);
            var typeAttr = TypeDescriptor.GetAttributes(type);
           
            var arrs = TypeDescriptor.GetProperties(type);
            TableName =MetaDataHelper. GetTableName(type);
            for (int idx = 0; idx < arrs.Count; idx++)
            {
                var prop = PropertyEx.Create(arrs[idx]);
                Properties[prop.Name] = prop;
                
                if (prop is IDProperty)
                    KeyColumn = (IDProperty)prop;

            }
        }


    }
    internal class MetaData<T> 
    {
        internal static MetaData Instance = new MetaData(typeof(T));

    }
    public class MetaDataHelper
    {
        internal static string GetTableName(Type type)
        {
            var baseType = type;
            while (!baseType.IsAbstract)
            {
                var typeAttr = TypeDescriptor.GetAttributes(type);
                TableAttribute table = typeAttr.OfType<TableAttribute>().FirstOrDefault();
                if (table != null)
                    return table.Name;

                if (baseType.BaseType==null|| baseType.BaseType.IsAbstract)
                    return baseType.Name;
                baseType = baseType.BaseType;
            }
            return baseType.Name;
        }
        public static MetaData GetMetaData<T>()
        {
            return MetaData<T>.Instance;
        }
        public static MetaData GetMetaData(Type type)
        {
            return _cacheMetaData.GetOrAdd(type, t => MyReflectionHelper.InvokeMethod(typeof(MetaDataHelper), "GetMetaData", new Type[] { t }) as MetaData);


        }

        public static object GetValue<T>(T entity, string propName) {
            var meta = MetaDataHelper.GetMetaData(entity.GetType());
            return meta.Properties[propName].GetValue(entity);
        }
        public static void SetValue<T>(T entity, string propName,object newVal)
        {
            var meta = MetaDataHelper.GetMetaData(entity.GetType());
             meta.Properties[propName].SetValue(entity,newVal);
        }
        static ConcurrentDictionary<Type, MetaData> _cacheMetaData = new ConcurrentDictionary<Type, MetaData>();

       
    }
}

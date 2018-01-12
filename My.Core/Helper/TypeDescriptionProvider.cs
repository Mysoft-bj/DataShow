using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;


namespace My.Core
{



    public class MyTypeDescriptionProvider : TypeDescriptionProvider
    {

        public MyTypeDescriptionProvider(Type type, Type entityType) : this(TypeDescriptor.GetProvider(type), entityType) { }
        public MyTypeDescriptionProvider(TypeDescriptionProvider parent, Type entityType) : base(parent) { EntityType = entityType; }
        public static void Add<TEntity>(Type type)
        {
            var entityMetaData = MetaDataHelper.GetMetaData<TEntity>();
           var table=   TypeDescriptor.GetAttributes(type).OfType<TableAttribute>().FirstOrDefault();
           if (table == null)
               TypeDescriptor.AddAttributes(type, new TableAttribute(entityMetaData.TableName));
            TypeDescriptionProvider parent = TypeDescriptor.GetProvider(type);
            var entityType = typeof(TEntity);
            MyTypeDescriptionProvider td = new MyTypeDescriptionProvider(parent, entityType);


            TypeDescriptor.AddProvider(td, type);
           
        }
        public Type EntityType { get; set; }
        public static void Clear(Type type)
        {
            lock (descriptors)
            {
                descriptors.Remove(type);
            }
        }
        public static void Clear()
        {
            lock (descriptors)
            {
                descriptors.Clear();
            }
        }
        private static readonly Dictionary<Type, ICustomTypeDescriptor> descriptors = new Dictionary<Type, ICustomTypeDescriptor>();
        public sealed override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor descriptor;
            lock (descriptors)
            {
                if (!descriptors.TryGetValue(objectType, out descriptor))
                {
                    try
                    {
                        var subclass = descriptors.Keys.FirstOrDefault(o => objectType.IsAssignableFrom(o));
                        if (subclass != default(Type))
                        {
                            return descriptors[subclass];
                        }
                        descriptor = BuildDescriptor(objectType);

                    }
                    catch
                    {
                        return base.GetTypeDescriptor(objectType, instance);
                    }
                }
                return descriptor;
            }
        }

        private ICustomTypeDescriptor BuildDescriptor(Type objectType)
        {
            // NOTE: "descriptors" already locked here

            // get the parent descriptor and add to the dictionary so that
            // building the new descriptor will use the base rather than recursing
            ICustomTypeDescriptor descriptor = base.GetTypeDescriptor(objectType, null);
            descriptors.Add(objectType, descriptor);
            try
            {
                // build a new descriptor from this, and replace the lookup
                descriptor = new MyCustomTypeDescriptor(descriptor, this.EntityType);
                descriptors[objectType] = descriptor;
               
                return descriptor;
            }
            catch
            {   // rollback and throw
                // (perhaps because the specific caller lacked permissions;
                // another caller may be successful)
                descriptors.Remove(objectType);
                throw;
            }
        }
    }

    /// <summary>
    /// Represents a custom TypeDescriptor that wraps the real TypeDescriptor, but overrides
    /// both GetProperties() *and* GetProperties(Attribute[]).
    /// </summary>
    internal class MyCustomTypeDescriptor : CustomTypeDescriptor
    {
        private readonly PropertyDescriptorCollection _propertyCollections;
        public Type EntityType { get; set; }

        public MyCustomTypeDescriptor(ICustomTypeDescriptor parent, Type genType)
            : base(parent)
        {
            EntityType = genType;
            var propertyCollections = parent.GetProperties();
            var entityMetaData = MetaDataHelper.GetMetaData(EntityType);

            var entityProps = entityMetaData.Properties;
            List<PropertyDescriptor> propertyList = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in propertyCollections)
            {
                var myProperty = new MyPropertyDescriptor(descriptor);
                var entityProperty = entityProps[myProperty.Name];

                if (entityProperty != null)
                {
                    List<Attribute> attriList = new List<Attribute>();
                    for (int i = 0; i < entityProperty.Attributes.Count; i++)
                    {
                        attriList.Add(entityProperty.Attributes[i]);
                    }
                    if (attriList.Count > 0)
                        myProperty.AddAttributes(attriList.ToArray());
                }
                else
                {
                    myProperty.AddAttributes(new NotMappedAttribute());
                }

                propertyList.Add(myProperty);
            }
            _propertyCollections = new PropertyDescriptorCollection(propertyList.ToArray());
        }

        public sealed override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _propertyCollections;
        }


        public sealed override PropertyDescriptorCollection GetProperties()
        {

            return _propertyCollections;
        }

    }

    /// <summary>
    /// Represents a custom PropertyDescriptor that wraps the real PropertyDescriptor, and enforces
    /// any ValidationAttributes defined on the associated property.
    /// </summary>
    public class MyPropertyDescriptor : PropertyDescriptor
    {

        public MyPropertyDescriptor(PropertyDescriptor wrappedPropertyDescriptor)
            : base(wrappedPropertyDescriptor)
        {
            _orginal = wrappedPropertyDescriptor;
        }

        PropertyDescriptor _orginal;


        public override void AddValueChanged(object component, EventHandler handler)
        {
            this._orginal.AddValueChanged(component, handler);
        }

        public override bool CanResetValue(object component)
        {
            return this._orginal.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get
            {
                return this._orginal.ComponentType;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this._orginal.IsReadOnly;
            }
        }

        public override object GetValue(object component)
        {
            return this._orginal.GetValue(component);
        }

        public override Type PropertyType
        {
            get
            {
                return this._orginal.PropertyType;
            }
        }

        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            this._orginal.RemoveValueChanged(component, handler);
        }

        public override void ResetValue(object component)
        {
            this._orginal.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            List<Attribute> attributes = new List<Attribute>();
            this.FillAttributes(attributes);

            foreach (Attribute attribute in attributes)
            {
                ValidationAttribute validationAttribute = attribute as ValidationAttribute;
                if (null == validationAttribute)
                    continue;

                //if (!validationAttribute.IsValid(value))
                //    throw new ValidationException(validationAttribute.ErrorMessage, validationAttribute, component);
            }

            this._orginal.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this._orginal.ShouldSerializeValue(component);
        }

        public override bool SupportsChangeEvents
        {
            get
            {
                return this._orginal.SupportsChangeEvents;
            }
        }

        public void AddAttributes(params Attribute[] newAttrs)
        {

            if (_orginal.Attributes == null)
                throw new ArgumentNullException("existing");

            List<Attribute> newattributes = new List<Attribute>();

            for (int idx = 0; idx < _orginal.Attributes.Count; idx++)
            {
                newattributes.Add(_orginal.Attributes[idx]);
            }
            foreach (var attribute in newAttrs)
            {
                if (!newattributes.Exists(o => o.TypeId.Equals(attribute.TypeId)))
                {
                    newattributes.Add(attribute);
                }
            }

            this.AttributeArray = newattributes.ToArray();
        }
    }

}
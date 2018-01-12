using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate;
using FluentNHibernate.Mapping;
namespace My.NHibernate

{
   // public class EnumConventionMap : ClassMap<EnumConvention> { 
   //public EnumConvention(){
  
   //}
   // }
    public class EnumConvention : IUserTypeConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            //criteria.Expect(x => x.Property.PropertyType.IsEnum);
            criteria.Expect(x => x.Property.PropertyType.IsEnum ||
          (x.Property.PropertyType.IsGenericType &&
           x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
           x.Property.PropertyType.GetGenericArguments()[0].IsEnum)
          );
        }

        public void Apply(IPropertyInstance target)
        {
            target.CustomType(target.Property.PropertyType);
        }
    } 

}

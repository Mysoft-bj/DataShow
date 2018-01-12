using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace My.Core.Sql
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ComputedColumnAttribute : ColumnAttribute
    {
        public ComputedColumnAttribute() { }
        public ComputedColumnAttribute(string name) : base(name) { }
    }
}
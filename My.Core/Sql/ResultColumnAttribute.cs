using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace My.Core.Sql
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ResultColumnAttribute : ColumnAttribute
    {
        public ResultColumnAttribute() { }
        public ResultColumnAttribute(string name) : base(name) { }
    }
}
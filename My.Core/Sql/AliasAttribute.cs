using System;

namespace My.Core.Sql
{
    public class AliasAttribute : Attribute
    {
        public string Alias { get; set; }

        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
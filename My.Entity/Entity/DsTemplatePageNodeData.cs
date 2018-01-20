using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_TemplatePageNodeData")]
    public partial class DsTemplatePageNodeData : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid TemplatePageNodeDataGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string TemplatePageNodeDataName { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? TemplatePageGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string CurNodeCode { get; set; }
        [DataColumn(IsNullable = true)]
        public string ParentNodeCode { get; set; }
        [DataColumn(IsNullable = true)]
        public string NodeSql { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IfEnd { get; set; }
    }
}

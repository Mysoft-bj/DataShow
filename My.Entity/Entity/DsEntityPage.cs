using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_EntityPage")]
    public partial class DsEntityPage : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid EntityPageGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? EntityRenter2TemplateGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? TemplateGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? TemplatePageGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public int? OriginalPageNum { get; set; }
        [DataColumn(IsNullable = true)]
        public int? PageNum { get; set; }
        [DataColumn(IsNullable = true)]
        public string JsonExample { get; set; }
        [DataColumn(IsNullable = true)]
        public string PageMemo { get; set; }
    }
}

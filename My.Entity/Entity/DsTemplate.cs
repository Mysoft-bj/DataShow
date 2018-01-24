using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_Template")]
    public partial class DsTemplate : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid TemplateGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string TemplateName { get; set; }
        [DataColumn(IsNullable = true)]
        public string TemplateMemo { get; set; }
        [DataColumn(IsNullable = true)]
        public string TemplateIcon { get; set; }
        [DataColumn(IsNullable = true)]
        public string TemplatePicSrc { get; set; }
    }
}

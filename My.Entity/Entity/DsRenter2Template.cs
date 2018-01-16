using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Domain.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_Renter2Template")]
    public partial class DsRenter2Template : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid Renter2TemplateGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? RenterGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? TemplateGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IsDisabled { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_Renter")]
    public partial class DsRenter : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid RenterGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string RenterCode { get; set; }
        [DataColumn(IsNullable = true)]
        public string RenterName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_User")]
    public partial class DsUser : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid UserGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string UserName { get; set; }
        [DataColumn(IsNullable = true)]
        public string UserCode { get; set; }
        [DataColumn(IsNullable = true)]
        public string PassWord { get; set; }
        [DataColumn(IsNullable = true)]
        public string MobilePhone { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IsAdmin { get; set; }
    }
}

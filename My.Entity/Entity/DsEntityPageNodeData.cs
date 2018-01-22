﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysoft.Map.Extensions.DAL;

namespace My.Entity
{
    [Serializable]
    [DataEntity(Alias = "ds_EntityPageNodeData")]
    public partial class DsEntityPageNodeData : BaseEntity
    {
        [DataColumn(PrimaryKey = true)]
        public Guid EntityPageNodeDataGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string Name { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? EntityPageGUID { get; set; }
        [DataColumn(IsNullable = true)]
        public string Code { get; set; }
        [DataColumn(IsNullable = true)]
        public string ParentCode { get; set; }
        [DataColumn(IsNullable = true)]
        public string Sql { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IfSqlExcutedLooped { get; set; }
        [DataColumn(IsNullable = true)]
        public int? SqlExcutedLoopedNum { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IfEnd { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IfDataHandled { get; set; }
        [DataColumn(IsNullable = true)]
        public string DataHandleAssemble { get; set; }
        [DataColumn(IsNullable = true)]
        public string DataHandleFunction { get; set; }
        [DataColumn(IsNullable = true)]
        public int? Level { get; set; }
        [DataColumn(IsNullable = true)]
        public int? DataType { get; set; }
        [DataColumn(IsNullable = true)]
        public string  StaticData { get; set; }
    }
}

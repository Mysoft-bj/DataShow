using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using My.Entity;

namespace DataShow.Services
{
    public class DataTest
    {
        public List<DsEntityPageNodeData> PageNodeList() {
            List<DsEntityPageNodeData> PageNodeList = new List<DsEntityPageNodeData>();

            #region 构建数据
            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A",
                ParentCode = "",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 0,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 1,
                DataType = 0,
                StaticData = ""
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "a",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.01",
                ParentCode = "A",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 1,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 2,
                DataType = 0,
                StaticData = "5555"
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "b",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.02",
                ParentCode = "A",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 1,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 2,
                DataType = 0,
                StaticData = "8888"
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "c",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.03",
                ParentCode = "A",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 0,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 2,
                DataType = 0,
                StaticData = ""
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "c.1",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.03.01",
                ParentCode = "A.03",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 1,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 3,
                DataType = 0,
                StaticData = "8888"
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "d",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.04",
                ParentCode = "A",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 0,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 2,
                DataType = 0,
                StaticData = ""
            });

            PageNodeList.Add(new DsEntityPageNodeData
            {
                EntityPageGUID = new Guid("56A73056-F31B-49E4-A386-8CB220041082"),
                Name = "d.1",
                EntityPageNodeDataGUID = Guid.NewGuid(),
                Code = "A.04.01",
                ParentCode = "A.04",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 1,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 3,
                DataType = 0,
                StaticData = ""
            });
            #endregion

            return PageNodeList;
        } 
    }
}

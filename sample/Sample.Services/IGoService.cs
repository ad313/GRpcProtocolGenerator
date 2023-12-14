using System.Collections.Generic;
using GRpcProtocolGenerator.Common.Attributes;
using System.ComponentModel;
using Sample.Services.Models;

namespace Sample.Services
{
    [GRpcGenerator]
    [Description("go 接口")]
    public interface IGoService
    {
        [Description("备注哈哈哈哈")]
        [HttpGet("a")]
        void Test1();

        [HttpPost("b/{id}")]
        void Test2(IdModel a);

        [HttpGet("c")]
        void Test3(IdModel a);

        [HttpPost("d")]
        IdModel Test4(IdModel a);

        [HttpPost("Test5/{id}")]
        IdListModel Test5(IdModel a);

        [HttpPost("Test6")]
        IdList2Model Test6(IdList2Model a);

        [HttpPost("Test6-2")]
        IdList3Model Test6(IdList3Model a);

        //[Description("更新")]
        //[HttpPut("update/{stringColumn}")]
        //void Update(IdList3Model a);
    }

    [Description("IdModel的注释")]
    public class IdModel
    {
        public string Id { get; set; }
    }

    public class IdListModel
    {
        public List<IdModel> Data { get; set; }
    }

    public class IdList2Model
    {
        public List<int> Data { get; set; }
    }

    public class IdList3Model
    {
        [Description("string column.....")]
        public string StringColumn { get; set; }

        public List<int> Ids { get; set; }

        public List<IdModel> Data { get; set; }
    }
}
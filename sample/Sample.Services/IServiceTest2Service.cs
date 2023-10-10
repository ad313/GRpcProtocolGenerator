using System;
using System.Collections.Generic;
using GRpcProtocolGenerator.Common.Attributes;
using Sample.Services.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sample.Services
{
    /// <summary>
    /// IServiceTest
    /// </summary>
    [Description("IServiceTest2")]
    [GRpcGenerator]
    public interface IServiceTest2Service
    {
        [Description("Test1")]
        Task Test1Async();

        [Description("这是修改")]
        [HttpPut("aaa/{a}")]
        Task Test2Async(SampleClass model);

        [Description("获取单个")]
        [HttpGet("{id}")]
        Task<SampleClass> GetByIdAsync(int id);

        [Description("查询列表")]
        [HttpGet("aaa")]
        Task<List<SampleClass>> Test4Async(SampleClass model);

        [Description("这是删除")]
        [HttpDelete("aaa/{id}")]
        Task<List<SampleClass>> Test5Async(string id);
    }

    public class ServiceTest2 : IServiceTest2Service
    {
        public async Task Test1Async()
        {
            throw new System.NotImplementedException();
        }

        public async Task Test2Async(SampleClass model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SampleClass> GetByIdAsync(int id)
        {
            return new SampleClass() { DateTimeColumn = DateTime.Now };
        }

        public async Task<SampleClass> Test3Async()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SampleClass>> Test4Async(SampleClass model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SampleClass>> Test5Async(string id)
        {
            return new List<SampleClass>();
        }
    }
}
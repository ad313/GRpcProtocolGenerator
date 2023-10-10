/*
	 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Sample.GRpc.Protocol;

namespace Sample.ClientWrapper
{
    /// IServiceTest2
    public interface IGRpcServiceTest2ServiceClient
    {
        /// Test1
        Task<Empty> Test1(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 这是修改
        Task<Empty> Test2(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 获取单个
        Task<GRpcSampleClass> GetById(GRpcServiceTest2Service_GetByIdAsync_Request1 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 查询列表
        Task<GRpcServiceTest2ServiceListSampleClassResponse> Test4(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 这是删除
        Task<GRpcServiceTest2ServiceListSampleClassResponse> Test5(GRpcServiceTest2Service_Test5Async_Request2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
    }

    public sealed class GRpcServiceTest2ServiceClient : IGRpcServiceTest2ServiceClient
    {
        private readonly Lazy<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient> _client;

        public GRpcServiceTest2ServiceClient(IGRpcClientProvider clientProvider)
        {
            _client = new Lazy<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient>(clientProvider.CreateClient<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient>);
        }
        
        /// Test1
        public async Task<Empty> Test1(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test1Async(request, header, deadline, cancellationToken);
        }
        
        /// 这是修改
        public async Task<Empty> Test2(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test2Async(request, header, deadline, cancellationToken);
        }
        
        /// 获取单个
        public async Task<GRpcSampleClass> GetById(GRpcServiceTest2Service_GetByIdAsync_Request1 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.GetByIdAsync(request, header, deadline, cancellationToken);
        }
        
        /// 查询列表
        public async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test4(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test4Async(request, header, deadline, cancellationToken);
        }
        
        /// 这是删除
        public async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test5(GRpcServiceTest2Service_Test5Async_Request2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test5Async(request, header, deadline, cancellationToken);
        }
    }
}
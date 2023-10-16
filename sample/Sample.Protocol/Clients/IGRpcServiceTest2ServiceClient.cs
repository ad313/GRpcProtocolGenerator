/*
	 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Sample.GRpc.Protocol;

namespace Sample.Protocol.Clients
{
    /// IServiceTest2
    public interface IGRpcServiceTest2ServiceClient
    {
        /// Test1
        Task<Empty> Test1Async(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 这是修改
        Task<Empty> Test2Async(GRpcServiceTest2Service_Test2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 这是修改2
        Task<Empty> Test2_2Async(GRpcServiceTest2Service_Test2_2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 获取单个
        Task<GRpcSampleClass> GetByIdAsync(GRpcServiceTest2Service_GetByIdAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 查询列表
        Task<GRpcServiceTest2ServiceListSampleClassResponse> Test4Async(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 这是删除
        Task<GRpcServiceTest2ServiceListSampleClassResponse> Test5Async(GRpcServiceTest2Service_Test5Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest2Service_Test6Async_Response> Test6Async(GRpcServiceTest2Service_Test6Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> Test7(GRpcServiceTest2ServiceIdRequest request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
    }

    /// IServiceTest2
    public sealed class GRpcServiceTest2ServiceClient : IGRpcServiceTest2ServiceClient
    {
        private readonly Lazy<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient> _client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="clientProvider"></param>
        public GRpcServiceTest2ServiceClient(IGRpcClientProvider clientProvider)
        {
            _client = new Lazy<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient>(clientProvider.CreateClient<GRpcServiceTest2Service.GRpcServiceTest2ServiceClient>);
        }
        
        /// Test1
        public async Task<Empty> Test1Async(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test1Async(request, header, deadline, cancellationToken);
        }
        
        /// 这是修改
        public async Task<Empty> Test2Async(GRpcServiceTest2Service_Test2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test2Async(request, header, deadline, cancellationToken);
        }
        
        /// 这是修改2
        public async Task<Empty> Test2_2Async(GRpcServiceTest2Service_Test2_2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test2_2Async(request, header, deadline, cancellationToken);
        }
        
        /// 获取单个
        public async Task<GRpcSampleClass> GetByIdAsync(GRpcServiceTest2Service_GetByIdAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.GetByIdAsync(request, header, deadline, cancellationToken);
        }
        
        /// 查询列表
        public async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test4Async(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test4Async(request, header, deadline, cancellationToken);
        }
        
        /// 这是删除
        public async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test5Async(GRpcServiceTest2Service_Test5Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test5Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest2Service_Test6Async_Response> Test6Async(GRpcServiceTest2Service_Test6Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test6Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> Test7(GRpcServiceTest2ServiceIdRequest request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.Test7Async(request, header, deadline, cancellationToken);
        }
    }
}
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
    /// desc from Description
    public interface IGRpcServiceTestClient
    {
        /// title from Display attr
        Task<Empty> VoidMethodSync(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoidSync(GRpcServiceTest_MethodWithInputReturnVoidSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid2Sync(GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid3Sync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid4Sync(GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputEnumReturnVoid4Sync(GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputTreeReturnVoid5Sync(GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> VoidMethodAsync(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoidAsync(GRpcServiceTest_MethodWithInputReturnVoidAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid2Async(GRpcServiceTest_MethodWithInputReturnVoid2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid3Async(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid4Async(GRpcServiceTest_MethodWithInputReturnVoid4Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputEnumReturnVoid4Async(GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnIntSync_Response> MethodWithInputReturnIntSync(GRpcServiceTest_MethodWithInputReturnIntSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringSync(GRpcServiceTest_MethodWithInputReturnStringSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcSampleClass> MethodWithInputReturnClassSync(GRpcServiceTest_MethodWithInputReturnClassSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumSync_Response> MethodWithInputReturnEnumSync(GRpcServiceTest_MethodWithInputReturnEnumSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnIntAsync_Response> MethodWithInputReturnIntAsync(GRpcServiceTest_MethodWithInputReturnIntAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringAsync(GRpcServiceTest_MethodWithInputReturnStringAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcSampleClass> MethodWithInputReturnClassAsync(GRpcServiceTest_MethodWithInputReturnClassAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcNullableClass> MethodWithInputReturnNullableClassAsync(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassAsync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntAsync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumAsync_Response> MethodWithInputReturnEnumAsync(GRpcServiceTest_MethodWithInputReturnEnumAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response> MethodWithInputReturnEnumValueTaskAsync(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> ChangeToSupportMethod7(GRpcServiceTest_ChangeToSupportMethod7_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestCancellationToken(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestCancellationToken2(GRpcServiceTest_TestCancellationToken2_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestExtClass2(GRpcExtClass2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
    }

    /// desc from Description
    public sealed class GRpcServiceTestClient : IGRpcServiceTestClient
    {
        private readonly Lazy<GRpcServiceTest.GRpcServiceTestClient> _client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="clientProvider"></param>
        public GRpcServiceTestClient(IGRpcClientProvider clientProvider)
        {
            _client = new Lazy<GRpcServiceTest.GRpcServiceTestClient>(clientProvider.CreateClient<GRpcServiceTest.GRpcServiceTestClient>);
        }
        
        /// title from Display attr
        public async Task<Empty> VoidMethodSync(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.VoidMethodSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoidSync(GRpcServiceTest_MethodWithInputReturnVoidSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoidSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid2Sync(GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid2SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid3Sync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid3SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid4Sync(GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid4SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputEnumReturnVoid4Sync(GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputEnumReturnVoid4SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputTreeReturnVoid5Sync(GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputTreeReturnVoid5SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> VoidMethodAsync(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.VoidMethodAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoidAsync(GRpcServiceTest_MethodWithInputReturnVoidAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoidAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid2Async(GRpcServiceTest_MethodWithInputReturnVoid2Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid2Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid3Async(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid3Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid4Async(GRpcServiceTest_MethodWithInputReturnVoid4Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid4Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputEnumReturnVoid4Async(GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputEnumReturnVoid4Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnIntSync_Response> MethodWithInputReturnIntSync(GRpcServiceTest_MethodWithInputReturnIntSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnIntSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringSync(GRpcServiceTest_MethodWithInputReturnStringSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnStringSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcSampleClass> MethodWithInputReturnClassSync(GRpcServiceTest_MethodWithInputReturnClassSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnClassSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListClassSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListIntSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumSync_Response> MethodWithInputReturnEnumSync(GRpcServiceTest_MethodWithInputReturnEnumSync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnIntAsync_Response> MethodWithInputReturnIntAsync(GRpcServiceTest_MethodWithInputReturnIntAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnIntAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringAsync(GRpcServiceTest_MethodWithInputReturnStringAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnStringAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcSampleClass> MethodWithInputReturnClassAsync(GRpcServiceTest_MethodWithInputReturnClassAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcNullableClass> MethodWithInputReturnNullableClassAsync(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnNullableClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassAsync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntAsync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListIntAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumAsync_Response> MethodWithInputReturnEnumAsync(GRpcServiceTest_MethodWithInputReturnEnumAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response> MethodWithInputReturnEnumValueTaskAsync(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumValueTaskAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> ChangeToSupportMethod7(GRpcServiceTest_ChangeToSupportMethod7_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.ChangeToSupportMethod7Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> TestCancellationToken(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.TestCancellationTokenAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> TestCancellationToken2(GRpcServiceTest_TestCancellationToken2_Request request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.TestCancellationToken2Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> TestExtClass2(GRpcExtClass2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.TestExtClass2Async(request, header, deadline, cancellationToken);
        }
    }
}
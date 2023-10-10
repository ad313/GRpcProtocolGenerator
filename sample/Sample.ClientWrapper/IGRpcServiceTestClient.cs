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
    /// desc from Description
    public interface IGRpcServiceTestClient
    {
        /// title from Display attr
        Task<Empty> VoidMethodSync(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoidSync(GRpcServiceTest_MethodWithInputReturnVoidSync_Request18 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid2Sync(GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request15 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid3Sync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid4Sync(GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request17 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputEnumReturnVoid4Sync(GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputTreeReturnVoid5Sync(GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request19 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> VoidMethod(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid(GRpcServiceTest_MethodWithInputReturnVoidAsync_Request13 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid2(GRpcServiceTest_MethodWithInputReturnVoid2Async_Request14 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid3(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputReturnVoid4(GRpcServiceTest_MethodWithInputReturnVoid4Async_Request16 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> MethodWithInputEnumReturnVoid4(GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request1 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnIntSync_Response5> MethodWithInputReturnIntSync(GRpcServiceTest_MethodWithInputReturnIntSync_Request9 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringSync(GRpcServiceTest_MethodWithInputReturnStringSync_Request12 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcSampleClass> MethodWithInputReturnClassSync(GRpcServiceTest_MethodWithInputReturnClassSync_Request4 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntSync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumSync_Response2> MethodWithInputReturnEnumSync(GRpcServiceTest_MethodWithInputReturnEnumSync_Request6 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnIntAsync_Response4> MethodWithInputReturnInt(GRpcServiceTest_MethodWithInputReturnIntAsync_Request8 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestStringResponse> MethodWithInputReturnString(GRpcServiceTest_MethodWithInputReturnStringAsync_Request11 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcSampleClass> MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request3 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcNullableClass> MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request10 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClass(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListInt(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumAsync_Response1> MethodWithInputReturnEnum(GRpcServiceTest_MethodWithInputReturnEnumAsync_Request5 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response3> MethodWithInputReturnEnumValueTask(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request7 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestCancellationToken(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestCancellationToken2(GRpcServiceTest_TestCancellationToken2_Request20 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
        /// 
        Task<Empty> TestExtClass2(GRpcExtClass2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        
    }

    public sealed class GRpcServiceTestClient : IGRpcServiceTestClient
    {
        private readonly Lazy<GRpcServiceTest.GRpcServiceTestClient> _client;

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
        public async Task<Empty> MethodWithInputReturnVoidSync(GRpcServiceTest_MethodWithInputReturnVoidSync_Request18 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoidSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid2Sync(GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request15 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid2SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid3Sync(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid3SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid4Sync(GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request17 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid4SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputEnumReturnVoid4Sync(GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request2 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputEnumReturnVoid4SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputTreeReturnVoid5Sync(GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request19 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputTreeReturnVoid5SyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> VoidMethod(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.VoidMethodAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid(GRpcServiceTest_MethodWithInputReturnVoidAsync_Request13 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoidAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid2(GRpcServiceTest_MethodWithInputReturnVoid2Async_Request14 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid2Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid3(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid3Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputReturnVoid4(GRpcServiceTest_MethodWithInputReturnVoid4Async_Request16 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnVoid4Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> MethodWithInputEnumReturnVoid4(GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request1 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputEnumReturnVoid4Async(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnIntSync_Response5> MethodWithInputReturnIntSync(GRpcServiceTest_MethodWithInputReturnIntSync_Request9 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnIntSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringSync(GRpcServiceTest_MethodWithInputReturnStringSync_Request12 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnStringSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcSampleClass> MethodWithInputReturnClassSync(GRpcServiceTest_MethodWithInputReturnClassSync_Request4 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
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
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumSync_Response2> MethodWithInputReturnEnumSync(GRpcServiceTest_MethodWithInputReturnEnumSync_Request6 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumSyncAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnIntAsync_Response4> MethodWithInputReturnInt(GRpcServiceTest_MethodWithInputReturnIntAsync_Request8 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnIntAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestStringResponse> MethodWithInputReturnString(GRpcServiceTest_MethodWithInputReturnStringAsync_Request11 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnStringAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcSampleClass> MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request3 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcNullableClass> MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request10 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnNullableClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClass(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListClassAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListInt(GRpcSampleClass request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnListIntAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumAsync_Response1> MethodWithInputReturnEnum(GRpcServiceTest_MethodWithInputReturnEnumAsync_Request5 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response3> MethodWithInputReturnEnumValueTask(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request7 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.MethodWithInputReturnEnumValueTaskAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> TestCancellationToken(Empty request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            return await _client.Value.TestCancellationTokenAsync(request, header, deadline, cancellationToken);
        }
        
        /// 
        public async Task<Empty> TestCancellationToken2(GRpcServiceTest_TestCancellationToken2_Request20 request, Metadata header = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
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
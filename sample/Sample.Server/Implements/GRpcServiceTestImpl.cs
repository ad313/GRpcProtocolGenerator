﻿/*
	 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Mapster;
using Sample.GRpc.Protocol;
using Sample.Services;

namespace Sample.Server.Implements
{
	/// <summary>
	/// desc from Description - Sample.Services.IServiceTest
	/// </summary>
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class GRpcServiceTestImpl : GRpcServiceTest.GRpcServiceTestBase
	{
		private readonly IServiceTest _service;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="service"></param>
		public GRpcServiceTestImpl(IServiceTest service)
		{
			_service = service;
		}

		/// title from Display attr
		public override async Task<GRpcServiceTest_EmptyWrapper> VoidMethodSync(Empty request, ServerCallContext context)
		{
			_service.VoidMethodSync();
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoidSync(GRpcServiceTest_MethodWithInputReturnVoidSync_Request18 request, ServerCallContext context)
		{
			_service.MethodWithInputReturnVoidSync(request.A);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid2Sync(GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request15 request, ServerCallContext context)
		{
			_service.MethodWithInputReturnVoid2Sync(request.A, request.B);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid3Sync(GRpcSampleClass request, ServerCallContext context)
		{
			_service.MethodWithInputReturnVoid3Sync(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid4Sync(GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request17 request, ServerCallContext context)
		{
			_service.MethodWithInputReturnVoid4Sync(request.A.ToList());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputEnumReturnVoid4Sync(GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request2 request, ServerCallContext context)
		{
			_service.MethodWithInputEnumReturnVoid4Sync(request.A.Adapt<Sample.Services.ApplicationEnumType>());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputTreeReturnVoid5Sync(GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request19 request, ServerCallContext context)
		{
			_service.MethodWithInputTreeReturnVoid5Sync(request.A?.Adapt<Sample.Services.Models.TreeNode<Sample.Services.Models.SampleClass>>(), request.B?.Adapt<Sample.Services.Models.TreeClass>(), request.C?.Adapt<Sample.Services.Models.ExtClass>(), request.D);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> VoidMethod(Empty request, ServerCallContext context)
		{
			await _service.VoidMethodAsync();
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid(GRpcServiceTest_MethodWithInputReturnVoidAsync_Request13 request, ServerCallContext context)
		{
			await _service.MethodWithInputReturnVoidAsync(request.A);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid2(GRpcServiceTest_MethodWithInputReturnVoid2Async_Request14 request, ServerCallContext context)
		{
			await _service.MethodWithInputReturnVoid2Async(request.A, request.B);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid3(GRpcSampleClass request, ServerCallContext context)
		{
			await _service.MethodWithInputReturnVoid3Async(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputReturnVoid4(GRpcServiceTest_MethodWithInputReturnVoid4Async_Request16 request, ServerCallContext context)
		{
			await _service.MethodWithInputReturnVoid4Async(request.A.ToList());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> MethodWithInputEnumReturnVoid4(GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request1 request, ServerCallContext context)
		{
			await _service.MethodWithInputEnumReturnVoid4Async(request.A.Adapt<Sample.Services.ApplicationEnumType>());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_MethodWithInputReturnIntSync_Response5> MethodWithInputReturnIntSync(GRpcServiceTest_MethodWithInputReturnIntSync_Request9 request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnIntSync(request.A);
			return new GRpcServiceTest_MethodWithInputReturnIntSync_Response5
			{
				Code = 1,
				Data = data
			};
		}

		/// 
		public override async Task<GRpcServiceTestStringResponse> MethodWithInputReturnStringSync(GRpcServiceTest_MethodWithInputReturnStringSync_Request12 request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnStringSync(request.A, request.B);
			return new GRpcServiceTestStringResponse
			{
				Code = 1,
				Data = data
			};
		}

		/// 
		public override async Task<GRpcServiceTest_GRpcSampleClassWrapper> MethodWithInputReturnClassSync(GRpcServiceTest_MethodWithInputReturnClassSync_Request4 request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnClassSync(request.A, request.B);
			return new GRpcServiceTest_GRpcSampleClassWrapper
			{
				Code = 1,
				Data = data.Adapt<GRpcSampleClass>()
			};
		}

		/// 
		public override async Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClassSync(GRpcSampleClass request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnListClassSync(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTestListSampleClassResponse
			{
				Code = 1,
				Data = { data.Adapt<List<GRpcSampleClass>>() }
			};
		}

		/// 
		public override async Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListIntSync(GRpcSampleClass request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnListIntSync(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTestListInt32Response
			{
				Code = 1,
				Data = { data.ToList() }
			};
		}

		/// 
		public override async Task<GRpcServiceTest_MethodWithInputReturnEnumSync_Response2> MethodWithInputReturnEnumSync(GRpcServiceTest_MethodWithInputReturnEnumSync_Request6 request, ServerCallContext context)
		{
			var data = _service.MethodWithInputReturnEnumSync(request.A.Adapt<Sample.Services.ApplicationEnumType>());
			return new GRpcServiceTest_MethodWithInputReturnEnumSync_Response2
			{
				Code = 1,
				Data = (int)data
			};
		}

		/// 
		public override async Task<GRpcServiceTest_MethodWithInputReturnIntAsync_Response4> MethodWithInputReturnInt(GRpcServiceTest_MethodWithInputReturnIntAsync_Request8 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnIntAsync(request.A);
			return new GRpcServiceTest_MethodWithInputReturnIntAsync_Response4
			{
				Code = 1,
				Data = data
			};
		}

		/// 
		public override async Task<GRpcServiceTestStringResponse> MethodWithInputReturnString(GRpcServiceTest_MethodWithInputReturnStringAsync_Request11 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnStringAsync(request.A, request.B);
			return new GRpcServiceTestStringResponse
			{
				Code = 1,
				Data = data
			};
		}

		/// 
		public override async Task<GRpcServiceTest_GRpcSampleClassWrapper> MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request3 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnClassAsync(request.A, request.B);
			return new GRpcServiceTest_GRpcSampleClassWrapper
			{
				Code = 1,
				Data = data.Adapt<GRpcSampleClass>()
			};
		}

		/// 
		public override async Task<GRpcServiceTest_GRpcNullableClassWrapper> MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request10 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnNullableClassAsync(request.A, request.B);
			return new GRpcServiceTest_GRpcNullableClassWrapper
			{
				Code = 1,
				Data = data.Adapt<GRpcNullableClass>()
			};
		}

		/// 
		public override async Task<GRpcServiceTestListSampleClassResponse> MethodWithInputReturnListClass(GRpcSampleClass request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnListClassAsync(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTestListSampleClassResponse
			{
				Code = 1,
				Data = { data.Adapt<List<GRpcSampleClass>>() }
			};
		}

		/// 
		public override async Task<GRpcServiceTestListInt32Response> MethodWithInputReturnListInt(GRpcSampleClass request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnListIntAsync(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTestListInt32Response
			{
				Code = 1,
				Data = { data.ToList() }
			};
		}

		/// 
		public override async Task<GRpcServiceTest_MethodWithInputReturnEnumAsync_Response1> MethodWithInputReturnEnum(GRpcServiceTest_MethodWithInputReturnEnumAsync_Request5 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnEnumAsync(request.A.Adapt<Sample.Services.ApplicationEnumType>());
			return new GRpcServiceTest_MethodWithInputReturnEnumAsync_Response1
			{
				Code = 1,
				Data = (int)data
			};
		}

		/// 
		public override async Task<GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response3> MethodWithInputReturnEnumValueTask(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request7 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnEnumValueTaskAsync(request.A.Adapt<Sample.Services.ApplicationEnumType>());
			return new GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response3
			{
				Code = 1,
				Data = (int)data
			};
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> TestCancellationToken(Empty request, ServerCallContext context)
		{
			_service.TestCancellationToken(context.CancellationToken);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> TestCancellationToken2(GRpcServiceTest_TestCancellationToken2_Request20 request, ServerCallContext context)
		{
			_service.TestCancellationToken2(request.A, context.CancellationToken);
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

		/// 
		public override async Task<GRpcServiceTest_EmptyWrapper> TestExtClass2(GRpcExtClass2 request, ServerCallContext context)
		{
			_service.TestExtClass2(request?.Adapt<Sample.Services.Models.ExtClass2>());
			return new GRpcServiceTest_EmptyWrapper { Code = 1 };
		}

	}
}

/*
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
	/// IServiceTest2 - Sample.Services.IServiceTest2Service
	/// </summary>
	public class GRpcServiceTest2ServiceImpl : GRpcServiceTest2Service.GRpcServiceTest2ServiceBase
	{
		private readonly IServiceTest2Service _service;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="service"></param>
		public GRpcServiceTest2ServiceImpl(IServiceTest2Service service)
		{
			_service = service;
		}

		/// Test1
		public override async Task<Empty> Test1(Empty request, ServerCallContext context)
		{
			await _service.Test1Async();
			return new Empty();
		}

		/// 这是修改
		public override async Task<Empty> Test2(GRpcServiceTest2Service_Test2Async_Request request, ServerCallContext context)
		{
			await _service.Test2Async(request.A, request.Model?.Adapt<Sample.Services.Models.SampleClass>());
			return new Empty();
		}

		/// 这是修改2
		public override async Task<Empty> Test2_2(GRpcServiceTest2Service_Test2_2Async_Request request, ServerCallContext context)
		{
			await _service.Test2_2Async(request.Model?.Adapt<List<Sample.Services.Models.SampleClass>>());
			return new Empty();
		}

		/// 获取单个
		public override async Task<GRpcSampleClass> GetById(GRpcServiceTest2Service_GetByIdAsync_Request request, ServerCallContext context)
		{
			var data = await _service.GetByIdAsync(request.Id);
			return data.Adapt<GRpcSampleClass>();
		}

		/// 查询列表
		public override async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test4(GRpcSampleClass request, ServerCallContext context)
		{
			var data = await _service.Test4Async(request?.Adapt<Sample.Services.Models.SampleClass>());
			return new GRpcServiceTest2ServiceListSampleClassResponse
			{
				Data = { data.Adapt<List<GRpcSampleClass>>() }
			};
		}

		/// 这是删除
		public override async Task<GRpcServiceTest2ServiceListSampleClassResponse> Test5(GRpcServiceTest2Service_Test5Async_Request request, ServerCallContext context)
		{
			var data = await _service.Test5Async(request.Id);
			return new GRpcServiceTest2ServiceListSampleClassResponse
			{
				Data = { data.Adapt<List<GRpcSampleClass>>() }
			};
		}

		/// 
		public override async Task<GRpcServiceTest2Service_Test6Async_Response> Test6(GRpcServiceTest2Service_Test6Async_Request request, ServerCallContext context)
		{
			var data = await _service.Test6Async(request.A, request.B, request.C?.Adapt<Sample.Services.Models.SampleEnum>());
			return new GRpcServiceTest2Service_Test6Async_Response
			{
				Data = data
			};
		}

		/// 
		public override async Task<Empty> Test7(GRpcServiceTest2ServiceIdRequest request, ServerCallContext context)
		{
			await _service.Test7(request.Id);
			return new Empty();
		}

	}
}

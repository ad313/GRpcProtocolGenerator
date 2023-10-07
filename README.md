# GRpcProtocolGenerator
generator grpc protocol from c# interface

## 能干什么？
这是一个代码片段生成器，通过扫描指定程序集，解析得到接口、方法和参数的元数据。然后根据配置生成 grpc 相关的代码片段：
- 接口 生成 .proto 文件
- 接口方法 生成 proto 中的 service
- 方法参数 生成 proto 中的 message
- 每个 proto 的 service 生成对应的实现
- 生成 Restful API
- 生成 Swagger

## 使用限制
- 不支持 字典：Dictionary<xxx,xxx>
- 不支持 Task 嵌套 Task
- 不支持 object 参数
- 不支持 StringBuilder 参数
- 枚举会被转换成数字，因为当启用 grpc json 转码时，生成的 swagger 会有问题
- C# 的 时间 会被转换成 protobuf 的 string 字符串
- 待补充

## 怎么使用？
- 1、新建一个空白项目，用做生成代码的宿主程序，例子中是 Sample.Start，此时这个项目目录作为基准地址，可以使用相对路径来配置代码生成后的存放位置。参考：https://github.com/ad313/GRpcProtocolGenerator/tree/develop/sample/Sample.Start
```
    //引入相关包
    <PackageReference Include="GRpcProtocolGenerator" Version="0.1.0" />
```
- 2、在接口上打标记 [GRpcGenerator]，只有加了这个标记的接口才会被扫描到
  - Description、Display 和 DisplayName 这三个特性会被读取为 注释
  - 使用特性 [GRpcIgnore] 忽略方法或模型中的字段
```
//引入特性包
<PackageReference Include="GRpcProtocolGenerator.Common" Version="0.1.0" />

[Description("desc from Description")]
[GRpcGenerator]
public interface IServiceTest
{
        /// <summary>
        /// 这个方法会被忽略
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [GRpcIgnore]
        Task<string> MethodWithInputReturnStringAsync(int a, string b);

        [Description("这是备注")]
        Task<SampleClass> MethodWithInputReturnClassAsync(int a, string b);

        [Description("这个方法加了 HttpPost，配置 Restful API 的路由，只有当 启用 grpc json 转码时，路由才有效")]
        [HttpPost("MethodWithInputReturnNullableClassAsync")]
        Task<NullableClass> MethodWithInputReturnNullableClassAsync(int a, string b);
}
```
- 3、具体的配置文件参考：https://github.com/ad313/GRpcProtocolGenerator/blob/develop/sample/Sample.Start/Start.cs

## 查看生成的代码：
### proto：不启用 json 转码
```
syntax = "proto3";
import "google/protobuf/wrappers.proto";
import "google/protobuf/empty.proto";
option csharp_namespace = "Sample.GRpc.Protocol";
package _grpc;

//desc from Description Sample.Services.IServiceTest
service GRpcServiceTest {
	
	// 这是备注
	rpc MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request1) returns(GRpcSampleClass);
	
	// 这个方法加了 HttpPost，配置 Restful API 的路由，只有当 启用 grpc json 转码时，路由才有效
	rpc MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request2) returns(GRpcNullableClass);
}

// GRpcServiceTest_MethodWithInputReturnClassAsync_Request1
message GRpcServiceTest_MethodWithInputReturnClassAsync_Request1 {
	int32 a = 1;
	google.protobuf.StringValue b = 2;
}

// GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request2
message GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request2 {
	int32 a = 1;
	google.protobuf.StringValue b = 2;
}

//SampleStruct Sample.Services.Models.SampleStruct
message GRpcSampleStruct {
	int32 IntColumn = 1; // IntColumn
	int64 LongColumn = 2; // LongColumn
	double DecimalColumn = 3; // DecimalColumn
	double DoubleColumn = 4; // DoubleColumn
	float FloatColumn = 5; // FloatColumn
	uint32 UintColumn = 6; // UintColumn
	uint64 UlongColumn = 7; // UlongColumn
	google.protobuf.StringValue StringColumn = 8; // StringColumn
	bool BoolColumn = 9; // BoolColumn
	string GuidColumn = 10; // GuidColumn
	string DateTimeColumn = 11; // DateTimeColumn
	int32 ByteColumn = 12; // ByteColumn
}

// Sample.Services.Models.NullableClass
message GRpcNullableClass {
	google.protobuf.Int32Value NullableIntColumn = 1;
	google.protobuf.Int64Value NullableLongColumn = 2;
	google.protobuf.DoubleValue NullableDecimalColumn = 3;
	google.protobuf.DoubleValue NullableDoubleColumn = 4;
	google.protobuf.FloatValue NullableFloatColumn = 5;
	google.protobuf.UInt32Value NullableUintColumn = 6;
	google.protobuf.UInt64Value NullableUlongColumn = 7;
	google.protobuf.StringValue NullableStringColumn = 8;
	google.protobuf.BoolValue NullableBoolColumn = 9;
	google.protobuf.StringValue NullableGuidColumn = 10;
	google.protobuf.StringValue NullableDateTimeColumn = 11;
	google.protobuf.Int32Value NullableByteColumn = 12;
	GRpcNullableClass NullableClassColumn = 13;
	google.protobuf.Int32Value NullableEnumColumn = 14;
	GRpcSampleStruct NullableStructColumn = 15;
}

//SampleClass Sample.Services.Models.SampleClass
message GRpcSampleClass {
	int32 IntColumn = 1; // IntColumn
	int64 LongColumn = 2; // LongColumn
	double DecimalColumn = 3; // DecimalColumn
	double DoubleColumn = 4; // DoubleColumn
	float FloatColumn = 5; // FloatColumn
	uint32 UintColumn = 6; // UintColumn
	uint64 UlongColumn = 7; // UlongColumn
	google.protobuf.StringValue StringColumn = 8; // StringColumn
	bool BoolColumn = 9; // BoolColumn
	string DateTimeColumn = 10; // DateTimeColumn
	int32 ByteColumn = 11; // ByteColumn
	GRpcSampleClass ClassColumn = 12; // ClassColumn
	int32 EnumColumn = 13; // EnumColumn
	GRpcSampleStruct StructColumn = 14; // StructColumn
}
```

### proto：启用 json 转码
```
syntax = "proto3";
import "google/protobuf/wrappers.proto";
import "google/protobuf/empty.proto";
import "google/api/annotations.proto";
option csharp_namespace = "Sample.GRpc.Protocol";
package _grpc;

//desc from Description Sample.Services.IServiceTest
service GRpcServiceTest {
	
	// 这是备注
	rpc MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request1) returns(GRpcServiceTest_GRpcSampleClassWrapper) {
		option (google.api.http) = {
			get:"/Sample/api/v1/GRpcServiceTest/MethodWithInputReturnClass",
		};
	}
	
	// 这个方法加了 HttpPost，配置 Restful API 的路由，只有当 启用 grpc json 转码时，路由才有效
	rpc MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request2) returns(GRpcServiceTest_GRpcNullableClassWrapper) {
		option (google.api.http) = {
			post:"/Sample/api/v1/GRpcServiceTest/MethodWithInputReturnNullableClassAsync",
			body:"*"
		};
	}
}

```

### service 实现
```
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

		/// 这是备注
		public override async Task<GRpcServiceTest_GRpcSampleClassWrapper> MethodWithInputReturnClass(GRpcServiceTest_MethodWithInputReturnClassAsync_Request1 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnClassAsync(request.A, request.B);
			return new GRpcServiceTest_GRpcSampleClassWrapper
			{
				Code = 1,
				Data = data.Adapt<GRpcSampleClass>()
			};
		}

		/// 这个方法加了 HttpPost，配置 Restful API 的路由，只有当 启用 grpc json 转码时，路由才有效
		public override async Task<GRpcServiceTest_GRpcNullableClassWrapper> MethodWithInputReturnNullableClass(GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request2 request, ServerCallContext context)
		{
			var data = await _service.MethodWithInputReturnNullableClassAsync(request.A, request.B);
			return new GRpcServiceTest_GRpcNullableClassWrapper
			{
				Code = 1,
				Data = data.Adapt<GRpcNullableClass>()
			};
		}

	}
}

```
## 参考文档
- grpc json 转发，.net 7 的新特性：https://learn.microsoft.com/zh-cn/aspnet/core/grpc/json-transcoding-binding?view=aspnetcore-7.0
- protobuf 与 C# 字段映射：https://learn.microsoft.com/zh-cn/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types

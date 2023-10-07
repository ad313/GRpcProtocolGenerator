# GRpcProtocolGenerator
generator grpc protocol from c# interface

## 能生成什么？
这是一个代码片段生成器，通过扫描指定程序集，解析得到接口、方法和参数的元数据。然后根据配置生成 grpc 相关的代码片段：
- 接口 生成 .proto 文件
- 接口方法 生成 proto 中的 service
- 方法参数 生成 proto 中的 message
- 每个 proto 的 service 生成对应的实现
- 生成 Restful API
- 生成 Swagger

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
## 类型限制
由于 protobuf 和 C# 字段对应关系，

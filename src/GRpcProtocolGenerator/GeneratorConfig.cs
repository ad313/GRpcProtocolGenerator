using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GRpcProtocolGenerator.Common;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator
{
    /// <summary>
    /// 生成相关配置
    /// </summary>
    public class GeneratorConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// 提取接口的程序集集合
        /// </summary>
        public string Assemblies { get; set; }

        /// <summary>
        /// Proto 配置
        /// </summary>
        public ProtoConfig Proto { get; set; }

        /// <summary>
        /// 服务端配置
        /// </summary>
        public ServerConfig Server { get; set; }

        /// <summary>
        /// 是否生成Server
        /// </summary>
        public bool HasServer => Server != null;

        /// <summary>
        /// 数据过滤
        /// </summary>
        public Filter Filter { get; set; }

        /// <summary>
        /// Json 转码 为 gRPC 服务创建 RESTful JSON API 
        /// </summary>
        public JsonTranscoding JsonTranscoding { get; set; }

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="basePath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GeneratorConfig(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(basePath));

            BasePath = basePath;
        }

        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Assemblies))
                throw new Exception("请指定程序集");

            ArgumentNullException.ThrowIfNull(Proto, nameof(Proto));

            Proto.BasePath = BasePath;

            if (Server != null)
                Server.BasePath = BasePath;
        }
    }

    /// <summary>
    /// 数据过滤
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 接口过滤
        /// </summary>
        public Func<InterfaceMetaData, bool> InterfaceFilterFunc { get; set; }

        /// <summary>
        /// 方法过滤
        /// </summary>
        public Func<InterfaceMetaData, MethodMetaData, bool> MethodFilterFunc { get; set; }
    }

    /// <summary>
    /// 生成proto 配置
    /// </summary>
    public class ProtoConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// proto 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// proto 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 存放 Proto 的文件夹
        /// </summary>
        public string ProtoDirectory { get; set; }

        /// <summary>
        /// 引用包时是否加上 ProtoDirectory
        /// </summary>
        public bool UseProtoDirectoryWhenImportPackage { get; set; } = true;

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = System.IO.Path.GetFullPath(Path.Combine(BasePath, Output));
        }

        /// <summary>
        /// 获取 proto 文件完整路径
        /// </summary>
        /// <returns></returns>
        public string GetProtoFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ProtoDirectory ?? "");
            return System.IO.Path.GetFullPath(path);
        }

        /// <summary>
        /// 获取 csproj 文件完整路径
        /// </summary>
        /// <returns></returns>
        public string GetCsprojFilePath()
        {
            return System.IO.Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        #region 包名

        public string PackageName { get; set; }

        public Func<InterfaceMetaData, string> PackageNameFunc { get; set; }

        public string GetPackageName(InterfaceMetaData meta)
        {
            return PackageNameFunc?.Invoke(meta) ?? PackageName;
        }

        #endregion

        #region 命名空间

        public string CSharpNamespace { get; set; }

        public Func<InterfaceMetaData, string> CSharpNamespaceFunc { get; set; }

        public string GetCSharpNamespace(InterfaceMetaData meta)
        {
            return CSharpNamespaceFunc?.Invoke(meta) ?? CSharpNamespace;
        }

        #endregion

        #region 注释

        /// <summary>
        /// 属性注释
        /// </summary>
        public Func<CommentMetaData, string> PropertyDescriptionFunc { get; set; }

        #endregion

        #region 服务名称

        /// <summary>
        /// 服务名称
        /// </summary>
        public Func<InterfaceMetaData, string> ServiceNameFunc { get; set; }

        #endregion

        #region 方法名称

        /// <summary>
        /// 方法名称
        /// </summary>
        public Func<MethodMetaData, string> MethodNameFunc { get; set; }

        #endregion

        #region 传入参数 新建类名称

        /// <summary>
        /// 传入传出参数新建类名称
        /// </summary>
        public Func<MethodMetaData, List<PropertyMetaData>, string> MethodInOutParamNameFunc { get; set; }

        #endregion

        #region 原始类名称

        /// <summary>
        /// 原始类名称
        /// </summary>
        public Func<string, string> OriginalClassNameFunc { get; set; }

        #endregion

        #region 类型转换

        /// <summary>
        /// C# 类型 转换成 Protobuf 对应的类型
        /// </summary>
        public Func<Type, bool, string> CSharpTypeToProtobufString { get; set; }

        #endregion
    }

    /// <summary>
    /// gRpc 服务端生成配置
    /// </summary>
    public class ServerConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// gRpc Server 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// gRpc Server 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get;private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 存放 Server 的文件夹
        /// </summary>
        public string ServerDirectory { get; set; }

        /// <summary>
        /// 给 server 附加 属性
        /// </summary>
        public List<string> AppendAttributeToServer { get; set; } = new List<string>();

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = System.IO.Path.GetFullPath(Path.Combine(BasePath, Output));
        }

        public string GetServerFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ServerDirectory ?? "");
            return System.IO.Path.GetFullPath(path);
        }

        public string GetServerMapperFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, "Mapper");
            return System.IO.Path.GetFullPath(path);
        }

        #region 命名空间

        public Func<InterfaceMetaData, string> NamespaceFunc { get; set; }

        public string GetNamespace(InterfaceMetaData meta)
        {
            return NamespaceFunc?.Invoke(meta) ?? meta.Namespace;
        }
        
        public string GetCsprojFilePath()
        {
            return System.IO.Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        public string GetProgramFilePath()
        {
            return System.IO.Path.GetFullPath(Path.Combine(OutputFullPath, "Program.cs"));
        }

        public string GetServerNamespace()
        {
            return $"{ProjectName}.{ServerDirectory}";
        }

        #endregion
    }

    /// <summary>
    /// gRpc Json 转码
    /// </summary>
    public class JsonTranscoding
    {
        /// <summary>
        /// 是否开启 gRPC JSON 转码，开启后，为 gRPC 服务创建 RESTful JSON API 
        /// </summary>
        public bool UseJsonTranscoding { get; set; }

        /// <summary>
        /// 是否统一包装返回结果，主要用于 Http api 返回结果给前端
        /// code：结果状态码，标识成功或失败
        /// data：返回结果数据
        /// message：错误信息
        /// </summary>
        public bool UseResultWrapper { get; set; }

        /// <summary>
        /// 启用认证授权
        /// </summary>
        public bool UseJwtAuthentication { get; set; }

        /// <summary>
        /// swagger
        /// </summary>
        public SwaggerConfig Swagger { get; set; }

        /// <summary>
        /// 成功状态码
        /// </summary>
        public int SuccessCode { get; set; } = 1;

        /// <summary>
        /// 失败状态码
        /// </summary>
        public int ErrorCode { get; set; } = 2;

        /// <summary>
        /// 路由
        /// </summary>
        public Func<string, string> RouteFunc { get; set; }
    }
}

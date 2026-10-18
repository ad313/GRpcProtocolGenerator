using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Models.Configs;

/// <summary>
/// gRpc 服务端生成配置
/// </summary>
public class ServerConfig
{
    /// <summary>
    /// 宿主程序运行根目录，用于相对路径定位到具体的路径
    /// </summary>
    public string CurrentPath { get; private set; }

    /// <summary>
    /// gRpc Server 文件 输出路径，路径的最后一层就是项目名称
    /// </summary>
    public string Output { get; set; } = null!;

    /// <summary>
    /// gRpc Server 文件 输出路径，绝对路径
    /// </summary>
    public string OutputFullPath { get; private set; } = null!;

    /// <summary>
    /// 最末级目录名称，项目名称
    /// </summary>
    public string ProjectName { get; private set; } = null!;

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; } = 6010;

    /// <summary>
    /// 存放 grpc service 实现
    /// </summary>
    public string? ImplementsDirectory { get; set; }

    /// <summary>
    /// 启用 gRPC 健康检查（Grpc.AspNetCore.HealthChecks），生成代码会注册并映射健康检查服务
    /// </summary>
    public bool EnableHealthChecks { get; set; }

    /// <summary>
    /// 启用 gRPC 反射（Grpc.AspNetCore.Server.Reflection），生成代码会注册并映射反射服务，建议仅在开发环境启用
    /// </summary>
    public bool EnableReflection { get; set; }

    /// <summary>
    /// 给 server 附加 属性
    /// </summary>
    public List<string> AppendAttributeToServer { get; set; } = [];

    /// <summary>
    /// 给 csproj 附加 包引用
    /// </summary>
    public List<string> AppendPackageToCsproj { get; set; } = [];

    /// <summary>
    /// 初始化，传入宿主程序地址，不是bin地址
    /// </summary>
    /// <param name="currentPath">宿主程序地址，不是bin地址</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ServerConfig(string currentPath)
    {
        if (string.IsNullOrWhiteSpace(currentPath))
            throw new ArgumentNullException(nameof(currentPath));

        CurrentPath = currentPath;
    }

    public void Check()
    {
        ArgumentNullException.ThrowIfNull(Output);

        var output = Output;
        ProjectName = output.Split('/').LastOrDefault() ?? "";
        OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, output));
    }

    public string GetServerFileOutputPath()
    {
        var path = Path.Combine(OutputFullPath, ImplementsDirectory ?? "");
        return Path.GetFullPath(path);
    }

    public string GetServerMapperFileOutputPath()
    {
        var path = Path.Combine(OutputFullPath, "Mapper");
        return Path.GetFullPath(path);
    }

    #region 命名空间

    public string GetCsprojFilePath()
    {
        return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
    }

    public string GetProgramFilePath()
    {
        return Path.GetFullPath(Path.Combine(OutputFullPath, "Program.cs"));
    }

    public string GetServiceImplNamespace()
    {
        return $"{ProjectName}.{ImplementsDirectory}";
    }

    #endregion
}

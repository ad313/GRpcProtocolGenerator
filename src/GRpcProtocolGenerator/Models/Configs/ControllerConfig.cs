using GRpcProtocolGenerator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Models.Configs
{
    /// <summary>
    /// 生成控制器配置
    /// </summary>
    public class ControllerConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; private set; }

        /// <summary>
        /// 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 存放 Controller
        /// </summary>
        public string ControllerDirectory { get; set; }

        /// <summary>
        /// Controller 基类
        /// </summary>
        public string BaseController { get; set; } = "ControllerBase";

        /// <summary>
        /// 定义控制器的路由
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 从控制器返回给前端的方法名，可能是自己封装，默认是 "Ok"
        /// </summary>
        public string ReturnMethodName { get; set; } = "Ok";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6011;

        /// <summary>
        /// swagger
        /// </summary>
        public SwaggerConfig Swagger { get; set; } = new SwaggerConfig();

        /// <summary>
        /// 给 Controller 附加 属性
        /// </summary>
        public List<string> AppendAttributeToController { get; set; } = new List<string>();

        /// <summary>
        /// 给 csproj 附加 包引用
        /// </summary>
        public List<string> AppendPackageToCsproj { get; set; } = new List<string>();

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ControllerConfig(string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath))
                throw new ArgumentNullException(nameof(currentPath));

            CurrentPath = currentPath;
        }

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, Output));
        }

        public string GetControllerFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ControllerDirectory ?? "");
            return Path.GetFullPath(path);
        }
        
        #region 命名空间

        //public Func<InterfaceMetaData, string> NamespaceFunc { get; set; }

        //public string GetNamespace(InterfaceMetaData meta)
        //{
        //    return NamespaceFunc?.Invoke(meta) ?? meta.Namespace;
        //}

        public string GetCsprojFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        public string GetProgramFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, "Program.cs"));
        }

        public string GetControllerNamespace()
        {
            return $"{ProjectName}.{ControllerDirectory.Replace("//",".").Replace("\\", ".")}";
        }

        #endregion
    }

    /// <summary>
    /// 生成控制器配置
    /// </summary>
    public class GoControllerConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; private set; }

        /// <summary>
        /// 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 存放 Controller
        /// </summary>
        public string ControllerDirectory { get; set; }

        /// <summary>
        /// Controller 基类
        /// </summary>
        public string BaseController { get; set; } = "ControllerBase";

        /// <summary>
        /// api 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 定义控制器的路由
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 从控制器返回给前端的方法名，可能是自己封装，默认是 "Ok"
        /// </summary>
        public string ReturnMethodName { get; set; } = "Ok";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6011;

        /// <summary>
        /// swagger
        /// </summary>
        public SwaggerConfig Swagger { get; set; } = new SwaggerConfig();

        /// <summary>
        /// 给 Controller 附加 属性
        /// </summary>
        public List<string> AppendAttributeToController { get; set; } = new List<string>();

        /// <summary>
        /// 给 csproj 附加 包引用
        /// </summary>
        public List<string> AppendPackageToCsproj { get; set; } = new List<string>();

        /// <summary>
        /// 命名空间、包名称
        /// </summary>
        public string NamespaceOrPackage { get; set; }

        /// <summary>
        /// 依赖外部命名空间、引入包路径等
        /// </summary>
        public List<string> Dependency { get; set; }

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GoControllerConfig(string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath))
                throw new ArgumentNullException(nameof(currentPath));

            CurrentPath = currentPath;
        }

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, Output));
        }

        public string GetControllerFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ControllerDirectory ?? "");
            return Path.GetFullPath(path);
        }

        #region 命名空间

        //public Func<InterfaceMetaData, string> NamespaceFunc { get; set; }

        //public string GetNamespace(InterfaceMetaData meta)
        //{
        //    return NamespaceFunc?.Invoke(meta) ?? meta.Namespace;
        //}

        public string GetCsprojFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        public string GetProgramFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, "Program.cs"));
        }

        public string GetControllerNamespace()
        {
            return $"{ProjectName}.{ControllerDirectory.Replace("//", ".").Replace("\\", ".")}";
        }

        #endregion
    }
}

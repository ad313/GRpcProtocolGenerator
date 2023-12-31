﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Models.Configs
{
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
        public string Output { get; set; }

        /// <summary>
        /// gRpc Server 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6010;

        /// <summary>
        /// 存放 grpc service 实现
        /// </summary>
        public string ImplementsDirectory { get; set; }

        /// <summary>
        /// 给 server 附加 属性
        /// </summary>
        public List<string> AppendAttributeToServer { get; set; } = new List<string>();

        /// <summary>
        /// 给 csproj 附加 包引用
        /// </summary>
        public List<string> AppendPackageToCsproj { get; set; } = new List<string>();

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
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, Output));
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
}

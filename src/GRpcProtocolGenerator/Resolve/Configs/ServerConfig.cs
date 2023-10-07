using GRpcProtocolGenerator.Models.MetaData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Resolve.Configs
{
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
        public string OutputFullPath { get; private set; }

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
            OutputFullPath = Path.GetFullPath(Path.Combine(BasePath, Output));
        }

        public string GetServerFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ServerDirectory ?? "");
            return Path.GetFullPath(path);
        }

        public string GetServerMapperFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, "Mapper");
            return Path.GetFullPath(path);
        }

        #region 命名空间

        public Func<InterfaceMetaData, string> NamespaceFunc { get; set; }

        public string GetNamespace(InterfaceMetaData meta)
        {
            return NamespaceFunc?.Invoke(meta) ?? meta.Namespace;
        }

        public string GetCsprojFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        public string GetProgramFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, "Program.cs"));
        }

        public string GetServerNamespace()
        {
            return $"{ProjectName}.{ServerDirectory}";
        }

        #endregion
    }
}

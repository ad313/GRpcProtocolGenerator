using System;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Models.Configs
{
    /// <summary>
    /// 客户端包装器，包装一层接口
    /// </summary>
    public class ClientWrapperConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; private set; }

        /// <summary>
        /// gRpc Client 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// gRpc Client 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; private set; }
        
        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ClientWrapperConfig(string currentPath)
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

        public string GetCsprojFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }
    }
}

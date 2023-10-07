using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Resolve.Configs;

namespace GRpcProtocolGenerator.Renders
{
    internal class BuilderPath
    {
        private static string ProtoOutput { get; set; }

        private static string ProtoRoot { get; set; }

        private static string GoogleApiProtoPath { get; set; }

        private static string ServerOutput { get; set; }

        private static string ServerRoot { get; set; }

        private static string ServerMapperOutput { get; set; }

        public static void Init(Config config)
        {
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            ProtoOutput = config.Proto.GetProtoFileOutputPath();
            ProtoRoot = config.Proto.OutputFullPath;
            GoogleApiProtoPath = Path.Combine(ProtoRoot, "google\\api");

            ServerRoot = config.Server?.OutputFullPath;
            ServerOutput = config.Server?.GetServerFileOutputPath();
            ServerMapperOutput = config.Server?.GetServerMapperFileOutputPath();

            CreateDirectory(ProtoOutput);

            if (config.JsonTranscoding.UseJsonTranscoding)
            {
                CreateDirectory(GoogleApiProtoPath);
            }

            if (!string.IsNullOrWhiteSpace(ServerRoot))
            {
                CreateDirectory(ServerOutput);
                CreateDirectory(ServerMapperOutput);
            }
        }

        public static async Task CreateFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(ProtoOutput, fileName + ".proto");
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }

        public static async Task CreateProtoRootFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(ProtoRoot, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }

        public static async Task CreateProtoGoogleApiFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(GoogleApiProtoPath, fileName);
            if (File.Exists(path))
                return;

            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }

        public static async Task CreateServerFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(ServerOutput, fileName + ".cs");
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }

        public static async Task CreateServerMapperFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(ServerMapperOutput, fileName + ".cs");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }

        public static async Task CreateServerRootFile(string fileName, string content, CancellationToken token = default)
        {
            var path = Path.Combine(ServerRoot, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await File.WriteAllTextAsync(path, content, Encoding.UTF8, token);
        }


        public static void CreateDirectory(string path)
        {
            ArgumentNullException.ThrowIfNull(path, nameof(path));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                Console.WriteLine($"创建目录：{path}");
            }
        }
    }
}
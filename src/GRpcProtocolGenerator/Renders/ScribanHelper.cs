using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Renders.Protocol;
using Scriban;
using Scriban.Runtime;

namespace GRpcProtocolGenerator.Renders
{
    internal sealed partial class ScribanHelper
    {
        public static async Task<string> RenderByTemplate(object data, string template)
        {
            var scriptObject1 = new FilterFunctions();
            scriptObject1.Import(data);

            var scContext = new TemplateContext();
            scContext.PushGlobal(scriptObject1);

            return await Scriban.Template.Parse(template).RenderAsync(scContext);
        }

        public static async Task<string> Render(object data, string templateName)
        {
            return await RenderByTemplate(data, await GetTemplate(templateName));
        }

        public static async Task<string> GetTemplate(string name)
        {
            var path = $"Renders.Templates.{name}.txt";
            var template = Assembly.GetExecutingAssembly().GetResourceString(path);
            return template ?? throw new FileNotFoundException(path);
        }
    }

    public class FilterFunctions : ScriptObject
    {
        public static string GetMethodReturnString(Config config, ProtoServiceItem item)
        {
            return BuilderPart.BuildMethodReturnType(config.JsonTranscoding.UseResultWrapper,
                item.MethodMetaData.OutParamMetaDataList.Count == 0,
                item.OutParam.GetGRpcName(),
                "Empty").Substring(6);
        }

        public static string GetMethodReturnType(Config config, ProtoServiceItem item)
        {
            var str = GetMethodReturnString(config, item);
            return str.Substring(5).TrimEnd('>');
        }

        public static string GetMethodInString(ProtoServiceItem item)
        {
            return item.MethodMetaData.InParamMetaDataListFilter().Count == 0
                ? "Empty"
                : item.InParam.GetGRpcName();
        }

        public static string GetMethodDescription(ProtoServiceItem item)
        {
            return Config.ConfigInstance.Proto.PropertyDescriptionFunc(item.MethodMetaData);
        }
    }

    /// <summary>
    /// 嵌入资源扩展
    /// </summary>
    public static class AssemblyResourceExtension
    {
        public static Stream GetResourceStream(this Assembly assembly, string name)
        {
            return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{name}");
        }

        public static string GetResourceString(this Assembly assembly, string name)
        {
            var stream = GetResourceStream(assembly, name);
            return stream.GetString();
        }

        public static string GetString(this Stream stream)
        {
            if (stream == null) return string.Empty;
            using (stream)
            {
                if (stream.Length <= 0) return string.Empty;
                if (stream.Position != 0) stream.Position = 0;

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Dispose();
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
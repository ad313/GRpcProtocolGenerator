using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Renders.Protocol;
using Sample.Services;
using Sample.Services.Models;

namespace GRpcProtocolGenerator.Test.Metadatas
{
    /// <summary>
    /// gRPC 流式支持测试
    /// </summary>
    public class StreamingTest
    {
        private readonly MetaDataTest _metaDataTest;
        private readonly Builder _builder;
        private readonly ProtocolService _streamService;

        public StreamingTest()
        {
            _metaDataTest = new MetaDataTest();
            _builder = _metaDataTest.Handler.CreateBuilder();
            _streamService = _builder.Services.First(d => d.InterfaceMetaData.Type == typeof(IStreamService));
        }

        private MethodMetaData GetMethod(string name)
        {
            var method = _streamService.InterfaceMetaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(method);
            return method!;
        }

        private ProtoServiceItem GetItem(string grpcMethodName)
        {
            var item = _streamService.Items.FirstOrDefault(d => d.Name == grpcMethodName);
            Assert.NotNull(item);
            return item!;
        }

        [Fact]
        public void StreamKindDetectionTest()
        {
            //服务端流
            var serverStreaming = GetMethod(nameof(IStreamService.GetSampleStream));
            Assert.Equal(StreamKind.ServerStreaming, serverStreaming.StreamKind);
            Assert.True(serverStreaming.IsServerStreaming);
            Assert.False(serverStreaming.IsClientStreaming);
            Assert.False(serverStreaming.IsTask);
            Assert.NotNull(serverStreaming.StreamResponseElement);
            Assert.True(serverStreaming.StreamResponseElement.TypeWrapper.Type == typeof(SampleClass));
            Assert.Null(serverStreaming.StreamRequestElement);

            //服务端流（Task 包裹）
            var serverStreamingTask = GetMethod(nameof(IStreamService.GetStringStreamAsync));
            Assert.Equal(StreamKind.ServerStreaming, serverStreamingTask.StreamKind);
            Assert.True(serverStreamingTask.IsTask);
            Assert.NotNull(serverStreamingTask.StreamResponseElement);
            Assert.True(serverStreamingTask.StreamResponseElement.TypeWrapper.Type == typeof(string));

            //客户端流
            var clientStreaming = GetMethod(nameof(IStreamService.UploadSampleStream));
            Assert.Equal(StreamKind.ClientStreaming, clientStreaming.StreamKind);
            Assert.False(clientStreaming.IsServerStreaming);
            Assert.True(clientStreaming.IsClientStreaming);
            Assert.NotNull(clientStreaming.StreamRequestElement);
            Assert.True(clientStreaming.StreamRequestElement.TypeWrapper.Type == typeof(SampleClass));
            Assert.Null(clientStreaming.StreamResponseElement);
            Assert.True(clientStreaming.HasCancellationToken);

            //双向流
            var duplex = GetMethod(nameof(IStreamService.ChatStream));
            Assert.Equal(StreamKind.DuplexStreaming, duplex.StreamKind);
            Assert.True(duplex.IsServerStreaming);
            Assert.True(duplex.IsClientStreaming);
            Assert.NotNull(duplex.StreamRequestElement);
            Assert.NotNull(duplex.StreamResponseElement);
        }

        [Fact]
        public void ProtoStreamKeywordTest()
        {
            var proto = new ProtocolContent(_streamService).ToContent();

            //服务端流：rpc M(Request) returns(stream Response)
            Assert.Contains("rpc GetSampleStream(GRpcStreamService_GetSampleStream_Request) returns(stream GRpcSampleClass);", proto);

            //服务端流（Task 包裹，string 元素）
            Assert.Contains("returns(stream GRpcStreamServiceStringResponse);", proto);

            //客户端流：rpc M(stream Request) returns(Response)
            Assert.Contains("rpc UploadSampleStream(stream GRpcSampleClass) returns(GRpcStreamService_UploadSampleStream_Response);", proto);

            //双向流：rpc M(stream Request) returns(stream Response)
            Assert.Contains("rpc ChatStream(stream GRpcSampleClass) returns(stream GRpcSampleClass);", proto);
        }

        [Fact]
        public void ServerImplSignatureTest()
        {
            var impl = new ServerServiceImpl(new ProtocolContent(_streamService));
            var code = impl.ToContent();

            //服务端流
            Assert.Contains(
                "public override async Task GetSampleStream(GRpcStreamService_GetSampleStream_Request request, IServerStreamWriter<GRpcSampleClass> responseStream, ServerCallContext context)",
                code);
            Assert.Contains("await responseStream.WriteAsync(data.Adapt<GRpcSampleClass>());", code);

            //客户端流
            Assert.Contains(
                "public override async Task<GRpcStreamService_UploadSampleStream_Response> UploadSampleStream(IAsyncStreamReader<GRpcSampleClass> requestStream, ServerCallContext context)",
                code);
            Assert.Contains("IAsyncEnumerable<Sample.Services.Models.SampleClass> ReadRequestStreamAsync()", code);

            //双向流
            Assert.Contains(
                "public override async Task ChatStream(IAsyncStreamReader<GRpcSampleClass> requestStream, IServerStreamWriter<GRpcSampleClass> responseStream, ServerCallContext context)",
                code);
        }

        [Fact]
        public async Task ClientTemplateStreamingTest()
        {
            var name = $"GRpc{_streamService.Name.TrimStart('I')}Client";
            var str = await ScribanHelper.Render(new
            {
                config = GRpcProtocolGenerator.Models.Configs.Config.ConfigInstance,
                data = new
                {
                    service = _streamService,
                    name = name,
                    protoNamespace = GRpcProtocolGenerator.Models.Configs.Config.ConfigInstance.Proto.GetCSharpNamespace(_streamService.InterfaceMetaData),
                    description = "",
                    gRpcServiceName = _streamService.InterfaceMetaData.FormatServiceName(),
                }
            }, "Client.Client");

            //服务端流：IAsyncEnumerable 返回
            Assert.Contains("IAsyncEnumerable<GRpcSampleClass> GetSampleStream(GRpcStreamService_GetSampleStream_Request request", str);
            Assert.Contains("call.ResponseStream.ReadAllAsync(cancellationToken)", str);

            //客户端流：Task 返回，IAsyncEnumerable 入参
            Assert.Contains("Task<GRpcStreamService_UploadSampleStream_Response> UploadSampleStream(IAsyncEnumerable<GRpcSampleClass> requestStream", str);
            Assert.Contains("await call.RequestStream.WriteAsync(item);", str);
            Assert.Contains("await call.RequestStream.CompleteAsync();", str);

            //双向流
            Assert.Contains("IAsyncEnumerable<GRpcSampleClass> ChatStream(IAsyncEnumerable<GRpcSampleClass> requestStream", str);
        }

        [Fact]
        public void UnsupportedStreamingTest()
        {
            var notSupported = _metaDataTest.Handler.AssemblyMetaData!.NotSupportMethodList;

            //多个流式参数
            var multi = notSupported.FirstOrDefault(d => d.MethodInfo.Name == nameof(IStreamService.StreamNotSupport_MultiStreamParam));
            Assert.NotNull(multi);
            Assert.Contains("只支持一个流式参数", multi!.Reason);

            //流式参数与其他业务参数混用
            var mixed = notSupported.FirstOrDefault(d => d.MethodInfo.Name == nameof(IStreamService.StreamNotSupport_MixedParam));
            Assert.NotNull(mixed);
            Assert.Contains("不能与其他业务参数混用", mixed!.Reason);

            //字典作为流元素
            var dict = notSupported.FirstOrDefault(d => d.MethodInfo.Name == nameof(IStreamService.StreamNotSupport_DictElement));
            Assert.NotNull(dict);
            Assert.Contains("不受支持", dict!.Reason);

            //不支持的方法不会出现在服务中
            Assert.DoesNotContain(_streamService.Items, d => d.MethodMetaData.Name!.StartsWith("StreamNotSupport"));
        }
    }
}

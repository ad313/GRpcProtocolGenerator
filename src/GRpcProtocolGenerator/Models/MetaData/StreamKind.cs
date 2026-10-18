namespace GRpcProtocolGenerator.Models.MetaData;

/// <summary>
/// gRPC 流式类型
/// </summary>
public enum StreamKind
{
    /// <summary>
    /// 一元调用（非流式）
    /// </summary>
    None = 0,

    /// <summary>
    /// 服务端流：rpc M(Request) returns (stream Response)
    /// </summary>
    ServerStreaming = 1,

    /// <summary>
    /// 客户端流：rpc M(stream Request) returns (Response)
    /// </summary>
    ClientStreaming = 2,

    /// <summary>
    /// 双向流：rpc M(stream Request) returns (stream Response)
    /// </summary>
    DuplexStreaming = 3,
}

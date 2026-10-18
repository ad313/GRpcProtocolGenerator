using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Common.Attributes;
using Sample.Services.Models;

namespace Sample.Services
{
    /// <summary>
    /// gRPC 流式接口示例
    /// </summary>
    [Description("流式服务")]
    [GRpcGenerator]
    public interface IStreamService
    {
        /// <summary>
        /// 服务端流
        /// </summary>
        IAsyncEnumerable<SampleClass> GetSampleStream(int count, CancellationToken cancellationToken);

        /// <summary>
        /// 服务端流（Task 包裹）
        /// </summary>
        Task<IAsyncEnumerable<string>> GetStringStreamAsync(SampleClass request);

        /// <summary>
        /// 客户端流
        /// </summary>
        Task<int> UploadSampleStream(IAsyncEnumerable<SampleClass> stream, CancellationToken cancellationToken);

        /// <summary>
        /// 双向流
        /// </summary>
        IAsyncEnumerable<SampleClass> ChatStream(IAsyncEnumerable<SampleClass> stream);

        #region 不支持的流式写法

        /// <summary>
        /// 不支持：多个流式参数
        /// </summary>
        Task<int> StreamNotSupport_MultiStreamParam(IAsyncEnumerable<int> a, IAsyncEnumerable<int> b);

        /// <summary>
        /// 不支持：流式参数与其他业务参数混用
        /// </summary>
        Task<int> StreamNotSupport_MixedParam(IAsyncEnumerable<int> a, int b);

        /// <summary>
        /// 不支持：字典作为流元素
        /// </summary>
        IAsyncEnumerable<Dictionary<int, int>> StreamNotSupport_DictElement();

        #endregion
    }
}

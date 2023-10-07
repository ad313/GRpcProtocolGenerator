using GRpcProtocolGenerator.Common;
using System;

namespace GRpcProtocolGenerator.Resolve.Configs
{
    /// <summary>
    /// gRpc Json 转码
    /// </summary>
    public class JsonTranscodingConfig
    {
        /// <summary>
        /// 是否开启 gRPC JSON 转码，开启后，为 gRPC 服务创建 RESTful JSON API 
        /// </summary>
        public bool UseJsonTranscoding { get; set; }

        /// <summary>
        /// 是否统一包装返回结果，主要用于 Http api 返回结果给前端
        /// code：结果状态码，标识成功或失败
        /// data：返回结果数据
        /// message：错误信息
        /// </summary>
        public bool UseResultWrapper { get; set; }

        /// <summary>
        /// 启用认证授权
        /// </summary>
        public bool UseJwtAuthentication { get; set; }

        /// <summary>
        /// swagger
        /// </summary>
        public SwaggerConfig Swagger { get; set; }

        /// <summary>
        /// 成功状态码
        /// </summary>
        public int SuccessCode { get; set; } = 1;

        /// <summary>
        /// 失败状态码
        /// </summary>
        public int ErrorCode { get; set; } = 2;

        /// <summary>
        /// 路由
        /// </summary>
        public Func<string, string> RouteFunc { get; set; }
    }
}

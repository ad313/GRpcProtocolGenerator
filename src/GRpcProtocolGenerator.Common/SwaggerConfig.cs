using System;
using System.Collections.Generic;
using System.Linq;

namespace GRpcProtocolGenerator.Common
{
    /// <summary>
    /// swagger 扩展
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 类型，注意，当 类型为 IdentityLogin时，此时把此项目当成一个客户端，并且回调地址是 http(s)://localhost:端口/swagger/oauth2-redirect.html
        /// </summary>
        public SwaggerConfigType SwaggerConfigType { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Version { get; set; }

        /// <summary>
        /// 对应的xml文件名称，包含 .xml 后缀
        /// </summary>
        public string[] DocumentXml { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 当 类型为 IdentityLogin时必填
        /// </summary>
        public string IdentityUrl { get; set; }

        /// <summary>
        /// 当 类型为 IdentityLogin时必填
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 当 类型为 IdentityLogin时必填
        /// </summary>
        public string[] Scope { get; set; }

        /// <summary>
        /// 获取 Scope，过滤掉 openid 和 profile，不然会报错
        /// </summary>
        /// <returns></returns>
        public List<string> GetScopes()
        {
            if (Scope == null)
                Scope = Array.Empty<string>();

            return Scope.Concat(new[] { Audience })
                .Where(d => string.IsNullOrWhiteSpace(d) == false && d != "openid" && d != "profile").ToList();
        }
    }

    /// <summary>
    /// token方式
    /// </summary>
    public enum SwaggerConfigType
    {
        /// <summary>
        /// 不传入token
        /// </summary>
        None,
        /// <summary>
        /// 手动传入token
        /// </summary>
        Bearer,
        /// <summary>
        /// 集成系统登录获取token
        /// </summary>
        IdentityLogin
    }
}

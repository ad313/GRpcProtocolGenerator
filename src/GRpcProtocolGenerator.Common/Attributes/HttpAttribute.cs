using System;

namespace GRpcProtocolGenerator.Common.Attributes
{
    /// <summary>
    /// HttpGet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAttribute : Attribute
    {
        /// <summary>
        /// 路由内容
        /// </summary>
        public string Template { get; set; }

        public HttpGetAttribute() { }

        public HttpGetAttribute(string template)
        {
            Template = template;
        }
    }

    /// <summary>
    /// HttpPost
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPostAttribute : Attribute
    {
        /// <summary>
        /// 路由内容
        /// </summary>
        public string Template { get; set; }

        public HttpPostAttribute() { }

        public HttpPostAttribute(string template)
        {
            Template = template;
        }
    }

    /// <summary>
    /// HttpPut
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPutAttribute : Attribute
    {
        /// <summary>
        /// 路由内容
        /// </summary>
        public string Template { get; set; }

        public HttpPutAttribute() { }

        public HttpPutAttribute(string template)
        {
            Template = template;
        }
    }

    /// <summary>
    /// HttpDelete
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpDeleteAttribute : Attribute
    {
        /// <summary>
        /// 路由内容
        /// </summary>
        public string Template { get; set; }

        public HttpDeleteAttribute() { }

        public HttpDeleteAttribute(string template)
        {
            Template = template;
        }
    }

    /// <summary>
    /// HttpPatch
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPatchAttribute : Attribute
    {
        /// <summary>
        /// 路由内容
        /// </summary>
        public string Template { get; set; }

        public HttpPatchAttribute() { }

        public HttpPatchAttribute(string template)
        {
            Template = template;
        }
    }
}

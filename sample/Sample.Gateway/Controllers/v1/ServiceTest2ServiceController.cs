/*
     此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using Sample.GRpc.Protocol;
using Sample.ClientWrapper;

namespace Sample.Gateway.Controllers.v1
{
    /// IServiceTest2
    [ApiController]
    [Route("Sample/api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "IServiceTest2")]
    public partial class ServiceTest2Controller : ControllerBase
    {
        private readonly IGRpcServiceTest2ServiceClient _client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="client"></param>
        public ServiceTest2Controller(IGRpcServiceTest2ServiceClient client)
        {
            _client = client;
        }
                
        /// <summary>
        /// Test1
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test1")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Test1")]        
        public virtual async Task<IActionResult> Test1Async(CancellationToken cancellationToken = default)
        {
            var result = await _client.Test1(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 这是修改
        /// </summary>
        /// <returns></returns>
        [HttpPut("aaa/{a}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("这是修改")]        
        public virtual async Task<IActionResult> Test2Async([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.Test2(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("获取单个")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcSampleClass))]        
        public virtual async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _client.GetById(new GRpcServiceTest2Service_GetByIdAsync_Request1(){Id = id}, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("aaa")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("查询列表")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest2ServiceListSampleClassResponse))]        
        public virtual async Task<IActionResult> Test4Async([FromQuery] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.Test4(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 这是删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("aaa/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("这是删除")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest2ServiceListSampleClassResponse))]        
        public virtual async Task<IActionResult> Test5Async([FromQuery] GRpcServiceTest2Service_Test5Async_Request2 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.Test5(request, cancellationToken: cancellationToken);
            return Ok(result);
        }        
    }
}
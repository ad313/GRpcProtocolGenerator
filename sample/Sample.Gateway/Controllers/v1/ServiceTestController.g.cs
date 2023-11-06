/*
     此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sample.GRpc.Protocol;
using Sample.Protocol.Clients;

namespace Sample.Gateway.Controllers.v1
{
    /// desc from Description
    [ApiController]
    [Route("Sample/api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "desc from Description")]
    public partial class ServiceTestController : ControllerBase
    {
        private readonly IGRpcServiceTestClient _client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="client"></param>
        public ServiceTestController(IGRpcServiceTestClient client)
        {
            _client = client;
        }
                
        /// <summary>
        /// title from Display attr
        /// </summary>
        /// <returns></returns>
        [HttpGet("VoidMethodSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("title from Display attr")]        
        public virtual async Task<IActionResult> VoidMethodSync(CancellationToken cancellationToken = default)
        {
            var result = await _client.VoidMethodSync(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok();
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnVoid3Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid3Sync([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid3Sync(clientInput, cancellationToken: cancellationToken);
            return Ok();
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputTreeReturnVoid5Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputTreeReturnVoid5Sync([FromQuery] GRpcTreeNode_SampleClass a, [FromQuery] GRpcTreeClass b, [FromQuery] GRpcExtClass c, System.Int32 d, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputTreeReturnVoid5Sync(new GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request() { A = a, B = b, C = c, D = d }, cancellationToken: cancellationToken);
            return Ok();
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnVoid3Async")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid3Async([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid3Async(clientInput, cancellationToken: cancellationToken);
            return Ok();
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListClassSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(List<GRpcSampleClass>))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListClassSync([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListClassSync(clientInput, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListIntSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(List<System.Int32>))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListIntSync([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListIntSync(clientInput, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListClassAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(List<GRpcSampleClass>))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListClassAsync([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListClassAsync(clientInput, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListIntAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(List<System.Int32>))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListIntAsync([FromBody] GRpcSampleClass clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListIntAsync(clientInput, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("TestExtClass2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestExtClass2([FromBody] GRpcExtClass2 clientInput, CancellationToken cancellationToken = default)
        {
            var result = await _client.TestExtClass2(clientInput, cancellationToken: cancellationToken);
            return Ok();
        }        
    }
}
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
using Sample.ClientWrapper;

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
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoidSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoidSync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoidSync(new GRpcServiceTest_MethodWithInputReturnVoidSync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid2Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid2Sync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid2Sync(new GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result);
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
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid4Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid4Sync(List<System.Int32> a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid4Sync(new GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request() { A = { a } }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputEnumReturnVoid4Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputEnumReturnVoid4Sync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputEnumReturnVoid4Sync(new GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result);
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
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("VoidMethod")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> VoidMethodAsync(CancellationToken cancellationToken = default)
        {
            var result = await _client.VoidMethod(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoidAsync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid(new GRpcServiceTest_MethodWithInputReturnVoidAsync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid2Async(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid2(new GRpcServiceTest_MethodWithInputReturnVoid2Async_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result);
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
            var result = await _client.MethodWithInputReturnVoid3(clientInput, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid4")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid4Async(List<System.Int32> a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid4(new GRpcServiceTest_MethodWithInputReturnVoid4Async_Request() { A = { a } }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputEnumReturnVoid4")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputEnumReturnVoid4Async(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputEnumReturnVoid4(new GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnIntSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.Int32))]        
        public virtual async Task<IActionResult> MethodWithInputReturnIntSync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnIntSync(new GRpcServiceTest_MethodWithInputReturnIntSync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnStringSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.String))]        
        public virtual async Task<IActionResult> MethodWithInputReturnStringSync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnStringSync(new GRpcServiceTest_MethodWithInputReturnStringSync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnClassSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcSampleClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnClassSync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnClassSync(new GRpcServiceTest_MethodWithInputReturnClassSync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result);
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
        [HttpGet("MethodWithInputReturnEnumSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.Int32))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumSync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnumSync(new GRpcServiceTest_MethodWithInputReturnEnumSync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnInt")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.Int32))]        
        public virtual async Task<IActionResult> MethodWithInputReturnIntAsync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnInt(new GRpcServiceTest_MethodWithInputReturnIntAsync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnString")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.String))]        
        public virtual async Task<IActionResult> MethodWithInputReturnStringAsync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnString(new GRpcServiceTest_MethodWithInputReturnStringAsync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnClass")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcSampleClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnClassAsync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnClass(new GRpcServiceTest_MethodWithInputReturnClassAsync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnNullableClass")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcNullableClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnNullableClassAsync(System.Int32 a, System.String? b, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnNullableClass(new GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request() { A = a, B = b }, cancellationToken: cancellationToken);
            return Ok(result);
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
            var result = await _client.MethodWithInputReturnListClass(clientInput, cancellationToken: cancellationToken);
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
            var result = await _client.MethodWithInputReturnListInt(clientInput, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnEnum")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.Int32))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumAsync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnum(new GRpcServiceTest_MethodWithInputReturnEnumAsync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnEnumValueTask")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(System.Int32))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumValueTaskAsync(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnumValueTask(new GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result.Data);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestCancellationToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestCancellationToken(CancellationToken cancellationToken = default)
        {
            var result = await _client.TestCancellationToken(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestCancellationToken2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestCancellationToken2(System.Int32 a, CancellationToken cancellationToken = default)
        {
            var result = await _client.TestCancellationToken2(new GRpcServiceTest_TestCancellationToken2_Request() { A = a }, cancellationToken: cancellationToken);
            return Ok(result);
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
            return Ok(result);
        }        
    }
}
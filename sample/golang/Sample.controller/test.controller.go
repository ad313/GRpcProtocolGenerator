// Package assets 此代码自动生成，请勿修改
package assets

import (
	"sailing.cn/v2/mapper"
	"sailing.cn/v2/restful"
	"github.com/gin-gonic/gin"
	"google.golang.org/protobuf/types/known/emptypb"
	"media.gateway.cn/assets/controller/v1/common"
	grpc "media.gateway.cn/common/client/assets"
	"media.gateway.cn/common/http/dto/assets"
	"sailing.cn/protobuf/assets"
)

type TestController struct{}

// 路由配置
func addTestRouter(router *gin.RouterGroup) {
	api := new(TestController)
	router = router.Group("/Test")
	router.GET("/VoidMethodSync", api.VoidMethodSync)
	router.("/", api.MethodWithInputReturnVoidSync)
	router.("/", api.MethodWithInputReturnVoid2Sync)
	router.POST("/MethodWithInputReturnVoid3Sync", api.MethodWithInputReturnVoid3Sync)
	router.("/", api.MethodWithInputReturnVoid4Sync)
	router.("/", api.MethodWithInputEnumReturnVoid4Sync)
	router.GET("/MethodWithInputTreeReturnVoid5Sync", api.MethodWithInputTreeReturnVoid5Sync)
	router.("/", api.VoidMethod)
	router.("/", api.MethodWithInputReturnVoid)
	router.("/", api.MethodWithInputReturnVoid2)
	router.POST("/MethodWithInputReturnVoid3Async", api.MethodWithInputReturnVoid3)
	router.("/", api.MethodWithInputReturnVoid4)
	router.("/", api.MethodWithInputEnumReturnVoid4)
	router.("/", api.MethodWithInputReturnIntSync)
	router.("/", api.MethodWithInputReturnStringSync)
	router.("/", api.MethodWithInputReturnClassSync)
	router.POST("/MethodWithInputReturnListClassSync", api.MethodWithInputReturnListClassSync)
	router.POST("/MethodWithInputReturnListIntSync", api.MethodWithInputReturnListIntSync)
	router.("/", api.MethodWithInputReturnEnumSync)
	router.("/", api.MethodWithInputReturnInt)
	router.("/", api.MethodWithInputReturnString)
	router.("/", api.MethodWithInputReturnClass)
	router.("/", api.MethodWithInputReturnNullableClass)
	router.POST("/MethodWithInputReturnListClassAsync", api.MethodWithInputReturnListClass)
	router.POST("/MethodWithInputReturnListIntAsync", api.MethodWithInputReturnListInt)
	router.("/", api.MethodWithInputReturnEnum)
	router.("/", api.MethodWithInputReturnEnumValueTask)
	router.("/", api.ChangeToSupportMethod7)
	router.("/", api.TestCancellationToken)
	router.("/", api.TestCancellationToken2)
	router.POST("/TestExtClass2", api.TestExtClass2)
}

// VoidMethodSync title from Display attr
// @Summary  title from Display attr
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/VoidMethodSync [get]
func (x *TestController) VoidMethodSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().VoidMethodSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoidSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoidSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoidSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid2Sync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid2Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid2Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid3Sync 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnVoid3Sync [post]
func (x *TestController) MethodWithInputReturnVoid3Sync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid3Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid4Sync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid4Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid4Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputEnumReturnVoid4Sync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputEnumReturnVoid4Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputEnumReturnVoid4Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputTreeReturnVoid5Sync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputTreeReturnVoid5Sync [get]
func (x *TestController) MethodWithInputTreeReturnVoid5Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputTreeReturnVoid5Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// VoidMethod 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) VoidMethod(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().VoidMethod(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid2 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid2(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid3 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnVoid3Async [post]
func (x *TestController) MethodWithInputReturnVoid3(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid3(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid4 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid4(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid4(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputEnumReturnVoid4 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputEnumReturnVoid4(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputEnumReturnVoid4(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnIntSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnIntSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnIntSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnStringSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnStringSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnStringSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnClassSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto.SampleClassDto }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnClassSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnClassSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.SampleClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListClassSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListClassSync [post]
func (x *TestController) MethodWithInputReturnListClassSync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListClassSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListIntSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListIntSync [post]
func (x *TestController) MethodWithInputReturnListIntSync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListIntSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnumSync 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnumSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnumSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnInt 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnInt(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnInt(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnString 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnString(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnString(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnClass 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto.SampleClassDto }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnClass(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.SampleClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnNullableClass 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto.NullableClassDto }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnNullableClass(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnNullableClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.NullableClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListClass 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListClassAsync [post]
func (x *TestController) MethodWithInputReturnListClass(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListInt 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListIntAsync [post]
func (x *TestController) MethodWithInputReturnListInt(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListInt(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnum 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnum(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnum(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnumValueTask 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnumValueTask(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnumValueTask(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// ChangeToSupportMethod7 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) ChangeToSupportMethod7(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().ChangeToSupportMethod7(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestCancellationToken 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) TestCancellationToken(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().TestCancellationToken(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestCancellationToken2 
// @Summary  
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) TestCancellationToken2(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().TestCancellationToken2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestExtClass2 
// @Summary  
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.ExtClass2Dto			false	"空"
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test/TestExtClass2 [post]
func (x *TestController) TestExtClass2(c *gin.Context) {

    var err error
	_request := &assets_dto.ExtClass2Dto{}
	err = c.ShouldBind(_request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcExtClass2{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().TestExtClass2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

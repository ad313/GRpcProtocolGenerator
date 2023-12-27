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
	//"sailing.cn/v2/restful/jwt"
	"google.golang.org/protobuf/types/known/emptypb"
)

type GoController struct{}

// 路由配置
func addGoRouter(router *gin.RouterGroup) {
	api := new(GoController)
	router = router.Group("/Go")
	router.GET("/a", common.CustomAuth, api.Test1)
	router.POST("/b/:id", common.CustomAuth, api.Test2)
	router.GET("/c", common.CustomAuth, api.Test3)
	router.POST("/d", common.CustomAuth, api.Test4)
	router.POST("/Test5/:id", common.CustomAuth, api.Test5)
	router.POST("/Test6", common.CustomAuth, api.Test6_1)
	router.POST("/Test6-2", common.CustomAuth, api.Test6_2)
}

// Test1 备注哈哈哈哈
// @Summary  [go 接口-控制器] - 备注哈哈哈哈
// @Tags     media-assets/Go
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Go/a [get]
func (x *GoController) Test1(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetGoClient().Test1(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// Test2 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		id		path		string							true	"输入参数"
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Go/b/{id} [post]
func (x *GoController) Test2(c *gin.Context) {

    var err error
	var id = c.Param("id")
	_request := &assets_dto.IdModelDto{}
	err = c.ShouldBind(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_request.Id = id

	request := &assets.GRpcIdModel{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// Test3 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		model	query		assets_dto.IdModelDto			false	"IdModel的注释"
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Go/c [get]
func (x *GoController) Test3(c *gin.Context) {

    var err error
	_request := &assets_dto.IdModelDto{}
	err = c.ShouldBindQuery(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcIdModel{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test3(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// Test4 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		model	body		assets_dto.IdModelDto			false	"IdModel的注释"
//	@success	200	{object}	restful.JSONResult{data=string}	"返回结果"
//
// @Router   /v1/media-assets/Go/d [post]
func (x *GoController) Test4(c *gin.Context) {

    var err error
	_request := &assets_dto.IdModelDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcIdModel{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test4(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	

	 _result2 := assets_dto.IdModelDto{}
	err = mapper.MapProtoToStruct(result, &_result2)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	restful.Success(c, _result2.Id)
}

// Test5 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		id		path		string							true	"输入参数"
//	@success	200	{object}	restful.JSONResult{data=[]assets_dto.IdModelDto}	"返回结果"
//
// @Router   /v1/media-assets/Go/Test5/{id} [post]
func (x *GoController) Test5(c *gin.Context) {

    var err error
	var id = c.Param("id")
	_request := &assets_dto.IdModelDto{}
	err = c.ShouldBind(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_request.Id = id

	request := &assets.GRpcIdModel{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test5(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	

	 _result2 := assets_dto.IdListModelDto{}
	err = mapper.MapProtoToStruct(result, &_result2)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	restful.Success(c, _result2.Data)
}

// Test6_1 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		model	body		assets_dto.IdList2ModelDto			false	"输入参数"
//	@success	200	{object}	restful.JSONResult{data=[]int32}	"返回结果"
//
// @Router   /v1/media-assets/Go/Test6 [post]
func (x *GoController) Test6_1(c *gin.Context) {

    var err error
	_request := &assets_dto.IdList2ModelDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcIdList2Model{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test6_1(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	

	 _result2 := assets_dto.IdList2ModelDto{}
	err = mapper.MapProtoToStruct(result, &_result2)
	if err != nil {
		restful.Fail(c, err)
		return
	}

	restful.Success(c, _result2.Data)
}

// Test6_2 
// @Summary  [go 接口-控制器] - 
// @Tags     media-assets/Go
//
//	@Param		model	body		assets_dto.IdList3ModelDto			false	"输入参数"
//	@success	200	{object}	restful.JSONResult{data=assets_dto.IdList3ModelDto}	"返回结果"
//
// @Router   /v1/media-assets/Go/Test6-2 [post]
func (x *GoController) Test6_2(c *gin.Context) {

    var err error
	_request := &assets_dto.IdList3ModelDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcIdList3Model{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetGoClient().Test6_2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.IdList3ModelDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

//----自动生成，请勿修改----//

package Server

import (
	"context"
	log "github.com/sirupsen/logrus"
	"sailing.cn/protobuf/core"
	"sailing.cn/protobuf/assets"
	"service.cn/assets/store111"
	"sailing.cn/v2/mapper"
	"google.golang.org/protobuf/types/known/emptypb"
)

// go 接口
type GoServer struct {
	assets.UnimplementedGoServiceServer
}

//接口类型
//type Go struct {
	
//}

// Test1 备注哈哈哈哈
func (g *GoServer) Test1(ctx context.Context, request *emptypb.Empty) (*emptypb.Empty, error) {
	model := &store.Go{}
	err := model.Test1(ctx)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// Test2 
func (g *GoServer) Test2(ctx context.Context, request *assets.GRpcIdModel) (*emptypb.Empty, error) {
	model := &store.Go{}
	err := model.Test2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// Test3 
func (g *GoServer) Test3(ctx context.Context, request *assets.GRpcIdModel) (*emptypb.Empty, error) {
	model := &store.Go{}
	err := model.Test3(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// Test4 
func (g *GoServer) Test4(ctx context.Context, request *assets.GRpcIdModel) (*assets.GRpcIdModel, error) {
	model := &store.Go{}
	data, err := model.Test4(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcIdModel{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test5 
func (g *GoServer) Test5(ctx context.Context, request *assets.GRpcIdModel) (*assets.GRpcIdListModel, error) {
	model := &store.Go{}
	data, err := model.Test5(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcIdListModel{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test6_1 
func (g *GoServer) Test6_1(ctx context.Context, request *assets.GRpcIdList2Model) (*assets.GRpcIdList2Model, error) {
	model := &store.Go{}
	data, err := model.Test6_1(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcIdList2Model{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test6_2 
func (g *GoServer) Test6_2(ctx context.Context, request *assets.GRpcIdList3Model) (*assets.GRpcIdList3Model, error) {
	model := &store.Go{}
	data, err := model.Test6_2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcIdList3Model{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

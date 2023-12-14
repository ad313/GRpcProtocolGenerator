package assets_dto

//
type IdList2ModelDto struct {
	Data  []int `json:"Data" form:"data"`  // 
}

//IdModel的注释
type IdModelDto struct {
	Id  string `json:"Id" form:"id"`  // 
}

//
type IdList3ModelDto struct {
	StringColumn  string `json:"StringColumn" form:"string_column"`  // string column.....
	Ids  []int `json:"Ids" form:"ids"`  // 
	Data  []*IdModelDto `json:"Data" form:"data"`  // 
}

//
type IdListModelDto struct {
	Data  []*IdModelDto `json:"Data" form:"data"`  // 
}


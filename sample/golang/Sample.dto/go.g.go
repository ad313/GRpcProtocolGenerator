package assets_dto

// IdList2ModelDto 
type IdList2ModelDto struct {
	Data  []int32 `json:"data" form:"data"`  // 
}

// IdModelDto IdModel的注释
type IdModelDto struct {
	Id  string `json:"id" form:"id"`  // 
}

// IdList3ModelDto 
type IdList3ModelDto struct {
	StringColumn  string `json:"string_column" form:"string_column"`  // string column.....
	Ids  []int32 `json:"ids" form:"ids"`  // 
	Data  []*IdModelDto `json:"data" form:"data"`  // 
}

// IdListModelDto 
type IdListModelDto struct {
	Data  []*IdModelDto `json:"data" form:"data"`  // 
}


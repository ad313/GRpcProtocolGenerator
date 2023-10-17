/**
 * 
 */
export class GRpcNullableClass {  
    nullableIntColumn? : number; //   
    nullableLongColumn? : number; //   
    nullableDecimalColumn? : number; //   
    nullableDoubleColumn? : number; //   
    nullableFloatColumn? : number; //   
    nullableUintColumn? : number; //   
    nullableUlongColumn? : number; //   
    nullableStringColumn? : string; //   
    nullableBoolColumn? : boolean; //   
    nullableGuidColumn? : string; //   
    nullableDateTimeColumn? : Date; //   
    nullableByteColumn? : number; //   
    nullableClassColumn : GRpcNullableClass; //   
    nullableEnumColumn? : number; // [枚举] -   
    nullableStructColumn? : GRpcSampleStruct; // 
}
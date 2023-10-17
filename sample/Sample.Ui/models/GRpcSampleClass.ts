/**
 * SampleClass
 */
export class GRpcSampleClass {  
    intColumn : number; // IntColumn  
    longColumn : number; // LongColumn  
    decimalColumn : number; // DecimalColumn  
    doubleColumn : number; // DoubleColumn  
    floatColumn : number; // FloatColumn  
    uintColumn : number; // UintColumn  
    ulongColumn : number; // UlongColumn  
    stringColumn? : string; // StringColumn  
    boolColumn : boolean; // BoolColumn  
    dateTimeColumn : Date; // DateTimeColumn  
    byteColumn : number; // ByteColumn  
    classColumn : GRpcSampleClass; // ClassColumn  
    enumColumn : number; // [枚举] - EnumColumn  
    structColumn : GRpcSampleStruct; // StructColumn
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Services.Models
{
    /// <summary>
    /// SampleStruct
    /// </summary>
    [Description("SampleStruct")]
    public struct SampleStruct
    {
        /// <summary>
        /// IntColumn
        /// </summary>
        [Display(Name="IntColumn")]
        [DisplayName("IntColumn")]
        [Description("IntColumn")]
        public int IntColumn { get; set; }

        /// <summary>
        /// LongColumn
        /// </summary>
        [Display(Name = "LongColumn")]
        [DisplayName("LongColumn")]
        [Description("LongColumn")]
        public long LongColumn { get; set; }

        /// <summary>
        /// DecimalColumn
        /// </summary>
        [Display(Name = "DecimalColumn")]
        [DisplayName("DecimalColumn")]
        [Description("DecimalColumn")]
        public decimal DecimalColumn { get; set; }

        /// <summary>
        /// DoubleColumn
        /// </summary>
        [Display(Name = "DoubleColumn")]
        [DisplayName("DoubleColumn")]
        [Description("DoubleColumn")]
        public double DoubleColumn { get; set; }

        /// <summary>
        /// FloatColumn
        /// </summary>
        [Display(Name = "FloatColumn")]
        [DisplayName("FloatColumn")]
        [Description("FloatColumn")]
        public float FloatColumn { get; set; }

        /// <summary>
        /// UintColumn
        /// </summary>
        [Display(Name = "UintColumn")]
        [DisplayName("UintColumn")]
        [Description("UintColumn")]
        public uint UintColumn { get; set; }

        /// <summary>
        /// UlongColumn
        /// </summary>
        [Display(Name = "UlongColumn")]
        [DisplayName("UlongColumn")]
        [Description("UlongColumn")]
        public ulong UlongColumn { get; set; }

        /// <summary>
        /// StringColumn
        /// </summary>
        [Display(Name = "StringColumn")]
        [DisplayName("StringColumn")]
        [Description("StringColumn")]
        public string StringColumn { get; set; }

        /// <summary>
        /// BoolColumn
        /// </summary>
        [Display(Name = "BoolColumn")]
        [DisplayName("BoolColumn")]
        [Description("BoolColumn")]
        public bool BoolColumn { get; set; }

        /// <summary>
        /// GuidColumn
        /// </summary>
        [Display(Name = "GuidColumn")]
        [DisplayName("GuidColumn")]
        [Description("GuidColumn")]
        public Guid GuidColumn { get; set; }

        /// <summary>
        /// DateTimeColumn
        /// </summary>
        [Display(Name = "DateTimeColumn")]
        [DisplayName("DateTimeColumn")]
        [Description("DateTimeColumn")]
        public DateTime DateTimeColumn { get; set; }

        /// <summary>
        /// ByteColumn
        /// </summary>
        [Display(Name = "ByteColumn")]
        [DisplayName("ByteColumn")]
        [Description("ByteColumn")]
        public byte ByteColumn { get; set; }
    }
}

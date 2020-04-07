using System;
using Dapper.Contrib.Extensions;

namespace AccessControl.API.ZebraModels
{
    [Table("Zebra_RMA_Receiving")]
    public class RMA_Receiving
    {
        public string PN { get; set; }
        [ExplicitKey]
        public string RMA_No { get; set; }
        public int Qty { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Tray_ID { get; set; }
    }
}
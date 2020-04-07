using System;
using Dapper.Contrib.Extensions;

namespace AccessControl.API.ZebraModels
{
    [Table("Zebra_User")]
    public class Zebra_User
    {
        [ExplicitKey]
        public string User_ID { get; set; }
        public string User_Station_ID { get; set; }
        public DateTime User_Login_Time { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
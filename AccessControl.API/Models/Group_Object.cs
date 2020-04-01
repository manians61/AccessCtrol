using System;

namespace AccessControl.API.Models
{
    public class Group_Object
    {
        public string Group_ID { get; set; } 
        public string PermissionObj_ID { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }

    }
}
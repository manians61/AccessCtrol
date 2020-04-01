using System.Collections.Generic;

namespace AccessControl.API.Dtos
{
    public class UserAccessDto
    {
        public string User_ID { get; set; }
        public string Username { get; set; }
        public string Group_Names { get; set; }
        public string PermissionObj_Name {get;set;}

    }
}
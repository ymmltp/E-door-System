using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_door_System.Models
{
    public class UserInfo
    {
        public string UID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool MailEnable { get; set; }
        public bool SMSEnable { get; set; }
        public int RoleID { get; set; }
        public bool Active { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
    }
}

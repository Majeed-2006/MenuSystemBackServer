using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Users
{
    public class UserChangePasswordDTO
    {
        public string UserName { get; set; }
      
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public UserChangePasswordDTO(string userName,string currentPassword, string newPassword)
        {
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}

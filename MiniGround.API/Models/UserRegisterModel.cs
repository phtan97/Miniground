using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public string NickName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string ReferalCode { get; set; }
        public CreateBankAccountModel createBankAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class CreateBankAccountModel
    {
        [Required]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        [Required]
        public string Password { get; set; }

    }
}

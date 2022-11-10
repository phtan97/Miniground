using MiniGround.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class UpdateBankAccountModel
    {
        public int UserId { get; set; }
        public string NumberAccount { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public EDefaultStatus Status { get; set; }
    }
}

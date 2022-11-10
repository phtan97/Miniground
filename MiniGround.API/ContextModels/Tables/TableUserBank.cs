using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels.Tables
{
    public class TableUserBank
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(256)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string BankName { get; set; }
        [StringLength(int.MaxValue)]
        public string Password { get; set; }
        [StringLength(50)]
        public string BankNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
    }
}

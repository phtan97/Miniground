using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels.Tables
{
    public class TableBankHistory
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int BankID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActived { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

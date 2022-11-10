using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels.Tables
{
    public class TableFieldPrice
    {
        [Key]
        public int Id { get; set; }
        public int IdFootballField { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Price { get; set; }
    }
}

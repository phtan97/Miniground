using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class InsertFieldPriceFootballModel
    {
        public int IdFootballField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>00:00:00</example>
        public string StartDate { get; set; } = "00:00:00";
        public string EndDate { get; set; } = "00:00:00";
        public double Price { get; set; }
    }
}

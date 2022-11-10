using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class InsertFieldPriceFootballModel
    {
        public int IdFootballField { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public double Price { get; set; }
    }
}

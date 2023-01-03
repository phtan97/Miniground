using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class CreateMatchModel
    {
        public int UserId { get; set; }
        public int FootballFieldId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

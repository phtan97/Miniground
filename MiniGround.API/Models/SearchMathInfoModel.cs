using MiniGround.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    public class SearchMathInfoModel
    {
        public int UserID { get; set; }
        public int FootBallFieldID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EMatchInfoStatus? Status { get; set; }
    }
}

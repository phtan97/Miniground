using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels.Tables
{
    public class TableFootBallField
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public virtual List<TableFieldPrice> FieldPrices { get; set; }
    }
}

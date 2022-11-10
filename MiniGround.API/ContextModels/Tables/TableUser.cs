using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels.Tables
{
    public class TableUser
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(int.MaxValue)]
        public string Password { get; set; }
        [StringLength(500)]
        public string FullName { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        [Phone, StringLength(100)]
        public string Phone { get; set; }
        public int Role { get; set; }
        [StringLength(100)]
        public string ReferalCode { get; set; }
        [StringLength(100)]
        public string NickName { get; set; }
    }
}

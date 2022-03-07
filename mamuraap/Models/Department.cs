using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace mamuraap.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }
    }
}

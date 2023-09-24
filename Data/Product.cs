using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myproject.Data
{
    public class Product
    {
         [Key]
        public int ProductId { get; set; }
        public bool IsAvailable { get; set; }
        public string ManufactureEmail { get; set; }
        public string ManufacturePhone { get; set; }
        public DateTime ProduceDate { get; set; }

        public string Name { get; set; }
    }
}
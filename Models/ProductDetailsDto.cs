using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myproject.Models
{
    public class ProductDetailsDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please specify the availability of the product")]

        public bool IsAvailable { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string ManufactureEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter ManufacturePhone ")]
        public string ManufacturePhone { get; set; }
        [Required(ErrorMessage = " Please insert ProductDate")]
        public DateTime ProduceDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the name of book ")]
        public string Name { get; set; }

    }
}
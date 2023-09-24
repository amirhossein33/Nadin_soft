using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myproject.Validation
{
    public class MyCustomValidation : ValidationAttribute
    {
        public MyCustomValidation()
        {
            BanKeyword = new List<string>() { "fuck" };
        }



        public List<String> BanKeyword { get; set; }
        public override string FormatErrorMessage(string name)
        {
            return "Please do not use offensive words";
        }
        public override bool IsValid(object value)
        {
            var name = (string)value;
            if (BanKeyword.Contains(name.ToLower())) return false;
            return true;
        }
    }
}
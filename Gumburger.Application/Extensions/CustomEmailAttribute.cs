using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Application.Extensions
{
    public class CustomEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string data;

            if (value == null) //null ise geçersizdir.
                return false;

            data = value.ToString();

            if (data.Where(d => d == ' ').ToList().Count > 0) //eğer boşluk var ise geçersizdir.
                return false;

            if (data.Split("@").Count() > 2) //birden fazla @ varsa geçersizdir.
                return false;

            if (data.EndsWith("@gmail.com") || data.EndsWith("@hotmail.com") || data.EndsWith("@yahoo.com") || data.EndsWith("@outlook.com") || data.EndsWith("@admin.com"))
                return true;

            return false;
        }
    }
}

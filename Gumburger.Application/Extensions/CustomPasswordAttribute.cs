using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gumburger.Application.Extensions
{
    public class CustomPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return false;

            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigit = new Regex(@"[0-9]+");
            var hasSpecialChar = new Regex(@"[!@#$%^&*()_+[\]{};':\""|<>/?]+");


            var isValid = hasUpperCase.IsMatch(password) &&
                          hasLowerCase.IsMatch(password) &&
                          hasDigit.IsMatch(password) &&
                          hasSpecialChar.IsMatch(password);

            return isValid;
        }
    }
}

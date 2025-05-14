using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Extensions;

namespace Gumburger.Application.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        [CustomEmail(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}

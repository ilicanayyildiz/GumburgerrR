using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Extensions;
using Gumburger.Domain.Enums;

namespace Gumburger.Application.Models.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Firstname cannot be more than 20 characters!", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Lastname cannot be more than 30 characters!", MinimumLength = 3)]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }

        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; }
        public string? ProfileImagePath { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Username cannot be more than 15 characters!", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [CustomEmail(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password must have more than 6 and less than 20 characters!")]
        [CustomPassword(ErrorMessage = "Password must have at least 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character.")]
        public string Password { get; set; }

    }
}

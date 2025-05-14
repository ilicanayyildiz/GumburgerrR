using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Application.Models.DTOs
{
    public class AddressDTO
    {
        [Required]
        public string FullAddress { get; set; }
        public string AppUserId { get; set; }

        public Guid Id { get; set; }

    }
}

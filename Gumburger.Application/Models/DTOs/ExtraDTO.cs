using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Gumburger.Application.Models.DTOs
{
    public class ExtraDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string ExtraName { get; set; }
        public string? ExtraImageUrl { get; set; }
        public decimal ExtraPrice { get; set; }
        public Status Status { get; set; }

        public IFormFile? ExtraUploadPath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bilicra.API.Domain.Models
{
    public class ProductCatolog
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Photo { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

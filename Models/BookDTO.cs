using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BaseProjectAPI.Models
{
    public class BookDTO
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        public double Price { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}

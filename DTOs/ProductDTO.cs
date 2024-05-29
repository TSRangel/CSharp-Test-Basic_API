using MinhaApi.Models;
using MinhaApi.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinhaApi.DTOs
{
    public class ProductDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(80)]
        [FisrtLetterCapitalized]
        public string? Name { get; set; }
        [Required]
        [StringLength(150)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
    }
}

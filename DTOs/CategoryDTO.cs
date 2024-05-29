using MinhaApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinhaApi.DTOs
{
    public sealed record CategoryDTO
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs
{
    public class ProductDTOUpdateRequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "O estoque deve estar entre 1 e 9999.")]
        public float Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CreatedAt.Date <= DateTime.Now)
                yield return new ValidationResult("A data deve ser maior que a data atual",
                    new[] { nameof(this.CreatedAt) });
        }
    }
}

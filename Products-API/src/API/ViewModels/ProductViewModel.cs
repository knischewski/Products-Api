using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        public string ImagemUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        // public string SupplierName { get; set; }
    }
}

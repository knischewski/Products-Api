using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Document { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int SupplierType { get; set; }

        public AddressViewModel Address { get; set; }

        public bool Active { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}

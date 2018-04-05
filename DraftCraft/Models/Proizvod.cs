using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DraftCraft.Models
{
    [Table("Proizvod")]
    public class Proizvod
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Unesi naziv proizvod")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Naziv proizvoda mora biti između 3 i 50 znakova")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Naziv proizvoda mora sadržavati slova i/ili brojeve")]
        [Display(Name = "Proizvod")]
        public string Naziv { get; set; }
        
        [Required(ErrorMessage = "Unesi opis proizvoda")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Opis proizvoda mora biti između 10 i 200 znakova")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Opis proizvoda mora sadržavati slova i/ili brojeve")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Unesi cijenu")]
        [Range(0.10,10000, ErrorMessage = "Unesi cijenu između 0.10 i 10 000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Cijena { get; set; }

        [Required(ErrorMessage = "Unesi širinu proizvoda")]
        [Range(0.01, 10000, ErrorMessage = "Unesi širinu proizvoda između 0.01 i 10 000")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Širina sadržava samo brojeve")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        public int Sirina { get; set; }

        [Required(ErrorMessage = "Unesi visinu proizvoda")]
        [Range(0.01, 10000, ErrorMessage = "Unesi visinu proizvoda između 0.01 i 10 000")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Visina sadržava samo brojeve")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        public int Visina { get; set; }

        [Required(ErrorMessage = "Unesi dubinu proizvoda")]
        [Range(0.01, 10000, ErrorMessage = "Unesi dubinu proizvoda između 0.01 i 10 000")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Dubina sadržava samo brojeve")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        public int Dubina { get; set; }

        public int? KategorijaID { get; set; }
        public virtual Kategorija Kategorija { get; set; }
    }
}
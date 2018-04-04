using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DraftCraft.Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesi naziv kategorije")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "Naziv kategorije mora biti između 3 i 50 znakova")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Nazov kategorije mora sadržavati veliko početno slovo, slova i razmak")]
        [Display(Name = "Kategorija")]
        public string Naziv { get; set; }
        public virtual ICollection<Proizvod> Proizvodi { get; set; }
    }
}
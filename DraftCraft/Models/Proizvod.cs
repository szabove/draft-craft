using System.ComponentModel.DataAnnotations.Schema;

namespace DraftCraft.Models
{
    [Table("Proizvod")]
    public class Proizvod
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public int? KategorijaID { get; set; }
        public virtual Kategorija Kategorija { get; set; }
    }
}
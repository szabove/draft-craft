using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraftCraft.Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}
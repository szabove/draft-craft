using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraftCraft.Models
{
    [Table("SlikeProizvoda")]
    public class SlikaProizvoda
    {
        public int ID { get; set; }
        [Display(Name = "Datoteka")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string ImeDatoteke { get; set; }

        
        public int ProizvodID { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
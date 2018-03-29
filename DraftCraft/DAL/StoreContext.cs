using DraftCraft.Models;
using System.Data.Entity;

namespace DraftCraft.DAL
{
    public class StoreContext : DbContext
    {
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
    }
}
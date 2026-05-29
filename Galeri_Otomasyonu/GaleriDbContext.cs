using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Galeri_Otomasyonu
{
    internal class GaleriDbContext : DbContext
    {
        public GaleriDbContext() : base("name=GaleriDbContext")
        {

        }
        public DbSet<Musteri> Table_Musteri { get; set; }
      
        public DbSet<Araclar> Table_Araclar { get; set; }
      
        public DbSet<Vites> Table_Vites { get; set; }       

        public DbSet<Yakit> Table_Yakit { get; set; }

        public DbSet<Durum> Table_Durum { get; set; }

        public DbSet<Satis> Table_Satislar { get; set; }
    }
}

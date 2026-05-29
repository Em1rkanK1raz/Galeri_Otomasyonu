using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeri_Otomasyonu
{
    [Table("Table_Satislar")]
    internal class Satis
    {
        [Key]
        public int SatisID { get; set; }

        [ForeignKey("Table_Musteri")]
        public int Musteri_Id { get; set; }

        [ForeignKey("Table_Araclar")]

        public int AracID { get; set; }

        [Required]

        public DateTime Satis_Tarihi { get; set; }

        [Required]
        public decimal Satis_Fiyati { get; set; }

        public virtual Musteri Table_Musteri { get; set; }

        public virtual Araclar Table_Araclar { get; set; }
    }
}

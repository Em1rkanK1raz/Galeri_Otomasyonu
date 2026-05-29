using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeri_Otomasyonu
{
    [Table("Table_Araclar")]
    internal class Araclar
    {
        [Key]
        public int AracID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Arac_Plaka { get; set; }

        [Required]
        [MaxLength(50)]
        public string Arac_Marka { get; set; }

        [Required]
        [MaxLength(50)]
        public string Arac_Model { get; set; }

        [Required]
        public int Arac_Model_Yili { get; set; }

        [Required]
        public int Arac_Kilometresi { get; set; }

        [Required]

        public decimal Arac_Fiyat { get; set; }

        [ForeignKey("Table_Vites")]

        public int VitesID { get; set; }

        [ForeignKey("Table_Yakit")]
        public int YakitID { get; set; }

        [ForeignKey("Table_Durum")]

        public int DurumID { get; set; }

        public virtual Vites Table_Vites { get; set; }

        public virtual Yakit Table_Yakit { get; set; }

        public virtual Durum Table_Durum { get; set; }

        public virtual ICollection<Satis> Table_Satislar { get; set; }

    }
}

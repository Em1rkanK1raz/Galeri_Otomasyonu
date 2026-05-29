using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Galeri_Otomasyonu
{
    [Table("Table_Musteri")]
    internal class Musteri
    {
        [Key]
        public int Musteri_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Musteri_Ad { get; set; }

        [Required]
        [MaxLength(50)]
        public string Musteri_Soyad { get; set; }

        [Required]
        [MaxLength(11)]
        public string Musteri_TcNo { get; set; }

        [Required]
        [MaxLength(15)]
        public string Musteri_Telefon { get; set; }

        [Required]
        [MaxLength(50)]
        public string Musteri_Email { get; set; }

        public virtual ICollection<Satis> Table_Satislar { get; set; }

    }
}

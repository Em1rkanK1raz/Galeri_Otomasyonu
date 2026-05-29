using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeri_Otomasyonu
{
    [Table("Table_Durum")]
    internal class Durum
    {
        [Key]
        public int DurumID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Arac_Durumu { get; set; }

        public virtual ICollection<Araclar> Table_Araclar { get; set; }
    }
}

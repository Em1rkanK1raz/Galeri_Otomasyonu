using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeri_Otomasyonu
{
    [Table("Table_Yakit")]
    internal class Yakit
    {
        [Key]
        public int YakitID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Yakit_Turu { get; set; }

        public virtual ICollection<Araclar> Table_Araclar { get; set; }
    }
}

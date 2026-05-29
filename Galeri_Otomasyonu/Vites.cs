using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeri_Otomasyonu
{
    [Table("Table_Vites")]
    internal class Vites
    {
        [Key]
        public int VitesID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Vites_Turu { get; set; }
        public virtual ICollection<Araclar> Table_Araclar { get; set; }


    }
}

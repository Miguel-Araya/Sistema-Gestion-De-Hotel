using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoomType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomTypeId { get; set; }
        [Required]
        public string RoomTypeName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Characteristics { get; set; }
        [Required]
        public string description { get; set; }

        public string Image { get; set; }

    }
}

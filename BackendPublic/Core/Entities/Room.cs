using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }

        public bool IsActice{ get; set; }

        public int RoomNumber { get; set; }
        public bool Status { get; set; }


    }
}

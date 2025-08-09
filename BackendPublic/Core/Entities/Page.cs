using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Page
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageID { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public ICollection<PageImage> PageImages { get; set; } = new List<PageImage>();
    }
}

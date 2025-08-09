using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PageImage
    {
        public int PageID { get; set; }
        public Page Page { get; set; }
        public int ImageID { get; set; }
        public Image Image { get; set; }
    }

}

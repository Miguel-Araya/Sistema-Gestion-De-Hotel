using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PageAboutUsDTO
    {
        public int PageID { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public List<ImageDTO> Images { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}

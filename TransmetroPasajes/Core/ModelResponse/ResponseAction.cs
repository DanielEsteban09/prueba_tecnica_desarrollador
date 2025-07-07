using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ModelResponse
{
    public class ResponseAction
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public int? Id { get; set; }
        public string error { get; set; }
    }
}

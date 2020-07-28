using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplate.Web.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }    
        public string HtmlData { get; set; }    
    }
}

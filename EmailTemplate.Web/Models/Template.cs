using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplate.Web.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplate.Web.Models
{
    public class TemplateMap
    {
        public TemplateMap(EntityTypeBuilder<Template> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Value).IsRequired();
            entityBuilder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}

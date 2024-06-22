using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.Domain.Entities
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)] 
        public string? BlogTitle { get; set; }

        [StringLength(500)]
        public string? BlogBody { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

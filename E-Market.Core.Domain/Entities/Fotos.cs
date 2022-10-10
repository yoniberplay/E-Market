using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entities
{
    public class Fotos : AuditableBaseEntity
    {
        public int AnuncioID { get; set; }
        public string? ImageUrl { get; set; }

        public int userId { get; set; }
        public Anuncio? anuncio { get; set; }
        public User? User { get; set; }
    }
}

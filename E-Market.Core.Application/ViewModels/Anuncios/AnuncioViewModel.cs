using E_Market.Core.Application.ViewModels.Fotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Anuncios
{
    public class AnuncioViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public List<FotoViewModel>? fotos { get; set; }
    }
}

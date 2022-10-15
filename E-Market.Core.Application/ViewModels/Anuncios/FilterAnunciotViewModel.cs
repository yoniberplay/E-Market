using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Market.Core.Application.ViewModels.Categories;

namespace E_Market.Core.Application.ViewModels.Anuncios
{
    public class FilterAnunciotViewModel
    {
        public Boolean[]? BusquedaCategoria { get; set; }
        public String? Nombre { get; set; }

        public int? CategoryId { get; set; }

        public List<CategoryViewModel>? Categories { get; set; }
    }
}

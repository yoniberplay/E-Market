using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Fotos
{
    public class SaveFotoViewModel
    {
        public int Id { get; set; }
        public int AnuncioID { get; set; }
        public string? ImageUrl { get; set; }

    }
}

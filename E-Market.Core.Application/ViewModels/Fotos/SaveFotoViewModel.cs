using E_Market.Core.Application.ViewModels.Anuncios;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Fotos
{
    public class SaveFotoViewModel : SaveAnuncioViewModel
    {
        public int Idfoto { get; set; }
        public int AnuncioID { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File2 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File3 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File4 { get; set; }


    }
}

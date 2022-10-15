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
        


    }
}

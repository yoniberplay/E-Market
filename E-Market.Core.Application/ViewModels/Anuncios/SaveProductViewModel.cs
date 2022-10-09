using Microsoft.AspNetCore.Http;
using E_Market.Core.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Anuncios
{
    public class SaveAnuncioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre del anuncio")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Debe colocar el precio del anuncio")]
        public double? Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la categoria del anuncio")]
        public int CategoryId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public int UserId { get; set; }

    }
}

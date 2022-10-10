using E_Market.Core.Application.ViewModels.Anuncios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Services
{
    public interface IAnuncioService : IGenericService<SaveAnuncioViewModel, AnuncioViewModel>
    {
        Task<List<AnuncioViewModel>> GetAllViewModelWithFilters(FilterAnunciotViewModel filters);
        Task<AnuncioViewModel> GetAnuncioyDetalles(int id);
    }
}

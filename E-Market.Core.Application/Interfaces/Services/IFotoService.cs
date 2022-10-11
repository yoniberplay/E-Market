using E_Market.Core.Application.ViewModels.Fotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Services
{
    public interface IFotoService : IGenericService<SaveFotoViewModel, FotoViewModel>
    {
        Task<List<FotoViewModel>> GetAllFotos(int id);
        Task<List<FotoViewModel>> GetFotosporIDanuncio(int Id);
    }
}

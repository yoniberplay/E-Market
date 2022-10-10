using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Fotos;
using E_Market.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository? _fotoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public FotoService(IFotoRepository fotoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _fotoRepository = fotoRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public Task<SaveFotoViewModel> Add(SaveFotoViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FotoViewModel>> GetAllFotos(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FotoViewModel>> GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<SaveFotoViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(SaveFotoViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}

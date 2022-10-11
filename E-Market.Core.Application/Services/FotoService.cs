using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Fotos;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
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


        public async Task<List<FotoViewModel>> GetFotosporIDanuncio(int Id)
        {
            var fotocioList = await _fotoRepository.GetAllAsync();

            var listViewModels = fotocioList.Where(x => x.AnuncioID == Id).Select(Foto => new FotoViewModel
            {
                AnuncioID = Foto.AnuncioID,
                ImageUrl = Foto.ImageUrl

            }).ToList();

            return listViewModels;
        }

        public async Task<SaveFotoViewModel> Add(SaveFotoViewModel vm)
        {
            Fotos foto = new();
            foto.AnuncioID = vm.AnuncioID;
            foto.ImageUrl = vm.ImageUrl;
            foto.UserId = vm.UserId;

            foto = await _fotoRepository.AddAsync(foto);

            SaveFotoViewModel fotoVm = new();

            fotoVm.Id = foto.Id;
            fotoVm.ImageUrl = foto.ImageUrl;
            fotoVm.UserId = foto.UserId;

            return fotoVm;
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

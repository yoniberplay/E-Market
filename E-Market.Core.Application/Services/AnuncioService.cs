using Microsoft.AspNetCore.Http;
using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Anuncios;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Market.Core.Application.ViewModels.Fotos;

namespace E_Market.Core.Application.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public AnuncioService(IAnuncioRepository anuncioRepository, IHttpContextAccessor httpContextAccessor)
        {
            _anuncioRepository = anuncioRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task Update(SaveAnuncioViewModel vm)
        {
            Anuncio anuncio = await _anuncioRepository.GetByIdAsync(vm.Id);
            anuncio.Id = vm.Id;
            anuncio.Name = vm.Name;
            anuncio.Price = vm.Price;
            anuncio.ImageUrl = vm.ImageUrl;
            anuncio.Description = vm.Description;
            anuncio.CategoryId = vm.CategoryId;

            await _anuncioRepository.UpdateAsync(anuncio);
        }

        public async Task<SaveAnuncioViewModel> Add(SaveAnuncioViewModel vm)
        {
            Anuncio anuncio = new();
            anuncio.Name = vm.Name;
            anuncio.Price = vm.Price;
            anuncio.ImageUrl = vm.ImageUrl;
            anuncio.Description = vm.Description;
            anuncio.CategoryId = vm.CategoryId;
            anuncio.UserId = userViewModel.Id;

            anuncio = await _anuncioRepository.AddAsync(anuncio);

            SaveAnuncioViewModel anuncioVm = new();

            anuncioVm.Id = anuncio.Id;
            anuncioVm.Name = anuncio.Name;
            anuncioVm.Price = anuncio.Price;
            anuncioVm.ImageUrl = anuncio.ImageUrl;
            anuncioVm.Description = anuncio.Description;
            anuncioVm.CategoryId = anuncio.CategoryId;
            anuncioVm.UserId = anuncio.UserId;

            return anuncioVm;
        }

        public async Task Delete(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            await _anuncioRepository.DeleteAsync(anuncio);
        }

        public async Task<SaveAnuncioViewModel> GetByIdSaveViewModel(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);

            SaveAnuncioViewModel vm = new();
            vm.Id = anuncio.Id;
            vm.Name = anuncio.Name;
            vm.Description = anuncio.Description;
            vm.CategoryId = anuncio.CategoryId;
            vm.Price = anuncio.Price;
            vm.ImageUrl = anuncio.ImageUrl;

            return vm;
        }

        public async Task<List<AnuncioViewModel>> GetAllViewModel()
        {
            var anuncioList = await _anuncioRepository.GetAllWithIncludeAsync(new List<string> { "Category" });
            
            return anuncioList.Where(anuncio => anuncio.UserId == userViewModel.Id).Select(anuncio => new AnuncioViewModel
            {
                Name = anuncio.Name,
                Description = anuncio.Description,
                Id = anuncio.Id,
                Price = anuncio.Price,
                ImageUrl = anuncio.ImageUrl,
                CategoryName = anuncio.Category.Name,
                CategoryId = anuncio.Category.Id,
                
            }).ToList();
        }

        public async Task<AnuncioViewModel> GetAnuncioyDetalles(int Id)
        {
            var anuncioList = await _anuncioRepository.GetBywithRelationship(Id);

            AnuncioViewModel Viwtemp = new AnuncioViewModel();

            Viwtemp.Name = anuncioList.Name;
            Viwtemp.Price = anuncioList.Price;
            Viwtemp.Description = anuncioList.Description;
            Viwtemp.CategoryId = anuncioList.CategoryId;
            Viwtemp.CategoryName = anuncioList.Category.Name;
            Viwtemp.Id = anuncioList.Id;
            Viwtemp.Created = anuncioList.Created;
            Viwtemp.UserId = anuncioList.UserId;

            Viwtemp.userViewModel.Name = anuncioList.User.Name;
            Viwtemp.userViewModel.Email = anuncioList.User.Email;
            Viwtemp.userViewModel.Phone = anuncioList.User.Phone;


            List<FotoViewModel> fttemp = new List<FotoViewModel>();

            foreach (var fa in anuncioList.Fotos)
            {
                fttemp.Add(new FotoViewModel()
                {
                 AnuncioID = fa.Id,
                ImageUrl = fa.ImageUrl
            });
            }

            Viwtemp.fotos = fttemp;
            return Viwtemp;
            
        }

        public async Task<List<AnuncioViewModel>> GetAllViewModelWithFilters(FilterAnunciotViewModel filters)
        {
            var anuncioList = await _anuncioRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModels = anuncioList.Where(anuncio => anuncio.UserId != userViewModel.Id).Select(anuncio => new AnuncioViewModel
            {
                Name = anuncio.Name,
                Description = anuncio.Description,
                Id = anuncio.Id,
                Price = anuncio.Price,
                ImageUrl = anuncio.ImageUrl,
                CategoryName = anuncio.Category.Name,
                CategoryId = anuncio.Category.Id
            }).ToList();

            if (filters.CategoryId != null)
            {
                listViewModels = listViewModels.Where(product => product.CategoryId == filters.CategoryId.Value).ToList();

            }

            return listViewModels;
        }




    }
}

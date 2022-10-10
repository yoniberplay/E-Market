using Microsoft.AspNetCore.Http;
using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Categories;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task Update(SaveCategoryViewModel vm)
        {
            Category category = await _categoryRepository.GetByIdAsync(vm.Id);
            category.Id = vm.Id;
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<SaveCategoryViewModel> Add(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;

            category = await _categoryRepository.AddAsync(category);

            SaveCategoryViewModel categoryVm = new();

            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Description = category.Description;

            return categoryVm;
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            SaveCategoryViewModel vm = new();
            vm.Id = category.Id;
            vm.Name = category.Name;
            vm.Description = category.Description;

            return vm;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var categoryList = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Anuncios" });

            return categoryList.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                ProductsQuantity = category.Anuncios.Where(anuncio => anuncio.UserId == userViewModel.Id).Count()
            }).ToList();
        }
    }
    }

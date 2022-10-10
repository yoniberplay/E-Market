using Microsoft.AspNetCore.Mvc;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Categories;
using E_Market.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;
using E_Market.Middlewares;

namespace StockApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public CategoryController(ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _categoryService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveCategory", new SaveCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Add(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveCategory", await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Update(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _categoryService.Delete(id);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
    }
}

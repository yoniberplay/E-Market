using E_Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Anuncios;
using E_Market.Middlewares;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Application.ViewModels.Fotos;

namespace E_Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnuncioService _anunciotService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IFotoService _IFotoService;

        public HomeController(IAnuncioService anuncioService, ICategoryService categoryService, IFotoService ifotoservice,
            ValidateUserSession validateUserSession, IUserService userService)
        {
            _anunciotService = anuncioService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
            _IFotoService = ifotoservice;
            _userService = userService; ;
        }

        public async Task<IActionResult> Index(FilterAnunciotViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            
            ViewBag.Categories = await _categoryService.GetAllViewModel();
            return View(await _anunciotService.GetAllViewModelWithFilters(vm));
        }


       
        public async Task<IActionResult> Detalleanuncio(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            //Todas las fotos de ese anuncio

            List<FotoViewModel> listafotos = await _IFotoService.GetFotosporIDanuncio(id);

            AnuncioViewModel savm = await _anunciotService.GetAnuncioyDetalles(id);
            //Datos del propietario del anuncio
            UserViewModel user = await _userService.GetByIdViewModel(savm.UserId);

            ViewBag.User = user;

            return View(listafotos);

        }

    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Anuncios;
using System;
using System.IO;
using System.Threading.Tasks;
using E_Market.Middlewares;

namespace E_Market.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly IAnuncioService _anunciotService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AnuncioController(IAnuncioService anunciotService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _anunciotService = anunciotService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _anunciotService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveAnuncioViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAnuncio", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAnuncioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveAnuncio", vm);
            }

            SaveAnuncioViewModel AnuncioVm = await _anunciotService.Add(vm);

            if (AnuncioVm.Id != 0 && AnuncioVm != null)
            {
                AnuncioVm.ImageUrl = UploadFile(vm.File, AnuncioVm.Id);

                await _anunciotService.Update(AnuncioVm);
            }

            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveAnuncioViewModel vm = await _anunciotService.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAnuncio", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAnuncioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveAnuncio", vm);
            }

            SaveAnuncioViewModel AnuncioVm = await _anunciotService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, AnuncioVm.ImageUrl);
            await _anunciotService.Update(vm);
            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _anunciotService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _anunciotService.Delete(id);

            string basePath = $"/Images/Anuncios/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Anuncios/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
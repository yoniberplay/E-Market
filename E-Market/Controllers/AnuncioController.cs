using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Anuncios;
using System;
using System.IO;
using System.Threading.Tasks;
using E_Market.Middlewares;
using E_Market.Core.Application.ViewModels.Fotos;

namespace E_Market.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly IAnuncioService _anunciotService;
        private readonly IFotoService _fotoService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AnuncioController(IAnuncioService anunciotService, ICategoryService categoryService, ValidateUserSession validateUserSession, IFotoService fotoService)
        {
            _anunciotService = anunciotService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
            _fotoService = fotoService;
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
            SaveFotoViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAnuncio", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveFotoViewModel vm)
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


                vm.ImageUrl = AnuncioVm.ImageUrl;
                vm.AnuncioID = AnuncioVm.Id;
                vm.UserId = AnuncioVm.UserId;

                await _fotoService.Add(vm);

                if (vm.File2 != null)
                    {
                        vm.ImageUrl = UploadFile(vm.File2, AnuncioVm.Id);
                        await _fotoService.Add(vm);
                    }
                    if (vm.File3 != null)
                    {
                        vm.ImageUrl = UploadFile(vm.File3, AnuncioVm.Id);
                        await _fotoService.Add(vm);
                    }
                    if (vm.File4 != null)
                    {
                        vm.ImageUrl = UploadFile(vm.File4, AnuncioVm.Id);
                        await _fotoService.Add(vm);
                    }

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

            SaveFotoViewModel sf = new();
            sf.Id = vm.Id;
            sf.Name = vm.Name;
            sf.Description = vm.Description;
            sf.Categories = vm.Categories;
            sf.Price = vm.Price;
            sf.CategoryId = vm.CategoryId;
            sf.UserId = vm.UserId;

            sf.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAnuncio", sf);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveFotoViewModel vm)
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

            SaveAnuncioViewModel sf = await _anunciotService.GetByIdSaveViewModel(vm.Id);
            sf.Name = vm.Name;
            sf.Description = vm.Description;
            sf.Categories = vm.Categories;
            sf.Price = vm.Price;
            sf.CategoryId = vm.CategoryId;
            sf.UserId = vm.UserId;

            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, sf.ImageUrl);
            await _anunciotService.Update(sf);

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
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await _userRepository.LoginAsync(vm);

            if(user == null)
            {
                return null;
            }

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Username = user.Username;
            userVm.Phone = user.Phone;
            userVm.Password = user.Password;
            userVm.Email = user.Email;

            return userVm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = await _userRepository.GetByIdAsync(vm.Id);
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;       

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Phone = user.Phone;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;

            return userVm;
        }

        public async Task Delete(int id)
        {
            var product = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(product);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.Username = user.Username;
            vm.Password = user.Password;
            vm.Phone = user.Phone;
            vm.Email = user.Email;
            return vm;

        }



        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return userList.Select(user => new UserViewModel
            {
                Name = user.Name,
                Username = user.Username,
                Id= user.Id,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            }).ToList();
        }

        public async Task<UserViewModel> GetByIdViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            UserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.Username = user.Username;
            vm.Password = user.Password;
            vm.Phone = user.Phone;
            vm.Email = user.Email;
            return vm;

        }
    }
}

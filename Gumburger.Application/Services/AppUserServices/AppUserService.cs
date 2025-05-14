using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Models.VMs;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gumburger.Application.Services.AppUserServices
{
    public class AppUserService : IAppUserService
    {
        private readonly IBaseRepository<AppUser> _repository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AppUserService(IBaseRepository<AppUser> repository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task Create(CustomerDTO model)
        {
            AppUser newUser = new AppUser();
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.Gender = model.Gender;
            newUser.BirthDate = model.BirthDate;
            newUser.PhoneNumber = model.Phone;
            newUser.ProfileImagePath = model.ProfileImagePath;
            newUser.Status = Domain.Enums.Status.Active;
            newUser.CreatedDate = DateTime.Now;
            newUser.UserName = model.UserName;
            newUser.NormalizedEmail = model.UserName.ToUpper();
            newUser.Email = model.Email;
            newUser.NormalizedEmail = model.Email.ToUpper();
            newUser.SecurityStamp = Guid.NewGuid().ToString();

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, model.Password);
            await _repository.Insert(newUser);
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _repository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    Password = x.PasswordHash,
                    Email = x.Email

                },
                where: x => x.UserName == userName
                );
            return result;
        }

        public async Task<List<CustomerVM>> GetCustomers()
        {
            return await (_repository.GetFilteredList(
                select: x => new CustomerVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    BirthDate = x.BirthDate,
                    Phone = x.PhoneNumber,
                    ProfileImagePath = x.ProfileImagePath,
                    Status = x.Status,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedDate = x.DeletedDate,
                    Orders = x.Orders,
                    Addresses = x.Addresses
                },
                where: x => x.Email != "admin@admin.com",
                orderBy: x => x.OrderByDescending(x => x.CreatedDate),
                include: x => x.Include(x => x.Orders).Include(x => x.Addresses)
                ));
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            AppUser appUser = await _repository.GetSingleDefault(x => x.Email == model.Email);
            if (appUser == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, model.Password, false, false);

            return result;
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            AppUser newUser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Status = model.Status,
                CreatedDate = DateTime.Now
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            //if(result.Succeeded)
            //{
            //    //_userManager.AddToRoleAsync(newUser, "User");

            //}
            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _repository.GetDefault(x => x.Id == model.Id);
            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                await _userManager.UpdateAsync(user);
            }

            if (model.Email != null)
            {
                AppUser isUserEmailExists = await _userManager.FindByEmailAsync(model.Email);
                if (isUserEmailExists != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }
    }
}

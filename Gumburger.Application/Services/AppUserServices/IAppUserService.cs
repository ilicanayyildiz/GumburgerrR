using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Models.VMs;
using Microsoft.AspNetCore.Identity;

namespace Gumburger.Application.Services.AppUserServices
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);

        Task<SignInResult> Login(LoginDTO model);

        Task<UpdateProfileDTO> GetByUserName(string userName);

        Task UpdateUser(UpdateProfileDTO model);

        Task Logout();

        Task<List<CustomerVM>> GetCustomers();

        Task Create(CustomerDTO model);



    }
}

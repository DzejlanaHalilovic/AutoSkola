using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> acceptJuru(int id);
        public Task<bool> declineJury(int id);

        public Task<UserResponse> register(RegisterRequest request);
        public Task<LoginResponse>login(LoginRequest request);
        public Task<string> forgotPassword(string email);
        public Task<string> chechToken(string token);
        public Task<bool>resetPassword(ResetPasswordRequest request);
    }
}

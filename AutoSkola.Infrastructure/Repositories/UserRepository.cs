using AutoMapper;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserRepository( DataContext context,IMapper mapper, UserManager<User>manager) : base(context, mapper)
        {
            
            this.mapper = mapper;
            this.userManager = manager;
        }

        public async Task<bool> acceptUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            user.EmailConfirmed = true;
            string to = user.Email;
            string from = "nordingsoftversko@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are accepted bytthe adminstrator. Now, you can use our site and rate many art paintings";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential basicCredential = new NetworkCredential("nordingsoftversko@gmail.com", "vbahpxfxlkowjabt");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential;
            await userManager.UpdateAsync(user);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public Task<string> chechToken(string token)
        {
            throw new NotImplementedException();
        }

        public async  Task<bool> declineUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            string to = user.Email;
            string from = "nordingsoftversko@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are not accepted by the administrator";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential networkCredential = new NetworkCredential("nordingsoftversko@gmail.com", "vbahpxfxlkowjabt");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = networkCredential;
            try
            {
                client.Send(message);
            }
            catch (Exception ex) { throw ex; }
            await userManager.DeleteAsync(user);
            return true;
        }

        public Task<string> forgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new LoginResponse { error = "User with this email does not exist" };
            if (await userManager.CheckPasswordAsync(user, request.Password)) 
            {
                if (!user.EmailConfirmed)
                    return new LoginResponse { error = "You are not accepted by the administrator" };
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTk"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, request.Email)
                };
                var token = new JwtSecurityToken(
                   expires: DateTime.Now.AddHours(1),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                   );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var role = await userManager.GetRolesAsync(user);
                var mappedUser = mapper.Map<UserResponse>(user);

                return new LoginResponse
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    painter = mappedUser,
                    role = role,
                    error = ""
                };

            }
            else 
            {
                return new LoginResponse { error = "Password is not correct" };
            }
        }

        public async Task<UserResponse> register(RegisterRequest request)
        {
            var existUser = await userManager.FindByEmailAsync(request.Email);
            if (existUser != null)
                return new UserResponse { Error = "User with this email already exists" };
            var user = mapper.Map<User>(request);
            var result = await userManager.CreateAsync(user, request.Password);
            if(request.Role == 2)
            {
                await userManager.AddToRoleAsync(user, "Polaznik");
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }
            else if(request.Role == 3)
            {
                await userManager.AddToRoleAsync(user, "Instuktor");
            }
            if (!result.Succeeded)
            {
                return null;
            }
            var userMapped = mapper.Map<UserResponse>(user);
            return userMapped;
        }

        public Task<bool> resetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

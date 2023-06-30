using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Data.Models;
using AutoSkola.Mediator.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public AuthController(UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [HttpPost("check-token")]
        public async Task<IActionResult> checkToken([FromBody] string token)
        {
            var user = await userManager.Users.Where(x => x.SecurityStamp == token).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest(new { error = "User with this token does not exist" });
            return Ok(true);

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(RegisterRequest request)
        {
            var result = await mediator.Send(new UserRegisterCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult>login(LoginRequest request)
        {
            var result = await mediator.Send(new UserLoginCommand(request));
            if (!result.IsSuccess)
                return BadRequest(new { msg = result.Errors.FirstOrDefault() });
            return Ok(result.Data);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> forgotPassword([FromBody] string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new { error = "User with this email does not exist" });
            string to = user.Email;
            string from = "nordingsoftversko@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailbody = $"Hi {user.Ime}, \n" + Environment.NewLine + $"Click here to change your password: http://localhost:4200/forgot-password/{user.SecurityStamp}";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential basicCredential = new NetworkCredential("nordingsoftversko@gmail.com", "enlqgxnvslrfhxsv");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(true);




        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> resetPassword(ResetPasswordRequest request)
        {
            var user = await userManager.Users.Where(x => x.SecurityStamp == request.token).FirstOrDefaultAsync();
            var tokenReset = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, tokenReset, request.newPassword);
            return Ok(result);
        }



    }
}

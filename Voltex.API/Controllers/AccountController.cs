using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Security.Claims;
using Voltex.API.Helper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;
namespace Voltex.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO data)
        {
            var result = await _unitOfWork.authentication.RegisterAsync(data);
            if(result.StartsWith("This"))
            {
                return Conflict(new ProblemDetails()
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Conflict on some data",
                    Detail = result
                });
            }
            else if(result != "Done")
            {
                return BadRequest(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = result
                });
            }
            else return StatusCode(statusCode: StatusCodes.Status201Created , new ResponseApi<string>(message: "User Created Successfully" , data: $"\"email\":\"{data.Email}\""));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO data) {

            var res = await _unitOfWork.authentication.LoginAsync(data);
            if (res.StartsWith("Please"))
            {
                return BadRequest(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = res
                });
            }
            Response.Cookies.Append("token", res, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Domain = "localhost",
                SameSite = SameSiteMode.Strict,
                IsEssential = true,
                Expires = DateTime.Now.AddHours(1)
            });
            return Ok(new { message = "Logged in successfully" });
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ActiveEmailDTO emailDTO)
        {
            var res = await _unitOfWork.authentication.ConfirmEmailAsync(emailDTO);

            if (res)
            {
                return Ok(new { message = "Email Confirmed" });

            }
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Not Found",
                Detail = "Email is not exist"
            });

        }
        [HttpGet("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var res = await _unitOfWork.authentication.SendResetPasswordEmailAsync(email);
            if (res)
            {
                return Ok(new { message = "password reset link sent for this email" });
            }
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Not Found",
                Detail = "This email is not exist"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var res = await _unitOfWork.authentication.ResetPasswordAsync(resetPassword);
            if(res == "Password Reset Successfully")
            {
                return Ok(new { message = res });
            }
            else if(res == "This user is not exist")
            {
                return NotFound(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Not Found",
                    Detail = res
                });
            }
            else return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = res
            });

        }

        [HttpPut]
        public async Task<IActionResult> Update(AddressDTO addressdto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = _mapper.Map<Address>(addressdto);
            var res = await _unitOfWork.authentication.UpdateAddressAsync(email, address);
            if (res)
            {
                return Ok(new { message = "Address Upaded Successfully" });
            }
            return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request"
            });
        }
    }
}

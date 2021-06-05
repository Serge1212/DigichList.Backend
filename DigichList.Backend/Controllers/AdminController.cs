using DigichList.Backend.Helpers;
using DigichList.Backend.Options;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly JwtService _jwtService;

        public AdminController(IAdminRepository repo,
            IOptions<AuthOptions> authOptions,
            JwtService jwtService)
        {
            _repo = repo;
            _authOptions = authOptions;
            _jwtService = jwtService;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAdmins()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            return await CommonControllerMethods
                .GetByIdAsync<Admin, IAdminRepository>(id, _repo);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            await _repo.AddAsync(admin);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + admin.Id, admin);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            return await CommonControllerMethods
                .DeleteAsync<Admin, IAdminRepository>(id, _repo);
        }

        [HttpPost]
        [Route("api/[controller]/UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(admin);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var admin = await _repo.GetAdminByEmail(request.Email);

            if (admin == null)
                return BadRequest(new { message = "Invalid Credentials" });

            var passwordsMatch = BCrypt.Net.BCrypt.Verify(request.Password, admin.Password);
            if (!passwordsMatch)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(admin.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions 
            {
                HttpOnly = true
            });

            return Ok(new 
            { 
                message = "Success"
            });
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Admin()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int adminId = int.Parse(token.Issuer);

                var admin = await _repo.GetByIdAsync(adminId);

                return Ok(admin);
            }
            catch(Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
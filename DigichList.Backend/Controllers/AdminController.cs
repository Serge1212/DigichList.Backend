using DigichList.Backend.Helpers;
using DigichList.Backend.Options;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepositury _repo;
        private readonly IOptions<AuthOptions> authOptions;
        private readonly JwtService jwtService;

        public AdminController(IAdminRepositury repo, IOptions<AuthOptions> authOptions, JwtService jwtService)
        {
            _repo = repo;
            this.authOptions = authOptions;
            this.jwtService = jwtService;
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
            var user = await _repo.GetByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"admin whith id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            await _repo.AddAsync(admin);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + admin.Id, admin);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _repo.GetByIdAsync(id);
            if (admin != null)
            {
                await _repo.DeleteAsync(admin);
                return Ok();
            }
            return NotFound($"admin whith id: {id} was not found");
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
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var admin = await _repo.GetAdminByEmail(request.Email);
            if (admin == null) return BadRequest(new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = jwtService.Generate(admin.Id);

            return Ok(new { jwt });
        }
    }
}
using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetUsers()
        {
            var users = _repo.GetUsersWithRoles();
            
            return Ok(users.Select(x => new 
            { 
                x.Id,
                x.FirstName,
                x.LastName,
                x.Username,
                rolename = x?.Role?.Name,
                x.IsRegistered
            }));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            Func<int, Task<dynamic>> predicate = _repo.ReturnUserWithRoleByIdRequest;

            return await CommonControllerMethods
                .GetDynamicDatayByIdAsync<User, dynamic>(id, predicate);
        }

        [HttpGet]
        [Route("api/[controller]/GetTechnicians")]
        public IActionResult GetTechnicians()
        {
            var technicians = _repo.GetTechnicians();
            if(technicians != null)
            {
                return Ok(technicians.Select(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.LastName
                }));
            }

            return NotFound("No technicians available");

        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _repo.AddAsync(user);
            return Created(HttpContext.Request.Scheme + 
                "://" + HttpContext.Request.Host +
                HttpContext.Request.Path + "/" + user.Id, user);
        }

        [HttpPost]
        [Route("api/[controller]/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await CommonControllerMethods.UpdateAsync(user, _repo);    
            }
            return BadRequest();
        }
    }
}

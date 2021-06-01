using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"user with id: {id} was not found");
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
        public async Task<IActionResult> UpdatePost([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await UpdateControllerMethod.UpdateAsync(user, _repo);
                
            }
            return BadRequest();
        }
    }
}

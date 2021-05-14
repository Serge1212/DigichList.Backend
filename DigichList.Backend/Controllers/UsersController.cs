using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Core.Repositories.Base;
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
            var user = await _repo.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"user whith id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetUser(User user)
        {
            await _repo.AddAsync(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Id, user);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.GetById(id);
            if (user != null)
            {
                await _repo.DeleteAsync(user);
                return Ok();
            }
            return NotFound($"user whith id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            var exsistingUser = _repo.GetById(id);
            if (exsistingUser != null)
            {
                //user.Id = exsistingUser.Id;
                await _repo.UpdateAsync(user);
                return Ok();
            }
            return Ok(user);
        }
    }
}

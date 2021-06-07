using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetRoless()
        {
            var roles = await _repo.GetAllAsync();
            return Ok(roles.Select(x => new { x.Id, x.Name}));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            return await CommonControllerMethods
                .GetEntityByIdAsync<Role, IRoleRepository>(id, _repo);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateRole(Role role)
        {
            await _repo.AddAsync(role);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + role.Id, role);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            return await CommonControllerMethods
                .DeleteAsync<Role, IRoleRepository>(id, _repo);
        }

        [HttpPost]
        [Route("api/[controller]/UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                return await CommonControllerMethods.UpdateAsync(role, _repo);
            }
            return BadRequest();
            
        }

    }
}

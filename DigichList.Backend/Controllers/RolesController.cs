using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _repo.GetAllAsync();
            return Ok(roles.Select(x => new { x.Id, x.Name}));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            Func<int, Task<dynamic>> predicate = _repo.ReturnRoleByIdRequest;
            return await CommonControllerMethods
                .GetDynamicDatayByIdAsync<Role, dynamic>(id, predicate);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateRole(Role role)
        {
            await _repo.AddAsync(role);
            return Ok("The role has been created");
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

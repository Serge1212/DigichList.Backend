using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Core.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var defect = await _repo.GetByIdAsync(id);
            if (defect != null)
            {
                return Ok(defect);
            }
            return NotFound($"role whith id: {id} was not found");
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
            var role = await _repo.GetByIdAsync(id);
            if (role != null)
            {
                await _repo.DeleteAsync(role);
                return Ok();
            }
            return NotFound($"role with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]/UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                return await UpdateControllerMethod.UpdateAsync(role, _repo);
            }
            return BadRequest();
            
        }

    }
}

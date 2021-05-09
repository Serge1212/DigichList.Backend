using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRepository<Role, int> _repo;

        public RolesController(IRepository<Role, int> repo)
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
            var defect = await _repo.GetById(id);
            if (defect != null)
            {
                return Ok(defect);
            }
            return NotFound($"role whith id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAssignedDefect(Role role)
        {
            await _repo.AddAsync(role);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + role.Id, role);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _repo.GetById(id);
            if (role != null)
            {
                await _repo.DeleteAsync(role);
                return Ok();
            }
            return NotFound($"role whith id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditRole(int id, Role role)
        {
            var exsistingRole = _repo.GetById(id);
            if (exsistingRole != null)
            {
                //deefct.Id = exsistingDefect.Id;
                await _repo.UpdateAsync(role);
                return Ok();
            }
            return Ok(role);
        }
    }
}

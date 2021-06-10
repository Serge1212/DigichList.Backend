using AutoMapper;
using DigichList.Backend.Helpers;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;

        public RolesController(IRoleRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoleViewModel>>(roles));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            return role != null ?
                Ok(_mapper.Map<ExtendedRoleViewModel>(role)) :
                NotFound($"Role with id of {id} was not found");
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

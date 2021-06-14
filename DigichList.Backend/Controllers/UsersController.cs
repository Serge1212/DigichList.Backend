using AutoMapper;
using DigichList.Backend.Helpers;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetUsers()
        {
            var users = _repo.GetUsersWithRoles();
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/GetRegisteredUsers")]
        public IActionResult GetRegisteredUsers()
        {
            var users = _repo.GetUsersWithRoles().Where(x => x.IsRegistered);
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/GetUnregisteredUsers")]
        public IActionResult GetUnregisteredUsers()
        {
            var users = _repo.GetUsersWithRoles().Where(x => !x.IsRegistered);
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUserWithRoleAsync(id);
            return user != null ?
                Ok(_mapper.Map<UserViewModel>(user)) :
                NotFound($"User with id of {id} was not found");
        }

        [HttpGet]
        [Route("api/[controller]/GetTechnicians")]
        public IActionResult GetTechnicians()
        {
            var technicians = _repo.GetTechnicians();
            return technicians != null ?
                Ok(_mapper.Map<IEnumerable<TechnicianViewModel>>(technicians)) :
                NotFound("No technicians available");

        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _repo.AddAsync(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
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

        [HttpDelete]
        [Route("api/[controller]/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
           return await CommonControllerMethods.DeleteAsync<User, IUserRepository>(id, _repo);
        }
    }
}

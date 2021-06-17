using AutoMapper;
using DigichList.Backend.Helpers;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IBotNotificationSender _botNotificationSender;

        public RolesController(IRoleRepository repo,
            IUserRepository userRepo,
            IMapper mapper,
            IBotNotificationSender botNotificationSender)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
            _botNotificationSender = botNotificationSender;
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
            return CreatedAtAction("GetRole", new { id = role.Id }, role);
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

        [HttpPost]
        [Route("api/[controller]/AssignRole")]
        public async Task<IActionResult> AssignRole(int userId, int roleId)
        {
            var user = await _userRepo.GetUserWithRoleAsync(userId);
            if(user == null)
            {
                return NotFound("Cannot assign a role to nonexistent user");
            }

            if(await _repo.AssignRole(user, roleId))
            {
                await _botNotificationSender.NotifyUserGotRole(user.TelegramId, user?.Role?.Name);
                return Ok();
            }

            return NotFound("User or role was not found");

        }

        [HttpPost]
        [Route("api/[controller]/RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId)
        {
            var user = await _userRepo.GetUserWithRolesAndAssignedDefectsByIdAsync(userId);
            
            if(user?.Role?.Name == null)
            {
                return BadRequest("User does not have any role");
            }
            string roleName = user.Role.Name;
            var ok = _repo.RemoveRoleFromUser(user);
            if (ok)
            {
                try
                {
                    await _botNotificationSender.NotifyUserLostRole(user.TelegramId, roleName);
                }
                catch(Exception ex)
                {
                    return Ok("Cound not send a message to user in telegram " + ex.Message);
                }
                return Ok();
            }
            else
            {
                return NotFound($"User with id of {userId} was not found");
            }
        }

    }
}

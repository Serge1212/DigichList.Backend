using AutoMapper;
using DigichList.Backend.ApiModels;
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
    [Route("api/[controller]")]
    public class DefectController : ControllerBase
    {
        private readonly IDefectRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IBotNotificationSender _botNotificationSender;
        private readonly IMapper _mapper;

        public DefectController(IDefectRepository repo,
            IUserRepository userRepo,
            IBotNotificationSender sender,
            IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _botNotificationSender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDefects()
        {
            var defects = _repo.GetDefectsWithUsersAndAssignedDefects();
            return Ok(_mapper.Map<IEnumerable<DefectViewModel>>(defects)); 
        }

        [HttpGet("GetDefect")]
        public async Task<IActionResult> GetDefect(int id)
        {
            var defect = await _repo.GetByIdAsync(id);
            return defect != null ?
                Ok(_mapper.Map<DefectViewModel>(defect)) :
                NotFound($"Defect with id of {id} was not found");

        }

        [HttpPost]
        [Route("UpdateDefect")]
        public async Task<IActionResult> UpdateDefect([FromBody]DefectViewModel defectViewModel)
        {
            var defect = await _repo.GetDefectWithAssignedDefectByIdAsync(defectViewModel.Id);

            await _repo.UpdateAsync(defect);

            var worker = await _userRepo.GetByIdAsync(defectViewModel.AssignedWorkerId);
            if(worker!= null)
            {
                defect.AssignedDefect.AssignedWorker = worker;
                await _repo.UpdateAsync(defect);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteDefect")]
        public async Task<IActionResult> DeleteDefect(int id)
        {
            return await CommonControllerMethods
                .DeleteAsync<Defect, IDefectRepository>(id, _repo);
        }

        [HttpDelete("DeleteDefects")]
        public async Task<IActionResult> DeleteDefects([FromQuery(Name = "idArr")] int[] idArr)
        {
            if(idArr.Length < 1)
            {
                return NotFound("There wasn't any id provided to delete");
            }
            await _repo.DeleteRangeAsync(idArr);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(DescriptionResponseApiModel), 404)]
        [ProducesResponseType(typeof(DescriptionResponseApiModel), 409)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> AssignDefect(int userId, int defectId)
        {
            try
            {
                var assignedDefect = await _repo.GetAssignedDefectAsync(userId, defectId);
                if (assignedDefect != null)
                {
                    await _repo.SaveAssignedDefect(assignedDefect);
                    await _botNotificationSender.NotifyUserWasGivenWithDefect(assignedDefect.AssignedWorker.TelegramId, assignedDefect.Defect);
                    return Ok($"The defect was assigned successfully to {assignedDefect.AssignedWorker.FirstName} {assignedDefect.AssignedWorker?.LastName}");
                }
                else
                {
                    return NotFound("User or defect was not found, or user has no permission to fix defects");
                }

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

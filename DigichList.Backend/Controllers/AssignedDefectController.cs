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
    public class AssignedDefectController : ControllerBase
    {
        private IRepository<AssignedDefect, int> _repo;

        public AssignedDefectController(IRepository<AssignedDefect, int> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAsignedDefects()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAssignedDefect(int id)
        {
            var defect = await _repo.GetById(id);
            if (defect != null)
            {
                return Ok(defect);
            }
            return NotFound($"defect whith id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAssignedDefect(AssignedDefect defect)
        {
            await _repo.AddAsync(defect);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + defect.Id, defect);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAssignedDefect(int id)
        {
            var defect = await _repo.GetById(id);
            if (defect != null)
            {
                await _repo.DeleteAsync(defect);
                return Ok();
            }
            return NotFound($"defect whith id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditAssignedDefect(int id, AssignedDefect defect)
        {
            var exsistingDefect = _repo.GetById(id);
            if (exsistingDefect != null)
            {
                //deefct.Id = exsistingDefect.Id;
                await _repo.UpdateAsync(defect);
                return Ok();
            }
            return Ok(defect);
        }
    }
}
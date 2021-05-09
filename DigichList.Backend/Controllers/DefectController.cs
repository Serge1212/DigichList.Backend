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
    public class DefectController : ControllerBase
    {
        private IRepository<Defect, int> _repo;

        public DefectController(IRepository<Defect, int> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetDefects()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDefect(int id)
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
        public async Task<IActionResult> GetDefect(Defect Defect)
        {
            await _repo.AddAsync(Defect);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + Defect.Id, Defect);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteDefect(int id)
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
        public IActionResult EditDefect(int id, Defect defect)
        {
            var exsistingDefect = _repo.GetById(id);
            if (exsistingDefect != null)
            {
                //deefct.Id = exsistingDefect.Id;
                _repo.UpdateAsync(defect);
                return Ok();
            }
            return Ok(defect);
        }
    }
}

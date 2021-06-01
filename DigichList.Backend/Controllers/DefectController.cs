using DigichList.Backend.Helpers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefectController : ControllerBase
    {
        private IDefectRepository _repo;

        public DefectController(IDefectRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDefects()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("GetDefect")]
        public async Task<IActionResult> GetDefect(int id)
        {
            var defect = await _repo.GetByIdAsync(id);

            return defect != null ?
                Ok(defect) :
                NotFound($"defect with id: {id} was not found");

        }

        [HttpPost]
        [Route("api/[controller]/UpdateDefect")]
        public async Task<IActionResult> UpdateDefect([FromBody] Defect defect)
        {
            if (ModelState.IsValid)
            {
                return await UpdateControllerMethod.UpdateAsync(defect, _repo);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteDefect")]
        public async Task<IActionResult> DeleteDefect(int id)
        {
            var defect = await _repo.GetByIdAsync(id);
            if (defect != null)
            {
                await _repo.DeleteAsync(defect);
                return Ok();
            }
            return NotFound($"defect with id {id} was not found");
        }

        [HttpDelete("DeleteDefects")]
        public async Task<IActionResult> DeleteDefects([FromQuery(Name = "idArr")] int[] idArr)
        {
            await _repo.DeleteRangeAsync(idArr);
            return Ok();
        }

        //[HttpPost]
        //[Route("api/[controller]/UpdateDefect")]
        //public async Task<IActionResult> UpdatePost([FromBody] Defect defect)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await UpdateControllerMethod.UpdateAsync(defect, _repo);
        //    }
        //    return BadRequest();
        //}
    }
}

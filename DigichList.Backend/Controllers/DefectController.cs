using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Core.Repositories.Base;
using Microsoft.AspNetCore.Http;
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
        private IDefectRepository _repo;

        public DefectController(IDefectRepository repo)
        {
            _repo = repo;
        }


        // get all defects
        [HttpGet]
        public async Task<IActionResult> GetDefects()
        {
            return Ok(await _repo.GetAllAsync());
        }


        // get defect by id
        [HttpGet("GetDefect")]
        public async Task<IActionResult> GetDefect(int id)
        {
            var defect = await _repo.GetByIdAsync(id);
            if (defect != null)
            {
                return Ok(defect);
            }
            return NotFound($"defect whith id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]/UpdateDefect")]
        public async Task<IActionResult> UpdateDefect([FromBody] Defect defect)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(defect);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }
        //// create new defect
        //[HttpPost]
        //[Route("api/[controller]")]
        //public async Task<IActionResult> GetDefect(Defect Defect)
        //{
        //    await _repo.AddAsync(Defect);
        //    return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + Defect.Id, Defect);
        //}


        //delete defect by id
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

        [HttpPost]
        [Route("api/[controller]/UpdateDefect")]
        public async Task<IActionResult> UpdatePost([FromBody] Defect defect)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(defect);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }

    }
}

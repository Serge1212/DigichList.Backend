using DigichList.Core.Repositories.Base;
using DigichList.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DigichList.Backend.Helpers
{
    public static class UpdateControllerMethod
    {
        public static async Task<IActionResult> UpdateAsync<R, T>(T entity, R repo) where R : IRepository<T, int>
        {
            try
            {
                await repo.UpdateAsync(entity);

                return new OkResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new NotFoundResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}

using DigichList.Core.Entities.Base;
using DigichList.Core.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DigichList.Backend.Helpers
{
    public static class CommonControllerMethods
    {
        public static async Task<IActionResult> UpdateAsync<R, T>(T entity, R repo) 
            where R : IRepository<T, int>
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

        public static async Task<IActionResult> GetEntityByIdAsync<TEntity, TRepo>(int id, TRepo repo) 
            where TRepo : IRepository<TEntity, int>
        {

            TEntity entity = await repo.GetByIdAsync(id);

            return entity != null ?
                new OkObjectResult(entity) :
                new NotFoundObjectResult($"{typeof(TEntity)} with id {id} was not found");
        }

        public static async Task<IActionResult> DeleteAsync<T, R>(int id, R repo)
            where R : IRepository<T, int>
        {
            var admin = await repo.GetByIdAsync(id);
            if (admin != null)
            {
                await repo.DeleteAsync(admin);
                return new OkResult();
            }
            return new NotFoundObjectResult($"{typeof(T)} with id {id} was not found");
        }
    }
}

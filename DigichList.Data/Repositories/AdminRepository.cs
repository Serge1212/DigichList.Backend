using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class AdminRepository: Repository<Admin, int>, IAdminRepository
    {
        public AdminRepository(DigichListContext context) : base(context) { }

        public async Task DeleteRangeAsync(int[] idArr)
        {
            var defectsToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(defectsToDelete);
            await SaveChangesAsync();
        }
        public IEnumerable<Admin> GetRangeByIds(int[] idArr)
        {
            return _context.Admins.Where(d => idArr.Contains(d.Id));
        }

        public async Task<Admin> GetAdminByEmail(string email)
        {
            return await _context.Admins.GetAdminByEmail(email);
        }
        public async Task<Admin> GetAdminForChangePassword(int id, string password)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null)
            {
                throw new ArgumentException($"Cannot find admin with id: {id}");
            }

            if (password == null)
            {
                throw new ArgumentException("Password is null");
            }

            else
            {
                return admin;
            }
        }
    }
}

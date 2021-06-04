using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Extensions
{
    public static class AdminDbContextExtensions
    {
        public static async Task<Admin> GetAdminByEmail(this DbSet<Admin> admins, string email)
        {
            return await admins.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

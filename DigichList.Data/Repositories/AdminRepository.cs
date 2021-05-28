using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using System.Text;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class AdminRepository: Repository<Admin, int>, IAdminRepositury
    {
        public AdminRepository(DigichListContext context) : base(context) { }

        public async Task<Admin> GetAdminByEmailAndPassword(string email, string password)
        {
            return await _context.Admins.GetAdminByEmailAndPassword(email,password);
        }
    }
}

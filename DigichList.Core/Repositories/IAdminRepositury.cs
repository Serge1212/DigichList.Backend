using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IAdminRepositury: IRepository<Admin, int>
    {
        public Task<Admin> GetAdminByEmailAndPassword(string email, string password);
    }
}

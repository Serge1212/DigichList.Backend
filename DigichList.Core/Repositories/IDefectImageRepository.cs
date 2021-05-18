using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IDefectImageRepository
    {
        public Task<DefectImage> SaveImageAsStringByteArray(string path);
    }
}

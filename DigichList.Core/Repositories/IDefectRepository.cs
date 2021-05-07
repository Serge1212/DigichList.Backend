using DigichList.Core.Entities;
using System.Collections.Generic;

namespace DigichList.Core.Repositories
{
    public interface IDefectRepository
    {
        public IEnumerable<Defect> GetAllAsTracking();
    }
}

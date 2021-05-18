using DigichList.Core.Entities.Base;
using System.Collections.Generic;

namespace DigichList.Core.Entities
{
    public class Role : Entity
    {
        
        public string Name { get; set; }
        public bool CanPublishDefects { get; set; } = false;
        public bool CanFixDefects { get; set; } = false;
        public List<User> Users { get; set; } = new List<User>();

    }
}

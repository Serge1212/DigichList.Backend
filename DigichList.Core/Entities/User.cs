using DigichList.Core.Entities.Base;
using System.Collections.Generic;

namespace DigichList.Core.Entities
{
    public class User : Entity
    {
        public int TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsRegistered { get; set; } = false;
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Defect> Defects { get; set; } = new List<Defect>();
        public List<AssignedDefect> AssignedDefects { get; set; } = new List<AssignedDefect>();

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

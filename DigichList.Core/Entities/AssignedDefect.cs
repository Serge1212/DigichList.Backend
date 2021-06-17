using DigichList.Core.Entities.Base;
using System;

namespace DigichList.Core.Entities
{
    public enum Status
    {
        Opened = 1,
        Fixing,
        Solved
    }
    public class AssignedDefect : Entity
    {
        public DateTime? ClosedAt { get; set; }
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
        public Status Status { get; set; } = Status.Opened;
        public int? UserId { get; set; }
        public User AssignedWorker { get; set; }
    }
}

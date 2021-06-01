using DigichList.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigichList.Core.Entities
{
    public class Defect : Entity
    {
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User Publisher { get; set; }
        public AssignedDefect AssignedDefect { get; set; }
        public List<DefectImage> DefectImages { get; set; } = new List<DefectImage>();

        public override string ToString()
        {
            return $"Номер в готелі: {RoomNumber}. Опис дефекту: {Description}";
        }
    }
}

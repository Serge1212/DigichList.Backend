
using System.ComponentModel.DataAnnotations.Schema;

namespace DigichList.Core.Entities
{
    public class DefectImage
    {
        public int Id { get; set; }
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string Image { get; set; }
    }
}

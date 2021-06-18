namespace DigichList.Backend.ViewModel
{
    public class DefectViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public int RoomNumber { get; set; }
        public string Publisher { get; set; }
        public string UserThatFixesDefect { get; set; }
        public int AssignedWorkerId { get; set; }
        public string DefectStatus { get; set; }
        
    }
}

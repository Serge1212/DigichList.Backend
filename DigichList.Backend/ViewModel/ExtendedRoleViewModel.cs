namespace DigichList.Backend.ViewModel
{
    public class ExtendedRoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool CanPublishDefects { get; set; } = false;
        public bool CanFixDefects { get; set; } = false;
    }
}

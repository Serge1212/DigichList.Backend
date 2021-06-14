namespace DigichList.Backend.ApiModels
{
    public class DescriptionResponseApiModel
    {
        public string Message {get; set;}
        public DescriptionResponseApiModel(string message = null)
        {
            Message = message;
        }
    }
}

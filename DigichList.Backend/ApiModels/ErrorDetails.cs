using System;

namespace DigichList.Backend.ApiModels
{
    public class ErrorDetails
    {
        public string ErrorId { get; set; }

        public string RequestPath { get; set; }

        public string EndpointPath { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }
    }
}

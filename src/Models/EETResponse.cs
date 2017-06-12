using System;

namespace EETLib.Models
{
    public class EETResponse : IEETResponse
    {
        public string Response { get; set; }

        public string Request { get; set; }

        public string BKP { get; set; }

        public string PKP { get; set; }

        public string Message { get; set; }

        public bool HasError { get; set; }

        public string Fik { get; set; }

        public string Warning { get; set; }

        public DateTime EETDate { get; set; }

        public EETResponseModel ResponseModel { get; set; }
    }
}

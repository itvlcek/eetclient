

using System;

namespace EETLib.Models
{

    public interface IEETResponse
    {
        string Response { get; set; }
        string Request { get; set; }
        string BKP { get; set; }
        string PKP { get; set; }
        string Message { get; set; }
        bool HasError { get; set; }
        string Fik { get; set; }
        string Warning { get; set; }
        DateTime EETDate { get; set; }
        EETResponseModel ResponseModel { get; set; }
    }
}

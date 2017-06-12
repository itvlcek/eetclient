using System;

namespace EETLib.Models
{

    public interface IEETHeader 
    {
        string UUID { get; set; }

        DateTime SendDate { get; set; }

        bool FirstSend { get; set; }

        bool? Check { get; set; }
    }
}

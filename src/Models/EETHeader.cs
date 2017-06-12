using System;

namespace EETLib.Models
{
    public class EETHeader : IEETHeader
    {
        public string UUID { get; set; }

        public DateTime SendDate { get; set; }

        public bool FirstSend { get; set; }

        public bool? Check { get; set; }
    }
}

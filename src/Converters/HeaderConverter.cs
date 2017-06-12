using EETLib.Models;
using System;

namespace EETLib.Converters
{
    public class HeaderConverter
    {
        public static IEETHeader Convert(EETService.TrzbaHlavickaType hlavicka)
        {
            return new EETHeader
            {
                Check = hlavicka.overeniSpecified ? (bool?)hlavicka.overeni : null,
                FirstSend = hlavicka.prvni_zaslani,
                SendDate = hlavicka.dat_odesl,
                UUID = hlavicka.uuid_zpravy
            };
        }
        

        public static EETService.TrzbaHlavickaType Convert(IEETHeader hlavicka)
        {
            return new EETService.TrzbaHlavickaType
            {
                overeni = hlavicka.Check.HasValue ? true : false,
                overeniSpecified = hlavicka.Check.HasValue,
                prvni_zaslani = hlavicka.FirstSend,
                dat_odesl = new DateTime(hlavicka.SendDate.Year, hlavicka.SendDate.Month, hlavicka.SendDate.Day, hlavicka.SendDate.Hour, hlavicka.SendDate.Minute, hlavicka.SendDate.Second, DateTimeKind.Local),
                uuid_zpravy = hlavicka.UUID
            };
        }

        public static DateTime Truncate(DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}

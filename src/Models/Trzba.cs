using System;
using System.Xml.Serialization;

namespace EETLib.Models
{
    [Serializable]
    public class Trzba
    {
        private Trzba() { }
        internal Trzba(EETService.TrzbaHlavickaType hlavicka, EETService.TrzbaDataType data, EETService.TrzbaKontrolniKodyType kontrolniKody)
        {
            this.Hlavicka = hlavicka;
            this.Data = data;
            this.KontrolniKody = kontrolniKody;
        }

        [XmlElement]
        public EETService.TrzbaHlavickaType Hlavicka { get; set; }

        [XmlElement]
        public EETService.TrzbaDataType Data { get; set; }

        [XmlElement]
        public EETService.TrzbaKontrolniKodyType KontrolniKody { get; set; }
    }
}

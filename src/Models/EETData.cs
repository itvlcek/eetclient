using System;

namespace EETLib.Models
{
    public class EETData : IEETData
    {
        public string DIC { get; set; }

        /// <summary>
        /// Dic pověřujícího
        /// </summary>
        public string DICEntrusting { get; set; }

        /// <summary>
        /// ID provozovny
        /// </summary>
        public int WorkshopId { get; set; }

        /// <summary>
        /// ID pokladny
        /// </summary>
        public string CashId { get; set; }

        /// <summary>
        /// poradove cislo
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Datum tržby
        /// </summary>
        public System.DateTime ReceiptDate { get; set; }

        /// <summary>
        /// celkem trzba
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// celkova castka plneni osvobozenych od DPH
        /// </summary>
        public decimal? TotalFreeTaxPrice { get; set; }

        /// <summary>
        /// Celkový základ daně
        /// zakl_dan1Field;
        /// </summary>
        public decimal? Tax1Base { get; set; }

        /// <summary>
        /// Celková DPH se základní sazbou
        /// dan1Field;
        /// </summary>
        public decimal? Tax1 { get; set; }

        /// <summary>
        /// celkový základ daně s první sníženou sazbou DPB
        /// zakl_dan2Field;
        /// </summary>
        public decimal? Tax2Base { get; set; }

        /// <summary>
        /// Celková DPH s první sníženou sazbou
        /// dan2Field;
        /// </summary>
        public decimal? Tax2 { get; set; }

        /// <summary>
        /// Celkový základ daně s druhou sníženou sazbou
        /// </summary>
        public decimal? Tax3Base { get; set; }

        /// <summary>
        /// Celková DPH s druhou sníženou sazbou
        /// </summary>
        public decimal? Tax3 { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro cestovní službu
        /// </summary>
        public decimal? TotalForTravelService { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží se základní sazbou
        /// </summary>
        public decimal? TotalSoldUsedGoodsBaseRate { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží s první sníženou sazbou
        /// </summary>
        public decimal? TotalSoldUsedGoodsFirstRate { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží s druhou sníženou sazbou
        /// </summary>
        public decimal? TotalSoldUsedGoodsSecondRate { get; set; }

        /// <summary>
        /// Celková částka plateb určená k následnému čerpání nebo zůčtování
        /// </summary>
        public decimal? TotalDrawOrBill { get; set; }

        /// <summary>
        /// Celková částka plateb, které jsou následným čerpáním nebo zúčtováním platby
        /// </summary>
        public decimal? TotalThenDrawOrBill { get; set; }
        
        /// <summary>
        /// režim tržby
        /// </summary>
        public int Regime { get; set; }

    }
}

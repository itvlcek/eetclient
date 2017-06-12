namespace EETLib.Models
{
    public interface IEETData
    {
        string DIC { get; set; }

        /// <summary>
        /// Dic pověřujícího
        /// dic_poverujicihoField;
        /// </summary>
        string DICEntrusting { get; set; }

        /// <summary>
        /// ID provozovny
        /// id_provozField;
        /// </summary>
        int WorkshopId { get; set; }

        /// <summary>
        /// ID pokladny
        /// id_poklField;
        /// </summary>
        string CashId { get; set; }

        /// <summary>
        /// poradove cislo
        /// porad_cisField;
        /// </summary>
        string OrderNumber { get; set; }

        /// <summary>
        /// Datum tržby
        /// dat_trzbyField;
        /// </summary>
        System.DateTime ReceiptDate { get; set; }

        /// <summary>
        /// celkem trzba
        /// celk_trzbaField;
        /// </summary>
        decimal TotalPrice { get; set; }

        /// <summary>
        /// celkova castka plneni osvobozenych od DPH
        /// zakl_nepodl_dphField;
        /// </summary>
        decimal? TotalFreeTaxPrice { get; set; }

        /// <summary>
        /// Celkový základ daně
        /// zakl_dan1Field;
        /// </summary>
        decimal? Tax1Base { get; set; }

        /// <summary>
        /// Celková DPH se základní sazbou
        /// dan1Field;
        /// </summary>
        decimal? Tax1 { get; set; }

        /// <summary>
        /// celkový základ daně s první sníženou sazbou DPB
        /// zakl_dan2Field;
        /// </summary>
        decimal? Tax2Base { get; set; }

        /// <summary>
        /// Celková DPH s první sníženou sazbou
        /// dan2Field;
        /// </summary>
        decimal? Tax2 { get; set; }

        /// <summary>
        /// Celkový základ daně s druhou sníženou sazbou
        /// zakl_dan3Field;
        /// </summary>
        decimal? Tax3Base { get; set; }

        /// <summary>
        /// Celková DPH s druhou sníženou sazbou
        /// dan3Field;
        /// </summary>
        decimal? Tax3 { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro cestovní službu
        /// cest_sluzField;
        /// </summary>
        decimal? TotalForTravelService { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží se základní sazbou
        /// pouzit_zboz1Field;
        /// </summary>
        decimal? TotalSoldUsedGoodsBaseRate { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží s první sníženou sazbou
        /// pouzit_zboz2Field;
        /// </summary>
        decimal? TotalSoldUsedGoodsFirstRate { get; set; }

        /// <summary>
        /// Celková částka v režimu DPH pro prodej použitého zboží s druhou sníženou sazbou
        /// pouzit_zboz3Field;
        /// </summary>
        decimal? TotalSoldUsedGoodsSecondRate { get; set; }

        /// <summary>
        /// Celková částka plateb určená k následnému čerpání nebo zůčtování
        /// urceno_cerp_zuctField;
        /// </summary>
        decimal? TotalDrawOrBill { get; set; }

        /// <summary>
        /// Celková částka plateb, které jsou následným čerpáním nebo zúčtováním platby
        /// cerp_zuctField;
        /// </summary>
        decimal? TotalThenDrawOrBill { get; set; }

        /// <summary>
        /// režim tržby
        /// rezimField;
        /// </summary>
        int Regime { get; set; }
    }
}

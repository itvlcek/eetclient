using EETLib.Models;
using System;

namespace EETLib.Converters
{
    public class DataConverter
    {
        public static EETService.TrzbaDataType Convert(IEETData data)
        {
            return new EETService.TrzbaDataType
            {
                dic_popl = data.DIC,
                dic_poverujiciho = data.DICEntrusting,
                cena_celkem = data.TotalPrice,
                id_provoz = data.WorkshopId,
                porad_cis = data.OrderNumber,
                dat_trzby = new DateTime(data.ReceiptDate.Year, data.ReceiptDate.Month, data.ReceiptDate.Day, data.ReceiptDate.Hour, data.ReceiptDate.Minute, data.ReceiptDate.Second, DateTimeKind.Local),
                zakl_nepodl_dph = data.TotalFreeTaxPrice ?? 0,
                zakl_nepodl_dphSpecified = data.TotalFreeTaxPrice.HasValue,
                dan1 = data.Tax1 ?? 0,
                dan1Specified = data.Tax1.HasValue,
                zakl_dan1 = data.Tax1Base ?? 0,
                zakl_dan1Specified = data.Tax1Base.HasValue,                
                zakl_dan2 = data.Tax2Base ?? 0,
                zakl_dan2Specified = data.Tax2Base.HasValue,
                dan2 = data.Tax2 ?? 0,
                dan2Specified = data.Tax2.HasValue,
                dan3 = data.Tax3 ?? 0,
                dan3Specified = data.Tax3.HasValue,
                cest_sluz = data.TotalForTravelService ?? 0,
                cest_sluzSpecified = data.TotalForTravelService.HasValue,
                pouzit_zboz1 = data.TotalSoldUsedGoodsBaseRate ?? 0,
                pouzit_zboz1Specified = data.TotalSoldUsedGoodsBaseRate.HasValue,
                pouzit_zboz2 = data.TotalSoldUsedGoodsFirstRate ?? 0,
                pouzit_zboz2Specified = data.TotalSoldUsedGoodsFirstRate.HasValue,
                urceno_cerp_zuct = data.TotalDrawOrBill ?? 0,
                urceno_cerp_zuctSpecified = data.TotalDrawOrBill.HasValue,
                cerp_zuct = data.TotalThenDrawOrBill ?? 0,
                cerp_zuctSpecified = data.TotalThenDrawOrBill.HasValue,
                id_pokl = data.CashId,
                pouzit_zboz3 = data.TotalSoldUsedGoodsSecondRate ?? 0,
                pouzit_zboz3Specified = data.TotalSoldUsedGoodsSecondRate.HasValue,
                rezim = data.Regime,
                zakl_dan3 = data.Tax3Base ?? 0,
                zakl_dan3Specified = data.Tax3Base.HasValue

            };
        }
    }
}

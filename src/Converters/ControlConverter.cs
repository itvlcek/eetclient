using EETLib.Models;

namespace EETLib.Converters
{
    public class ControlConverter
    {
        public static EETService.TrzbaKontrolniKodyType Convert(IEETControl control)
        {
            return new EETService.TrzbaKontrolniKodyType
            {
                bkp = new EETService.BkpElementType { Text = control.BKP.Text, digest = EETService.BkpDigestType.SHA1, encoding = EETService.BkpEncodingType.base16 },
                pkp = new EETService.PkpElementType { Text = control.PKP.Text, encoding = EETService.PkpEncodingType.base64, digest = EETService.PkpDigestType.SHA256, cipher = EETService.PkpCipherType.RSA2048 }
            };
        }
    }
}

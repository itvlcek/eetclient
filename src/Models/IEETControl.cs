namespace EETLib.Models
{

    public interface IEETControl
    {
        PkpElement PKP { get; set; }

        BkpElement BKP { get; set; }
    }

}

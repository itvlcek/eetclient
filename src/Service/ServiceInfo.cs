
namespace EETLib.Service
{
    public class ServiceInfo
    {
        public static ServiceInfoModel GetServiceInfo()
        {
            return new ServiceInfoModel { Action = "http://fs.mfcr.cz/eet/OdeslaniTrzby", ServiceNamespace = "http://fs.mfcr.cz/eet/schema/v3" };            
        }
    }

    public class ServiceInfoModel
    {
        public string ServiceNamespace { get; set; }
        public string Action { get; set; }
    }
}

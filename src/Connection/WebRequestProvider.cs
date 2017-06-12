using System.IO;
using System.Net;
using System.Text;

namespace EETLib.Connection
{
    /// <summary>
    /// WebRequest
    /// </summary>
    public class WebRequestProvider
    {
        /// <summary>
        /// Call eet service url and post xml.
        /// </summary>
        /// <param name="urlAddress">eet service url</param>
        /// <param name="soapAction">soap action</param>
        /// <param name="soapEnvelope">xml document</param>
        /// <param name="proxy"></param>
        /// <returns>response from EET server</returns>
        public string CallWebService(string urlAddress, string soapAction, string soapEnvelope, string proxy = null)
        {
            byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
            WebRequest req = WebRequest.Create(urlAddress);
            if (!string.IsNullOrEmpty(proxy))
            {
                req.Proxy = new WebProxy(proxy, false);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            }
            else
            {
                req.Proxy = null;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            }
            req.ContentType = "text/xml;charset=UTF-8";
            req.ContentLength = content.Length;
            req.Headers.Add("SOAPAction", soapAction);
            req.Method = "POST";
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(content, 0, content.Length);
            reqStream.Close();

            WebResponse resp = req.GetResponse();
            Stream respStream = resp.GetResponseStream();
            StreamReader rdr = new StreamReader(respStream, Encoding.UTF8);
            string responseString = rdr.ReadToEnd();
            return responseString;
        }
    }
}

using EETLib.Models;
using EETLib.Signature;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace EETLib.Xml
{
    /// <summary>
    /// Creating EET xml
    /// </summary>
    public class XmlCreator
    {
        private IEETSignature signatureManager { get; set; }
        public XmlCreator(IEETSignature SignatureManager)
        {
            signatureManager = SignatureManager;
        }

        /// <summary>
        /// Create EET xml
        /// </summary>
        /// <param name="data">body data</param>
        /// <param name="header">header data</param>
        /// <param name="control">control data - pkp and bkp</param>
        /// <param name="certificatePath">path to certificate</param>
        /// <param name="serviceNamespace">service namespace</param>
        /// <returns></returns>
        public string CreateXml(IEETData data, IEETHeader header, EETControl control, string serviceNamespace)
        {
            var trzba = new Trzba(Converters.HeaderConverter.Convert(header), Converters.DataConverter.Convert(data), Converters.ControlConverter.Convert(control));
            XmlDocument doc = new XmlDocument();
            MemoryStream ms = new MemoryStream();
            new XmlSerializer(typeof(Trzba)).Serialize(ms, trzba);
            ms.Position = 0;
            using (StreamReader rd = new StreamReader(ms))
            {
                doc.LoadXml(rd.ReadToEnd());
            }
            XmlNode node = doc.SelectSingleNode("//*[local-name()='Trzba']");
            XmlAttribute xa = doc.CreateAttribute("xmlns");
            xa.Value = serviceNamespace;
            node.Attributes.Append(xa);
            var xmlRequest = node.OuterXml;

            return SignMessage(xmlRequest, signatureManager.GetCertificate());

        }     

        private string SignMessage(string soapBody, X509Certificate2 certificate)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(soapBody);
            
            XmlDocument result = signatureManager.GetSignedXml(certificate, doc.DocumentElement);
            
            return result.OuterXml;
        }
    }
}

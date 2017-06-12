using EETLib.Models;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace EETLib.Signature
{
    /// <summary>
    /// Class for signature EET
    /// </summary>
    public interface IEETSignature
    {
        /// <summary>
        /// Return certificate
        /// </summary>
        /// <returns></returns>
        X509Certificate2 GetCertificate();

        /// <summary>
        /// Create BKP key form PKP
        /// </summary>
        /// <param name="PKP">pkp key</param>
        /// <returns></returns>
        string CreateBKP(string PKP);

        /// <summary>
        /// Create key
        /// </summary>
        /// <param name="data">IEET data</param>
        /// <param name="certificatePath">path to certificate</param>
        /// <returns></returns>
        string CreatePKP(IEETData data);

        /// <summary>
        /// Sign xml
        /// </summary>
        /// <param name="certificate">Certificate</param>
        /// <param name="body">Body xml</param>
        /// <returns></returns>
        XmlDocument GetSignedXml(X509Certificate2 certificate, XmlElement body);       
    }
}

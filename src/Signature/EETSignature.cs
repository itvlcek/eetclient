using EETLib.Models;
using System;
using System.Collections.Generic;
using System.Deployment.Internal.CodeSigning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace EETLib.Signature
{
    /// <summary>
    /// Class for signature EET
    /// </summary>
    public class EETSignature : IEETSignature
    {
        private string certificatePassword { get; set; }
        private string certificatePath { get; set; }

        static EETSignature()
        {
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
        }

        public EETSignature(string CertificatePath, string CertificatePassword)
        {
            certificatePassword = CertificatePassword;
            certificatePath = CertificatePath;
        }

        /// <summary>
        /// Load certificate
        /// </summary>
        /// <param name="certificatePath"></param>
        /// <returns></returns>
        public X509Certificate2 GetCertificate()
        { 
            return new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.Exportable);
        }       

        /// <summary>
        /// Create BKP key form PKP
        /// </summary>
        /// <param name="PKP">pkp key</param>
        /// <returns></returns>
        public string CreateBKP(string PKP)
        {
            StringBuilder sb = new StringBuilder();
            byte[] decodedBytes = SHA1.Create().ComputeHash(Convert.FromBase64String(PKP));
            for (int i = 0; i < decodedBytes.Length;)
            {
                sb.Append('-');
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
                sb.Append(decodedBytes[i++].ToString("X2"));
            }
            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// Create key
        /// </summary>
        /// <param name="data">IEET data</param>
        /// <param name="certificatePath">path to certificate</param>
        /// <returns></returns>
        public string CreatePKP(IEETData data)
        {
            string PKP = null;
            var values = new List<string>();
            const string separator = "|";
            values.Add(data.DIC);
            values.Add(data.WorkshopId.ToString());
            values.Add(data.CashId);
            values.Add(data.OrderNumber);
            values.Add(data.ReceiptDate.GetAsText());
            values.Add(data.TotalPrice.GetMoneyAsTextUSFormat());

            var plainText = string.Join(separator, values);
            // Sign data
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                byte[] dataToSign = Encoding.UTF8.GetBytes(plainText);
                csp.ImportParameters(((RSACryptoServiceProvider)GetCertificate().PrivateKey).ExportParameters(true));
                byte[] signature = csp.SignData(dataToSign, "SHA256");
                // Verify signature
                if (!csp.VerifyData(dataToSign, "SHA256", signature))
                    throw new Exception("Nepodařilo se vytvořit platný podpisový kód poplatníka.");
                PKP = Convert.ToBase64String(signature);
            }
            return PKP;
        }

        /// <summary>
        /// Sign xml
        /// http://komodosoft.net/post/2016/03/24/sign-a-soap-message-using-x-509-certificate.aspx
        /// </summary>
        /// <param name="certificate">Certificate</param>
        /// <param name="body">Body xml</param>
        /// <returns></returns>
        public XmlDocument GetSignedXml(X509Certificate2 certificate, XmlElement body)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;

            XmlElement envelopeXml = doc.CreateElement("s", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            envelopeXml.SetAttribute("xmlns:u", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            XmlElement headerXml = doc.CreateElement("s", "Header", "http://schemas.xmlsoap.org/soap/envelope/");

            envelopeXml.AppendChild(headerXml);

            XmlElement bodyXml = doc.CreateElement("s", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            bodyXml.SetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "_1");
            if (body == null)
            {
                throw new Exception("Body is required.");
            }
            else
            {
                bodyXml.AppendChild(doc.ImportNode(body, true));
            }

            envelopeXml.AppendChild(bodyXml);
            doc.AppendChild(envelopeXml);

            if (certificate == null)
            {
                throw new Exception("A X509 certificate is needed.");
            }

            XmlDocument tempDoc = new XmlDocument();
            tempDoc.PreserveWhitespace = true;
            tempDoc.LoadXml(doc.OuterXml);

            return SignMessage(tempDoc, certificate);
        }

        /// <summary>
        /// Sign xml message
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="certificate"></param>
        /// <param name="signAlgorithm"></param>
        /// <returns></returns>
        private XmlDocument SignMessage(XmlDocument xmlDoc, X509Certificate2 certificate)
        {
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");

            XmlElement soapHeader = xmlDoc.DocumentElement.SelectSingleNode("//s:Header", ns) as XmlElement;
            XmlElement body = xmlDoc.DocumentElement.SelectSingleNode("//s:Body", ns) as XmlElement;

            if (body == null)
                throw new Exception("No body tag found.");

            XmlElement securityNode = xmlDoc.CreateElement("wsse", "Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

            XmlElement binarySecurityToken = xmlDoc.CreateElement("wse", "BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            binarySecurityToken.SetAttribute("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            binarySecurityToken.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
            binarySecurityToken.SetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "BinaryToken1");

            binarySecurityToken.InnerText = Convert.ToBase64String(certificate.GetRawCertData());

            securityNode.AppendChild(binarySecurityToken);

            soapHeader.AppendChild(securityNode);

            SignedXmlWithId signedXml = new SignedXmlWithId(xmlDoc);

            signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

            var key = (RSACryptoServiceProvider)certificate.PrivateKey;
            var enhCsp = new RSACryptoServiceProvider().CspKeyContainerInfo;
            var cspparams = new CspParameters(enhCsp.ProviderType, enhCsp.ProviderName, key.CspKeyContainerInfo.KeyContainerName);
            var rsaKey = new RSACryptoServiceProvider(cspparams);

            signedXml.SigningKey = rsaKey;

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new SecurityTokenReference("BinaryToken1"));

            signedXml.KeyInfo = keyInfo;

            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            Reference reference = new Reference { Uri = "#_1" };
            reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

            reference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(reference);

            signedXml.ComputeSignature();

            XmlElement signedElement = signedXml.GetXml();

            securityNode.AppendChild(signedElement);

            if (soapHeader == null)
            {
                soapHeader = xmlDoc.CreateElement("s:Header", "");
                xmlDoc.DocumentElement.InsertBefore(soapHeader, xmlDoc.DocumentElement.ChildNodes[0]);
            }
            return xmlDoc;
        }
    }
}

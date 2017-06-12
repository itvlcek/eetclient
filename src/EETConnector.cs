using EETLib.Connection;
using EETLib.Models;
using EETLib.Service;
using EETLib.Signature;
using EETLib.Xml;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace EETLib
{
    [ComVisible(true)]
    public class EETConnector
    {
        public IEETResponse SendRequest(IEETHeader header, IEETData data, string certificatePath = null, string certificatePassword = null)
        {
            certificatePath = certificatePath ?? ConfigurationManager.AppSettings["eet.net.certificatePath"];
            certificatePassword = certificatePassword ?? ConfigurationManager.AppSettings["eet.net.certificatePass"];

            var signatureManager = new EETSignature(certificatePath, certificatePassword);

            var xmlCreator = new XmlCreator(signatureManager);
            var request = new WebRequestProvider();
            var serviceInfo = ServiceInfo.GetServiceInfo();

            var pkp = signatureManager.CreatePKP(data);
            var bkp = signatureManager.CreateBKP(pkp);
            var control = new EETControl { PKP = new PkpElement { Text = new string[] { pkp } }, BKP = new BkpElement { Text = new string[] { bkp } } };
            EETResponse eetResponse;
            try
            {
                var xml = xmlCreator.CreateXml(data, header, control, serviceInfo.ServiceNamespace);
                var response = request.CallWebService(ConfigurationManager.AppSettings["eet.net.url"], serviceInfo.Action, xml);

                eetResponse = new EETResponse { Response = response, Request = xml, BKP = bkp, PKP = pkp };
                var confirm = response.XmlDeserializeFromString<EETResponseModel>();
                if (confirm == null)
                {
                    eetResponse.Message = "Nevalidní zpráva ze serveru";
                    eetResponse.HasError = true;
                }
                else
                {
                    eetResponse.ResponseModel = confirm;
                    if (confirm.Body.Odpoved.Chyba == null)
                    {
                        eetResponse.HasError = false;
                        eetResponse.Fik = confirm.Body.Odpoved.Potvrzeni.fik;
                        eetResponse.EETDate = confirm.Body.Odpoved.Hlavicka.dat_prij;
                        eetResponse.Message = string.Format("FIK {0}, BKP {1}, PKP {2}", eetResponse.Fik, eetResponse.BKP, eetResponse.PKP);
                    }
                    else
                    {
                        eetResponse.HasError = true;
                        eetResponse.Message = string.Format("{0} ({1})", confirm.Body.Odpoved.Chyba.Value, confirm.Body.Odpoved.Chyba.kod);
                    }
                    if (confirm.Body.Odpoved.Varovani != null)
                    {
                        eetResponse.Warning = string.Format("{0} ({1})", confirm.Body.Odpoved.Varovani.Value, confirm.Body.Odpoved.Varovani.kod_varov);
                    }
                }
            }
            catch (WebException ex)
            {
                Trace.WriteLine(ex.ToString());
                eetResponse = new EETResponse();
                eetResponse.HasError = true;
                eetResponse.Message = "Nepodařilo se připojit k serveru";
            }
            catch (Exception ex)
            {
                eetResponse = new EETResponse();
                eetResponse.HasError = true;
                eetResponse.Message = "Chyba při zpracování odpovědi. " + ex.Message;
            }

            return eetResponse;
        }       
    }    
}

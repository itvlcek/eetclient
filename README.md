# eetclient
This is a library, that can be used for communication with EET. 

## Usage Examples

```
                      var connector = new EETLib.EETConnector();
                        var uid = Guid.NewGuid().ToString();
                        var result = connector.SendRequest(new EETHeader
                        {
                            FirstSend = true,
                            UUID = uid,
                            SendDate = DateTime.Now,
                            Check = TestMode ? (bool?)true : null
                        }, new EETData
                        {
                            DIC = DIC,
                            TotalPrice = request.castka,
                            CashId = request.cislo_pokladny.ToString(),
                            OrderNumber = request.cislo_uctenky,
                            ReceiptDate = DateTime.SpecifyKind(request.datum_objednavky, DateTimeKind.Local),
                            WorkshopId = request.cislo_provozovny     ,
                            Tax2Base     = request.zakl_dan2 ,
                            Tax2 = request.dan2,
                            Tax1 = request.dan1,
                            Tax1Base = request.zakl_dan1 
                                              
                        }, CertificatePath, CertificatePass);
```



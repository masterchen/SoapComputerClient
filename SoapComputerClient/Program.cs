using System;
using System.IO;
using System.Net;
using System.Xml;

namespace SoapClient
{
    class Program
    {
        /// <summary>
        /// Execute soap get all
        /// </summary>
        public static void Execute()
        {
            var url = @"http://localhost:60261/SoapComputer.asmx?op=GetAll";
            HttpWebRequest request = CreateWebRequest(url);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <GetAll xmlns=""http://tempuri.org/"" />
                  </soap:Body>
                </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
        }

        /// <summary>
        /// Execute soap GetById
        /// </summary>
        public static void Execute(string id)
        {
            var url = @"http://localhost:60261/SoapComputer.asmx?op=GetById";
            HttpWebRequest request = CreateWebRequest(url);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <GetById xmlns=""http://tempuri.org/"" >
                        <id>{id}</id>
                    </GetById>
                  </soap:Body>
                </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("*********************************** Liste des ordinateurs en XML ***********************************");
            Console.WriteLine("\n");
            Execute();
            Console.WriteLine("*********************************************************************************************************");
            Console.WriteLine("\n");
            var continuer = true;
            while (continuer)
            {
                Console.WriteLine("Entrez le id du produit dont vous souhaitez avoir l'information : ");
                string idComputer = Console.ReadLine();
                if (idComputer == "0")
                {
                    continuer = false;
                }
                else
                {
                    Execute(idComputer);
                }
            }

            Console.ReadKey();
        }
    }
}

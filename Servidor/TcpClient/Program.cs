using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TcpClientApp
{
    class Program
    {
        static void Main()
        {
            while (true) {
                Console.WriteLine("Escribe ud");
              string ud = Console.ReadLine();
                Console.WriteLine("Escribe moneda actual");
                string monactual = Console.ReadLine();
                Console.WriteLine("Escribe moneda a cambiar");
                string monedacambiar = Console.ReadLine(); 
                using (var client = new TcpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["port"].ToString())))
                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {

                    string xmlText = ConvertToXml(ud,monactual,monedacambiar);
                    writer.WriteLine(xmlText);
                    writer.Flush();

                    Task.Run(() => HandleConnection(client));

                }

            Console.WriteLine("Mensaje enviado!");
                

            }
        }


        private static void HandleConnection(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                String message = reader.ReadToEnd();
                
                
                Console.WriteLine("El resultado es" + message);
                


            }
        }
       
        public static string ConvertToXml(string ud, string actual, string cambiar)
        {
            XElement xml = new XElement("datos",  new XElement("ud", ud), new XElement("actual", actual), new XElement("cambiar", cambiar));

            return xml.ToString();
            
        }

    }
}

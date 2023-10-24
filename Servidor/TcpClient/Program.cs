using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TcpClientApp
{
    class Program
    {
        static void Main()
        {
            while (true) { 

              string texto = Console.ReadLine();
                using (var client = new TcpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["port"].ToString())))
                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {

                    writer.WriteLine(texto);
                    writer.Flush();
                }

            Console.WriteLine("Mensaje enviado!");

            }
        }
    }
}

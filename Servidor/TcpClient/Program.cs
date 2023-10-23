using System;
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
            using (var client = new TcpClient("127.0.0.1", 12345))
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

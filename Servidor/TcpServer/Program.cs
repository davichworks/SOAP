using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            int port = 12345;
            IPAddress localAddr = IPAddress.Parse(ConfigurationManager.AppSettings["host"]);

            TcpListener server = new TcpListener(localAddr, int.Parse(ConfigurationManager.AppSettings["port"].ToString()));
            server.Start();
            Console.WriteLine($"Servidor iniciado en {localAddr}:{port}");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Conexión aceptada!");

                // Maneja la conexión en una tarea separada
                Task.Run(() => HandleConnection(client));
            }
        }

        private static void HandleConnection(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            
            {
                String message = reader.ReadToEnd();
                Console.WriteLine($"Mensaje recibido: {message}");
                double resultado = RealizarOperacion(message);
                Console.WriteLine("El resultado es"+resultado);
                Task.Run(() => HandleConnection2(client,resultado));
              
                        
            }
        }

        private static void HandleConnection2(TcpClient client, double resultado)
        {
            using(client)
            using(NetworkStream stream = client.GetStream())
            using(StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine(resultado);
                writer.Flush();
            }
        }

        private static double RealizarOperacion(string datos)
        {
            XElement xml = XElement.Parse(datos);

            string ud= xml.Element("ud")?.Value;
            string actual = xml.Element("actual")?.Value;
            string cambiar = xml.Element("cambiar")?.Value;

            if(actual == "EUR" && cambiar == "USD")
            {
                return double.Parse(ud)*1.06;
            }
            if (actual == "USD" && cambiar == "EUR")
            {
                return double.Parse(ud) / 1.06;
            }
            return double.Parse(ud);
        }
    }
}

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

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
                String message = reader.ReadLine();
                Console.WriteLine($"Mensaje recibido: {message}");
                string numeroAConvertir;
                var tipoConversion =
                        XmlConverter.ProcesarXmlConvertRequest(message, out numeroAConvertir);
                if (tipoConversion == ConversorEurUsdConstants.Euro)
                    

                    message = XmlConverter.GenerarPaqueteXmlConvertResponse("", "");
                else if (tipoConversion == ConversorEurUsdConstants.Dolar)
                    //Hacer conversión
                    message = XmlConverter.GenerarPaqueteXmlConvertResponse("", "");
                else
                    message = XmlConverter.GenerarPaqueteXmlConvertResponseError(
                    "ERROR: Divisa no reconocida " + tipoConversion);

            }
        }
    }
}

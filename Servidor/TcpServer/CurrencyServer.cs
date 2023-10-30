using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace TcpServer
{
    class CurrencyServer
    {

        
        public static void Arrancar()
        {
            try
            {
                // Hacemos que el TcpListener escuche en host:port.
                IPAddress localAddr =
                IPAddress.Parse(ConfigurationManager.AppSettings["host"]);
                TcpListener server = new TcpListener(localAddr,
                Int32.Parse(ConfigurationManager.AppSettings["port"]));
                server.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    // Se bloquea aceptando una petición de un
                    TcpClient client = server.AcceptTcpClient();
                    data = null;
                    NetworkStream stream = client.GetStream();
                    Int32 i = stream.Read(bytes, 0,
                    bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i).Trim();
                    Decimal numeroAConvertir;
                    var tipoConversion =
                    XmlConverter.ProcesarXmlConvertRequest(data, out numeroAConvertir);
                    if (tipoConversion == "")
                        //Hacer conversión
                        data = XmlConverter.GenerarPaqueteXmlConvertResponse("", "");
                    else if (tipoConversion == "")
                        //Hacer conversión
                        data = XmlConverter.GenerarPaqueteXmlConvertResponse("", "");
                    else
                        data = XmlConverter.GenerarPaqueteXmlConvertResponseError(
                        "ERROR: Divisa no reconocida " + tipoConversion);
                    Byte[] msg =
                    System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public static void Main(String[] args)
        {
            CurrencyServer.Arrancar();
        }
    }

    



}

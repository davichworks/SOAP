using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TcpClientApp
{
    class Program
    {

        

        static void Connect(String server, String XmlFilePath)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.

                Int32 port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
                TcpClient client = new TcpClient(server, port);
                // Translate the passed message into ASCII and store it as a Byte array.

                string xmlContent = File.ReadAllText(XmlFilePath);
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(xmlContent);
                // Get a client stream for reading and writing
                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", xmlContent);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                data = new Byte[256];
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        public static void Main(string[] args)
        {

            Connect("127.0.0.1", "C:\\Users\\david.perez1\\source\\repos\\SOAP\\Servidor\\TcpClient\\XMLRequest.xml");
        }
    }
}


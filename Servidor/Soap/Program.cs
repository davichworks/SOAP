using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            int port = 12345;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(localAddr, port);

            server.Start();
            Console.WriteLine($"Servidor iniciado en {localAddr}:{port}");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Conexión aceptada!");

            NetworkStream stream = client.GetStream();

            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string message = reader.ReadLine();
            Console.WriteLine($"Mensaje recibido: {message}");

            client.Close();
            server.Stop();
        }
    }
}
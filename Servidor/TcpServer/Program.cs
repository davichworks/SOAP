using System;
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
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(localAddr, port);
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
                string message = reader.ReadLine();
                Console.WriteLine($"Mensaje recibido: {message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.IO;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        //this is where all of the sections of the arguments are being used
        public List<string> listArgs = new List<string>();

        public static void runServer()
        {

            Console.WriteLine("Server (Y), Client (N)");
            if (Console.ReadLine() == "y")//server
            {
                Console.WriteLine("Server");

                Socket connection;
                TcpListener listener;
                Handler requesthandler;
                try
                {
                    listener = new TcpListener(IPAddress.Any, 43);
                    listener.Start();
                    Console.WriteLine("Server is listening");

                    while (true)
                    {
                        connection = listener.AcceptSocket();
                        requesthandler = new Handler();
                        Thread t = new Thread(() => requesthandler.doRequest(connection));
                        t.Start();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else//client
            {
                Console.WriteLine("Client");
                Console.WriteLine("Server name: ");
                string server = Console.ReadLine();
                Console.WriteLine("Port: ");
                int port = int.Parse(Console.ReadLine());

                TcpClient client = new TcpClient();
                client.Connect(server, port);
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());

                sr.BaseStream.ReadTimeout = 2000;
                sw.BaseStream.WriteTimeout = 2000;

                string request = sr.ReadToEnd().Trim();

                String[] sections = request.Split(new char[] { '|' });

                for(int i = 0; i < sections.Length; i++)
                {

                }

            }


        }

        class Handler
        {
            public void doRequest(Socket connection)
            {

                NetworkStream socketStream;
                socketStream = new NetworkStream(connection);

                Console.WriteLine("Connected to Client");

                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);

                socketStream.ReadTimeout = 1000;
                socketStream.WriteTimeout = 1000;

                string start = sr.ReadToEnd().Trim();

            }
        }

        static void Main(string[] args)
        {
            runServer();
        }
    }
}

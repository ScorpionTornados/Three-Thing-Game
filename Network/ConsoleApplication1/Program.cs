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

                //playerNum = 2;
                //have a function that puts the playerNum into a class where its sorts out the client stuff.
                //sw.WriteLine("GAME|START|" + playerNum + "|" + );


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
                int objectNum = 1;

                NetworkStream socketStream;
                socketStream = new NetworkStream(connection);

                Console.WriteLine("Connected to Client");

                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);

                socketStream.ReadTimeout = 1000;
                socketStream.WriteTimeout = 1000;

                string start = sr.ReadToEnd().Trim();

                String[] sections = start.Split(new char[] { '|' });

                List<string> listargs = new List<string>();

                foreach(string a in sections)
                {
                    listargs.Add(a);
                }

                serverCommands.commandsGiven(listargs, sw, sr, objectNum);

            }
        }

        class serverCommands
        {
            public static void commandsGiven(List<string> listArgs, StreamWriter sw, StreamReader sr, int objectNum)
            {
                for(int i = 0; i < listArgs.Count; i++)
                {
                    listArgs.RemoveAt(i);
                    i--;
                }

                switch(listArgs[0])
                {
                    case "GAME":
                        switch(listArgs[1])
                        {
                            case "START"://has the starting positions of the spheres
                                //if statement that checks to see if it is client or server and goes to the right class (should only do this for UPDATE at the moment)
                                START.startGame(listArgs, sw, sr, objectNum);
                                break;

                            case "END"://ends the game
                                break;

                            case "PAUSE"://pauses the game to stop all spheres moving and weapons firing
                                //have to make it so that the person who paused the game can only unpause
                                break;
                        }
                        break;
                    case "UPDATE"://constently updates the positions of the weapons and the spheres
                        break;
                }
            }
        }

        class START
        {
            //
            //this need to be changed by andy to make sure that the spheres spawn where he wants
            //put in a loop and matrix
            public void startMatrix()
            { 

            }
            public static void startGame(List<string>listArgs, StreamWriter sw, StreamReader sr, int objectNum)
            {
                   double m0 = 0;
                   double m1 = 0;
                   double m2 = 0;
                   double m3 = 0;
                   double m4 = 0;
                   double m5 = 0;
                   double m6 = 0;
                   double m7 = 0;
                   double m8 = 0;
                   double m9 = 0;
                   double m10 = 0;
                   double m11 = 0;
                   double m12 = 0;
                   double m13 = 0;
                   double m14 = 0;
                   double m15 = 0;
                   double m16 = 0;


                sw.WriteLine("GAME|START|" + m0 + "|" + m1 + "|" + m2 + "|" + m3 + "|" + m4 + "|" + m5 + "|" + m6 + "|" + m7 + "|" + m8 + "|" + m9 + "|" + m10 + "|" + m11 + "|" + m12 + "|" + m13 + "|" + m14 + "|" + m15 + "|" + m16);
            }
        }

        static void Main(string[] args)
        {
            runServer();
        }
    }
}

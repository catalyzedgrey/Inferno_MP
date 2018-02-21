using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using GameForm.objects;// for State
using System.Threading;
using System.Windows;

namespace GameForm.window
{
    public static class Network
    {
        public static TcpClient tcpclient;
        public static TcpListener tcplistener;
        private static string ipToBeConnected;
        public static bool Connecting;
        public static bool Connected;
        private static Thread th;
        private static Thread timeOutThread;

        public static UdpClient udpclient;
        public static IPEndPoint endpt;

        private static int failcount = 0;

        //public static PlayerName plynameform = new PlayerName();


        static Network()
        {
            Connecting = false;
            Connected = false;
            udpclient = new UdpClient(7000);
            tcplistener = TcpListener.Create(7000);
        }

        public static void TCPconnect(String ip, bool host)
        {
            if (Connecting)
                return;
            Connecting = true;

            if (host)
                th = new Thread(Host);

            else
            {
                ipToBeConnected = ip;
                th = new Thread(Join);
            }

            th.Start();
        }

        private static void timeOut()
        {
            Thread.Sleep(10000);
            if (th.IsAlive)
            {
                th.Abort();
                Console.WriteLine("Join Failed, 10 seconds timeout");
                closeClientNetwork();
            }
        }

        public static void continueConnection()
        {

            if (timeOutThread != null)
                if (timeOutThread.IsAlive)
                    timeOutThread.Abort();

            Connecting = false;
            Connected = true;
            Console.WriteLine("CONNECTED");

            Window.game.menu.joinipform.Close();

            //start game
            sendTCP(new State(Player.playerName));
            Window.game.lobbyToGame();
        }

        public static void Host()
        {
            try
            {
                tcplistener.Start();
                Console.WriteLine("Hosted server. Waiting for player to Join.");
                tcpclient = tcplistener.AcceptTcpClient();
                tcplistener.Stop();
                endpt = (IPEndPoint)tcpclient.Client.RemoteEndPoint;
                continueConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString()); return;
            }
        }
        public static void Join()
        {
            try
            {
                timeOutThread = new Thread(timeOut);
                timeOutThread.Start();

                Console.WriteLine("Trying to join..");
                tcpclient = new TcpClient(ipToBeConnected, 7000);
                endpt = new IPEndPoint(IPAddress.Parse(ipToBeConnected), 7000);
                continueConnection();
            }
            catch (Exception e)
            {
                timeOutThread.Abort();
                Console.WriteLine("Join Failed");
                closeClientNetwork();
                Console.WriteLine(e.StackTrace.ToString()); return;
            }
        }

        public static void closeGameNetwork()
        {
            closeServerNetwork();
            closeClientNetwork();
        }

        public static void closeClientNetwork()
        {
            if (tcpclient != null)
                tcpclient.Close();

            endpt = null;

            Connecting = false;
            Connected = false;

            ipToBeConnected = null;

            if (th != null)
                if (th.IsAlive)
                    th.Abort();

            if (timeOutThread != null)
                if (timeOutThread.IsAlive)
                    timeOutThread.Abort();
        }
        public static void closeServerNetwork()
        {
            Player.t.Abort();
            Player.t2.Abort();

            try
            {
                udpclient.Close();
                tcplistener.Stop();
                tcplistener = null;
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace.ToString()); return; }
        }
        public static State getUDP()
        {
            try
            {
                return (State)new BinaryFormatter().Deserialize(new MemoryStream(udpclient.Receive(ref endpt)));
            }
            catch
            {
                Console.WriteLine("Couldn't recieve UDP state");
            }
            return new State(-50, -50, 0, 0, false);
        }
        public static void sendUDP(State p)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                new BinaryFormatter().Serialize(ms, p);
                byte[] data = ms.GetBuffer();
                udpclient.Send(data, data.Length, endpt);
            }
            catch
            {
                Console.WriteLine("Couldn't send UDP state");
            }
        }
        public static State getTCP()
        {
            try
            {
                return (State)new BinaryFormatter().Deserialize(tcpclient.GetStream());
            }
            catch
            {
                failcount++;
                Console.WriteLine("Couldn't recieve TCP state");

                if (failcount > 100)
                {
                    MessageBox.Show("Other player Disconnected!");
                    closeServerNetwork();
                }
            }
            return new State(-50, -50, 0, 0, false);
        }

        public static void sendTCP(State p)
        {
            try
            {
                NetworkStream ns = tcpclient.GetStream();
                new BinaryFormatter().Serialize(ns, p);
                ns.Flush();
            }
            catch
            {
                Console.WriteLine("Couldn't send TCP state");
            }
        }
    }
}
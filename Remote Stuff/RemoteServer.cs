﻿/*
	Copyright 2011 MCForge/ForgeCraft team
	
	Dual-licensed under the	Educational Community License, Version 2.0 and
	the GNU General Public License, Version 3 (the "Licenses"); you may
	not use this file except in compliance with the Licenses. You may
	obtain a copy of the Licenses at
	
	http://www.opensource.org/licenses/ecl2.php
	http://www.gnu.org/licenses/gpl-3.0.html
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the Licenses are distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the Licenses for the specific language governing
	permissions and limitations under the Licenses.
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace MCForge.Remote
{
    public class RemoteServer
    {
        //public static Socket listen;
        public static TcpListener listener;
        public static int port = 5050;
        public static int Protocol = 2;
        public static string Username = "head";
        public static string Password = "lols";
        public static bool enableRemote = false;
        public static int tries = 0;


        public void Start()
        {
            RemoteProperties.Load();

            if (!enableRemote) return;
            try
            {

                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
                listener = new TcpListener(endpoint);
                listener.Start();
                listener.BeginAcceptTcpClient(new AsyncCallback(Accept), this);
                Server.s.Log(string.Format("Creating listening socket on port {0} for remote console...", port));
            }
            catch (SocketException e) { Server.s.Log(e.Message + e.StackTrace); }
            catch (Exception e) { Server.s.Log(e.Message + e.StackTrace); }
        }

        void Accept(IAsyncResult result)
        {
            if (Server.shuttingDown) return;
            Remote p = null;
            var begin = false;
            try
            {
                

                if (tries < 7)
                {
                    if (tries < 5)
                    {

                        TcpClient sock = listener.EndAcceptTcpClient(result);
                        
                        
                        var read = new BinaryReader(sock.GetStream());
                        var write = new BinaryWriter(sock.GetStream());
                            byte l = read.ReadByte();
                            if (l == 1)
                            {
                                p = new RemoteMobile(sock, read, write);
                            }
                            else
                            {
                                p = new RemoteDesktop(sock, read, write);
                            }

                            new Thread(p.Start).Start();

                            listener.BeginAcceptSocket(Accept, null);
                            begin = true;
                        
                    }
                    else
                    {
                        new Thread(() =>
                                       {
                                           Thread.Sleep(0x927c0);  //10 mins
                                           Command.all.Find("remote").Use(null, "resettry");
                                       }).Start();
                    }
                }


            }
            catch (SocketException)
            {
                if (p != null)
                    p.Disconnect();
                if (begin) return;
                listener.BeginAcceptSocket(Accept, null);
            }
            catch (Exception e)
            {
                Server.s.Log(e.Message);
                Server.s.Log(e.StackTrace);
                if (p != null)
                    p.Disconnect();
                if (begin) return;
                listener.BeginAcceptSocket(Accept, null);
            }
        }


        public static void Close()
        {
            if (listener != null)
                listener.Stop();
        }
    }
}
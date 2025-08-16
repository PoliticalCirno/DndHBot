using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace DndBotHook
{
    public static class Server
    {
        private static List<IWebSocketConnection> sockets = new List<IWebSocketConnection>();


        public static void StartWebSocketServer()
        {
            var server = new WebSocketServer("ws://0.0.0.0:8181");

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Connected");
                    sockets.Add(socket);
                };

                socket.OnClose = () => sockets.Remove(socket);
                socket.OnMessage = msg => Console.WriteLine("Unity says:" + msg);
            });

            Console.WriteLine("Websocket server started.");
        }

        public static void SendToAllClients(string message, string name)
        {
            foreach (var socket in sockets)
            socket.Send($"{message}|{name}");
            
        }
    }
}

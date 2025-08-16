using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using Discord;

namespace DndBotHook
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CmdHandler.InitializeAsync();
            Server.StartWebSocketServer();
            Console.WriteLine("Test");
            await Task.Delay(-1);
        }
    }
}

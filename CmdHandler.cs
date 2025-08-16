using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DndBotHook;

namespace DndBotHook
{
    public static class CmdHandler
    {
        private static DiscordSocketClient _client;
        private static ISocketMessageChannel _cmdChannel;
        public static async Task InitializeAsync()
        {
            _client = new DiscordSocketClient();
            _client.Ready += OnReady;
            _client.MessageReceived += HandleCommand;
            
            await _client.LoginAsync(TokenType.Bot, "MTM2NDI1MTU2OTQzMjAzOTUzNg.GeUL2y.qrGAj3C-4Sm9_LVf2vKOmY6Bym5WMtRVmzHMRg");
            await _client.StartAsync();
        }

        private static Task OnReady()
        {
            string targetChannelName = "dnd-cmd";

            foreach (var guild in _client.Guilds)
            {
                var channel = guild.TextChannels.FirstOrDefault(c => c.Name == targetChannelName);
                if (channel != null)
                {
                    _cmdChannel = channel;
                    break;
                }
            }

            if(_cmdChannel == null)
            {
                Console.WriteLine($"SYSCALL:TYPE:ERROR:0x000001:Couldn't Find channel Named {targetChannelName}");
            }
            else
            {
                Console.WriteLine($"SYSCALL:TYPE:MSG:0x000001:Successfully connected to {_cmdChannel.Name}");
            }
            return Task.CompletedTask; 
        }
        

        private static Task HandleCommand(SocketMessage msg)
        {
            if (msg.Author.IsBot) return Task.CompletedTask;
            
            //if (_cmdChannel == null || msg.Channel.Id != _cmdChannel.Id) return Task.CompletedTask;
            
            
            var parts = msg.Content.Trim().Split(' ');
            if (parts.Length == 0 || !parts[0].StartsWith("!")) return Task.CompletedTask;
            string command = parts[0].ToLower();
            string args = string.Join(" ", parts.Skip(1));
            
            switch(command)
            {
                case "!debug-cat-status":
                    ConCatStatusDebug();
                    break;
                case "!atk":
                    AtkCall(msg.Author.Username, args, msg.Author.Id.ToString());
                break;
                default:
                    _cmdChannel?.SendMessageAsync("INVALID");
                break;
            }

            return Task.CompletedTask;
        }

        private static void ConCatStatusDebug()
        {
            _cmdChannel?.SendMessageAsync("BOT: ONLINE \nUNITY: CONNECTION ESTABLISHED \nDiscord.WebSocket: CONNECTED \nNativeWebSocket: CONNECTED \nFleck: INITIALIZED \nVERSION: 0.521-Miniprog\n\n Please use the DM when using commands.");
        }
        
        private static void AtkCall(string uid, string args, string aid)
        {
            Console.WriteLine($"SYSCALL:TYPE:MSG:0x000002:{uid}@{aid} || {args}");
            if(args == "t1")
            {
                _cmdChannel?.SendMessageAsync($"@{uid} did a Tier 1 attack!");
                Server.SendToAllClients("t1", uid);
            }
            if (args == "t2")
            {
                _cmdChannel?.SendMessageAsync($"@{uid} did a Tier 2 attack!");
                Server.SendToAllClients("t2", uid);
            }
            if (args == "t3")
            {
                _cmdChannel?.SendMessageAsync($"@{uid} did a Tier 3 attack!");
                Server.SendToAllClients("t3", uid);
            }
        }
    }
}

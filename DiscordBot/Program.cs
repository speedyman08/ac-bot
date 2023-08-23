using DiscordBot.commands;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;

namespace DiscordBot
{
    public class DiscordBot
    {
        private static async Task Main(string[] args)
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("token.txt"),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Information,
            });
            await discord.ConnectAsync();
            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<AcCommands>(1115372571605610757);
            await Task.Delay(-1);
        }
        
    }
}
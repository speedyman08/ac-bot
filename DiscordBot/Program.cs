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
                Token = GetToken(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Information,
            });
            await discord.ConnectAsync();
            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<AcCommands>(1143929777766543441);
            await Task.Delay(-1);
        }

        private static string GetToken()
        {
            var response = "";
            try
            {
                response = File.ReadAllText("token.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("token.txt not found. Exiting");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            return response;
        }
    }
}
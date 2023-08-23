using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordBot.commands;
public class AcCommands : ApplicationCommandModule
{
    [SlashCommand("ac-on", "Turns AC on")]
    public async Task AcOn(InteractionContext e, 
        [Option("mode", "AC mode (heat,cool,dry)")] string mode,
        [Option("temperature", "AC temperature (16-31)")] string temperature, 
        [Option("time", "AC time (multiples of 30)")] string time 
        ) 
    {
        await e.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        var client = new HttpClient();
        var response = await client.GetStringAsync($"http://10.50.0.111:5000/homekit/ac-{mode}-{temperature}c-{time}min");
        await e.EditResponseAsync(new DiscordWebhookBuilder().WithContent(response));
    }
    [SlashCommand("ac-off", "Turns AC off")]
    public async Task AcOff(InteractionContext e)
    {
        await e.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        var client = new HttpClient();
        var response = await client.GetStringAsync("http://10.50.0.111:5000/homekit/ac-off");
        await e.EditResponseAsync(new DiscordWebhookBuilder().WithContent(response));
    }
}
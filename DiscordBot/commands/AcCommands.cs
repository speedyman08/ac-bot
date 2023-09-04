using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using System.Text.Json;

namespace DiscordBot.commands;
public class AcCommands : ApplicationCommandModule
{
    [SlashCommand("ac-on", "turns ac on")]
    public async Task AcOn(InteractionContext e, 
            [Choice("cool","cool")] 
            [Choice("heat","heat")]
            [Choice("dry","dry")]
            [Option("mode", "ac mode")] string mode,
        
            [Choice("16c", "16")]
            [Choice("17c", "17")]
            [Choice("18c", "18")]
            [Choice("19c", "19")]
            [Choice("20c", "20")]
            [Choice("21c", "21")]
            [Choice("22c", "22")]
            [Choice("23c", "23")]
            [Choice("24c", "24")]
            [Choice("25c", "25")]
            [Choice("26c", "26")]
            [Choice("27c", "27")]
            [Choice("28c", "28")]
            [Choice("29c", "29")]
            [Choice("30c", "30")]
            [Choice("31c", "31")]
            [Option("temperature", "ac temperature")] string temp,
            
        
            [Choice("30 minutes", "30")]
            [Choice("60 minutes", "60")]
            [Choice("90 minutes", "90")]
            [Choice("120 minutes", "120")]
            [Choice("150 minutes", "150")]
            [Choice("180 minutes", "180")]
            [Choice("210 minutes", "210")]
            [Choice("240 minutes", "240")]
            [Choice("270 minutes", "270")]
            [Choice("300 minutes", "300")]
            [Choice("330 minutes", "330")]
            [Choice("360 minutes", "360")]
            [Choice("390 minutes", "390")]
            [Choice("420 minutes", "420")]
            [Choice("450 minutes", "450")]
            [Choice("480 minutes", "480")]
            [Choice("510 minutes", "510")]
            [Choice("540 minutes", "540")]
            [Choice("570 minutes", "570")]
            [Choice("600 minutes", "600")]
            [Choice("630 minutes", "630")]
            [Choice("660 minutes", "660")]
            [Choice("690 minutes", "690")]
            [Choice("720 minutes", "720")]
            [Option("time", "ac time")] string time
            
            
        )
    {
        await e.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        try
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://10.50.0.111:5000/homekit/ac-{mode}-{temp}c-{time}min");
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(responseBody).RootElement;

                if (doc.TryGetProperty("result", out var resultProperty) && resultProperty.GetBoolean())
                {
                    var embed = new DiscordEmbedBuilder()
                        .WithTitle($"\u2705  AC successfully turned on for {time} minutes")
                        .WithColor(DiscordColor.Green);

                    await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
                }
                else
                {
                    var embed = new DiscordEmbedBuilder()
                        .WithTitle($"\u274C Failed to turn AC on")
                        .WithColor(DiscordColor.Red);
                    await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
                }
            }
            else
            {
                var embed = new DiscordEmbedBuilder()
                    .WithTitle($"\u274C HTTP error: {response.StatusCode}")
                    .WithColor(DiscordColor.Red);
                await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
            }
        }
        catch (HttpRequestException exception)
        {
            var embed = new DiscordEmbedBuilder()
                .WithTitle("\u274C No response")
                .WithColor(DiscordColor.Red);
            await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }
    }
    [SlashCommand("ac-off", "turns ac off")]
    public async Task AcOff(InteractionContext e)
    {
        await e.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        try
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://10.50.0.11:5000/homekit/ac-off");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseBody).RootElement;

                if (json.TryGetProperty("result", out var resultProperty) && resultProperty.GetBoolean())
                {
                    var embed = new DiscordEmbedBuilder()
                        .WithTitle($"\u2705 AC turned off")
                        .WithColor(DiscordColor.Green);

                    await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
                }
                else
                {
                    var embed = new DiscordEmbedBuilder()
                        .WithTitle($"\u274C Failed to turn AC off")
                        .WithColor(DiscordColor.Red);
                    await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
                }

            }
            else
            {
                var embed = new DiscordEmbedBuilder()
                    .WithTitle($"\u274C HTTP error: {response.StatusCode}")
                    .WithColor(DiscordColor.Red);
                await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
            }
        }
        catch (HttpRequestException exception)
        {
            var embed = new DiscordEmbedBuilder()
                .WithTitle("\u274C No response")
                .WithColor(DiscordColor.Red);
            await e.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }
        
    }
    
}
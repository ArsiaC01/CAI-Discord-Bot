using CAIV2.Services;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CAIV2
{
    class Program
    {
        static async Task Main()//Async task is capable of starting the Discord bot
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();
                    x.AddConfiguration(configuration);
                })
                .ConfigureLogging(x =>
                {
                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureDiscordHost((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Debug,
                        AlwaysDownloadUsers = false,
                        MessageCacheSize = 200,
                    };
                    config.Token = context.Configuration["Token"]; //Gives token to Discord bot
                })
                .UseCommandService((context, config) => //Various command services we want to configure
                {
                    config.CaseSensitiveCommands = false;//Sets whether commands are case sensetive
                    config.LogLevel = LogSeverity.Debug;
                    config.DefaultRunMode = RunMode.Sync;   //Whether you want a command to be threaded (multiple people can use commands at same time) or on Gateway
                })
                .ConfigureServices((context, services) =>  //Allows you to configure services used throughout your application and give them a certain implementation.
                {
                    services
                        .AddHostedService<CommandHandler>();    //Adds the commandhandler as a hosted service
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}

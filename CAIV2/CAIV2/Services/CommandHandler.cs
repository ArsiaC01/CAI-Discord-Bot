using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CAIV2.Services //In the services folder, thus has a .Services added to it
{
    public class CommandHandler : InitializedService  //The command handler class that is responsible for handling incoming commands. Inherits from InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _configuration;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration) //CommandHandler constructor
        {
            _provider = provider;
            _client = client;
            _service = service;
            _configuration = configuration;
        }
        public override async Task InitializeAsync(CancellationToken cancellationToken)    //InitializedService class has the task InitializeAsyc which we need to override in the CommandHandler.
            //Within this task, we can bind events to methods, so that when something happens, a method is ran
        {
            _client.MessageReceived += WhenMessageRecieved;
            _service.CommandExecuted += OnCommandExecuted;  //Means that whenever a command is executed, we will run our own method.
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (result.IsSuccess)
            {
                return;
            }

            await commandContext.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task WhenMessageRecieved(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message))     //Checks if the message is a user sent message
                return; //If this socket message is not castable to a socket user message, just return.
            if (message.Source != MessageSource.User)       //Checks if message source is a user
                return;
            
            //Checks if message has correct prefix
            var argPos = 0;
            if (!message.HasStringPrefix(_configuration["Prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos))       //Checking if the message, does not have a string prefix and does not have a bot mention prefix.
                return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }
    }
}

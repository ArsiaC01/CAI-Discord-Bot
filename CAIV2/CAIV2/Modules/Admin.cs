using CAIV2.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//A module is a class containing a related group of commands.

namespace CAIV2.Modules2
{
    public class General : ModuleBase<SocketCommandContext>  //Public class General inherits from the ModuleBase, meaning we can use commands such as ReplyAsync(). Press F12 to see definition
    {
        [Command("rolelist28731828312")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RoleListAsync()
        {
            foreach (var role in Context.Guild.Roles)
            {
                await Context.Channel.SendMessageAsync(role.Name);
            }
        }

        [Command("testpurge")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireBotPermission(ChannelPermission.ManageMessages)]
        [RequireOwner]
        public async Task TestPurgeAsync(int max)
        {
            var messages = Context.Channel.GetMessagesAsync(max).Flatten();
            foreach (var h in await messages.ToArrayAsync())
            {
                await this.Context.Channel.DeleteMessageAsync(h);
            }
        }

        [Command("beJed")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task SpecificDeleteAsync()
        {
            await this.Context.Channel.DeleteMessageAsync(908857026065629204);
        }

        [Command("mutetest")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireBotPermission(ChannelPermission.ManageRoles)]
        [RequireOwner]
        public async Task MuteTestAsync(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
            await (user as IGuildUser).AddRoleAsync(role);
        }

        [Command("unmute")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireBotPermission(ChannelPermission.ManageRoles)]
        [RequireOwner]

        public async Task UnmuteAsync(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
            await (user as IGuildUser).RemoveRoleAsync(role);
        }
    }
}
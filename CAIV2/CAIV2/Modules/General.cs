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

namespace CAIV2.Modules
{
    public class General : ModuleBase<SocketCommandContext>  //Public class General inherits from the ModuleBase, meaning we can use commands such as ReplyAsync(). Press F12 to see definition
    {
        //Square brackets mean a new attribute
        [Command("ping")]
        [Alias("AreYouAlive", "p")]   //Allias for existing command
        /*[RequireUserPermission(Discord.GuildPermission.Administrator)]  //Checks what user permissions are needed to run the command.
        [RequireBotPermission(Discord.GuildPermission.Administrator)]   //Checks if bot has the administrator permission.
        [RequireOwner]  //Checks if the person executing the bot is the bot owner
        */
        public async Task PingAsync()
        {
            await ReplyAsync("Pong! :ping_pong:");
            //Context.  //Context refers to the context of the command, such as which channel is this command is being run.
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync("Pong"); //Alternate way to send message instead of using ReplyAsync
            await Context.User.SendMessageAsync("Hey! This is an annoying test message");   //Sends dm to user
        }

        [Command("info")]
        public async Task InfoAsync(SocketGuildUser socketGuildUser = null)     //Takes in a user mention parameter, default is user not equal to anything.
        {
            if (socketGuildUser == null)    //If there isn't a user specified in the command
                socketGuildUser = Context.User as SocketGuildUser;      //Set the user calling the command as socketGuildUser

            var theEmbed = new CAIEmbedBuilder()  //Get's "theme" from cai embed builder, and automatically runs all statements in there 1st.
                .WithTitle($"{socketGuildUser.Username}#{socketGuildUser.Discriminator}")
                .AddField($"UserID: ", socketGuildUser.Id, true)    //True sets if it's inline (multiple columns in a single row)
                .AddField("Name", $"{socketGuildUser.Username}#{socketGuildUser.Discriminator}", true)
                .AddField("Created at", socketGuildUser.CreatedAt, true)
                .WithThumbnailUrl(socketGuildUser.GetAvatarUrl() ?? socketGuildUser.GetDefaultAvatarUrl())   //Get's profile picture. ?? means that if the value to the left is null, do the thing to the right.
                .WithCurrentTimestamp()     //Displays current time at bottom of embed.
                .Build();   //Grabs all the info, builds it, and returns the embed.

            await Context.Channel.SendMessageAsync(embed: theEmbed);
        }

        [Command("say something wise")]
        public async Task SaySomethingWisdomAsync()
        {
            var theEmbed = new CAIEmbedBuilder()  //Get's "theme" from cai embed builder, and automatically runs all statements in there 1st.
                .WithTitle($"A few Words of Wisdom from DuckWizard")
                .WithDescription("Thy holy DuckWizard hath dispensed words of ingenuous wisdom to the masses, spreading his shrewd, astute, and sagacious insight, " +
                "truly serving the needs of his devoters, pupils, and worshipers.")
                .WithCurrentTimestamp()     //Displays current time at bottom of embed.
                .Build();   //Grabs all the info, builds it, and returns the embed.

            List<string> memeFilePaths = new List<string>(5);
            memeFilePaths.Add("1DoUpon.jpg");
            memeFilePaths.Add("2CopPaper.jpg");
            memeFilePaths.Add("3AppleThrow.jpg");
            memeFilePaths.Add("4HideTryEvidence.jpg");
            memeFilePaths.Add("5SameMistake.jpg");
            memeFilePaths.Add("6StupidCelebrity.jpg");
            memeFilePaths.Add("7Karate.jpg");
            memeFilePaths.Add("8PoliceSpeed.jpg");
            memeFilePaths.Add("9Vacuum.jpg");

            string memePath = ""; //<<<<<<<Insert PATH to images here:
            memePath += memeFilePaths[RandomNumber(0, 8)];

            await Context.Channel.SendFileAsync(memePath, embed: theEmbed);
        }

        [Command("wisdom")]
        public async Task WisdomAsync()
        {
            List<string> quoteBuildUp = new List<string>(5);
            quoteBuildUp.Add("Do unto others before they can do it to you.");
            quoteBuildUp.Add("If a cop pulls you over and says \"papers\" to you...");
            quoteBuildUp.Add("An apple a day keeps...");
            quoteBuildUp.Add("If you don't succeed at first...");
            quoteBuildUp.Add("Never make the same mistake twice...");
            quoteBuildUp.Add("Don't be stupid...");
            quoteBuildUp.Add("Don't mess with me.\nI know Karate, Judo, Juitsu, Kung Fu...");
            quoteBuildUp.Add("Yes officer I saw the speed limit...");
            quoteBuildUp.Add("Don't vacuum while listening to loud music with your headphones...");

            List<string> quotePunchLine = new List<string>(5);
            quotePunchLine.Add("");
            quotePunchLine.Add("say \"scissors, I win\" and drive off.");
            quotePunchLine.Add("~~anyone away if you throw it hard enough.~~");
            quotePunchLine.Add("~~hide all evidence you tried.~~");
            quotePunchLine.Add("make it three or four times. You know... just to be sure!");
            quotePunchLine.Add("~~it could make you famous~~");
            quotePunchLine.Add("and 20 other dangerous words.");
            quotePunchLine.Add("~~I just didn't see your car.~~");
            quotePunchLine.Add("you might finish a room before realizing it wasn't even on.");


            int randomNumber = RandomNumber(0, 8);

            var theEmbed = new CAIEmbedBuilder()  //Get's "theme" from cai embed builder, and automatically runs all statements in there 1st.
                .WithTitle($"Select Words of Wisdom from DuckWizard")
                .WithDescription($"> {quoteBuildUp[randomNumber]}\n> ||{quotePunchLine[randomNumber]}||")
                .WithCurrentTimestamp()     //Displays current time at bottom of embed.
                .Build();   //Grabs all the info, builds it, and returns the embed.

            await Context.Channel.SendMessageAsync(embed: theEmbed);
        }

        [Command("test")]
        public async Task TestAsync()
        {
            await ReplyAsync(Context.Guild.Emotes.ToString());
        }

        [Command("emote")]
        public async Task EmojiAsync()
        {
            var emote = Emote.Parse("<:duckdab:875092307261677638>");
            await Context.Channel.SendMessageAsync(emote.ToString());
        }

        [Command("reacttest")]
        public async Task ReactTest()
        {
            var emote = new[] {
                Emote.Parse("<:BackArrow:906751681012850739>"),
                Emote.Parse("<:Checkmark:906751682107551764>"),
                Emote.Parse("<:Click:906751682812199022>"),
                Emote.Parse("<:Loading:906751682535387136>"),
                Emote.Parse("<:PlayButton:906751682346631218>"),
                Emote.Parse("<:UpArrow:906751683013533746>"),
                Emote.Parse("<:DownArrow:906752927463530536>"),
                Emote.Parse("<:RightArrow:906752927585173544>"),
                Emote.Parse("<:LeftArrow:906751682040451103>"),
            };

            var theMessage = await Context.Channel.SendMessageAsync(emote[0].ToString()+emote[1].ToString()+emote[2].ToString()+emote[3].ToString()+emote[4].ToString()+emote[5].ToString()+emote[6].ToString()+emote[7].ToString()+emote[8].ToString());

            await theMessage.AddReactionsAsync(emote);
        }


        Random _random = new Random();      //Instantiates Random num gen
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}

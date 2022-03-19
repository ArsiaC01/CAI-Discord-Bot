using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CAIV2.Common
{
    // A custom embed builder with a theme.
    internal class CAIEmbedBuilder : EmbedBuilder     //Inherits methods from existing Discord.net EmbedBuilder
    {
        public CAIEmbedBuilder()
        {
            WithColor(new Color(82, 94, 130));     //Sets color to CAI custom color. Takes in rgb value.
        }
    }
}
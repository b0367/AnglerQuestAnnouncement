using Terraria;
using Terraria.ModLoader;

namespace AnglerQuestAnnouncement
{
    public class ConfigReloadCommand : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "aqa"; }
        }

        public override string Usage
        {
            get { return "/aqa reload"; }
        }

        public override string Description
        {
            get { return "Current options: reload, which Reloads Angler Quest Announcements config file (used /aqa reload)"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Config.Load();
            Main.NewText("Config reloaded!", Config.mColorReal);
        }
    }
}
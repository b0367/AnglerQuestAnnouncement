using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AnglerQuestAnnouncement
{
    class AQAPlayer : ModPlayer
    {
        bool announcedFirstTime = false;
        public override void PostUpdateEquips()
        {  
            if (Main.netMode == NetmodeID.Server && !announcedFirstTime)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Config.showDayCounter ? "Welcome to the morning of day " + AnglerAnnouncementWorld.currentDay + "!" : "Good morning!"), Config.mColorReal);
            }
            announcedFirstTime = true;
            base.PostUpdateEquips();
        }
    }
}

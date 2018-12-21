using Terraria.ModLoader;

namespace AnglerQuestAnnouncement
{
    public class AnglerQuestAnnouncement : Mod
    {
        public override void Load()
        {
            base.Load();
            On.Terraria.Main.AnglerQuestSwap += (orig) =>
            {
                orig();
                AnglerAnnouncementWorld.Announce();
            };
        }
        public static string GithubUserName { get { return "b0367"; } }
        public static string GithubProjectName { get { return "AnglerQuestAnnouncement"; } }


    }
}

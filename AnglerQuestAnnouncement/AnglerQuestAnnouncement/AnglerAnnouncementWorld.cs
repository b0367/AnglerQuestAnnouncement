using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Terraria.ID;
using Terraria.Localization;

namespace AnglerQuestAnnouncement
{
    class AnglerAnnouncementWorld : ModWorld
    {
        public static bool updatedDayCounter = false;
        public static int? currentDay = null;
        public static List<string> completedYesterday = new List<string>();
        public static Dictionary<int, string> fishLocations = new Dictionary<int, string>();
        public static Color c = Config.mColorReal;


        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            currentDay = 0;
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }

        public override TagCompound Save()
        {
            if (currentDay == null)
            {
                currentDay = 0;
            }
            var dayCounter = new List<string>
            {
                currentDay.ToString()
            };

            return new TagCompound {
                {"dayCounter", dayCounter}
            };
        }

        public override void PreUpdate()
        {
            if (Main.dayTime && !updatedDayCounter)
            {
                updatedDayCounter = true;
                currentDay++;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Config.showDayCounter ? "Welcome to the morning of day " + currentDay + "!" : "Good morning!"), Config.mColorReal);
                }
                else if(Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Config.showDayCounter ? "Welcome to the morning of day " + currentDay + "!" : "Good morning!", Config.mColorReal);
                }

            }
            else if (!Main.dayTime)
            {
                updatedDayCounter = false;
            }
            base.PreUpdate();
        }

        public override void Load(TagCompound tag)
        {
            Config.Load();
            var dayCounter = tag.GetList<string>("dayCounter");
            currentDay = int.Parse(dayCounter[0]);
            if (currentDay == null)
            {
                currentDay = 0;
            }
        }
        public override void Initialize()
        {
            fishLocations = new Dictionary<int, string>
            {
                { 2475, "is found at any height in the mushroom biome" },
                { 2476, "is found in the sky in the forest biome" },
                { 2450, "is found in the underground, cavern, and underworld heights in the forest biome" },
                { 2477, "is found at any height in the crimson" },
                { 2478, "is found in the underground, cavern, and underworld heights in the forest biome" },
                { 2451, "is found at any height in any biome in honey" },
                { 2479, "is found on the surface in the forest biome" },
                { 2480, "is found in the sky and surface in the ocean biome in hardmode" },
                { 2452, "is found on the surface in the jungle" },
                { 2453, "is found in the sky in the forest biome" },
                { 2481, "is found in the sky and surface in the ocean biome" },
                { 2454, "is found at any height in the corruption in hardmode" },
                { 2482, "is found in the cavern and underworld heights in the forest biome" },
                { 2483, "is found on the surface in the jungle in hardmode" },
                { 2455, "is found on the surface, underground and cavern heights in the forest biome" },
                { 2456, "is found on the surface in the forest biome" },
                { 2457, "is found at any height in the corruption" },
                { 2458, "is found in the sky and surface in the forest biome" },
                { 2460, "is found in the cavern and underworld heights in the forest biome" },
                { 2484, "is found in the cavern and underground heights in the snow biome in hardmode" },
                { 2472, "is found in the cavern and underworld heights in the forest biome" },
                { 2461, "is found in the sky and surface in the forest biome" },
                { 2462, "is found in the cavern and underworld heights in the forest biome in hardmode" },
                { 2463, "is found at any height in the crimson biome in hardmode" },
                { 2485, "is found at any height in the corruption" },
                { 2464, "is found in the underground, cavern, and underworld heights in the forest biome" },
                { 2465, "is found in the underground, cavern in the hallow" },
                { 2486, "is found at any height in the jungle" },
                { 2466, "is found in the underground, cavern, and underworld heights in the snow biome" },
                { 2467, "is found in the sky and surface in the snow biome" },
                { 2468, "is found in the sky and surface in the hallow" },
                { 2487, "is found in at any height in the forest biome" },
                { 2469, "is found in the underground, cavern, and underworld heights in the forest biome" },
                { 2459, "is found in the sky and surface in the forest biome" },
                { 2488, "is found on the surface in the jungle" },
                { 2470, "is found on the surface in the snow biome" },
                { 2471, "is found at any height in the hallow" },
                { 2473, "is found in the sky and surface in any biome in hardmode" },
                { 2474, "is found on the surface in the forest biome" }
            };
            base.Initialize();
        }
        public static void Announce()
        {
            string fishText = "";
            string catchLocation = "";

            if (!fishLocations.TryGetValue(Main.anglerQuestItemNetIDs[Main.anglerQuest], out catchLocation))
            {
                catchLocation = "is a modded fish, so unfortunately no data is available";
            }

            fishText += "Today's angler quest is [i:" + Main.anglerQuestItemNetIDs[Main.anglerQuest] + "]" + (Config.showFishCatchLocation ? " which " + catchLocation + "." : ". Good luck!");

            if (Main.netMode == NetmodeID.Server)
            {
                if (NPC.savedAngler)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(fishText), Config.mColorReal);
                }
            }
            else if(Main.netMode == NetmodeID.SinglePlayer)
            {

                if (NPC.savedAngler)
                {
                    Main.NewText(fishText, Config.mColorReal);
                }
            }
        }

    }
}

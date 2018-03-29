using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace AnglerQuestAnnouncement
{
    class AnglerAnnouncementWorld : ModWorld
    {
        public bool alreadyAnnouncedDay = false;
        public int? currentDay = null;

        public override void PreUpdate()
        {
            if (Main.dayTime && !alreadyAnnouncedDay)
            {
                alreadyAnnouncedDay = true;
                Main.NewText("Welcome to the morning of day " + currentDay + (NPC.savedAngler ? "! Today's angler quest is [i:" + Main.anglerQuestItemNetIDs[Main.anglerQuest] + "]. Good luck!" : "!"), Color.Pink);
                currentDay++;
            }
            if (!Main.dayTime)
            {
                alreadyAnnouncedDay = false;
            }


            base.PreUpdate();
        }

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
            var dayCounter = new List<string>();
            dayCounter.Add(currentDay.ToString());

            return new TagCompound {
                {"dayCounter", dayCounter}
            };
        }

        public override void Load(TagCompound tag)
        {
            var dayCounter = tag.GetList<string>("dayCounter");
            currentDay = int.Parse(dayCounter[0]);
            if (currentDay == null)
            {
                currentDay = 0;
            }
        }

        

    }
}

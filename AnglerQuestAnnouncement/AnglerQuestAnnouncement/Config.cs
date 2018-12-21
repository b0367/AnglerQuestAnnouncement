using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using System.Globalization;

namespace AnglerQuestAnnouncement
{
    public static class Config
    {
        public static bool showDayCounter = true;
        public static bool showFishCatchLocation = true;
        public static string messageColor = "0,0,255";
        public static Color mColorReal = Color.Blue;
        private static bool success = true;

        static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "AnglerQuestAnnouncement.json");

        static Preferences Configuration = new Preferences(ConfigPath);

        public static bool Load()
        {
            bool success = ReadConfig();

            if (!success)
            {
                ErrorLogger.Log("Failed to read/reload Angler Quest Announcement's config file! Generating new config.");
                CreateConfig();
            }
            return success;
        }

        static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get("showDayCounter", ref showDayCounter);
                Configuration.Get("showFishCatchLocation", ref showFishCatchLocation);
                Configuration.Get("messageColor", ref messageColor);
                if (messageColor.StartsWith("#") && messageColor.Length == 7)
                {
                    string colors = messageColor.Substring(1);
                    int red = int.Parse(colors.Substring(0, 2), NumberStyles.HexNumber);
                    int green = int.Parse(colors.Substring(2, 4).Substring(0, 2), NumberStyles.HexNumber);
                    int blue = int.Parse(colors.Substring(4), NumberStyles.HexNumber);

                    mColorReal = new Color(red, green, blue);
                }
                else
                {
                    string[] rgbs = messageColor.Split(',');
                    if (rgbs.Length == 3)
                    {
                        int[] rgb = new int[3];
                        for (int i = 0; i < 3; i++)
                        {
                            if (!int.TryParse(rgbs[i], out rgb[i]))
                            {
                                Main.NewText("Please input a color in the form \"r,g,b\"");
                                success = false;
                                break;
                            }
                        }
                        if (success)
                        {
                            mColorReal = new Color(rgb[0], rgb[1], rgb[2]);
                        }
                    }
                    else
                    {
                        Main.NewText("Invalid color!");
                    }

                }

            }
            return false;
        }
        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("showDayCounter", showDayCounter);
            Configuration.Put("showFishCatchLocation", showFishCatchLocation);
            Configuration.Put("messageColor", messageColor);
            Configuration.Save();
        }
    }
}
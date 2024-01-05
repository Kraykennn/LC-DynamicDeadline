using BepInEx.Configuration;
using LethalConfig.ConfigItems;
using LethalConfig;
using System.IO;
using System.Reflection;
using UnityEngine;
using LethalConfig.ConfigItems.Options;

namespace DynamicDeadlineMod.Helpers
{
    public static class LethalConfigHelper
    {
        static internal ConfigEntry<float> minScrapValuePerDay;
        static internal ConfigEntry<bool> legacyCal;
        static internal ConfigEntry<float> legacyDailyValue;
        static internal ConfigEntry<bool> useMinMax;
        static internal ConfigEntry<float> setMinimumDays;
        static internal ConfigEntry<float> setMaximumDays;

        public static void SetLehalConfig(ConfigFile config)
        {
            minScrapValuePerDay = config.Bind("General", "Minimum Daily ScrapValue", 200f, "Set this value to the minimum scrap value you should achieve per day. This will ignore the calculation for daily scrap if it's below this number.");
            useMinMax = config.Bind("General", "Use Custom Deadline Range", false, "Set to true if you want to use the custom minimum/maximum deadline range.");
            setMinimumDays = config.Bind("General", "Minimum Deadline (if enable)", 3f, "If Use Custom Deadline Range is enabled, this is the minimum deadline you will have.");
            setMaximumDays = config.Bind("General", "Maximum Deadline (if enable)", float.MaxValue, "If use Custom Deadline Range is enabled, this is the maximum deadline you will have.");
            legacyCal = config.Bind("Legacy", "Use Legacy Calculations", false, "Set to true if you want to use the deadline calculation from 1.1.0 prior.");
            legacyDailyValue = config.Bind("Legacy", "Daily Scrap Value (if enable)", 200f, "Set this number to the value of scrap you can reasonably achieve in a single day.");

            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(minScrapValuePerDay, new FloatSliderOptions
            {
                Min = 200f,
                Max = 1000f,
            }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(useMinMax, false));
            LethalConfigManager.AddConfigItem(new FloatInputFieldConfigItem(setMinimumDays, false));
            LethalConfigManager.AddConfigItem(new FloatInputFieldConfigItem(setMaximumDays, false));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(legacyCal, false));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(legacyDailyValue, new FloatSliderOptions
            {
                Min = 200f,
                Max = 1000f,
            }));

            LethalConfigManager.SetModIcon(LoadNewSprite(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icon.png")));
            LethalConfigManager.SetModDescription("Dynamic Deadline gives you the ability to extend out your deadline by dividing your quota by the average of your daily scrap attained or by a value that you set.");
        }

        private static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 100.0f)
        {
            try
            {
                Texture2D SpriteTexture = LoadTexture(filePath);
                var NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), pixelsPerUnit);

                return NewSprite;
            }
            catch
            {
                return null;
            }
        }

        private static Texture2D LoadTexture(string filePath)
        {
            Texture2D Tex2D;
            byte[] FileData;

            if (File.Exists(filePath))
            {
                FileData = File.ReadAllBytes(filePath);
                Tex2D = new Texture2D(2, 2);
                if (Tex2D.LoadImage(FileData))
                {
                    return Tex2D;
                }
            }
            return null;
        }
    }
}

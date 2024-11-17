using System.Linq;
using System.Reflection;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using StardewModdingAPI;
using StardewValley;

namespace ConfigUI.Framework.Gui;

public class AllModScreen : ScreenGui
{
    public AllModScreen() : base(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.AllModScreen.Title"))
    {
        foreach (var modInfo in ModEntry.GetInstance().Helper.ModRegistry.GetAll())
        {
            var mod = modInfo.GetType().GetMethod("get_Mod")!.Invoke(modInfo, null) as IMod;

            if (mod == null)
            {
                ModEntry.GetInstance().Monitor.Log(
                    ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Log.NoModInstance",
                        new { ModName = modInfo.Manifest.Name }),
                    LogLevel.Warn);
                continue;
            }

            var configField = mod.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                .FirstOrDefault(it =>
                    it.Name.Contains("Config") || it.Name.Contains("config") || it.Name.Contains("_config"));

            if (configField == null) continue;

            var config = configField.GetValue(mod);

            if (config == null)
            {
                ModEntry.GetInstance().Monitor.Log(
                    ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Log.NoConfigInstance",
                        new { ModName = modInfo.Manifest.Name }),
                    LogLevel.Warn);
                continue;
            }

            AddElement(new Button(modInfo.Manifest.Name, modInfo.Manifest.Description)
            {
                OnLeftClicked = () => { OpenScreenGui(new AllConfigScreen(modInfo, mod, config)); }
            });
        }
    }
}
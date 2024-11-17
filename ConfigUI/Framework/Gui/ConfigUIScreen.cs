using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;

namespace ConfigUI.Framework.Gui;

public class ConfigUIScreen : ScreenGui
{
    public ConfigUIScreen()
        : base(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.ConfigUIScreen.Title"))
    {
        AddElement(
            new Button(
                ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.ConfigUIScreen.ViewAllModScreen")
            )
            {
                OnLeftClicked = () => { OpenScreenGui(new AllModScreen()); }
            }
        );
    }
}
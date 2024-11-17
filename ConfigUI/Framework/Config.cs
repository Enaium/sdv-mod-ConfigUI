using ConfigUI.Framework.Attribute;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace ConfigUI.Framework;

public class Config
{
    public static Config Default { get; } = new();

    [Display("{ConfigUI.Config.OpenUI.Name}", "{ConfigUI.Config.OpenUI.Description}")]
    public KeybindList OpenUI { get; set; } = new(SButton.OemTilde);

    [Display("{ConfigUI.Config.Position.Name}", "{ConfigUI.Config.Position.Description}")]
    public PositionType Position { get; set; } = PositionType.Anywhere;

    public enum PositionType
    {
        [Display("{ConfigUI.Config.PositionType.GameOptionsPage.Name}",
            "{ConfigUI.Config.PositionType.GameOptionsPage.Description}")]
        GameOptionsPage,

        [Display("{ConfigUI.Config.PositionType.TitleMenu.Name}",
            "{ConfigUI.Config.PositionType.TitleMenu.Description}")]
        TitleMenu,

        [Display("{ConfigUI.Config.PositionType.Anywhere.Name}",
            "{ConfigUI.Config.PositionType.Anywhere.Description}")]
        Anywhere
    }
}
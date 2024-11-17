using EnaiumToolKit.Framework.Extensions;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ConfigUI.Framework.Gui.Input;

public class KeyBindInputScreen : GuiScreen
{
    private readonly Action<Keybind> _append;
    private readonly HashSet<SButton> _sButtons = new();
    private Button _confirm = null!;

    public KeyBindInputScreen(Action<Keybind> append)
    {
        _append = append;
    }

    protected override void Init()
    {
        var x = Game1.uiViewport.Width / 2 - 100;
        var y = Game1.uiViewport.Height / 2 - 35;

        _confirm = new Button(
            ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Confirm"),
            "",
            x, y + 100, 200, 80
        )
        {
            OnLeftClicked = () =>
            {
                _append(new Keybind(_sButtons.ToArray()));
                Back();
            }
        };
        AddComponentRange(
            _confirm,
            new Button(
                ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Cancel"),
                "",
                x, y + 200, 200, 80
            )
            {
                OnLeftClicked = Back
            }
        );

        base.Init();
    }

    public override void draw(SpriteBatch b)
    {
        _confirm.Visibled = _sButtons.Count > 0;
        base.draw(b);
        b.DrawStringCenter(_sButtons.Count == 0
                ? Game1.content.LoadString("Strings\\StringsFromCSFiles:OptionsElement.cs.11225")
                : string.Join(" + ", _sButtons.ToArray()),
            new Rectangle(0, 0, Game1.graphics.GraphicsDevice.Viewport.Width,
                Game1.graphics.GraphicsDevice.Viewport.Height), color: Color.White);
    }

    public override void receiveKeyPress(Keys key)
    {
        _sButtons.Add(key.ToSButton());
        base.receiveKeyPress(key);
    }

    private void Back()
    {
        if (PreviousMenu != null)
        {
            OpenScreenGui(PreviousMenu);
        }
        else
        {
            Game1.exitActiveMenu();
        }
    }
}
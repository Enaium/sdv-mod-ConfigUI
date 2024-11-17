using EnaiumToolKit.Framework.Extensions;
using EnaiumToolKit.Framework.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;

namespace ConfigUI.Framework.Gui.Input;

public class SButtonInputScreen : GuiScreen
{
    private readonly Action<SButton> _set;

    public SButtonInputScreen(Action<SButton> set)
    {
        _set = set;
    }

    public override void draw(SpriteBatch b)
    {
        b.DrawStringCenter(Game1.content.LoadString("Strings\\StringsFromCSFiles:OptionsElement.cs.11225"),
            new Rectangle(0, 0, Game1.graphics.GraphicsDevice.Viewport.Width,
                Game1.graphics.GraphicsDevice.Viewport.Height), color: Color.White);
    }

    public override void receiveKeyPress(Keys key)
    {
        _set(key.ToSButton());
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
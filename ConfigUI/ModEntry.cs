using ConfigUI.Framework;
using ConfigUI.Framework.Gui;
using EnaiumToolKit.Framework.Screen.Components;
using Force.DeepCloner;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace ConfigUI;

public class ModEntry : Mod
{
    public Config Config;

    private static ModEntry _instance;

    private readonly Button _configUIButton = new("", "", 0, 0, 200, 80)
    {
        OnLeftClicked = () =>
        {
            if (Game1.activeClickableMenu is TitleMenu)
            {
                TitleMenu.subMenu = new ConfigUIScreen();
            }

            Game1.playSound("drumkit6");
        }
    };

    public ModEntry()
    {
        _instance = this;
    }

    public override void Entry(IModHelper helper)
    {
        Config = helper.ReadConfig<Config>();
        helper.Events.Input.ButtonsChanged += OnButtonsChanged;
        helper.Events.Display.RenderingActiveMenu += OnRenderingActiveMenu;
        helper.Events.Display.RenderedActiveMenu += OnRenderedActiveMenu;
        helper.Events.Input.ButtonPressed += OnButtonPressed;
    }

    private void OnRenderingActiveMenu(object? sender, RenderingActiveMenuEventArgs e)
    {
        if (Config.Position is Config.PositionType.GameOptionsPage or Config.PositionType.Anywhere &&
            Game1.activeClickableMenu is GameMenu gameMenu && gameMenu.GetCurrentPage() is OptionsPage optionsPage)
        {
            var whichOption = "ConfigUI".GetHashCode();
            if (optionsPage.options.Find(it => it.whichOption == whichOption) == null)
            {
                optionsPage.options.Insert(0,
                    new OptionsButton(Helper.Translation.Get("ConfigUI.ConfigUIScreen.Title"),
                        () =>
                        {
                            Game1.playSound("drumkit6");
                            Game1.activeClickableMenu = new ConfigUIScreen();
                        }
                    )
                    {
                        whichOption = whichOption
                    }
                );
            }
        }
    }

    private void OnRenderedActiveMenu(object? sender, RenderedActiveMenuEventArgs e)
    {
        if (Config.Position is Config.PositionType.TitleMenu or Config.PositionType.Anywhere &&
            Game1.activeClickableMenu is TitleMenu titleMenu)
        {
            if (TitleMenu.subMenu == null && titleMenu is
                    { isTransitioningButtons: false, titleInPosition: true, transitioningCharacterCreationMenu: false })
            {
                _configUIButton.X = titleMenu.languageButton.bounds.X - _configUIButton.Width / 2;
                _configUIButton.Y = titleMenu.languageButton.bounds.Y - 100;
                _configUIButton.Title = Helper.Translation.Get("ConfigUI.ConfigUIScreen.Title");
                _configUIButton.Render(e.SpriteBatch);
                titleMenu.drawMouse(e.SpriteBatch);
            }
            else
            {
                _configUIButton.Hovered = false;
            }
        }
    }


    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (e.Button != SButton.MouseLeft || !_configUIButton.Hovered) return;
        _configUIButton.Hovered = false;
        _configUIButton.MouseLeftClicked(Game1.getMouseX(), Game1.getMouseY());
    }

    private void OnButtonsChanged(object? sender, ButtonsChangedEventArgs e)
    {
        if (!Config.OpenUI.JustPressed())
            return;
        if (Game1.activeClickableMenu is TitleMenu)
        {
            TitleMenu.subMenu = new ConfigUIScreen();
        }
        else
        {
            Game1.activeClickableMenu = new ConfigUIScreen();
        }
    }

    public static ModEntry GetInstance()
    {
        return _instance;
    }
}
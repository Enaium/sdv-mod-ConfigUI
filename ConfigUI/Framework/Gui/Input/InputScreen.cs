using ConfigUI.Framework.Attribute;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Components;
using StardewValley;

namespace ConfigUI.Framework.Gui.Input;

public class InputScreen : GuiScreen
{
    private readonly Action<object> _set;
    private readonly Size? _size;
    private readonly object _current;
    private readonly string _name;
    private readonly string? _description;

    public InputScreen(Action<object> set, Size? size, object current, string name, string? description)
    {
        _set = set;
        _size = size;
        _current = current;
        _name = name;
        _description = description;
    }

    protected override void Init()
    {
        var x = Game1.uiViewport.Width / 2 - 100;
        var y = Game1.uiViewport.Height / 2 - 35;

        var description = _name;

        if (_description != null)
        {
            description = $"{_name}-{_description}";
        }

        var textField = new TextField(null, description, x - 200, y, 600, 80)
        {
            Text = $"{_current}"
        };

        width = 400;

        AddComponent(textField);

        AddComponent(new Button(
            ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Confirm"),
            "",
            x, y + 100, 200, 80
        )
        {
            OnLeftClicked = () =>
            {
                try
                {
                    switch (_current)
                    {
                        case long:
                        {
                            var modified = long.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = long.Parse(_size.GetMin());
                                var max = long.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case ulong:
                        {
                            var modified = ulong.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = ulong.Parse(_size.GetMin());
                                var max = ulong.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case int:
                        {
                            var modified = int.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = int.Parse(_size.GetMin());
                                var max = int.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case uint:
                        {
                            var modified = uint.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = uint.Parse(_size.GetMin());
                                var max = uint.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case short:
                        {
                            var modified = short.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = short.Parse(_size.GetMin());
                                var max = short.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case ushort:
                        {
                            var modified = ushort.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = ushort.Parse(_size.GetMin());
                                var max = ushort.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case byte:
                        {
                            var modified = byte.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = byte.Parse(_size.GetMin());
                                var max = byte.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case sbyte:
                        {
                            var modified = sbyte.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = sbyte.Parse(_size.GetMin());
                                var max = sbyte.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case float:
                        {
                            var modified = float.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = float.Parse(_size.GetMin());
                                var max = float.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case double:
                        {
                            var modified = double.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = double.Parse(_size.GetMin());
                                var max = double.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case decimal:
                        {
                            var modified = decimal.Parse(textField.Text);
                            if (_size != null)
                            {
                                var min = decimal.Parse(_size.GetMin());
                                var max = decimal.Parse(_size.GetMax());

                                if (modified >= min && modified <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                        case string:
                        {
                            var modified = textField.Text;
                            if (_size != null)
                            {
                                var min = int.Parse(_size.GetMin());
                                var max = int.Parse(_size.GetMax());

                                if (modified.Length >= min && modified.Length <= max)
                                {
                                    _set(modified);
                                }
                            }
                            else
                            {
                                _set(modified);
                            }

                            break;
                        }
                    }

                    Back();
                }
                catch (FormatException e)
                {
                    Game1.addHUDMessage(new HUDMessage(e.Message, HUDMessage.error_type));
                }
            }
        });

        AddComponent(new Button(
            ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Cancel"),
            "",
            x, y + 200, 200, 80
        )
        {
            OnLeftClicked = Back
        });

        base.Init();
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
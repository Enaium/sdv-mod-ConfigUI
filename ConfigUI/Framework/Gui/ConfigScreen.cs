using System.Reflection;
using ConfigUI.Framework.Attribute;
using ConfigUI.Framework.Gui.Input;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace ConfigUI.Framework.Gui;

public class ConfigScreen : ScreenGui
{
    public ConfigScreen(string title, string name, string? description,
        IMod mod, object config, PropertyInfo property, object? defaultConfig) : base(title)
    {
        var current = property.GetValue(config)!;
        AddElement(new Label(name, description));

        if (defaultConfig != null)
        {
            var defaultProperty = property.GetValue(defaultConfig);
            if (defaultProperty != null)
            {
                AddElement(new Button(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Reset"))
                {
                    OnLeftClicked = () =>
                    {
                        SetValue(defaultProperty);
                        if (PreviousMenu != null) OpenScreenGui(PreviousMenu);
                    }
                });
            }
        }

        void SetValue(object value)
        {
            property.SetValue(config, value);
            mod.Helper.WriteConfig(config);
        }

        if (property.PropertyType == typeof(bool))
        {
            AddElement(new CheckBox(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.CheckBox.Enable"))
            {
                Current = (bool)current,
                OnCurrentChanged = value => { SetValue(value); }
            });
        }
        else if (
            property.PropertyType == typeof(long) ||
            property.PropertyType == typeof(ulong) ||
            property.PropertyType == typeof(int) ||
            property.PropertyType == typeof(uint) ||
            property.PropertyType == typeof(short) ||
            property.PropertyType == typeof(ushort) ||
            property.PropertyType == typeof(byte) ||
            property.PropertyType == typeof(byte) ||
            property.PropertyType == typeof(sbyte) ||
            property.PropertyType == typeof(float) ||
            property.PropertyType == typeof(double) ||
            property.PropertyType == typeof(decimal) ||
            property.PropertyType == typeof(string)
        )
        {
            var size = property.GetCustomAttribute<Size>();

            if (size != null && property.PropertyType == typeof(int))
            {
                AddElement(new SliderBar("", null, int.Parse(size.GetMin()), int.Parse(size.GetMax()))
                {
                    Current = (int)current,
                    OnCurrentChanged = value => { SetValue(value); }
                });
            }

            AddElement(
                new Button(
                    $"{ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.ConfigScreen.SetValue")}({current})"
                )
                {
                    OnLeftClicked = () =>
                    {
                        OpenScreenGui(new InputScreen(value =>
                        {
                            SetValue(value);
                            if (PreviousMenu != null) OpenScreenGui(PreviousMenu);
                        }, size, current, name, description));
                    }
                }
            );
        }
        else if (property.PropertyType == typeof(Color))
        {
            AddElement(new ColorPicker("", null, (Color)current)
            {
                OnCurrentChanged = color => { SetValue(color); }
            });
        }
        else if (property.PropertyType == typeof(SButton))
        {
            var setButton = new SetButton(current.ToString()!, null);
            setButton.OnLeftClicked = () =>
            {
                OpenScreenGui(new SButtonInputScreen(value =>
                {
                    setButton.Title = value.ToString();
                    SetValue(value);
                }));
            };
            AddElement(setButton);
        }
        else if (property.PropertyType == typeof(KeybindList))
        {
            var keyBindList = ((KeybindList)current).Keybinds.ToList();
            var modifiedKeys = new List<string>();
            AddElement(new Button(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.Button.Confirm"))
            {
                OnLeftClicked = () =>
                {
                    keyBindList = keyBindList.Where(keyBind => !modifiedKeys.Contains(keyBind.ToString())).ToList();
                    SetValue(new KeybindList(keyBindList.ToArray()));
                    if (PreviousMenu != null) OpenScreenGui(PreviousMenu);
                }
            });
            AddElement(new Button(ModEntry.GetInstance().Helper.Translation.Get("ConfigUI.ConfigScreen.Add"))
            {
                OnLeftClicked = () =>
                {
                    OpenScreenGui(new KeyBindInputScreen(value =>
                    {
                        keyBindList.Add(value);
                        AddElement(KeyBindCheckBox(value, modifiedKeys));
                    }));
                }
            });
            foreach (var keyBind in keyBindList)
            {
                AddElement(KeyBindCheckBox(keyBind, modifiedKeys));
            }
        }
        else if (property.PropertyType.IsEnum)
        {
            var enumOptions = new List<EnumOption>();

            foreach (var value in Enum.GetNames(property.PropertyType))
            {
                var fieldInfo = property.PropertyType.GetField(value);
                if (fieldInfo == null) continue;
                var display = fieldInfo.GetCustomAttribute<Display>();
                if (display != null)
                {
                    var displayName = display.GetName();
                    if (displayName.StartsWith("{") && displayName.EndsWith("}"))
                    {
                        displayName = mod.Helper.Translation.Get(displayName[1..^1]);
                    }

                    var displayDisplayName = display.GetDescription();
                    if (displayDisplayName != null && displayDisplayName.StartsWith("{") &&
                        displayDisplayName.EndsWith("}"))
                    {
                        displayDisplayName = mod.Helper.Translation.Get(displayDisplayName[1..^1]);
                    }

                    display = new Display(displayName, displayDisplayName);
                }

                enumOptions.Add(display != null
                    ? new EnumOption(display, value)
                    : new EnumOption(new Display(value, null), value));
            }

            AddElement(new ComboBox<EnumOption>(name, string.Join(',', enumOptions.Select(it =>
                $"{it.Display.GetName()}{(it.Display.GetDescription() != null ? $" - {it.Display.GetDescription()}" : "")}")))
            {
                Options = enumOptions,
                Current = enumOptions.Find(it => it.Value == current.ToString()),
                OnCurrentChanged = value => { SetValue(Enum.Parse(property.PropertyType, value.Value.ToString())); }
            });
        }
    }

    private CheckBox KeyBindCheckBox(Keybind keyBind, List<string> modifiedKeys)
    {
        return new CheckBox(keyBind.ToString())
        {
            Current = true,
            OnCurrentChanged = value =>
            {
                if (!value)
                {
                    modifiedKeys.Add(keyBind.ToString());
                }
                else
                {
                    modifiedKeys.Remove(keyBind.ToString());
                }
            }
        };
    }

    private record EnumOption(Display Display, string Value)
    {
        public override string ToString()
        {
            return Display.GetName();
        }
    };
}
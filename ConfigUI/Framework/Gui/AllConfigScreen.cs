using System;
using System.Reflection;
using ConfigUI.Framework.Attribute;
using EnaiumToolKit.Framework.Screen;
using StardewModdingAPI;
using Button = EnaiumToolKit.Framework.Screen.Elements.Button;

namespace ConfigUI.Framework.Gui;

public class AllConfigScreen : ScreenGui
{
    public AllConfigScreen(IModInfo modInfo, IMod mod, object config)
        : base(ModEntry.GetInstance().Helper.Translation
            .Get("ConfigUI.AllConfigScreen.Title", new { ModName = modInfo.Manifest.Name }))
    {
        var configProperties = config.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

        object? defaultConfig = null;
        foreach (var configPropertyInfo in configProperties)
        {
            if (!configPropertyInfo.Name.Equals("Default", StringComparison.CurrentCultureIgnoreCase)) continue;
            defaultConfig = configPropertyInfo.GetValue(config);
        }

        if (defaultConfig == null)
        {
            try
            {
                var fullName = config.GetType().FullName;
                if (fullName != null)
                    defaultConfig = config.GetType().Assembly.CreateInstance(fullName);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        foreach (var configPropertyInfo in configProperties)
        {
            if (configPropertyInfo.Name.Equals("Default", StringComparison.CurrentCultureIgnoreCase))
            {
                defaultConfig = configPropertyInfo.GetValue(config);
                continue;
            }

            var display = configPropertyInfo.GetCustomAttribute<Display>();

            var name = display?.GetName();

            name ??= configPropertyInfo.Name;

            if (name.StartsWith("{") && name.EndsWith("}"))
            {
                name = mod.Helper.Translation.Get(name[1..^1]);
            }

            var description = display?.GetDescription();

            if (description != null)
            {
                if (description.StartsWith("{") && description.EndsWith("}"))
                {
                    description = mod.Helper.Translation.Get(description[1..^1]);
                }
            }
            
            var nameTranslation =
                mod.Helper.Translation.Get($"{config.GetType().FullName}.{configPropertyInfo.Name}.Name");
            var descriptionTranslation =
                mod.Helper.Translation.Get($"{config.GetType().FullName}.{configPropertyInfo.Name}.Description");

            if (!nameTranslation.ToString().Contains("no translation:"))
            {
                name = nameTranslation;
            }

            if (!descriptionTranslation.ToString().Contains("no translation:"))
            {
                description = descriptionTranslation;
            }

            var config1 = defaultConfig;
            AddElement(new Button(
                name,
                description
            )
            {
                OnLeftClicked = () =>
                {
                    OpenScreenGui(new ConfigScreen(
                            title: modInfo.Manifest.Name,
                            name: name,
                            description: description,
                            mod: mod,
                            config: config,
                            property: configPropertyInfo,
                            defaultConfig: config1
                        )
                    );
                }
            });
        }
    }
}
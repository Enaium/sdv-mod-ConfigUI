# ConfigUI

This mod can config all mods and the mod doesn't need to do special things.

## Install

1. [Install the latest version of SMAPI](https://smapi.io/).
2. Install [this mod](https://www.curseforge.com/stardewvalley/mods/configui).
3. Install [EnaiumToolKit](https://www.curseforge.com/stardewvalley/mods/enaiumtoolkit).
4. Run the game using SMAPI.

## Fully Adapted

### Translation

#### Attribute

Property `Name` and `Description` support translation.

```csharp
[Display("{ConfigUI.Config.OpenUI.Name}", "{ConfigUI.Config.OpenUI.Description}")]
public KeybindList OpenUI { get; set; } = new(SButton.OemTilde);
```

Enum support translation.

```csharp
public enum PositionType
{
    [Display("{ConfigUI.Config.PositionType.GameOptionsPage.Name}",
            "{ConfigUI.Config.PositionType.GameOptionsPage.Description}")]
    GameOptionsPage
}
```

#### I18n

Property `Name` and `Description` support translation.

Format: `<namespace>.<class>.<property>.<Name|Description>`

```json
{
  "ConfigUI.Framework.Config.OpenUI.Name": "Open UI",
  "ConfigUI.Framework.Config.OpenUI.Description": "Open ConfigUI"
}
```

Enum support translation.

Format: `<namespace>.<enum|class+enum>.<value>.<Name|Description>`

```json
{
  "ConfigUI.Framework.Config+PositionType.GameOptionsPage.Name": "Game Options Page",
  "ConfigUI.Framework.Config+PositionType.GameOptionsPage.Description": "Game Options Page"
}
```

### Limit

#### Attribute

```csharp
[Size(0, 10)]
public int Value { get; set; } = 5;
```

```csharp
[Size(0, 10)]
public string Value { get; set; } = "12345";
```


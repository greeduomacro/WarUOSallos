// Decompiled with JetBrains decompiler
// Type: PlayUO.CharacterQualityVisualizerGroup
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class CharacterQualityVisualizerGroup : SettingVisualizerGroup
  {
    public CharacterQualityVisualizerGroup()
      : base(275, 16, 2, "character-quality")
    {
      SettingVisualizerPanel settingVisualizerPanel = new SettingVisualizerPanel();
      settingVisualizerPanel.AddSetting((IVisualizableSetting) new SmoothCharactersSetting());
      settingVisualizerPanel.AddSetting((IVisualizableSetting) new AnimatedCharactersSetting());
      this.Children.Add((Gump) settingVisualizerPanel);
    }
  }
}

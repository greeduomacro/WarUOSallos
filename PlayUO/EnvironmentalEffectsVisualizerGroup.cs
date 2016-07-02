// Decompiled with JetBrains decompiler
// Type: PlayUO.EnvironmentalEffectsVisualizerGroup
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class EnvironmentalEffectsVisualizerGroup : SettingVisualizerGroup
  {
    public EnvironmentalEffectsVisualizerGroup()
      : base(275, 157, 2, "environmental-effects")
    {
      SettingVisualizerPanel settingVisualizerPanel = new SettingVisualizerPanel();
      settingVisualizerPanel.AddSetting((IVisualizableSetting) new CharacterShadowSetting());
      settingVisualizerPanel.AddSetting((IVisualizableSetting) new ItemShadowSetting());
      this.Children.Add((Gump) settingVisualizerPanel);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.AnimatedCharactersSetting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class AnimatedCharactersSetting : IVisualizableSetting
  {
    public bool Enabled
    {
      get
      {
        return Preferences.Current.RenderSettings.AnimatedCharacters;
      }
      set
      {
        Preferences.Current.RenderSettings.AnimatedCharacters = value;
      }
    }

    public string LabelKey
    {
      get
      {
        return !this.Enabled ? "lockstep" : "animated";
      }
    }

    public void Draw(int x, int y)
    {
      int xCenter = x + 35;
      int yCenter = y + 63;
      int TextureX = 0;
      int TextureY = 0;
      Engine.m_Animations.GetFrame((IAnimationOwner) null, 400, this.Enabled ? 2 : 0, 1, 0, xCenter, yCenter, Hues.Load(1004), ref TextureX, ref TextureY, false).Image.Draw(TextureX, TextureY);
    }
  }
}

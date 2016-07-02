// Decompiled with JetBrains decompiler
// Type: PlayUO.CharacterShadowSetting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class CharacterShadowSetting : IVisualizableSetting
  {
    public bool Enabled
    {
      get
      {
        return Preferences.Current.RenderSettings.CharacterShadows;
      }
      set
      {
        Preferences.Current.RenderSettings.CharacterShadows = value;
      }
    }

    public string LabelKey
    {
      get
      {
        return !this.Enabled ? "shadowless" : "shadows";
      }
    }

    public void Draw(int x, int y)
    {
      int xCenter = x + 35;
      int yCenter = y + 63;
      int TextureX = 0;
      int TextureY = 0;
      if (this.Enabled)
      {
        Renderer.PushAlpha(0.5f);
        Engine.m_Animations.GetFrame((IAnimationOwner) null, 400, 4, 1, 0, xCenter, yCenter, (IHue) Hues.Shadow, ref TextureX, ref TextureY, false).Image.DrawShadow(TextureX, TextureY, (float) (xCenter - TextureX), (float) (yCenter - TextureY));
        Renderer.PopAlpha();
      }
      Engine.m_Animations.GetFrame((IAnimationOwner) null, 400, 4, 1, 0, xCenter, yCenter, Hues.Load(1004), ref TextureX, ref TextureY, false).Image.Draw(TextureX, TextureY);
    }
  }
}

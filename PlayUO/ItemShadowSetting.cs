// Decompiled with JetBrains decompiler
// Type: PlayUO.ItemShadowSetting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class ItemShadowSetting : IVisualizableSetting
  {
    public bool Enabled
    {
      get
      {
        return Preferences.Current.RenderSettings.ItemShadows;
      }
      set
      {
        Preferences.Current.RenderSettings.ItemShadows = value;
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
      int num1 = x + 34;
      int num2 = y + 76;
      if (this.Enabled)
      {
        Texture texture = Hues.Shadow.GetItem(4967);
        Renderer.PushAlpha(0.5f);
        texture.DrawShadow(num1 - texture.Width / 2, num2 - texture.Height + 8, (float) ((texture.xMax - texture.xMin) / 2), (float) texture.yMax);
        Renderer.PopAlpha();
      }
      Texture texture1 = Hues.Default.GetItem(4967);
      texture1.Draw(num1 - texture1.Width / 2, num2 - texture1.Height);
    }
  }
}

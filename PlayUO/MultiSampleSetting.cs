// Decompiled with JetBrains decompiler
// Type: PlayUO.MultiSampleSetting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class MultiSampleSetting : IVisualizableSetting
  {
    private static Texture[] _textures;
    private int _mode;

    public bool Enabled
    {
      get
      {
        return Preferences.Current.RenderSettings.SmoothingMode == this._mode;
      }
      set
      {
        if (!value)
          return;
        Preferences.Current.RenderSettings.SmoothingMode = this._mode;
      }
    }

    public string LabelKey
    {
      get
      {
        switch (this._mode)
        {
          case 1:
            return "2x-medium";
          case 2:
            return "4x-high";
          default:
            return "none";
        }
      }
    }

    public MultiSampleSetting(int mode)
    {
      this._mode = mode;
    }

    public void Draw(int x, int y)
    {
      if (MultiSampleSetting._textures == null)
      {
        MultiSampleSetting._textures = new Texture[3];
        for (int index = 0; index < MultiSampleSetting._textures.Length; ++index)
          MultiSampleSetting._textures[index] = Engine.LoadArchivedTexture(string.Format("visualizer/rs-ta-{0}.png", (object) index));
      }
      MultiSampleSetting._textures[this._mode].Draw(x + 2, y + 2);
    }
  }
}

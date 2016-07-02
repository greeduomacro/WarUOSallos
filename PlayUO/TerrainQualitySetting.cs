// Decompiled with JetBrains decompiler
// Type: PlayUO.TerrainQualitySetting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class TerrainQualitySetting : IVisualizableSetting
  {
    private static Texture[] _textures;
    private int _mode;

    public bool Enabled
    {
      get
      {
        return Preferences.Current.RenderSettings.TerrainQuality == this._mode;
      }
      set
      {
        if (!value || Preferences.Current.RenderSettings.TerrainQuality == this._mode)
          return;
        Preferences.Current.RenderSettings.TerrainQuality = this._mode;
        TerrainMeshProviders.Reset();
        Renderer.SetupTerrainFormats();
      }
    }

    public string LabelKey
    {
      get
      {
        switch (this._mode)
        {
          case 1:
            return "medium";
          case 2:
            return "high";
          default:
            return "low";
        }
      }
    }

    public TerrainQualitySetting(int mode)
    {
      this._mode = mode;
    }

    public void Draw(int x, int y)
    {
      if (TerrainQualitySetting._textures == null)
      {
        TerrainQualitySetting._textures = new Texture[3];
        for (int index = 0; index < TerrainQualitySetting._textures.Length; ++index)
          TerrainQualitySetting._textures[index] = Engine.LoadArchivedTexture(string.Format("visualizer/rs-tq-{0}.png", (object) index));
      }
      TerrainQualitySetting._textures[this._mode].Draw(x + 2, y + 2);
    }
  }
}

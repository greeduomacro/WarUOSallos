// Decompiled with JetBrains decompiler
// Type: PlayUO.SettingVisualizerGroup
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class SettingVisualizerGroup : Gump
  {
    private int _settingCount;
    private Texture _title;

    public override int Width
    {
      get
      {
        return this._settingCount != 3 ? 179 : 254;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        return 136;
      }
      set
      {
      }
    }

    public SettingVisualizerGroup(int x, int y, int settingCount, string titleKey)
      : base(x, y)
    {
      this._settingCount = settingCount;
      this._title = Engine.LoadArchivedTexture(string.Format("visualizer/titles/{0}.png", (object) titleKey));
    }

    protected internal override void OnDispose()
    {
      base.OnDispose();
      this._title.Dispose();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return false;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      Renderer.SetTexture((Texture) null);
      Renderer.SolidRect(13421772, X, Y, this.Width, this.Height);
      Renderer.SolidRect(15658734, X + 1, Y + 1, this.Width - 2, this.Height - 2);
      this._title.Draw(X + 5, Y + 5, 3342387);
    }
  }
}

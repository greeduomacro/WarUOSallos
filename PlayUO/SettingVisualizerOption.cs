// Decompiled with JetBrains decompiler
// Type: PlayUO.SettingVisualizerOption
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;
using System.Windows.Forms;

namespace PlayUO
{
  public class SettingVisualizerOption : Gump
  {
    private IVisualizableSetting _setting;
    private Dictionary<string, Texture> _labels;

    public override int Width
    {
      get
      {
        return 68;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        return 88;
      }
      set
      {
      }
    }

    public SettingVisualizerOption(int index, IVisualizableSetting setting)
      : base(1 + index * 75, 1)
    {
      this._labels = new Dictionary<string, Texture>();
      this._setting = setting;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      base.OnMouseUp(X, Y, mb);
      if (mb != MouseButtons.Left)
        return;
      this._setting.Enabled = !this._setting.Enabled;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      this._setting.Draw(X, Y);
      string labelKey = this._setting.LabelKey;
      Texture texture;
      if (!this._labels.TryGetValue(labelKey, out texture))
        this._labels.Add(labelKey, texture = Engine.LoadArchivedTexture(string.Format("visualizer/labels/{0}.png", (object) labelKey)));
      Renderer.PushAlpha(this._setting.Enabled ? 1f : 0.5f);
      texture.Draw(X + 2, Y + this.Height - 21, 3342387);
      Renderer.PopAlpha();
      if (Gumps.LastOver != this)
        return;
      Renderer.PushAlpha(0.1f);
      Renderer.SetTexture((Texture) null);
      Renderer.SolidRect(3342387, X, Y, this.Width, this.Height);
      Renderer.PopAlpha();
    }
  }
}

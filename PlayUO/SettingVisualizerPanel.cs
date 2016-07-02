// Decompiled with JetBrains decompiler
// Type: PlayUO.SettingVisualizerPanel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class SettingVisualizerPanel : Gump
  {
    public override int Width
    {
      get
      {
        return 2 + this.Children.Count * 68 + Math.Max(this.Children.Count - 1, 0) * 7;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        return 90;
      }
      set
      {
      }
    }

    public SettingVisualizerPanel()
      : base(18, 30)
    {
    }

    public void AddSetting(IVisualizableSetting setting)
    {
      this.Children.Add((Gump) new SettingVisualizerOption(this.Children.Count, setting));
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      Renderer.SetTexture((Texture) null);
      Renderer.SolidRect(13421772, X, Y, this.Width, this.Height);
      Renderer.SolidRect(16777215, X + 1, Y + 1, this.Width - 2, this.Height - 22);
      Renderer.GradientRect(16777215, 14540253, X + 1, Y + this.Height - 21, this.Width - 2, 20);
    }
  }
}

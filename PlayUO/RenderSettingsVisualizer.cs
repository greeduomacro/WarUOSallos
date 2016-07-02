// Decompiled with JetBrains decompiler
// Type: PlayUO.RenderSettingsVisualizer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class RenderSettingsVisualizer : Gump
  {
    public override int Width
    {
      get
      {
        return 470;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        return 309;
      }
      set
      {
      }
    }

    public RenderSettingsVisualizer(int x, int y)
      : base(x, y)
    {
      this.Children.Add((Gump) new TerrainQualityVisualizerGroup());
      this.Children.Add((Gump) new MultiSampleVisualizerGroup());
      this.Children.Add((Gump) new CharacterQualityVisualizerGroup());
      this.Children.Add((Gump) new EnvironmentalEffectsVisualizerGroup());
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      base.OnMouseDown(X, Y, mb);
      this.BringToTop();
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
    }
  }
}

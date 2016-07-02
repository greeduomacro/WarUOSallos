// Decompiled with JetBrains decompiler
// Type: PlayUO.GVolumeControl
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GVolumeControl : Gump
  {
    private Texture m_Background;
    private Texture m_Slider;

    public override int Width
    {
      get
      {
        return this.m_Background.Width;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Background.Height;
      }
      set
      {
      }
    }

    public GVolumeControl()
      : base(0, 0)
    {
      this.m_Background = Engine.LoadArchivedTexture("volume.png");
      this.m_Slider = Engine.LoadArchivedTexture("volume_slider.png");
      this.m_Children.Add((Gump) new GVolumeSlider(true, this.m_Slider, 13, 48));
      this.m_Children.Add((Gump) new GVolumeSlider(false, this.m_Slider, 13, 94));
      this.GUID = "Volume";
    }

    protected internal override void OnDispose()
    {
      base.OnDispose();
      this.m_Background.Dispose();
      this.m_Slider.Dispose();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Right)
        return;
      Gumps.Destroy((Gump) this);
    }

    protected internal override void Render(int X, int Y)
    {
      this.X = Engine.GameX + Engine.GameWidth - this.Width;
      this.Y = Engine.GameY;
      base.Render(X, Y);
    }

    protected internal override void Draw(int X, int Y)
    {
      this.m_Background.Draw(X, Y);
    }
  }
}

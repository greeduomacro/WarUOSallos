// Decompiled with JetBrains decompiler
// Type: PlayUO.GVolumeSlider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GVolumeSlider : GSliderBase
  {
    private GVolumeSlider.State m_State = GVolumeSlider.State.Inactive;
    private bool m_Sound;
    private Texture m_Texture;
    private int m_Offset;

    public GVolumeSlider(bool sound, Texture texture, int x, int y)
      : base(x, y)
    {
      this.m_Sound = sound;
      this.m_Texture = texture;
      this.LargeOffset = 5;
      this.WheelOffset = 5;
      this.SmallOffset = 1;
      this.Minimum = 0;
      this.Maximum = 100;
      this.Value = sound ? VolumeControl.Sound : VolumeControl.Music;
      this.Width = 100;
      this.Height = 15;
    }

    protected internal override void Draw(int X, int Y)
    {
      int position = this.GetPosition(this.Width);
      if (this.m_State == GVolumeSlider.State.LargeScrollUp)
      {
        if (position > 0)
        {
          int x = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse)).X;
          if (position > x)
            this.Value -= this.LargeOffset;
          else
            this.m_State = GVolumeSlider.State.Inactive;
        }
      }
      else if (this.m_State == GVolumeSlider.State.LargeScrollDown && this.Width - position - this.m_Texture.Width > 0)
      {
        this.Value += this.LargeOffset;
        int x = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse)).X;
        if (position + this.m_Texture.Width < x)
          this.Value += this.LargeOffset;
        else
          this.m_State = GVolumeSlider.State.Inactive;
      }
      this.m_Texture.Draw(X + position - this.m_Texture.Width / 2, Y + (this.Height - this.m_Texture.Height) / 2);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Left)
        return;
      int num = this.m_Texture.Width;
      int position = this.GetPosition(this.Width);
      this.m_State = GVolumeSlider.State.Normal;
      this.m_Offset = X - position;
      this.Value = this.GetValue(X, this.Width);
      Gumps.Capture = (Gump) this;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture == this && this.m_State == GVolumeSlider.State.Normal)
        this.Value = this.GetValue(X, this.Width);
      else if (mb == MouseButtons.Right)
        Gumps.Destroy(this.Parent);
      this.m_State = GVolumeSlider.State.Inactive;
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture != this || this.m_State != GVolumeSlider.State.Normal)
        return;
      this.Value = this.GetValue(X, this.Width);
    }

    protected override void OnChanged(int oldValue)
    {
      if (this.m_Sound)
        VolumeControl.Sound = this.Value;
      else
        VolumeControl.Music = this.Value;
    }

    private enum State
    {
      Normal,
      SmallScrollUp,
      SmallScrollDown,
      LargeScrollUp,
      LargeScrollDown,
      Inactive,
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GSystemMessage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GSystemMessage : GLabel, IMessage
  {
    private TimeSync m_Dispose;
    private float m_SolidDuration;
    private Rectangle m_Rectangle;
    private Rectangle m_ImageRect;
    private DateTime m_UpdateTime;
    private int m_DupeCount;
    private string m_OrigText;
    private GLabel m_DupeLabel;

    public DateTime UpdateTime
    {
      get
      {
        return this.m_UpdateTime;
      }
      set
      {
        this.m_UpdateTime = value;
      }
    }

    public int DupeCount
    {
      get
      {
        return this.m_DupeCount;
      }
      set
      {
        this.m_DupeCount = value;
        if (this.m_DupeCount <= 1)
          return;
        string Text = "(" + this.m_DupeCount.ToString("N0") + ")";
        if (this.m_DupeLabel == null)
          this.m_DupeLabel = new GLabel(Text, this.Font, this.Hue, 0, 0);
        else
          this.m_DupeLabel.Text = Text;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.m_Rectangle;
      }
    }

    public Rectangle ImageRect
    {
      get
      {
        return this.m_ImageRect;
      }
    }

    public string OrigText
    {
      get
      {
        return this.m_OrigText;
      }
      set
      {
        this.m_OrigText = value;
      }
    }

    public GSystemMessage(string text, IFont font, IHue hue, float duration)
      : base(text, font, hue, 0, 0)
    {
      this.m_OverridesCursor = false;
      this.m_SolidDuration = duration;
      this.m_Dispose = new TimeSync((double) this.m_SolidDuration + 1.0);
      this.m_UpdateTime = DateTime.Now;
      this.m_DupeCount = 1;
      this.m_OrigText = text;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Engine.amMoving = false;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None || !Engine.amMoving)
        return;
      Point screen = this.PointToScreen(new Point(X, Y));
      int distance = 0;
      Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Point screen = this.PointToScreen(new Point(x, y));
      int distance = 0;
      Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
      Engine.amMoving = true;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void Draw(int x, int y)
    {
      Gump[] array = Gumps.Desktop.Children.ToArray();
      float num = 1f;
      for (int index = Array.IndexOf<Gump>(array, (Gump) this) + 1; index < array.Length; ++index)
      {
        Gump gump = array[index];
        if (gump is IMessage)
        {
          IMessage message = (IMessage) gump;
          if (message.Visible && message.ImageRect.IntersectsWith(this.m_ImageRect))
            num += message.Alpha;
        }
      }
      float alpha = this.Alpha;
      this.Alpha = 1f / num * alpha;
      if (this.m_DupeLabel != null)
      {
        this.m_DupeLabel.Alpha = this.Alpha;
        this.m_DupeLabel.Draw(x + 5 + this.Width, y);
      }
      base.Draw(x, y);
      this.Alpha = alpha;
    }

    public Rectangle OnBeginRender()
    {
      double elapsed = this.m_Dispose.Elapsed;
      if (elapsed >= (double) this.m_SolidDuration + 1.0)
      {
        this.Visible = false;
        MessageManager.Remove((IMessage) this);
        return this.m_ImageRect = this.m_Rectangle = Rectangle.Empty;
      }
      if (elapsed >= (double) this.m_SolidDuration)
        this.Alpha = (float) (1.0 - (elapsed - (double) this.m_SolidDuration));
      this.Visible = true;
      this.X = Engine.GameX + 2;
      this.Y = MessageManager.yStack - this.Height;
      MessageManager.yStack = this.Y - 2;
      this.m_Rectangle.X = this.X;
      this.m_Rectangle.Y = this.Y;
      this.m_Rectangle.Width = this.Width;
      this.m_Rectangle.Height = this.Height;
      this.m_ImageRect.X = this.X + this.Image.xMin;
      this.m_ImageRect.Y = this.Y + this.Image.yMin;
      this.m_ImageRect.Width = this.Image.xMax - this.Image.xMin + 1;
      this.m_ImageRect.Height = this.Image.yMax - this.Image.yMin + 1;
      this.Scissor(new Clipper(Engine.GameX, Engine.GameY, Engine.GameWidth, Engine.GameHeight));
      return this.m_Rectangle;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GDynamicMessage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GDynamicMessage : GTextButton, IMessage
  {
    private IMessageOwner m_Owner;
    private TimeSync m_Dispose;
    private float m_SolidDuration;
    private Rectangle m_Rectangle;
    private Rectangle m_ImageRect;
    private bool m_Unremovable;

    public bool Unremovable
    {
      get
      {
        return this.m_Unremovable;
      }
    }

    public IMessageOwner Owner
    {
      get
      {
        return this.m_Owner;
      }
    }

    public Rectangle ImageRect
    {
      get
      {
        return this.m_ImageRect;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.m_Rectangle;
      }
    }

    private GDynamicMessage(bool unremovable, IMessageOwner owner, string text, IFont font, IHue hue, float duration)
      : base(text, font, hue, Hues.Load(53), 0, 0, (OnClick) null)
    {
      this.m_Unremovable = unremovable;
      this.m_OverridesCursor = false;
      this.m_Owner = owner;
      this.m_SolidDuration = duration;
      this.m_Dispose = new TimeSync((double) this.m_SolidDuration + 1.0);
    }

    public GDynamicMessage(bool unremovable, Mobile m, string text, IFont font, IHue hue)
      : this(unremovable, (IMessageOwner) m, text, font, hue, Engine.MobileDuration)
    {
    }

    public GDynamicMessage(bool unremovable, Item i, string text, IFont font, IHue hue)
      : this(unremovable, (IMessageOwner) i, text, font, hue, Engine.ItemDuration)
    {
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      base.OnMouseEnter(x, y, mb);
      this.BringToTop();
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      if (Gumps.LastOver != this)
        return;
      if (TargetManager.IsActive)
        this.m_Owner.OnTarget();
      else
        this.m_Owner.OnSingleClick();
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
      base.Draw(x, y);
      this.Alpha = alpha;
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      this.m_Owner.OnDoubleClick();
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None || !Engine.amMoving)
        return;
      Point screen = this.PointToScreen(new Point(X, Y));
      int distance = 0;
      Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Engine.amMoving = false;
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
      if (this.m_Owner.MessageFrame == Renderer.m_ActFrames)
      {
        this.m_Owner.MessageY -= this.Height + 2;
        this.X = this.m_Owner.MessageX - this.Width / 2;
        this.Y = this.m_Owner.MessageY;
        this.Visible = true;
      }
      else
        this.Visible = false;
      if (this.m_Owner is Item && !((Agent) this.m_Owner).InWorld)
      {
        if (this.X < 2)
          this.X = 2;
        else if (this.X + this.Width > Engine.ScreenWidth - 2)
          this.X = Engine.ScreenWidth - 2 - this.Width;
        if (this.Y < 2)
          this.Y = 2;
        else if (this.Y + this.Height > Engine.ScreenHeight - 2)
          this.Y = Engine.ScreenHeight - 2 - this.Height;
      }
      else
      {
        if (this.X < Engine.GameX + 2)
          this.X = Engine.GameX + 2;
        else if (this.X + this.Width > Engine.GameX + Engine.GameWidth - 2)
          this.X = Engine.GameX + Engine.GameWidth - 2 - this.Width;
        if (this.Y < Engine.GameY + 2)
          this.Y = Engine.GameY + 2;
        else if (this.Y + this.Height > Engine.GameY + Engine.GameHeight - 2)
          this.Y = Engine.GameY + Engine.GameHeight - 2 - this.Height;
      }
      this.m_Rectangle.X = this.X;
      this.m_Rectangle.Y = this.Y;
      this.m_Rectangle.Width = this.Width;
      this.m_Rectangle.Height = this.Height;
      this.m_ImageRect.X = this.X + this.Image.xMin;
      this.m_ImageRect.Y = this.Y + this.Image.yMin;
      this.m_ImageRect.Width = this.Image.xMax - this.Image.xMin + 1;
      this.m_ImageRect.Height = this.Image.yMax - this.Image.yMin + 1;
      return this.m_Rectangle;
    }
  }
}

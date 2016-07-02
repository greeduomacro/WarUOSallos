// Decompiled with JetBrains decompiler
// Type: PlayUO.GEditorScroller
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GEditorScroller : GSliderBase
  {
    private GEditorScroller.State m_State = GEditorScroller.State.Inactive;
    private GEditorPanel m_Panel;
    private Texture m_ScrollTexture;
    private int m_Offset;

    public GEditorScroller(GEditorPanel panel)
      : base(0, 0)
    {
      this.m_Panel = panel;
      this.LargeOffset = 21;
      this.WheelOffset = 21;
      this.SmallOffset = 7;
    }

    protected internal override void OnDispose()
    {
      if (this.m_ScrollTexture != null)
        this.m_ScrollTexture.Dispose();
      this.m_ScrollTexture = (Texture) null;
      base.OnDispose();
    }

    private int GetBarHeight()
    {
      int num1 = this.Height - 32;
      int num2 = num1 * this.LargeOffset / (this.Maximum - this.Minimum + 1);
      if (num2 > num1)
        num2 = num1;
      if (num2 < 8)
        num2 = 8;
      return num2;
    }

    protected internal override unsafe void Draw(int X, int Y)
    {
      if (this.m_ScrollTexture == null)
      {
        this.m_ScrollTexture = new Texture(16, 16, TextureTransparency.None);
        LockData lockData = this.m_ScrollTexture.Lock(LockFlags.WriteOnly);
        ushort num1 = Engine.C32216(GumpColors.ControlLightLight);
        ushort num2 = Engine.C32216(GumpColors.ScrollBar);
        for (int index1 = 0; index1 < 16; ++index1)
        {
          ushort* numPtr = (ushort*) ((IntPtr) lockData.pvSrc + (IntPtr) index1 * lockData.Pitch);
          for (int index2 = 0; index2 < 16; ++index2)
            *numPtr++ = ((index1 & 1) + index2 & 1) != 0 ? num2 : num1;
        }
        this.m_ScrollTexture.Unlock();
      }
      this.m_ScrollTexture.Draw(X, Y, this.Width, this.Height);
      int barHeight = this.GetBarHeight();
      int num3 = Y + 16;
      int num4 = this.Height - 32;
      int position = this.GetPosition(num4 - barHeight);
      Renderer.SetTexture((Texture) null);
      if (this.m_State == GEditorScroller.State.LargeScrollUp)
      {
        if (position > 0)
        {
          Renderer.PushAlpha(0.9f);
          Renderer.SolidRect(GumpColors.ControlDarkDark, X, Y + this.Width, this.Width, position);
          Renderer.PopAlpha();
          int num1 = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse)).Y - 16;
          if (position > num1)
            this.Value -= this.LargeOffset;
          else
            this.m_State = GEditorScroller.State.Inactive;
        }
      }
      else if (this.m_State == GEditorScroller.State.LargeScrollDown && num4 - position - barHeight > 0)
      {
        Renderer.PushAlpha(0.9f);
        Renderer.SolidRect(GumpColors.ControlDarkDark, X, num3 + position + barHeight, this.Width, num4 - position - barHeight);
        Renderer.PopAlpha();
        int num1 = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse)).Y - 16;
        if (position + barHeight < num1)
          this.Value += this.LargeOffset;
        else
          this.m_State = GEditorScroller.State.Inactive;
      }
      GumpPaint.DrawRaised3D(X, num3 + position, 16, barHeight);
      if (this.m_State == GEditorScroller.State.SmallScrollUp)
      {
        GumpPaint.DrawFlat(X, Y, this.Width, this.Width);
        Engine.m_WinScrolls[0].Draw(X + 5, Y + 7, GumpColors.ControlText);
        this.Value -= this.SmallOffset;
      }
      else
      {
        GumpPaint.DrawRaised3D(X, Y, this.Width, this.Width);
        Engine.m_WinScrolls[0].Draw(X + 4, Y + 6, GumpColors.ControlText);
      }
      Renderer.SetTexture((Texture) null);
      if (this.m_State == GEditorScroller.State.SmallScrollDown)
      {
        GumpPaint.DrawFlat(X, Y + this.Height - this.Width, this.Width, this.Width);
        Engine.m_WinScrolls[1].Draw(X + 5, Y + this.Height - this.Width + 7, GumpColors.ControlText);
        this.Value += this.SmallOffset;
      }
      else
      {
        GumpPaint.DrawRaised3D(X, Y + this.Height - this.Width, this.Width, this.Width);
        Engine.m_WinScrolls[1].Draw(X + 4, Y + this.Height - this.Width + 6, GumpColors.ControlText);
      }
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      int barHeight = this.GetBarHeight();
      int num1 = 16;
      int num2 = this.Height - 32;
      if (Y < num1)
      {
        this.m_State = GEditorScroller.State.SmallScrollUp;
        Gumps.Capture = (Gump) this;
      }
      else if (Y >= num1 + num2)
      {
        this.m_State = GEditorScroller.State.SmallScrollDown;
        Gumps.Capture = (Gump) this;
      }
      else
      {
        int position = this.GetPosition(num2 - barHeight);
        int num3 = Y - num1 - position;
        if (num3 < 0)
        {
          this.m_State = GEditorScroller.State.LargeScrollUp;
          Gumps.Capture = (Gump) this;
        }
        else if (num3 >= barHeight)
        {
          this.m_State = GEditorScroller.State.LargeScrollDown;
          Gumps.Capture = (Gump) this;
        }
        else
        {
          this.m_State = GEditorScroller.State.Normal;
          this.m_Offset = num3;
          this.Value = this.GetValue(num3 - this.m_Offset + position, this.Height - 32 - barHeight);
          Gumps.Capture = (Gump) this;
        }
      }
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture == this && this.m_State == GEditorScroller.State.Normal)
      {
        int barHeight = this.GetBarHeight();
        this.Value = this.GetValue(Y - 16 - this.m_Offset, this.Height - 32 - barHeight);
      }
      this.m_State = GEditorScroller.State.Inactive;
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture != this || this.m_State != GEditorScroller.State.Normal)
        return;
      int barHeight = this.GetBarHeight();
      this.Value = this.GetValue(Y - 16 - this.m_Offset, this.Height - 32 - barHeight);
    }

    protected override void OnChanged(int oldValue)
    {
      this.m_Panel.Layout();
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

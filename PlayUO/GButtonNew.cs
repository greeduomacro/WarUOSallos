// Decompiled with JetBrains decompiler
// Type: PlayUO.GButtonNew
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Assets;
using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GButtonNew : Gump, IClickable
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    protected bool m_Enabled;
    protected int[] m_GumpID;
    protected int m_State;
    protected bool m_Invalidated;
    protected bool m_Draw;
    protected Texture m_Image;
    protected int m_Width;
    protected int m_Height;
    protected bool m_CanEnter;
    protected VertexCache m_vCache;

    protected VertexCachePool VCPool
    {
      get
      {
        return GButtonNew.m_vPool;
      }
    }

    public bool CanEnter
    {
      get
      {
        return this.m_CanEnter;
      }
      set
      {
        this.m_CanEnter = value;
      }
    }

    public int State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        if (this.m_State == value)
          return;
        this.m_State = value;
        this.Invalidate();
      }
    }

    public override int Width
    {
      get
      {
        if (this.m_Invalidated)
          this.Refresh();
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        if (this.m_Invalidated)
          this.Refresh();
        return this.m_Height;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.m_Enabled;
      }
      set
      {
        if (this.m_Enabled == value)
          return;
        this.m_Enabled = value;
        if (!this.m_Enabled)
          this.State = 0;
        this.Invalidate();
      }
    }

    public event EventHandler Clicked;

    public GButtonNew(int gumpID, int x, int y)
      : this(gumpID, gumpID + 1, gumpID + 2, x, y)
    {
    }

    public GButtonNew(int inactiveID, int focusID, int pressedID, int x, int y)
      : base(x, y)
    {
      this.m_GumpID = new int[3]
      {
        inactiveID,
        focusID,
        pressedID
      };
      this.m_Enabled = true;
      this.m_Invalidated = true;
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }

    protected internal override bool OnKeyDown(char c)
    {
      if (!this.m_CanEnter || (int) c != 13)
        return false;
      this.Click();
      return true;
    }

    protected void Invalidate()
    {
      this.m_Invalidated = true;
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (!this.m_Draw)
        return;
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      this.m_vCache.Draw(this.m_Image, x, y);
    }

    protected virtual void Refresh()
    {
      this.m_Image = (this.m_Enabled ? (IGraphicProvider) Hues.Default : (IGraphicProvider) Hues.Grayscale).GetGump(this.m_GumpID[this.m_State]);
      if (this.m_Image != null && !this.m_Image.IsEmpty())
      {
        this.m_Width = this.m_Image.Width;
        this.m_Height = this.m_Image.Height;
        this.m_Draw = true;
      }
      else
      {
        this.m_Width = 0;
        this.m_Height = 0;
        this.m_Draw = false;
      }
      this.m_Invalidated = false;
      if (this.m_vCache == null)
        return;
      this.m_vCache.Invalidate();
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (!this.m_Enabled)
        return;
      this.State = (mb & MouseButtons.Left) != MouseButtons.None ? 2 : 1;
    }

    protected internal override void OnMouseLeave()
    {
      if (!this.m_Enabled)
        return;
      this.State = 0;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.OnMouseEnter(x, y, mb);
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if (!this.m_Enabled || (mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.State = 1;
      this.InternalOnClicked();
    }

    public void Click()
    {
      for (int index = 1; index <= 3; ++index)
      {
        this.State = index % 3;
        Engine.DrawNow();
      }
      this.InternalOnClicked();
    }

    private void InternalOnClicked()
    {
      this.OnClicked();
      if (this.Clicked == null)
        return;
      this.Clicked((object) this, EventArgs.Empty);
    }

    protected virtual void OnClicked()
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GDragable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.Windows.Forms;

namespace PlayUO
{
  public class GDragable : Gump, ITranslucent
  {
    protected bool m_CanClose = true;
    protected float m_fAlpha = 1f;
    protected bool m_Draw;
    protected Texture m_Gump;
    protected VertexCache m_vCache;
    protected int m_Width;
    protected int m_Height;
    protected bool m_Drag;
    protected bool m_LinksMoved;
    protected ArrayList m_Dockers;
    protected ArrayList m_Linked;
    protected bool m_bAlpha;
    protected int m_GumpID;
    protected IHue m_Hue;

    public int GumpID
    {
      get
      {
        return this.m_GumpID;
      }
      set
      {
        if (this.m_GumpID == value)
          return;
        this.m_GumpID = value;
        this.m_vCache.Invalidate();
        this.m_Gump = this.m_Hue.GetGump(this.m_GumpID);
        if (this.m_Gump != null && !this.m_Gump.IsEmpty())
        {
          this.m_Width = this.m_Gump.Width;
          this.m_Height = this.m_Gump.Height;
          this.m_Draw = true;
        }
        else
          this.m_Draw = false;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if (this.m_Hue == value)
          return;
        this.m_Hue = value;
        this.m_Gump = this.m_Hue.GetGump(this.m_GumpID);
        this.m_vCache.Invalidate();
        if (this.m_Gump != null && !this.m_Gump.IsEmpty())
        {
          this.m_Width = this.m_Gump.Width;
          this.m_Height = this.m_Gump.Height;
          this.m_Draw = true;
        }
        else
          this.m_Draw = false;
      }
    }

    public float Alpha
    {
      get
      {
        return this.m_fAlpha;
      }
      set
      {
        this.m_fAlpha = value;
        this.m_bAlpha = (double) value != 1.0;
      }
    }

    public bool CanClose
    {
      get
      {
        return this.m_CanClose;
      }
      set
      {
        this.m_CanClose = value;
      }
    }

    public bool Drag
    {
      get
      {
        return this.m_Drag;
      }
      set
      {
        this.m_Drag = value;
      }
    }

    public int OffsetX
    {
      get
      {
        return this.m_OffsetX;
      }
      set
      {
        this.m_OffsetX = value;
      }
    }

    public int OffsetY
    {
      get
      {
        return this.m_OffsetY;
      }
      set
      {
        this.m_OffsetY = value;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public GDragable(int GumpID, int X, int Y)
      : this(GumpID, Hues.Default, X, Y)
    {
    }

    public GDragable(int GumpID, IHue Hue, int X, int Y)
      : base(X, Y)
    {
      this.m_vCache = new VertexCache();
      this.m_GumpID = GumpID;
      this.m_Hue = Hue;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_Dockers = new ArrayList();
      this.m_Linked = new ArrayList();
      this.m_Gump = Hue.GetGump(GumpID);
      if (this.m_Gump == null || this.m_Gump.IsEmpty())
        return;
      this.m_Width = this.m_Gump.Width;
      this.m_Height = this.m_Gump.Height;
      this.m_Draw = true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (!this.m_CanClose || mb != MouseButtons.Right)
        return;
      Gumps.Destroy((Gump) this);
      Engine.CancelClick();
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      Renderer.PushAlpha(this.m_fAlpha);
      this.m_vCache.Draw(this.m_Gump, X, Y);
      Renderer.PopAlpha();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Draw)
        return this.m_Gump.HitTest(X, Y);
      return false;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GButton : Gump, ITranslucent, IClickable
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    protected float m_fAlpha = 1f;
    protected bool[] m_Draw;
    protected Texture[] m_Gump;
    protected int[] m_GumpID;
    protected int m_Width;
    protected int m_Height;
    protected int m_State;
    protected bool m_Enabled;
    protected bool m_CanEnter;
    protected OnClick m_OnClick;
    protected bool m_bAlpha;
    protected VertexCache m_vCache;

    protected VertexCachePool VCPool
    {
      get
      {
        return GButton.m_vPool;
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

    public bool Enabled
    {
      get
      {
        return this.m_Enabled;
      }
      set
      {
        this.m_Enabled = value && this.m_OnClick != null;
        if (this.m_Enabled)
        {
          this.m_Gump = new Texture[3];
          this.m_Draw = new bool[3];
          IHue @default = Hues.Default;
          for (int index = 0; index < 3; ++index)
          {
            this.m_Gump[index] = @default.GetGump(this.m_GumpID[index]);
            this.m_Draw[index] = this.m_Gump[index] != null && !this.m_Gump[index].IsEmpty();
          }
          this.State = this.m_State;
        }
        else
        {
          this.m_Gump = new Texture[3];
          this.m_Draw = new bool[3];
          IHue grayscale = Hues.Grayscale;
          for (int index = 0; index < 3; ++index)
          {
            this.m_Gump[index] = grayscale.GetGump(this.m_GumpID[index]);
            this.m_Draw[index] = this.m_Gump[index] != null && !this.m_Gump[index].IsEmpty();
          }
          this.State = 0;
        }
        if (this.m_vCache == null)
          return;
        this.m_vCache.Invalidate();
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

    public int State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        this.m_State = value;
        if (this.m_Draw[this.m_State])
        {
          this.m_Width = this.m_Gump[this.m_State].Width;
          this.m_Height = this.m_Gump[this.m_State].Height;
        }
        else
        {
          this.m_Width = 0;
          this.m_Height = 0;
        }
        if (this.m_vCache == null)
          return;
        this.m_vCache.Invalidate();
      }
    }

    public GButton(int GumpID, int X, int Y, OnClick ClickHandler)
      : this(GumpID, GumpID + 1, GumpID + 2, X, Y, ClickHandler)
    {
    }

    public GButton(int NormalID, int OverID, int PressedID, int X, int Y, OnClick ClickHandler)
      : base(X, Y)
    {
      this.m_GumpID = new int[3];
      this.m_GumpID[0] = NormalID;
      this.m_GumpID[1] = OverID;
      this.m_GumpID[2] = PressedID;
      this.m_Gump = new Texture[3];
      this.m_Draw = new bool[3];
      IHue @default = Hues.Default;
      for (int index = 0; index < 3; ++index)
      {
        this.m_Gump[index] = @default.GetGump(this.m_GumpID[index]);
        this.m_Draw[index] = this.m_Gump[index] != null && !this.m_Gump[index].IsEmpty();
      }
      this.m_OnClick = ClickHandler;
      this.Enabled = this.m_OnClick != null;
      this.m_ITranslucent = true;
    }

    public void SetGumpID(int GumpID)
    {
      this.SetGumpID(GumpID, GumpID + 1, GumpID + 2);
    }

    public void SetGumpID(int NormalID, int OverID, int PressedID)
    {
      this.m_GumpID = new int[3];
      this.m_GumpID[0] = NormalID;
      this.m_GumpID[1] = OverID;
      this.m_GumpID[2] = PressedID;
      this.m_Gump = new Texture[3];
      this.m_Draw = new bool[3];
      IHue @default = Hues.Default;
      for (int index = 0; index < 3; ++index)
      {
        this.m_Gump[index] = @default.GetGump(this.m_GumpID[index]);
        this.m_Draw[index] = this.m_Gump[index] != null && !this.m_Gump[index].IsEmpty();
      }
      if (this.m_vCache == null)
        return;
      this.m_vCache.Invalidate();
    }

    protected internal override bool OnKeyDown(char c)
    {
      if ((int) c != 13 || !this.m_CanEnter)
        return false;
      this.Click();
      return true;
    }

    public void Click()
    {
      if (!this.m_Enabled)
        return;
      this.State = 1;
      Engine.DrawNow();
      this.State = 2;
      Engine.DrawNow();
      this.State = 0;
      Engine.DrawNow();
      if (this.m_OnClick == null)
        return;
      this.m_OnClick((Gump) this);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (!this.m_Enabled)
        return;
      this.State = mb == MouseButtons.None ? 1 : 2;
    }

    protected internal override void OnMouseLeave()
    {
      if (!this.m_Enabled)
        return;
      this.State = 0;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (!this.m_Enabled)
        return;
      this.State = 2;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (!this.m_Enabled)
        return;
      this.State = 1;
      if (this.m_OnClick == null)
        return;
      this.m_OnClick((Gump) this);
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_Draw[this.m_State])
        return;
      Renderer.PushAlpha(this.m_fAlpha);
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      this.m_vCache.Draw(this.m_Gump[this.m_State], x, y);
      Renderer.PopAlpha();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Enabled && this.m_Draw[this.m_State])
        return this.m_Gump[this.m_State].HitTest(X, Y);
      return false;
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }
  }
}

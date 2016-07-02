// Decompiled with JetBrains decompiler
// Type: PlayUO.GThreeToggle
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GThreeToggle : Gump
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    private Texture[] m_Images;
    private bool[] m_Draw;
    private int m_State;
    private Clipper m_Clipper;
    private OnStateChange m_OnStateChange;
    private Size[] m_Sizes;
    private VertexCache m_vCache;

    protected VertexCachePool VCPool
    {
      get
      {
        return GThreeToggle.m_vPool;
      }
    }

    public OnStateChange OnStateChange
    {
      get
      {
        return this.m_OnStateChange;
      }
      set
      {
        this.m_OnStateChange = value;
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
        if (this.m_vCache != null)
          this.m_vCache.Invalidate();
        this.m_State = value;
        if (this.m_OnStateChange == null)
          return;
        this.m_OnStateChange(this.m_State, (Gump) this);
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Sizes[this.m_State].Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Sizes[this.m_State].Height;
      }
    }

    public GThreeToggle(Texture state0, Texture state1, Texture state2, int initialState, int x, int y)
      : base(x, y)
    {
      this.m_Images = new Texture[3];
      this.m_Images[0] = state0;
      this.m_Images[1] = state1;
      this.m_Images[2] = state2;
      this.m_Draw = new bool[3];
      this.m_Draw[0] = this.m_Images[0] != null && !this.m_Images[0].IsEmpty();
      this.m_Draw[1] = this.m_Images[1] != null && !this.m_Images[1].IsEmpty();
      this.m_Draw[2] = this.m_Images[2] != null && !this.m_Images[2].IsEmpty();
      this.m_Sizes = new Size[3];
      this.m_Sizes[0] = this.m_Draw[0] ? new Size(this.m_Images[0].Width, this.m_Images[0].Height) : Size.Empty;
      this.m_Sizes[1] = this.m_Draw[1] ? new Size(this.m_Images[1].Width, this.m_Images[1].Height) : Size.Empty;
      this.m_Sizes[2] = this.m_Draw[2] ? new Size(this.m_Images[2].Width, this.m_Images[2].Height) : Size.Empty;
      this.m_State = initialState;
    }

    public void Scissor(Clipper c)
    {
      this.m_Clipper = c;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.State = (this.m_State + 1) % 3;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Draw[this.m_State] && (this.m_Clipper == null || this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)))))
        return this.m_Images[this.m_State].HitTest(x, y);
      return false;
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_Draw[this.m_State])
        return;
      if (this.m_Clipper != null)
      {
        this.m_Images[this.m_State].DrawClipped(x, y, this.m_Clipper);
      }
      else
      {
        if (this.m_vCache == null)
          this.m_vCache = this.VCPool.GetInstance();
        this.m_vCache.Draw(this.m_Images[this.m_State], x, y);
      }
    }
  }
}

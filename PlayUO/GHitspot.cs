// Decompiled with JetBrains decompiler
// Type: PlayUO.GHitspot
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GHitspot : Gump, IClipable
  {
    private int m_Width;
    private int m_Height;
    private bool m_WasDown;
    private OnClick m_Target;
    private Clipper m_Clipper;

    public Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
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

    public GHitspot(int X, int Y, int Width, int Height, OnClick Target)
      : base(X, Y)
    {
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Target = Target;
    }

    protected internal override void Draw(int X, int Y)
    {
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Clipper != null)
        return this.m_Clipper.Evaluate(this.PointToScreen(new Point(X, Y)));
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.m_WasDown = true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (this.m_WasDown && this.m_Target != null)
        this.m_Target((Gump) this);
      this.m_WasDown = false;
    }
  }
}

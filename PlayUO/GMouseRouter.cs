// Decompiled with JetBrains decompiler
// Type: PlayUO.GMouseRouter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GMouseRouter : Gump
  {
    private bool m_Draw;
    private Texture m_Gump;
    private int m_GumpID;
    private int m_Width;
    private int m_Height;
    private Gump m_Target;
    private VertexCache m_vCache;

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

    public GMouseRouter(int GumpID, int X, int Y, Gump Target)
      : this(GumpID, Hues.Default, X, Y, Target)
    {
    }

    public GMouseRouter(int GumpID, IHue Hue, int X, int Y, Gump Target)
      : base(X, Y)
    {
      this.m_vCache = new VertexCache();
      this.m_Target = Target;
      this.m_GumpID = GumpID;
      this.m_Gump = Hue.GetGump(this.m_GumpID);
      if (this.m_Gump == null || this.m_Gump.IsEmpty())
        return;
      this.m_Width = this.m_Gump.Width;
      this.m_Height = this.m_Gump.Height;
      this.m_Draw = true;
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      this.m_vCache.Draw(this.m_Gump, X, Y);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Draw)
        return this.m_Gump.HitTest(X, Y);
      return false;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseMove(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseDown(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseEnter(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseUp(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseLeave()
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseLeave();
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseWheel(Delta);
    }
  }
}

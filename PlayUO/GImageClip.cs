// Decompiled with JetBrains decompiler
// Type: PlayUO.GImageClip
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GImageClip : Gump, ITranslucent
  {
    protected float m_fAlpha = 1f;
    private bool m_Draw;
    private Texture m_Gump;
    private int m_Width;
    private int m_Height;
    private int m_Val;
    private int m_Max;
    private int m_GumpID;
    private IHue m_Hue;
    protected bool m_bAlpha;

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

    public double Normal
    {
      get
      {
        double num = (double) this.m_Val / (double) this.m_Max;
        if (num < 0.0)
          return 0.0;
        if (num > 1.0)
          return 1.0;
        return num;
      }
    }

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
        this.m_Gump = this.m_Hue.GetGump(this.m_GumpID);
        if (this.m_Gump != null && !this.m_Gump.IsEmpty())
        {
          this.m_Width = (int) ((double) this.m_Gump.Width * this.Normal);
          this.m_Height = this.m_Gump.Height;
          this.m_Draw = true;
        }
        else
        {
          this.m_Width = this.m_Height = 0;
          this.m_Draw = false;
        }
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
        if (this.m_Gump != null && !this.m_Gump.IsEmpty())
        {
          this.m_Width = (int) ((double) this.m_Gump.Width * this.Normal);
          this.m_Height = this.m_Gump.Height;
          this.m_Draw = true;
        }
        else
        {
          this.m_Width = this.m_Height = 0;
          this.m_Draw = false;
        }
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

    public GImageClip(int GumpID, int X, int Y, int Val, int Max)
      : this(GumpID, Hues.Default, X, Y, Val, Max)
    {
    }

    public GImageClip(int GumpID, IHue Hue, int X, int Y, int Val, int Max)
      : base(X, Y)
    {
      this.m_GumpID = GumpID;
      this.m_Hue = Hue;
      this.m_Val = Val;
      this.m_Max = Max;
      this.m_Gump = this.m_Hue.GetGump(GumpID);
      if (this.m_Gump != null && !this.m_Gump.IsEmpty())
      {
        this.m_Width = (int) ((double) this.m_Gump.Width * this.Normal);
        this.m_Height = this.m_Gump.Height;
        this.m_Draw = true;
      }
      else
      {
        this.m_Width = this.m_Height = 0;
        this.m_Draw = false;
      }
      this.m_ITranslucent = true;
    }

    public void Resize(int Val, int Max)
    {
      this.m_Val = Val;
      this.m_Max = Max;
      this.m_Width = (int) ((double) this.m_Gump.Width * this.Normal);
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      Renderer.PushAlpha(this.m_fAlpha);
      this.m_Gump.Draw(X, Y, this.m_Width, this.m_Height, 16777215);
      Renderer.PopAlpha();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return false;
    }
  }
}

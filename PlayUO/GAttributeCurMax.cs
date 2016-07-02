// Decompiled with JetBrains decompiler
// Type: PlayUO.GAttributeCurMax
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GAttributeCurMax : GEmpty
  {
    private int m_Current;
    private int m_Maximum;
    private GLabel m_GCurrent;
    private GLabel m_GMaximum;

    public int Current
    {
      get
      {
        return this.m_Current;
      }
      set
      {
        this.SetValues(value, this.m_Maximum);
      }
    }

    public int Maximum
    {
      get
      {
        return this.m_Maximum;
      }
      set
      {
        this.SetValues(this.m_Current, value);
      }
    }

    public GAttributeCurMax(int x, int y, int w, int h, int c, int m, IFont font, IHue hue)
      : base(x, y, w, h)
    {
      this.m_Current = c;
      this.m_Maximum = m;
      this.m_GCurrent = (GLabel) new GWrappedLabel(this.m_Current.ToString(), font, hue, 0, 0, w * 2);
      this.m_GMaximum = (GLabel) new GWrappedLabel(this.m_Maximum.ToString(), font, hue, 0, 11, w * 2);
      this.m_Children.Add((Gump) this.m_GCurrent);
      this.m_Children.Add((Gump) this.m_GMaximum);
      this.Update();
    }

    public void SetValues(int cur, int max)
    {
      if (this.m_Current == cur && this.m_Maximum == max)
        return;
      if (this.m_Current != cur)
      {
        this.m_Current = cur;
        this.m_GCurrent.Text = cur.ToString();
      }
      if (this.m_Maximum != max)
      {
        this.m_Maximum = max;
        this.m_GMaximum.Text = max.ToString();
      }
      this.Update();
    }

    public void Update()
    {
      this.m_GCurrent.X = (this.Width - this.m_GCurrent.Width) / 2;
      this.m_GCurrent.Y = 13 - this.m_GCurrent.Height;
      this.m_GMaximum.X = (this.Width - this.m_GMaximum.Width) / 2;
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      Renderer.SolidRect(2171185, X, Y + 10, this.Width, 1);
    }
  }
}

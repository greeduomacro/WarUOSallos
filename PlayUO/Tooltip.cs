// Decompiled with JetBrains decompiler
// Type: PlayUO.Tooltip
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Tooltip : ITooltip
  {
    protected bool m_Big;
    protected string m_Text;
    protected Gump m_Gump;
    protected float m_Delay;
    private int m_WrapWidth;

    public float Delay
    {
      get
      {
        return this.m_Delay;
      }
      set
      {
        this.m_Delay = value;
      }
    }

    public bool Big
    {
      get
      {
        return this.m_Big;
      }
      set
      {
        if (this.m_Big == value)
          return;
        this.m_Gump = (Gump) null;
        this.m_Big = value;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
      set
      {
        if (!(this.m_Text != value))
          return;
        this.m_Gump = (Gump) null;
        this.m_Text = value;
      }
    }

    public Tooltip(string Text)
      : this(Text, false)
    {
    }

    public Tooltip(string Text, bool Big)
      : this(Text, Big, Big ? Engine.ScreenWidth : 100)
    {
    }

    public Tooltip(string Text, bool Big, int Width)
    {
      this.m_Text = Text;
      this.m_Big = Big;
      this.m_Gump = (Gump) null;
      this.m_Delay = 1f;
      this.m_WrapWidth = Width;
    }

    public virtual Gump GetGump()
    {
      if (this.m_Gump != null)
        return this.m_Gump;
      if (this.m_Text == null || this.m_Text.Length <= 0)
        return this.m_Gump = (Gump) null;
      this.m_Gump = (Gump) new GAlphaBackground(0, 0, 100, 100);
      GWrappedLabel gwrappedLabel = new GWrappedLabel(this.m_Text, (IFont) Engine.GetUniFont(1), Hues.Load(1153), 4, 4, this.m_WrapWidth);
      gwrappedLabel.X -= gwrappedLabel.Image.xMin;
      gwrappedLabel.Y -= gwrappedLabel.Image.yMin;
      this.m_Gump.Width = gwrappedLabel.Image.xMax - gwrappedLabel.Image.xMin + 9;
      this.m_Gump.Height = gwrappedLabel.Image.yMax - gwrappedLabel.Image.yMin + 9;
      this.m_Gump.Children.Add((Gump) gwrappedLabel);
      return this.m_Gump;
    }
  }
}

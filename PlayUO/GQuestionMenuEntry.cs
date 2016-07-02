// Decompiled with JetBrains decompiler
// Type: PlayUO.GQuestionMenuEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GQuestionMenuEntry : Gump
  {
    private int m_Width;
    private int m_Height;
    private int m_yBase;
    private GRadioButton m_Radio;
    private GLabel m_Label;
    private AnswerEntry m_Answer;

    public AnswerEntry Answer
    {
      get
      {
        return this.m_Answer;
      }
    }

    public GRadioButton Radio
    {
      get
      {
        return this.m_Radio;
      }
    }

    public Clipper Clipper
    {
      set
      {
        this.m_Radio.Clipper = value;
        this.m_Label.Clipper = value;
      }
    }

    public int yBase
    {
      get
      {
        return this.m_yBase;
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

    public GQuestionMenuEntry(int x, int y, int xWidth, AnswerEntry a)
      : base(x, y)
    {
      this.m_yBase = y;
      this.m_Answer = a;
      this.m_Radio = new GRadioButton(210, 211, false, 0, 0);
      this.m_Label = (GLabel) new GWrappedLabel(a.Text, (IFont) Engine.GetFont(1), Hues.Load(1109), this.m_Radio.Width + 4, 5, xWidth - this.m_Radio.Width - 4);
      this.m_Width = xWidth;
      this.m_Height = this.m_Radio.Height;
      if (this.m_Label.Y + this.m_Label.Height > this.m_Height)
        this.m_Height = this.m_Label.Y + this.m_Label.Height;
      this.m_Children.Add((Gump) this.m_Radio);
      this.m_Children.Add((Gump) this.m_Label);
    }
  }
}

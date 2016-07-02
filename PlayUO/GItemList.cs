// Decompiled with JetBrains decompiler
// Type: PlayUO.GItemList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GItemList : GDragable
  {
    private int m_xLast = -1234;
    private int m_yLast = -1234;
    private GHitspot m_Left;
    private GHitspot m_Right;
    private GLabel m_EntryLabel;
    private double m_xOffsetCap;
    private double m_xOffset;
    private int m_Serial;
    private int m_MenuID;

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public int MenuID
    {
      get
      {
        return this.m_MenuID;
      }
    }

    public GLabel EntryLabel
    {
      get
      {
        return this.m_EntryLabel;
      }
    }

    public double xOffset
    {
      get
      {
        return this.m_xOffset;
      }
      set
      {
        if (value > 0.0)
          value = 0.0;
        else if (value < this.m_xOffsetCap)
          value = this.m_xOffsetCap;
        this.m_xOffset = value;
        int num = (int) value;
        for (int index = 0; index < this.m_Children.Count; ++index)
        {
          if (this.m_Children[index] is GItemListEntry)
            ((GItemListEntry) this.m_Children[index]).xOffset = num;
        }
      }
    }

    public GItemList(int serial, int menuID, string question, AnswerEntry[] answers)
      : base(2320, 25, 25)
    {
      this.m_Serial = serial;
      this.m_MenuID = menuID;
      this.m_EntryLabel = new GLabel("", (IFont) Engine.GetFont(1), Hues.Load(1887), 39, 106);
      this.m_Children.Add((Gump) this.m_EntryLabel);
      GLabel glabel = new GLabel(question, (IFont) Engine.GetFont(1), Hues.Load(1887), 39, 19);
      glabel.Scissor(0, 0, 218, 11);
      this.m_Children.Add((Gump) glabel);
      int x = 37;
      for (int index = 0; index < answers.Length; ++index)
      {
        GItemListEntry gitemListEntry = new GItemListEntry(x, answers[index], this);
        this.m_Children.Add((Gump) gitemListEntry);
        x += gitemListEntry.Width;
      }
      int num = x - 37;
      if (num < 222)
        return;
      this.m_xOffsetCap = (double) -(num - 222);
      this.m_Left = (GHitspot) new GItemListScroller(23, this, 150);
      this.m_Right = (GHitspot) new GItemListScroller(261, this, -150);
      this.m_Children.Add((Gump) this.m_Left);
      this.m_Children.Add((Gump) this.m_Right);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Network.Send((Packet) new PQuestionMenuCancel(this.m_Serial, this.m_MenuID));
      Gumps.Destroy((Gump) this);
    }

    protected internal override void Draw(int x, int y)
    {
      base.Draw(x, y);
      if (this.m_xLast == x && this.m_yLast == y)
        return;
      this.m_xLast = x;
      this.m_yLast = y;
      Clipper clipper = new Clipper(x + 37, y + 45, 222, 47);
      for (int index = 0; index < this.m_Children.Count; ++index)
      {
        if (this.m_Children[index] is GItemListEntry)
          ((GItemListEntry) this.m_Children[index]).Clipper = clipper;
      }
      this.m_EntryLabel.Scissor(new Clipper(x + 39, y + 106, 218, 11));
    }
  }
}

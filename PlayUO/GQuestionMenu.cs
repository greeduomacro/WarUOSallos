// Decompiled with JetBrains decompiler
// Type: PlayUO.GQuestionMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GQuestionMenu : GBackground
  {
    private int m_Serial;
    private int m_MenuID;
    private GVSlider m_Slider;
    private GQuestionMenuEntry[] m_Entries;

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

    public GQuestionMenu(int serial, int menuID, string question, AnswerEntry[] answers)
      : base(9204, Engine.ScreenWidth / 2, 100, 50, 50, true)
    {
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_Serial = serial;
      this.m_MenuID = menuID;
      GWrappedLabel gwrappedLabel = new GWrappedLabel(question, (IFont) Engine.GetFont(1), Hues.Load(1109), this.OffsetX + 4, this.OffsetY + 4, this.UseWidth - 8);
      this.m_Children.Add((Gump) gwrappedLabel);
      this.m_Entries = new GQuestionMenuEntry[answers.Length];
      GBackground gbackground = (GBackground) new GQuestionBackground(this.m_Entries, this.UseWidth - 8, this.UseHeight - 8 - gwrappedLabel.Height - 4, this.OffsetX + 4, gwrappedLabel.Y + gwrappedLabel.Height + 4);
      gbackground.SetMouseOverride((Gump) this);
      int offsetX = gbackground.OffsetX;
      int offsetY1 = gbackground.OffsetY;
      int useWidth = gbackground.UseWidth;
      for (int index = 0; index < answers.Length; ++index)
      {
        GQuestionMenuEntry gquestionMenuEntry = new GQuestionMenuEntry(offsetX, offsetY1, useWidth, answers[index]);
        gbackground.Children.Add((Gump) gquestionMenuEntry);
        gquestionMenuEntry.Radio.ParentOverride = (Gump) gbackground;
        this.m_Entries[index] = gquestionMenuEntry;
        offsetY1 += gquestionMenuEntry.Height + 4;
      }
      gbackground.Height = offsetY1 - 4 - gbackground.OffsetY + (gbackground.Height - gbackground.UseHeight);
      this.Height = this.Height - this.UseHeight + 4 + gwrappedLabel.Height + 4 + gbackground.Height + 4;
      int num1 = (int) ((double) Engine.ScreenHeight * 0.75);
      if (this.Height > num1)
      {
        this.Height = num1;
        gbackground.Height = this.UseHeight - 8 - gwrappedLabel.Height - 4;
      }
      int num2 = offsetY1 - 4 - gbackground.OffsetY;
      if (num2 > gbackground.UseHeight)
      {
        int num3 = num2;
        gbackground.Width += 19;
        this.Width += 19;
        int num4 = gbackground.OffsetX + gbackground.UseWidth - 15;
        int offsetY2 = gbackground.OffsetY;
        gbackground.Children.Add((Gump) new GImage(257, num4, offsetY2));
        gbackground.Children.Add((Gump) new GImage((int) byte.MaxValue, num4, offsetY2 + gbackground.UseHeight - 32));
        int y = offsetY2 + 30;
        while (y + 32 < gbackground.UseHeight)
        {
          gbackground.Children.Add((Gump) new GImage(256, num4, y));
          y += 30;
        }
        this.m_Slider = new GVSlider(254, num4 + 1, offsetY2 + 1 + 12, 13, gbackground.UseHeight - 2 - 24, 0.0, 0.0, (double) (num3 - gbackground.UseHeight), 1.0);
        this.m_Slider.OnValueChange = new OnValueChange(this.OnScroll);
        this.m_Slider.ScrollOffset = 20.0;
        gbackground.Children.Add((Gump) this.m_Slider);
        gbackground.Children.Add((Gump) new GHotspot(num4, offsetY2, 15, gbackground.UseHeight, (Gump) this.m_Slider));
      }
      GButtonNew gbuttonNew1 = new GButtonNew(243, 242, 241, 0, gbackground.Y + gbackground.Height + 4);
      GButtonNew gbuttonNew2 = new GButtonNew(249, 247, 248, 0, gbuttonNew1.Y);
      gbuttonNew1.Clicked += new EventHandler(this.Cancel_Clicked);
      gbuttonNew2.Clicked += new EventHandler(this.Okay_Clicked);
      gbuttonNew1.X = this.OffsetX + this.UseWidth - 4 - gbuttonNew1.Width;
      gbuttonNew2.X = gbuttonNew1.X - 4 - gbuttonNew2.Width;
      this.m_Children.Add((Gump) gbuttonNew1);
      this.m_Children.Add((Gump) gbuttonNew2);
      this.Height += 4 + gbuttonNew1.Height;
      this.m_Children.Add((Gump) gbackground);
      this.Center();
    }

    protected internal override void OnMouseWheel(int delta)
    {
      if (this.m_Slider == null)
        return;
      this.m_Slider.OnMouseWheel(delta);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      this.Cancel();
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
      this.Cancel();
    }

    private void Cancel()
    {
      Network.Send((Packet) new PQuestionMenuCancel(this.m_Serial, this.m_MenuID));
      Gumps.Destroy((Gump) this);
    }

    private void Okay_Clicked(object sender, EventArgs e)
    {
      for (int index = 0; index < this.m_Entries.Length; ++index)
      {
        if (this.m_Entries[index].Radio.State)
        {
          AnswerEntry answer = this.m_Entries[index].Answer;
          Network.Send((Packet) new PQuestionMenuResponse(this.m_Serial, this.m_MenuID, answer.Index, answer.ItemID, 0));
          Gumps.Destroy((Gump) this);
          break;
        }
      }
    }

    private void OnScroll(double vNew, double vOld, Gump g)
    {
      int num = (int) vNew;
      for (int index = 0; index < this.m_Entries.Length; ++index)
        this.m_Entries[index].Y = this.m_Entries[index].yBase - num;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GComboBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GComboBox : GBackground
  {
    protected int m_Index = -1;
    protected IFont m_Font;
    protected IHue m_HRegular;
    protected IHue m_HOver;
    protected int m_Count;
    protected int m_BackID;
    protected GLabel m_Text;
    protected string[] m_List;
    private GBackground m_Dropdown;
    private GButton m_Button;

    public string[] List
    {
      get
      {
        return this.m_List;
      }
      set
      {
        this.m_List = value;
        this.m_Count = this.m_List.Length;
      }
    }

    public int Index
    {
      get
      {
        return this.m_Index;
      }
      set
      {
        if (value < 0 || value >= this.m_Count)
        {
          this.m_Index = -1;
          if (this.m_Text == null)
            return;
          this.m_Text.Text = "";
        }
        else
        {
          this.m_Index = value;
          if (this.m_Text != null)
          {
            this.m_Text.Text = this.m_List[this.m_Index];
            this.m_Text.Y = this.OffsetY + this.UseHeight - this.m_Text.Image.yMax - 2;
          }
          else
          {
            this.m_Text = new GLabel(this.m_List[this.m_Index], this.m_Font, this.m_HRegular, this.OffsetX, this.OffsetY);
            this.m_Text.Scissor(0, 0, this.m_Button.X - this.OffsetX - 2, this.m_Text.Height);
            this.m_Text.Y = this.OffsetY + this.UseHeight - this.m_Text.Image.yMax - 2;
            this.m_Children.Add((Gump) this.m_Text);
          }
        }
      }
    }

    public GComboBox(string[] List, int Index, int BackID, int NormalID, int OverID, int PressedID, int X, int Y, int Width, int Height, IFont Font, IHue HRegular, IHue HOver)
      : base(BackID, Width, Height, X, Y, true)
    {
      this.m_BackID = BackID;
      this.m_Font = Font;
      this.m_HRegular = HRegular;
      this.m_HOver = HOver;
      this.m_Button = new GButton(NormalID, OverID, PressedID, 0, 0, new OnClick(this.OpenList_OnClick));
      this.m_Children.Add((Gump) this.m_Button);
      this.m_Button.Center();
      this.m_Button.X = this.OffsetX + this.UseWidth - this.m_Button.Width - 1;
      ++this.m_Button.Y;
      this.List = List;
      this.Index = Index;
    }

    private void SetIndex_OnClick(Gump g)
    {
      this.Index = (int) g.GetTag("Index");
      Gumps.Destroy((Gump) this.m_Dropdown);
    }

    private void OpenList_OnClick(Gump g)
    {
      if (this.m_Dropdown != null)
        Gumps.Destroy((Gump) this.m_Dropdown);
      Point screen = this.PointToScreen(new Point(0, 0));
      this.m_Dropdown = new GBackground(this.m_BackID, this.Width, this.m_Count * 20 + (this.Height - this.UseHeight), screen.X, screen.Y, true);
      this.m_Dropdown.DestroyOnUnfocus = true;
      int offsetY = this.m_Dropdown.OffsetY;
      int num1 = 0;
      for (int index = 0; index < this.m_Count; ++index)
      {
        GTextButton gtextButton = new GTextButton(this.m_List[index], this.m_Font, this.m_HRegular, this.m_HOver, this.m_Dropdown.OffsetX, offsetY, new OnClick(this.SetIndex_OnClick));
        gtextButton.SetTag("Index", (object) index);
        this.m_Dropdown.Children.Add((Gump) gtextButton);
        offsetY += gtextButton.Height;
        if (gtextButton.Width + 3 > num1)
          num1 = gtextButton.Width + 3;
      }
      this.m_Dropdown.Height = offsetY + (this.m_Dropdown.Height - (this.m_Dropdown.OffsetY + this.m_Dropdown.UseHeight));
      int num2 = num1 + (this.m_Dropdown.Width - this.m_Dropdown.UseWidth);
      if (num2 > this.m_Dropdown.Width)
        this.m_Dropdown.Width = num2;
      Gumps.Desktop.Children.Add((Gump) this.m_Dropdown);
    }
  }
}

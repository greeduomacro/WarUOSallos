// Decompiled with JetBrains decompiler
// Type: PlayUO.GTextBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GTextBox : Gump
  {
    private string m_String = "";
    private int m_MaxChars = -1;
    private int m_Width;
    private int m_Height;
    private char m_PassChar;
    private IFont m_Font;
    private bool m_Focus;
    private bool m_Transparent;
    private IHue m_HNormal;
    private IHue m_HFocus;
    private IHue m_HOver;
    private GLabel m_Text;
    private GBackground m_Back;
    private IClickable m_EnterButton;
    private OnTextChange m_OnTextChange;
    private OnBeforeTextChange m_OnBeforeTextChange;

    public OnBeforeTextChange OnBeforeTextChange
    {
      get
      {
        return this.m_OnBeforeTextChange;
      }
      set
      {
        this.m_OnBeforeTextChange = value;
      }
    }

    public OnTextChange OnTextChange
    {
      get
      {
        return this.m_OnTextChange;
      }
      set
      {
        this.m_OnTextChange = value;
      }
    }

    public int MaxChars
    {
      get
      {
        return this.m_MaxChars;
      }
      set
      {
        this.m_MaxChars = value;
        this.UpdateLabel(true, this.m_HNormal);
      }
    }

    public IClickable EnterButton
    {
      get
      {
        return this.m_EnterButton;
      }
      set
      {
        this.m_EnterButton = value;
      }
    }

    public virtual bool ShowCaret
    {
      get
      {
        return true;
      }
    }

    public string String
    {
      get
      {
        return this.m_String;
      }
      set
      {
        if (this.m_OnBeforeTextChange != null)
          this.m_OnBeforeTextChange((Gump) this);
        this.m_String = value;
        if (this.m_OnTextChange != null)
          this.m_OnTextChange(this.m_String, (Gump) this);
        if (this.m_Focus)
          this.Focus();
        else
          this.Unfocus();
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

    public GTextBox(int GumpID, bool HasBorder, int X, int Y, int Width, int Height, string StartText, IFont f, IHue HNormal, IHue HOver, IHue HFocus)
      : this(GumpID, HasBorder, X, Y, Width, Height, StartText, f, HNormal, HOver, HFocus, char.MinValue)
    {
    }

    public GTextBox(int GumpID, bool HasBorder, int X, int Y, int Width, int Height, string StartText, IFont f, IHue HNormal, IHue HOver, IHue HFocus, char PassChar)
      : base(X, Y)
    {
      this.m_Transparent = GumpID == 0;
      this.m_Back = new GBackground(GumpID, Width, Height, HasBorder);
      this.m_PassChar = PassChar;
      this.m_String = StartText;
      this.m_Font = f;
      this.m_HNormal = HNormal;
      this.m_HFocus = HFocus;
      this.m_HOver = HOver;
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_OverCursor = 14;
      this.UpdateLabel(false, this.m_HNormal);
    }

    internal virtual void Focus()
    {
      this.m_Focus = true;
      this.UpdateLabel(false, this.m_HFocus);
      Gumps.TextFocus = this;
    }

    internal virtual void Unfocus()
    {
      this.m_Focus = false;
      this.UpdateLabel(false, this.m_HNormal);
    }

    protected internal override bool OnKeyDown(char c)
    {
      if ((int) c == 8)
      {
        if (this.m_String.Length > 0)
        {
          if (this.m_OnBeforeTextChange != null)
            this.m_OnBeforeTextChange((Gump) this);
          if (this.m_String.Length > 0)
          {
            this.m_String = this.m_String.Substring(0, this.m_String.Length - 1);
            if (this.m_OnTextChange != null)
              this.m_OnTextChange(this.m_String, (Gump) this);
          }
        }
      }
      else
      {
        if ((int) c == 13)
        {
          if (this.m_EnterButton == null)
            return false;
          this.m_EnterButton.Click();
          return true;
        }
        if ((int) c == 9)
        {
          Gumps.TextBoxTab((Gump) this);
          return true;
        }
        if ((int) c < 32)
          return false;
        if (this.m_OnBeforeTextChange != null)
          this.m_OnBeforeTextChange((Gump) this);
        if (this.m_MaxChars >= 0)
        {
          if (this.m_String.Length >= this.m_MaxChars)
            return false;
        }
        else if (this.m_Font.GetStringWidth((int) this.m_PassChar == 0 ? string.Format(this.ShowCaret ? "{0}{1}_" : "{0}{1}", (object) this.m_String, (object) c) : string.Format(this.ShowCaret ? "{0}_" : "{0}", (object) new string(this.m_PassChar, this.m_String.Length + 1))) + 3 + this.m_Back.OffsetX * 2 >= this.m_Width)
          return false;
        this.m_String += (string) (object) c;
        if (this.m_OnTextChange != null)
          this.m_OnTextChange(this.m_String, (Gump) this);
      }
      this.UpdateLabel(false, this.m_HFocus);
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.m_Focus = true;
      this.UpdateLabel(false, this.m_HFocus);
      Gumps.TextFocus = this;
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (this.m_Focus)
        return;
      this.UpdateLabel(false, this.m_HOver);
    }

    private void UpdateLabel(bool forceUpdate, IHue hue)
    {
      string str = this.m_String;
      if ((int) this.m_PassChar != 0)
        str = new string(this.m_PassChar, str.Length);
      if (this.m_Focus && this.ShowCaret)
        str += "_";
      if (this.m_Text != null && !forceUpdate)
      {
        this.m_Text.Text = str;
        this.m_Text.Hue = hue;
      }
      else
      {
        if (this.m_Text != null)
          Gumps.Destroy((Gump) this.m_Text);
        if (this.m_MaxChars >= 0)
        {
          this.m_Text = (GLabel) new GWrappedLabel(str, this.m_Font, this.m_HNormal, 0, 0, this.m_Width);
          this.m_Text.Scissor(0, 0, this.m_Width, this.m_Height);
        }
        else
          this.m_Text = new GLabel(str, this.m_Font, this.m_HNormal, 0, 0);
      }
    }

    protected internal override void OnMouseLeave()
    {
      if (this.m_Focus)
        return;
      this.UpdateLabel(false, this.m_HNormal);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (!this.m_Transparent)
        return this.m_Back.HitTest(X, Y);
      return true;
    }

    protected internal override void Draw(int X, int Y)
    {
      this.m_Back.Draw(X, Y);
      if (this.m_MaxChars == -1)
        this.m_Text.Draw(X + this.m_Back.OffsetX + 3, Y + this.m_Back.Height - this.m_Text.Height - this.m_Back.OffsetY - 1);
      else
        this.m_Text.Draw(X + this.m_Back.OffsetX - 1, Y + this.m_Back.OffsetY);
    }
  }
}

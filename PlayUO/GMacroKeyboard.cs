// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroKeyboard
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GMacroKeyboard : GAlphaBackground
  {
    private bool m_Bold = true;
    private const int Size = 28;
    private MacroModifiers m_Mods;
    private object[] m_Buttons;
    private object[] m_HighButtons;
    private GSystemButton m_All;
    private GSystemButton m_Ctrl;
    private GSystemButton m_Alt;
    private GSystemButton m_Shift;
    private float m_fX;
    private float m_fY;

    public MacroModifiers Mods
    {
      get
      {
        return this.m_Mods;
      }
      set
      {
        this.m_Mods = value;
        this.Update();
        this.UpdateModifiers();
      }
    }

    public GMacroKeyboard()
      : base(0, 0, 639, 184)
    {
      this.m_Buttons = new object[256];
      this.m_HighButtons = new object[256];
      this.FillColor = GumpColors.Control;
      this.FillAlpha = 1f;
      this.m_NonRestrictivePicking = true;
      int x = this.Width - 98;
      this.m_All = new GSystemButton(x - 19, 10, 20, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), Color.Black, "", (IFont) Engine.GetUniFont(2));
      this.m_Ctrl = new GSystemButton(x, 10, 32, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), Color.Black, "Ctrl", (IFont) Engine.GetUniFont(2));
      this.m_Alt = new GSystemButton(x + 31, 10, 32, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), Color.Black, "Alt", (IFont) Engine.GetUniFont(2));
      this.m_Shift = new GSystemButton(x + 62, 10, 32, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), Color.Black, "Shift", (IFont) Engine.GetUniFont(2));
      this.m_All.OnClick = new OnClick(this.All_OnClick);
      this.m_Ctrl.OnClick = new OnClick(this.Ctrl_OnClick);
      this.m_Alt.OnClick = new OnClick(this.Alt_OnClick);
      this.m_Shift.OnClick = new OnClick(this.Shift_OnClick);
      this.m_Children.Add((Gump) this.m_All);
      this.m_Children.Add((Gump) this.m_Ctrl);
      this.m_Children.Add((Gump) this.m_Alt);
      this.m_Children.Add((Gump) this.m_Shift);
      this.PlaceKey(Keys.Escape, "Esc");
      this.Skip();
      this.PlaceKey(Keys.F1);
      this.PlaceKey(Keys.F2);
      this.PlaceKey(Keys.F3);
      this.PlaceKey(Keys.F4);
      this.Skip(0.5f);
      this.PlaceKey(Keys.F5);
      this.PlaceKey(Keys.F6);
      this.PlaceKey(Keys.F7);
      this.PlaceKey(Keys.F8);
      this.Skip(0.5f);
      this.PlaceKey(Keys.F9);
      this.PlaceKey(Keys.F10);
      this.PlaceKey(Keys.F11);
      this.PlaceKey(Keys.F12);
      this.Skip(0.25f);
      this.m_Bold = false;
      this.PlaceKey(Keys.Snapshot, "Prnt");
      this.PlaceKey(Keys.Scroll, "Scrl");
      this.PlaceKey(Keys.Pause, "Paus");
      this.m_Bold = true;
      this.m_fX = 0.0f;
      this.m_fY += 1.25f;
      this.PlaceKey(Keys.Oemtilde, "~");
      this.PlaceKey(Keys.D1, "1");
      this.PlaceKey(Keys.D2, "2");
      this.PlaceKey(Keys.D3, "3");
      this.PlaceKey(Keys.D4, "4");
      this.PlaceKey(Keys.D5, "5");
      this.PlaceKey(Keys.D6, "6");
      this.PlaceKey(Keys.D7, "7");
      this.PlaceKey(Keys.D8, "8");
      this.PlaceKey(Keys.D9, "9");
      this.PlaceKey(Keys.D0, "0");
      this.PlaceKey(Keys.OemMinus, "-");
      this.PlaceKey(Keys.Oemplus, "+");
      this.m_Bold = false;
      this.PlaceKey(Keys.Back, "Backspace", 2f);
      this.m_Bold = true;
      this.Skip(0.25f);
      this.m_Bold = false;
      this.PlaceKey(Keys.Insert, "Ins");
      this.PlaceKey(Keys.Home);
      this.m_Bold = true;
      this.PlaceKey(Keys.Prior, "↑");
      this.Skip(0.25f);
      this.PlaceKey(Keys.NumLock, "Num");
      this.PlaceKey(Keys.Divide, "/");
      this.PlaceKey(Keys.Multiply, "*");
      this.PlaceKey(Keys.Subtract, "-");
      this.m_fX = 0.0f;
      ++this.m_fY;
      this.PlaceKey(Keys.Tab, 1.5f);
      this.PlaceKey(Keys.Q);
      this.PlaceKey(Keys.W);
      this.PlaceKey(Keys.E);
      this.PlaceKey(Keys.R);
      this.PlaceKey(Keys.T);
      this.PlaceKey(Keys.Y);
      this.PlaceKey(Keys.U);
      this.PlaceKey(Keys.I);
      this.PlaceKey(Keys.O);
      this.PlaceKey(Keys.P);
      this.PlaceKey(Keys.OemOpenBrackets, "[");
      this.PlaceKey(Keys.OemCloseBrackets, "]");
      this.PlaceKey(Keys.OemPipe, "\\", 1.5f);
      this.Skip(0.25f);
      this.m_Bold = false;
      this.PlaceKey(Keys.Delete, "Del");
      this.PlaceKey(Keys.End);
      this.m_Bold = true;
      this.PlaceKey(Keys.Next, "↓");
      this.Skip(0.25f);
      this.PlaceKey(Keys.NumPad7, "7");
      this.PlaceKey(Keys.NumPad8, "8");
      this.PlaceKey(Keys.NumPad9, "9");
      this.PlaceKey(Keys.Add, "+", 1f, 2f);
      this.m_fX = 0.0f;
      ++this.m_fY;
      this.PlaceKey(Keys.Capital, "Caps", 1.75f);
      this.PlaceKey(Keys.A);
      this.PlaceKey(Keys.S);
      this.PlaceKey(Keys.D);
      this.PlaceKey(Keys.F);
      this.PlaceKey(Keys.G);
      this.PlaceKey(Keys.H);
      this.PlaceKey(Keys.J);
      this.PlaceKey(Keys.K);
      this.PlaceKey(Keys.L);
      this.PlaceKey(Keys.OemSemicolon, ";");
      this.PlaceKey(Keys.OemQuotes, "'");
      this.PlaceKey(Keys.Return, 2.25f);
      this.Skip(3.5f);
      this.PlaceKey(Keys.NumPad4, "4");
      this.PlaceKey(Keys.NumPad5, "5");
      this.PlaceKey(Keys.NumPad6, "6");
      this.m_fX = 0.0f;
      ++this.m_fY;
      this.PlaceKey(Keys.ShiftKey, "Shift", 2.25f);
      this.PlaceKey(Keys.Z);
      this.PlaceKey(Keys.X);
      this.PlaceKey(Keys.C);
      this.PlaceKey(Keys.V);
      this.PlaceKey(Keys.B);
      this.PlaceKey(Keys.N);
      this.PlaceKey(Keys.M);
      this.PlaceKey(Keys.Oemcomma, ",");
      this.PlaceKey(Keys.OemPeriod, ".");
      this.PlaceKey(Keys.OemQuestion, "/");
      this.PlaceKey(Keys.ShiftKey, "Shift", 2.75f);
      this.Skip(1.25f);
      this.PlaceKey(Keys.Up, "↑");
      this.Skip(1.25f);
      this.PlaceKey(Keys.NumPad1, "1");
      this.PlaceKey(Keys.NumPad2, "2");
      this.PlaceKey(Keys.NumPad3, "3");
      this.m_Bold = false;
      this.PlaceKey(Keys.Return, "Entr", 1f, 2f);
      this.m_Bold = true;
      this.m_fX = 0.0f;
      ++this.m_fY;
      this.PlaceKey(Keys.ControlKey, "Ctrl", 1.5f);
      this.PlaceKey(Keys.LWin, "Win", 1.25f);
      this.PlaceKey(Keys.Menu, "Alt", 1.25f);
      this.PlaceKey(Keys.Space, 5.75f);
      this.PlaceKey(Keys.Menu, "Alt", 1.25f);
      this.PlaceKey(Keys.RWin, "Win", 1.25f);
      this.PlaceKey(Keys.Apps, 1.25f);
      this.PlaceKey(Keys.ControlKey, "Ctrl", 1.5f);
      this.Skip(0.25f);
      this.PlaceKey(Keys.Left, "←");
      this.PlaceKey(Keys.Down, "↓");
      this.PlaceKey(Keys.Right, "→");
      this.Skip(0.25f);
      this.PlaceKey(Keys.NumPad0, "0", 2f);
      this.PlaceKey(Keys.Decimal, ".");
      this.Mods = MacroModifiers.All;
    }

    public void Update()
    {
      ArrayList dataStore = Engine.GetDataStore();
      foreach (Gump gump in this.m_Children.ToArray())
      {
        GMacroKeyButton gmacroKeyButton = gump as GMacroKeyButton;
        if (gmacroKeyButton != null && gmacroKeyButton.Macro != null)
          dataStore.Add((object) gmacroKeyButton);
      }
      bool flag1 = (this.m_Mods & MacroModifiers.All) != MacroModifiers.None;
      bool flag2 = (this.m_Mods & MacroModifiers.Ctrl) != MacroModifiers.None;
      bool flag3 = (this.m_Mods & MacroModifiers.Alt) != MacroModifiers.None;
      bool flag4 = (this.m_Mods & MacroModifiers.Shift) != MacroModifiers.None;
      MacroCollection list = Macros.List;
      for (int index = 0; index < list.Count; ++index)
      {
        Macro mc = list[index];
        if (flag1 || mc.Control == flag2 && mc.Alt == flag3 && mc.Shift == flag4)
        {
          object button = this.GetButton(mc.Key);
          if (button is GMacroKeyButton[])
          {
            foreach (GMacroKeyButton btn in (GMacroKeyButton[]) button)
              this.SetMacro(dataStore, btn, mc);
          }
          else if (button is GMacroKeyButton)
            this.SetMacro(dataStore, (GMacroKeyButton) button, mc);
        }
      }
      for (int index = 0; index < dataStore.Count; ++index)
        ((GMacroKeyButton) dataStore[index]).Macro = (object) null;
      Engine.ReleaseDataStore(dataStore);
    }

    private void SetMacro(ArrayList list, GMacroKeyButton btn, Macro mc)
    {
      if (list.Contains((object) btn) || btn.Macro == null)
      {
        list.Remove((object) btn);
        btn.Macro = (object) mc;
      }
      else if (btn.Macro is Macro)
      {
        btn.Macro = (object) new Macro[2]
        {
          (Macro) btn.Macro,
          mc
        };
      }
      else
      {
        if (!(btn.Macro is Macro[]))
          return;
        Macro[] macroArray1 = (Macro[]) btn.Macro;
        Macro[] macroArray2 = new Macro[macroArray1.Length + 1];
        for (int index = 0; index < macroArray1.Length; ++index)
          macroArray2[index] = macroArray1[index];
        macroArray2[macroArray1.Length] = mc;
        btn.Macro = (object) macroArray2;
      }
    }

    private object GetButton(Keys key)
    {
      int index1 = key != Keys.Shift ? (key != Keys.Alt ? (key != Keys.Control ? (int) key : 65538) : 65537) : 65536;
      if (index1 >= 0 && index1 < this.m_Buttons.Length)
        return this.m_Buttons[index1];
      int index2 = index1 - 65536;
      if (index2 >= 0 && index2 < this.m_HighButtons.Length)
        return this.m_HighButtons[index2];
      return (object) null;
    }

    public void SetButton(Keys key, GMacroKeyButton btn)
    {
      int index1 = key != Keys.Shift ? (key != Keys.Alt ? (key != Keys.Control ? (int) key : 65538) : 65537) : 65536;
      if (index1 >= 0 && index1 < this.m_Buttons.Length)
      {
        this.SetButton(this.m_Buttons, index1, btn);
      }
      else
      {
        int index2 = index1 - 65536;
        if (index2 < 0 || index2 >= this.m_HighButtons.Length)
          return;
        this.SetButton(this.m_HighButtons, index2, btn);
      }
    }

    private void SetButton(object[] objs, int index, GMacroKeyButton btn)
    {
      object obj = objs[index];
      if (obj is GMacroKeyButton[])
        return;
      if (obj is GMacroKeyButton)
        objs[index] = (object) new GMacroKeyButton[2]
        {
          (GMacroKeyButton) obj,
          btn
        };
      else
        objs[index] = (object) btn;
    }

    protected internal override void OnDragStart()
    {
      if (this.m_Parent == null)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Parent.PointToScreen(new Point(0, 0));
      this.m_Parent.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Parent.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Parent.m_IsDragging = true;
      Gumps.Drag = this.m_Parent;
    }

    private void UpdateModifiers()
    {
      bool flag = (this.m_Mods & MacroModifiers.All) == MacroModifiers.None;
      this.UpdateModifier(this.m_All, "", true, flag);
      this.UpdateModifier(this.m_Ctrl, "Ctrl", flag, (this.m_Mods & MacroModifiers.Ctrl) != MacroModifiers.None);
      this.UpdateModifier(this.m_Alt, "Alt", flag, (this.m_Mods & MacroModifiers.Alt) != MacroModifiers.None);
      this.UpdateModifier(this.m_Shift, "Shift", flag, (this.m_Mods & MacroModifiers.Shift) != MacroModifiers.None);
    }

    private void UpdateModifier(GSystemButton btn, string prefix, bool enabled, bool opt)
    {
      if (!enabled)
        btn.InactiveColor = btn.ActiveColor = btn.PressedColor = SystemColors.Control;
      else if (opt)
      {
        btn.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.5f));
      }
      else
      {
        btn.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.25f));
        btn.InactiveColor = GumpPaint.Blend(Color.White, SystemColors.Control, 0.5f);
      }
    }

    private void All_OnClick(Gump g)
    {
      this.Mods ^= MacroModifiers.All;
    }

    private void Ctrl_OnClick(Gump g)
    {
      if ((this.m_Mods & MacroModifiers.All) != MacroModifiers.None)
        return;
      this.Mods ^= MacroModifiers.Ctrl;
    }

    private void Alt_OnClick(Gump g)
    {
      if ((this.m_Mods & MacroModifiers.All) != MacroModifiers.None)
        return;
      this.Mods ^= MacroModifiers.Alt;
    }

    private void Shift_OnClick(Gump g)
    {
      if ((this.m_Mods & MacroModifiers.All) != MacroModifiers.None)
        return;
      this.Mods ^= MacroModifiers.Shift;
    }

    protected internal override void Render(int X, int Y)
    {
      base.Render(X, Y);
      if (!(Gumps.LastOver is GMenuItem))
        return;
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.4f);
      Renderer.SolidRect(0, X + this.m_X + 1, Y + this.m_Y + 1, this.m_Width - 2, this.m_Height - 2);
      Renderer.PopAlpha();
    }

    public void Skip()
    {
      this.Skip(1f);
    }

    public void Skip(float w)
    {
      this.m_fX += w;
    }

    public void PlaceKey(Keys key)
    {
      this.PlaceKey(key, key.ToString(), 1f);
    }

    public void PlaceKey(Keys key, string name)
    {
      this.PlaceKey(key, name, 1f);
    }

    public void PlaceKey(Keys key, float w)
    {
      this.PlaceKey(key, key.ToString(), w);
    }

    public void PlaceKey(Keys key, string name, float w)
    {
      this.PlaceKey(key, name, w, 1f);
    }

    public void PlaceKey(Keys key, string name, float w, float h)
    {
      GMacroKeyButton btn = new GMacroKeyButton(key, name, this.m_Bold, 4 + (int) ((double) this.m_fX * 28.0), 4 + (int) ((double) this.m_fY * 28.0), 1 + (int) ((double) w * 28.0), 1 + (int) ((double) h * 28.0));
      this.SetButton(key, btn);
      this.m_Children.Add((Gump) btn);
      this.m_fX += w;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroEditorPanel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GMacroEditorPanel : GAlphaBackground
  {
    private static string[] m_Aliases;
    private GSystemButton m_Ctrl;
    private GSystemButton m_Alt;
    private GSystemButton m_Shift;
    private Macro m_Macro;

    public Macro Macro
    {
      get
      {
        return this.m_Macro;
      }
    }

    public GMacroEditorPanel(Macro m)
      : base(0, 0, 259, 230)
    {
      this.m_Macro = m;
      this.m_CanDrag = false;
      this.m_NonRestrictivePicking = true;
      this.ShouldHitTest = false;
      this.m_Ctrl = new GSystemButton(10, 10, 40, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), SystemColors.ControlText, "Ctrl", (IFont) Engine.GetUniFont(2));
      this.m_Alt = new GSystemButton(49, 10, 40, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), SystemColors.ControlText, "Alt", (IFont) Engine.GetUniFont(2));
      this.m_Shift = new GSystemButton(88, 10, 42, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), SystemColors.ControlText, "Shift", (IFont) Engine.GetUniFont(2));
      this.m_Ctrl.OnClick = new OnClick(this.Ctrl_OnClick);
      this.m_Alt.OnClick = new OnClick(this.Alt_OnClick);
      this.m_Shift.OnClick = new OnClick(this.Shift_OnClick);
      this.m_Ctrl.Tooltip = (ITooltip) new Tooltip("Toggles the control key modifier", true);
      this.m_Alt.Tooltip = (ITooltip) new Tooltip("Toggles the alt key modifier", true);
      this.m_Shift.Tooltip = (ITooltip) new Tooltip("Toggles the shift key modifier", true);
      this.m_Children.Add((Gump) this.m_Ctrl);
      this.m_Children.Add((Gump) this.m_Alt);
      this.m_Children.Add((Gump) this.m_Shift);
      this.UpdateModifiers();
      this.m_Children.Add((Gump) new GAlphaBackground(129, 10, 74, 20)
      {
        FillAlpha = 1f,
        FillColor = GumpColors.Window
      });
      GMacroKeyEntry gmacroKeyEntry = new GMacroKeyEntry(GMacroEditorPanel.GetKeyName(m.Key), 129, 10, 74, 20);
      gmacroKeyEntry.Tooltip = (ITooltip) new Tooltip("Press any key here to change the macro", true);
      this.m_Children.Add((Gump) gmacroKeyEntry);
      GSystemButton gsystemButton = new GSystemButton(10, 10, 40, 20, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), SystemColors.ControlText, "Delete", (IFont) Engine.GetUniFont(2));
      gsystemButton.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.25f));
      gsystemButton.InactiveColor = GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f);
      gsystemButton.Tooltip = (ITooltip) new Tooltip("Deletes the entire macro", true);
      gsystemButton.OnClick = new OnClick(this.Delete_OnClick);
      gsystemButton.X = this.Width - 10 - gsystemButton.Width;
      this.m_Children.Add((Gump) gsystemButton);
      this.FillAlpha = 0.15f;
      for (int index = 0; index < m.Actions.Count; ++index)
      {
        try
        {
          Action action = m.Actions[index];
          if (action.Handler != null)
          {
            ActionHandler handler = action.Handler;
            GMainMenu gmainMenu = new GMainMenu(10, 35 + index * 23);
            GMenuItem mi = (GMenuItem) new GActionMenu(this, m, action);
            gmainMenu.Add(this.FormatMenu(mi));
            if (handler.Params == null)
            {
              GAlphaBackground galphaBackground = new GAlphaBackground(129, 35 + index * 23, 120, 24);
              galphaBackground.FillAlpha = 1f;
              galphaBackground.FillColor = GumpColors.Window;
              this.m_Children.Add((Gump) galphaBackground);
              IHue windowText = GumpHues.WindowText;
              GTextBox gtextBox = (GTextBox) new GMacroParamEntry(action, action.Param, galphaBackground.X + 4, galphaBackground.Y, galphaBackground.Width - 4, galphaBackground.Height);
              gtextBox.MaxChars = 239;
              this.m_Children.Add((Gump) gtextBox);
            }
            else if (handler.Params.Length != 0)
            {
              GMenuItem menuFrom = this.GetMenuFrom(new ParamNode(GMacroEditorPanel.Find(action.Param, handler.Params) ?? action.Param, handler.Params), action, handler);
              menuFrom.DropDown = index == m.Actions.Count - 1;
              gmainMenu.Add(menuFrom);
            }
            gmainMenu.LeftToRight = true;
            this.m_Children.Add((Gump) gmainMenu);
          }
        }
        catch
        {
        }
      }
      GMainMenu gmainMenu1 = new GMainMenu(10, 35 + m.Actions.Count * 23);
      GMenuItem menuFrom1 = this.GetMenuFrom(ActionHandler.Root);
      menuFrom1.Tooltip = (ITooltip) new Tooltip("To create a new instruction pick one from the menu below", false, 200);
      menuFrom1.Text = "New...";
      menuFrom1.DropDown = true;
      gmainMenu1.Add(this.FormatMenu(menuFrom1));
      gmainMenu1.LeftToRight = true;
      this.m_Children.Add((Gump) gmainMenu1);
    }

    public static string GetKeyName(Keys key)
    {
      if (key == (Keys) 69632)
        return "Wheel Up";
      if (key == (Keys) 69633)
        return "Wheel Down";
      if (key == (Keys) 69634)
        return "Wheel Press";
      if (GMacroEditorPanel.m_Aliases == null)
        GMacroEditorPanel.LoadAliases();
      int index = (int) key;
      if (index >= 0 && index < GMacroEditorPanel.m_Aliases.Length && GMacroEditorPanel.m_Aliases[index] != null)
        return GMacroEditorPanel.m_Aliases[index];
      return key.ToString();
    }

    private static void LoadAliases()
    {
      GMacroEditorPanel.m_Aliases = new string[256];
      GMacroEditorPanel.SetAlias(Keys.Add, "Num +");
      GMacroEditorPanel.SetAlias(Keys.Back, "Backspace");
      GMacroEditorPanel.SetAlias(Keys.Capital, "Caps Lock");
      GMacroEditorPanel.SetAlias(Keys.ControlKey, "Control");
      GMacroEditorPanel.SetAlias(Keys.D0, "0");
      GMacroEditorPanel.SetAlias(Keys.D1, "1");
      GMacroEditorPanel.SetAlias(Keys.D2, "2");
      GMacroEditorPanel.SetAlias(Keys.D3, "3");
      GMacroEditorPanel.SetAlias(Keys.D4, "4");
      GMacroEditorPanel.SetAlias(Keys.D5, "5");
      GMacroEditorPanel.SetAlias(Keys.D6, "6");
      GMacroEditorPanel.SetAlias(Keys.D7, "7");
      GMacroEditorPanel.SetAlias(Keys.D8, "8");
      GMacroEditorPanel.SetAlias(Keys.D9, "9");
      GMacroEditorPanel.SetAlias(Keys.Decimal, "Num .");
      GMacroEditorPanel.SetAlias(Keys.Divide, "Num /");
      GMacroEditorPanel.SetAlias(Keys.Menu, "Alt");
      GMacroEditorPanel.SetAlias(Keys.Multiply, "Num *");
      GMacroEditorPanel.SetAlias(Keys.NumLock, "Num Lock");
      GMacroEditorPanel.SetAlias(Keys.NumPad0, "Num 0");
      GMacroEditorPanel.SetAlias(Keys.NumPad1, "Num 1");
      GMacroEditorPanel.SetAlias(Keys.NumPad2, "Num 2");
      GMacroEditorPanel.SetAlias(Keys.NumPad3, "Num 3");
      GMacroEditorPanel.SetAlias(Keys.NumPad4, "Num 4");
      GMacroEditorPanel.SetAlias(Keys.NumPad5, "Num 5");
      GMacroEditorPanel.SetAlias(Keys.NumPad6, "Num 6");
      GMacroEditorPanel.SetAlias(Keys.NumPad7, "Num 7");
      GMacroEditorPanel.SetAlias(Keys.NumPad8, "Num 8");
      GMacroEditorPanel.SetAlias(Keys.NumPad9, "Num 9");
      GMacroEditorPanel.SetAlias(Keys.OemClear, "Clear");
      GMacroEditorPanel.SetAlias(Keys.OemCloseBrackets, "]");
      GMacroEditorPanel.SetAlias(Keys.Oemcomma, ",");
      GMacroEditorPanel.SetAlias(Keys.OemMinus, "-");
      GMacroEditorPanel.SetAlias(Keys.OemOpenBrackets, "[");
      GMacroEditorPanel.SetAlias(Keys.OemPeriod, ".");
      GMacroEditorPanel.SetAlias(Keys.OemPipe, "\\");
      GMacroEditorPanel.SetAlias(Keys.OemBackslash, "\\");
      GMacroEditorPanel.SetAlias(Keys.Oemplus, "+");
      GMacroEditorPanel.SetAlias(Keys.OemQuestion, "?");
      GMacroEditorPanel.SetAlias(Keys.OemQuotes, "'");
      GMacroEditorPanel.SetAlias(Keys.OemSemicolon, ";");
      GMacroEditorPanel.SetAlias(Keys.Oemtilde, "~");
      GMacroEditorPanel.SetAlias(Keys.Next, "Page Down");
      GMacroEditorPanel.SetAlias(Keys.Next, "Page Down");
      GMacroEditorPanel.SetAlias(Keys.Prior, "Page Up");
      GMacroEditorPanel.SetAlias(Keys.Prior, "Page Up");
      GMacroEditorPanel.SetAlias(Keys.Snapshot, "Print Screen");
      GMacroEditorPanel.SetAlias(Keys.Scroll, "Scroll Lock");
      GMacroEditorPanel.SetAlias(Keys.ShiftKey, "Shift");
      GMacroEditorPanel.SetAlias(Keys.Subtract, "Num -");
    }

    private static void SetAlias(Keys key, string alias)
    {
      GMacroEditorPanel.m_Aliases[(int) key] = alias;
    }

    public void NotifyParent()
    {
      GMacroEditorForm gmacroEditorForm = this.m_Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      gmacroEditorForm.UpdateKeyboard();
    }

    public void UpdateModifiers()
    {
      this.UpdateModifier(this.m_Ctrl, "Ctrl", this.m_Macro.Control);
      this.UpdateModifier(this.m_Alt, "Alt", this.m_Macro.Alt);
      this.UpdateModifier(this.m_Shift, "Shift", this.m_Macro.Shift);
    }

    protected internal override void Draw(int X, int Y)
    {
    }

    private void UpdateModifier(GSystemButton btn, string prefix, bool opt)
    {
      if (opt)
      {
        btn.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.5f));
      }
      else
      {
        btn.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.25f));
        btn.InactiveColor = GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f);
      }
    }

    private void Ctrl_OnClick(Gump g)
    {
      this.m_Macro.Control = !this.m_Macro.Control;
      this.UpdateModifier(this.m_Ctrl, "Ctrl", this.m_Macro.Control);
      this.NotifyParent();
    }

    private void Alt_OnClick(Gump g)
    {
      this.m_Macro.Alt = !this.m_Macro.Alt;
      this.UpdateModifier(this.m_Alt, "Alt", this.m_Macro.Alt);
      this.NotifyParent();
    }

    private void Shift_OnClick(Gump g)
    {
      this.m_Macro.Shift = !this.m_Macro.Shift;
      this.UpdateModifier(this.m_Shift, "Shift", this.m_Macro.Shift);
      this.NotifyParent();
    }

    private void Delete_OnClick(Gump g)
    {
      if (Macros.List.Contains(this.m_Macro))
        Macros.List.Remove(this.m_Macro);
      GMacroEditorForm gmacroEditorForm = this.m_Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      gmacroEditorForm.Current = (Macro) null;
      gmacroEditorForm.UpdateKeyboard();
    }

    private GMenuItem FormatMenu(GMenuItem mi)
    {
      mi.FillAlpha = 1f;
      mi.DefaultColor = GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f);
      mi.OverColor = GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.5f);
      mi.ExpandedColor = GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.5f);
      mi.SetHue(Hues.Load(1));
      return mi;
    }

    public static string Find(string toFind, ParamNode[] nodes)
    {
      for (int index = 0; nodes != null && index < nodes.Length; ++index)
      {
        string str = GMacroEditorPanel.Find(toFind, nodes[index]);
        if (str != null)
          return str;
      }
      return (string) null;
    }

    public static string Find(string toFind, ParamNode n)
    {
      if (n.Param == toFind)
        return n.Name;
      return GMacroEditorPanel.Find(toFind, n.Nodes);
    }

    private GMenuItem GetMenuFrom(ActionNode n)
    {
      GMenuItem mi1 = new GMenuItem(n.Name);
      for (int index = 0; index < n.Nodes.Count; ++index)
        mi1.Add(this.GetMenuFrom((ActionNode) n.Nodes[index]));
      for (int index1 = 0; index1 < n.Handlers.Count; ++index1)
      {
        ActionHandler actionHandler = (ActionHandler) n.Handlers[index1];
        GMenuItem mi2 = (GMenuItem) new GNewActionMenu(this, this.m_Macro, actionHandler);
        for (int index2 = 0; actionHandler.Params != null && index2 < actionHandler.Params.Length; ++index2)
          mi2.Add(this.GetMenuFrom(actionHandler.Params[index2], (Action) null, actionHandler));
        mi1.Add(this.FormatMenu(mi2));
      }
      return this.FormatMenu(mi1);
    }

    private GMenuItem GetMenuFrom(ParamNode n, Action a, ActionHandler ah)
    {
      GMenuItem mi = n.Param == null ? new GMenuItem(n.Name) : (GMenuItem) new GParamMenu(n, ah, a);
      if (n.Nodes != null)
      {
        for (int index = 0; index < n.Nodes.Length; ++index)
          mi.Add(this.GetMenuFrom(n.Nodes[index], a, ah));
      }
      return this.FormatMenu(mi);
    }
  }
}

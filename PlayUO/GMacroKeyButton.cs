// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroKeyButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GMacroKeyButton : GSystemButton
  {
    private object m_Macro;
    private Keys m_Key;
    private GLabel m_Dots;

    public Keys Key
    {
      get
      {
        return this.m_Key;
      }
    }

    public object Macro
    {
      get
      {
        return this.m_Macro;
      }
      set
      {
        if (this.m_Macro == value)
          return;
        this.m_Macro = value;
        if (this.m_Macro == null)
        {
          this.SetBackColor(GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f));
          if (this.m_Dots != null)
            Gumps.Destroy((Gump) this.m_Dots);
          this.m_Dots = (GLabel) null;
          this.Tooltip = (ITooltip) new Tooltip(string.Format("{0}\nClick to create", (object) GMacroEditorPanel.GetKeyName(this.m_Key)), true);
        }
        else
        {
          this.SetBackColor(GumpPaint.Blend(Color.SteelBlue, SystemColors.Control, 0.5f));
          int count = 1;
          if (this.m_Macro is PlayUO.Macro)
          {
            this.Tooltip = (ITooltip) new Tooltip("Jump to the macro", true);
          }
          else
          {
            this.Tooltip = (ITooltip) new Tooltip("Jump to the macros", true);
            count = ((PlayUO.Macro[]) this.m_Macro).Length;
          }
          if (this.m_Dots == null)
          {
            this.m_Dots = new GLabel(new string('.', count), (IFont) Engine.GetUniFont(0), Hues.Load(1153), 4, 4);
            this.m_Dots.X -= this.m_Dots.Image.xMin;
            this.m_Dots.Y -= this.m_Dots.Image.yMin;
            this.m_Children.Add((Gump) this.m_Dots);
          }
          else
          {
            if (this.m_Dots.Text.Length == count)
              return;
            this.m_Dots.Text = new string('.', count);
          }
        }
      }
    }

    public override float Darkness
    {
      get
      {
        return 0.25f;
      }
    }

    public GMacroKeyButton(Keys key, string name, bool bold, int x, int y, int w, int h)
      : base(x, y, w, h, GumpPaint.Blend(Color.WhiteSmoke, SystemColors.Control, 0.5f), SystemColors.ControlText, name, bold ? (IFont) Engine.GetUniFont(1) : (IFont) Engine.GetUniFont(2))
    {
      this.m_Key = key;
      this.Tooltip = (ITooltip) new Tooltip(string.Format("{0}\nClick to create", (object) GMacroEditorPanel.GetKeyName(this.m_Key)), true);
      this.FillAlpha = 1f;
      this.m_QuickDrag = false;
      this.m_CanDrag = true;
      this.OnClick = new OnClick(this.Clicked);
    }

    private void Clicked(Gump g)
    {
      GMacroEditorForm gmacroEditorForm = this.m_Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      if (this.m_Macro == null)
      {
        Keys keys = Keys.None;
        MacroModifiers macroModifiers = MacroModifiers.All;
        if (gmacroEditorForm.Keyboard != null)
          macroModifiers = gmacroEditorForm.Keyboard.Mods;
        if (macroModifiers == MacroModifiers.All)
        {
          keys = Control.ModifierKeys;
        }
        else
        {
          if ((macroModifiers & MacroModifiers.Alt) != MacroModifiers.None)
            keys |= Keys.Alt;
          if ((macroModifiers & MacroModifiers.Shift) != MacroModifiers.None)
            keys |= Keys.Shift;
          if ((macroModifiers & MacroModifiers.Ctrl) != MacroModifiers.None)
            keys |= Keys.Control;
        }
        MacroSet current = Macros.Current;
        PlayUO.Macro macro = new PlayUO.Macro(new MacroData() { Key = this.m_Key, Mods = keys });
        current.Macros.Add(macro);
        gmacroEditorForm.Current = macro;
        gmacroEditorForm.UpdateKeyboard();
      }
      else if (this.m_Macro is PlayUO.Macro)
      {
        gmacroEditorForm.Current = (PlayUO.Macro) this.m_Macro;
      }
      else
      {
        if (!(this.m_Macro is PlayUO.Macro[]))
          return;
        PlayUO.Macro[] array = (PlayUO.Macro[]) this.m_Macro;
        int num = Array.IndexOf<PlayUO.Macro>(array, gmacroEditorForm.Current);
        gmacroEditorForm.Current = array[(num + 1) % array.Length];
      }
    }

    protected internal override void OnDragStart()
    {
      if (this.m_Parent.Parent == null)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Parent.Parent.PointToScreen(new Point(0, 0));
      this.m_Parent.Parent.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Parent.Parent.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Parent.Parent.m_IsDragging = true;
      Gumps.Drag = this.m_Parent.Parent;
    }
  }
}

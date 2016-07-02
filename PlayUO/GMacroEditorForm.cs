// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroEditorForm
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Microsoft.Win32;
using System.Drawing;

namespace PlayUO
{
  public class GMacroEditorForm : GWindowsForm
  {
    private GSystemButton m_KeyboardFlipper;
    private GMacroKeyboard m_Keyboard;
    private Macro m_Current;
    private GMacroEditorPanel m_Panel;
    private static GMacroEditorForm m_Instance;
    private GLabel m_NoSel;
    private GAlphaBackground m_Sunken;

    public static bool IsOpen
    {
      get
      {
        return GMacroEditorForm.m_Instance != null;
      }
    }

    public GMacroKeyboard Keyboard
    {
      get
      {
        return this.m_Keyboard;
      }
    }

    public bool ShowKeyboard
    {
      get
      {
        return this.m_Keyboard != null;
      }
      set
      {
        if (value)
        {
          if (this.m_Keyboard != null)
            return;
          this.m_KeyboardFlipper.Text = "Hide Keyboard";
          this.m_Keyboard = new GMacroKeyboard();
          this.m_Children.Insert(0, (Gump) this.m_Keyboard);
          this.m_Keyboard.Center();
          this.m_Keyboard.Y = this.Height - 1;
        }
        else
        {
          if (this.m_Keyboard == null)
            return;
          this.m_KeyboardFlipper.Text = "Show Keyboard";
          Gumps.Destroy((Gump) this.m_Keyboard);
          this.m_Keyboard = (GMacroKeyboard) null;
        }
      }
    }

    public Macro Current
    {
      get
      {
        return this.m_Current;
      }
      set
      {
        bool flag = this.m_Current != null && this.m_Current != value && this.m_Current.Actions.Count == 0;
        if (flag && Macros.Current.Macros.Contains(this.m_Current))
          Macros.Current.Macros.Remove(this.m_Current);
        if (this.m_Panel != null)
          Gumps.Destroy((Gump) this.m_Panel);
        this.m_Panel = (GMacroEditorPanel) null;
        this.m_Current = value;
        if (this.m_Current != null)
        {
          this.m_Panel = new GMacroEditorPanel(this.m_Current);
          this.m_Panel.X = 1;
          this.m_Panel.Y = 2;
          this.Client.Children.Add((Gump) this.m_Panel);
        }
        if (this.m_Current == null && Macros.List.Count > 0)
          this.Current = Macros.List[0];
        else if (this.m_Current != null && this.m_NoSel != null)
        {
          Gumps.Destroy((Gump) this.m_NoSel);
          this.m_NoSel = (GLabel) null;
        }
        else if (this.m_Current == null && this.m_NoSel == null)
        {
          this.m_NoSel = new GLabel("No macro is currently selected", (IFont) Engine.GetUniFont(1), Hues.Load(1153), 16, 18);
          this.Client.Children.Add((Gump) this.m_NoSel);
        }
        if (!flag)
          return;
        this.UpdateKeyboard();
      }
    }

    public GMacroEditorForm()
      : base(0, 0, 269, 283)
    {
      Gumps.Focus = (Gump) this;
      this.m_NonRestrictivePicking = true;
      this.Client.m_NonRestrictivePicking = true;
      this.Text = "Macro Editor";
      GAlphaBackground galphaBackground = this.m_Sunken = new GAlphaBackground(1, 2, 259, 230);
      galphaBackground.ShouldHitTest = false;
      galphaBackground.FillAlpha = 1f;
      galphaBackground.FillColor = GumpColors.AppWorkspace;
      galphaBackground.DrawBorder = false;
      this.Client.Children.Add((Gump) galphaBackground);
      this.m_KeyboardFlipper = new GSystemButton(71, 236, 120, 20, SystemColors.Control, SystemColors.ControlText, "Show Keyboard", (IFont) Engine.GetUniFont(2));
      this.m_KeyboardFlipper.OnClick = new OnClick(this.KeyboardFlipper_OnClick);
      this.Client.Children.Add((Gump) this.m_KeyboardFlipper);
      GSystemButton gsystemButton1 = new GSystemButton(240, 236, 20, 20, SystemColors.Control, SystemColors.ControlText, "→", (IFont) Engine.GetUniFont(2));
      gsystemButton1.Tooltip = (ITooltip) new Tooltip("Advance to the next macro", true);
      gsystemButton1.OnClick = new OnClick(this.Next_OnClick);
      this.Client.Children.Add((Gump) gsystemButton1);
      GSystemButton gsystemButton2 = new GSystemButton(1, 236, 20, 20, SystemColors.Control, SystemColors.ControlText, "←", (IFont) Engine.GetUniFont(2));
      gsystemButton2.Tooltip = (ITooltip) new Tooltip("Go back to the previous macro", true);
      gsystemButton2.OnClick = new OnClick(this.Prev_OnClick);
      this.Client.Children.Add((Gump) gsystemButton2);
      this.Center();
      this.Y -= 92;
      if (Macros.List.Count > 0)
      {
        this.Current = Macros.List[0];
      }
      else
      {
        this.m_NoSel = new GLabel("No macro is currently selected", (IFont) Engine.GetUniFont(1), Hues.Load(1153), 16, 18);
        this.Client.Children.Add((Gump) this.m_NoSel);
      }
    }

    public static void Open()
    {
      if (GMacroEditorForm.m_Instance != null)
        return;
      GMacroEditorForm.m_Instance = new GMacroEditorForm();
      Gumps.Desktop.Children.Add((Gump) GMacroEditorForm.m_Instance);
      Gumps.Focus = (Gump) GMacroEditorForm.m_Instance;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      Renderer.SetTexture((Texture) null);
      GumpPaint.DrawSunken3D(X + this.Client.X + this.m_Sunken.X - 1, Y + this.Client.Y + this.m_Sunken.Y - 1, this.m_Sunken.Width + 2, this.m_Sunken.Height + 2);
    }

    private static System.Drawing.Color ReadRegistryColor(string name)
    {
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Colors", false))
        {
          string[] strArray = (registryKey.GetValue(name) as string).Split(' ');
          return System.Drawing.Color.FromArgb(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]));
        }
      }
      catch
      {
      }
      return System.Drawing.Color.White;
    }

    public void UpdateKeyboard()
    {
      if (this.m_Keyboard == null)
        return;
      this.m_Keyboard.Update();
    }

    private void Next_OnClick(Gump g)
    {
      if (Macros.List.Count == 0)
        return;
      int index = (Macros.List.IndexOf(this.m_Current) + 1) % Macros.List.Count;
      if (index < 0 || index >= Macros.List.Count)
        return;
      this.Current = Macros.List[index];
    }

    private void Prev_OnClick(Gump g)
    {
      if (Macros.List.Count == 0)
        return;
      int index = (Macros.List.IndexOf(this.m_Current) - 1) % Macros.List.Count;
      if (index < 0)
        index += Macros.List.Count;
      if (index < 0 || index >= Macros.List.Count)
        return;
      this.Current = Macros.List[index];
    }

    private void KeyboardFlipper_OnClick(Gump g)
    {
      this.ShowKeyboard = !this.ShowKeyboard;
    }

    protected internal override void OnDispose()
    {
      GMacroEditorForm.m_Instance = (GMacroEditorForm) null;
      Macros.Save();
      base.OnDispose();
    }
  }
}

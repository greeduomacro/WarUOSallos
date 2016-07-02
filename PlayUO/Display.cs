// Decompiled with JetBrains decompiler
// Type: PlayUO.Display
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Prompts;
using PlayUO.Targeting;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class Display : Form
  {
    private Container components;

    public Display()
    {
      this.InitializeComponent();
      this.KeyPress += new KeyPressEventHandler(this.Display_KeyPress);
      this.MouseDown += new MouseEventHandler(Engine.MouseDown);
      this.MouseMove += new MouseEventHandler(Engine.MouseMove);
      this.MouseUp += new MouseEventHandler(Engine.MouseUp);
      this.MouseWheel += new MouseEventHandler(Engine.MouseWheel);
      this.Cursor.Dispose();
    }

    protected override void OnClosed(EventArgs e)
    {
      Engine.exiting = true;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      e.Cancel = true;
      Engine.exiting = true;
    }

    protected override void OnSystemColorsChanged(EventArgs e)
    {
      base.OnSystemColorsChanged(e);
      GumpColors.Invalidate();
      GumpHues.Invalidate();
      GumpPaint.Invalidate();
    }

    protected override void OnLocationChanged(EventArgs e)
    {
      if (!Engine.m_EventOk || Engine.m_Fullscreen || this.WindowState == FormWindowState.Minimized)
        return;
      base.OnLocationChanged(e);
      Preferences.Current.Layout.Update();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (!Engine.m_EventOk || Engine.m_Fullscreen || this.WindowState == FormWindowState.Minimized)
        return;
      base.OnSizeChanged(e);
      Preferences.Current.Layout.Update();
    }

    protected override void OnResize(EventArgs ea)
    {
      if (!Engine.m_EventOk || Engine.m_Fullscreen || this.WindowState == FormWindowState.Minimized)
        return;
      base.OnResize(ea);
      Preferences.Current.Layout.Update();
      GC.Collect();
    }

    protected override void OnClick(EventArgs e)
    {
      if (!Engine.m_EventOk)
        return;
      Engine.ClickMessage((object) this, e);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      if (!Engine.m_EventOk)
        return;
      Engine.DoubleClick((object) this, e);
    }

    protected override bool ProcessDialogKey(Keys key)
    {
      if (!Engine.m_EventOk || Gumps.Focus is GMacroKeyEntry)
        return false;
      KeyEventArgs e = new KeyEventArgs(key);
      Engine.KeyDown((object) this, e);
      return e.Handled;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!Engine.m_EventOk || e.KeyCode == Keys.ShiftKey || (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu))
        return;
      GMacroKeyEntry gmacroKeyEntry = Gumps.Focus as GMacroKeyEntry;
      if (gmacroKeyEntry == null)
        return;
      gmacroKeyEntry.Start();
    }

    protected override void OnKeyUp(KeyEventArgs key)
    {
      if (!Engine.m_EventOk)
        return;
      Engine.KeyUp(key);
      GMacroKeyEntry gmacroKeyEntry = Gumps.Focus as GMacroKeyEntry;
      if (gmacroKeyEntry == null)
        return;
      gmacroKeyEntry.Finish(key.KeyCode, key.Modifiers);
    }

    public void Display_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!Engine.m_EventOk)
        return;
      if (Gumps.KeyDown(e.KeyChar))
      {
        e.Handled = true;
      }
      else
      {
        e.Handled = true;
        if ((int) e.KeyChar == 27)
        {
          if (TargetManager.IsActive)
            TargetManager.Active.Cancel();
          else if (Engine.Prompt != null)
          {
            Engine.Prompt.OnCancel(PromptCancelType.UserCancel);
            Engine.Prompt = (IPrompt) null;
            return;
          }
        }
        if (Engine.m_Locked)
          return;
        if ((int) e.KeyChar == 8)
        {
          if (Engine.m_Text.Length <= 0)
            return;
          Engine.m_Text = Engine.m_Text.Substring(0, Engine.m_Text.Length - 1);
          Renderer.SetText(Engine.m_Text);
        }
        else if ((int) e.KeyChar == 13)
        {
          Engine.commandEntered(Engine.Encode(Engine.m_Text));
          Engine.m_Text = "";
          Renderer.SetText("");
        }
        else if ((int) e.KeyChar < 32)
        {
          e.Handled = false;
          e.Handled = true;
        }
        else
        {
          string str = Engine.m_Text + (object) e.KeyChar;
          string text = Engine.Encode(str) + "_";
          Mobile player = World.Player;
          int num = player == null || !player.OpenedStatus || player.StatusBar != null ? Engine.GameWidth - 4 : Engine.GameWidth - 46;
          if (Engine.GetUniFont(3).GetStringWidth(text) >= num)
            return;
          Engine.m_Text = str;
          Renderer.SetText(str);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.AutoScaleDimensions = new SizeF(5f, 13f);
      this.BackColor = Color.Black;
      this.ClientSize = new Size(640, 480);
      this.ForeColor = SystemColors.ControlText;
      this.Name = "Display";
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Ultima Online";
    }
  }
}

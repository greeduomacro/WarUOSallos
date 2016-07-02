// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroKeyEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GMacroKeyEntry : GTextBox
  {
    private bool m_Recording;

    public override bool ShowCaret
    {
      get
      {
        return true;
      }
    }

    public GMacroKeyEntry(string text, int x, int y, int w, int h)
      : base(0, false, x, y, w, h, text, (IFont) Engine.GetUniFont(2), GumpHues.WindowText, GumpHues.WindowText, GumpHues.WindowText)
    {
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      base.OnMouseUp(X, Y, mb);
      if (mb != MouseButtons.Middle)
        return;
      this.Start();
      this.Finish((Keys) 69634, Control.ModifierKeys);
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      base.OnMouseWheel(Delta);
      if (Delta > 0)
      {
        this.Start();
        this.Finish((Keys) 69632, Control.ModifierKeys);
      }
      else
      {
        if (Delta >= 0)
          return;
        this.Start();
        this.Finish((Keys) 69633, Control.ModifierKeys);
      }
    }

    public void Finish(Keys key, Keys mods)
    {
      if (!this.m_Recording)
        return;
      this.m_Recording = false;
      this.String = GMacroEditorPanel.GetKeyName(key);
      GMacroEditorPanel gmacroEditorPanel = this.m_Parent as GMacroEditorPanel;
      if (gmacroEditorPanel == null)
        return;
      gmacroEditorPanel.Macro.Key = key;
      gmacroEditorPanel.Macro.Mods = mods;
      gmacroEditorPanel.UpdateModifiers();
      gmacroEditorPanel.NotifyParent();
    }

    public void Start()
    {
      if (this.m_Recording)
        return;
      this.m_Recording = true;
    }

    protected internal override bool OnKeyDown(char Key)
    {
      return true;
    }
  }
}

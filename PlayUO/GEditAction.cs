// Decompiled with JetBrains decompiler
// Type: PlayUO.GEditAction
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GEditAction : GWindowsForm
  {
    private GMacroEditorPanel m_Panel;
    private Macro m_Macro;
    private Action m_Action;

    public GEditAction(GMacroEditorPanel p, Macro macro, Action action)
      : base(0, 0, 103, 86)
    {
      this.m_Panel = p;
      this.m_Macro = macro;
      this.m_Action = action;
      Gumps.Modal = (Gump) this;
      Gumps.Focus = (Gump) this;
      this.Text = "Edit";
      this.m_NonRestrictivePicking = true;
      this.AddButton("↑", 6, 7, 24, 24, new OnClick(this.Up_OnClick)).Tooltip = (ITooltip) new Tooltip("Moves the instruction up", true);
      this.AddButton("↓", 6, 30, 24, 24, new OnClick(this.Down_OnClick)).Tooltip = (ITooltip) new Tooltip("Moves the instruction down", true);
      this.AddButton("Delete", 39, 7, 50, 24, new OnClick(this.Delete_OnClick)).Tooltip = (ITooltip) new Tooltip("Removes the instruction", true);
      this.Center();
    }

    private void Delete_OnClick(Gump g)
    {
      this.m_Macro.Actions.Remove(this.m_Action);
      GMacroEditorForm gmacroEditorForm = this.m_Panel.Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm != null)
        gmacroEditorForm.Current = gmacroEditorForm.Current;
      Gumps.Destroy((Gump) this);
      Gumps.Focus = (Gump) gmacroEditorForm;
    }

    private void Up_OnClick(Gump g)
    {
      int index = this.m_Macro.Actions.IndexOf(this.m_Action);
      if (index > 0)
      {
        this.m_Macro.Actions.RemoveAt(index);
        this.m_Macro.Actions.Insert(index - 1, this.m_Action);
      }
      GMacroEditorForm gmacroEditorForm = this.m_Panel.Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      gmacroEditorForm.Current = gmacroEditorForm.Current;
    }

    private void Down_OnClick(Gump g)
    {
      int index = this.m_Macro.Actions.IndexOf(this.m_Action);
      if (index > this.m_Macro.Actions.Count - 1)
      {
        this.m_Macro.Actions.RemoveAt(index);
        this.m_Macro.Actions.Insert(index + 1, this.m_Action);
      }
      GMacroEditorForm gmacroEditorForm = this.m_Panel.Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      gmacroEditorForm.Current = gmacroEditorForm.Current;
    }

    private GSystemButton AddButton(string name, int x, int y, int w, int h, OnClick onClick)
    {
      GSystemButton gsystemButton = new GSystemButton(x, y, w, h, SystemColors.Control, SystemColors.ControlText, name, (IFont) Engine.GetUniFont(2));
      gsystemButton.OnClick = onClick;
      this.Client.Children.Add((Gump) gsystemButton);
      return gsystemButton;
    }

    private GTextBox AddTextBox(string name, int index, string initialText, char pc)
    {
      int Y = 30 + index * 25;
      GLabel glabel = new GLabel(name, (IFont) Engine.GetUniFont(2), GumpHues.ControlText, 0, 0);
      glabel.X = 10 - glabel.Image.xMin;
      glabel.Y = Y + (22 - (glabel.Image.yMax - glabel.Image.yMin + 1)) / 2 - glabel.Image.yMin;
      this.m_Children.Add((Gump) glabel);
      this.m_Children.Add((Gump) new GAlphaBackground(60, Y, 200, 22)
      {
        ShouldHitTest = false,
        FillColor = GumpColors.Window,
        FillAlpha = 1f
      });
      IHue windowText = GumpHues.WindowText;
      GTextBox gtextBox = new GTextBox(0, false, 64, Y, 196, 22, initialText, (IFont) Engine.GetUniFont(2), windowText, windowText, windowText, pc);
      this.Client.Children.Add((Gump) gtextBox);
      return gtextBox;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Right)
        return;
      Gumps.Destroy((Gump) this);
    }
  }
}

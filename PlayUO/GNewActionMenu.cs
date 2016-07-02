// Decompiled with JetBrains decompiler
// Type: PlayUO.GNewActionMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GNewActionMenu : GMenuItem
  {
    private GMacroEditorPanel m_Panel;
    private Macro m_Macro;
    private ActionHandler m_Action;

    public GNewActionMenu(GMacroEditorPanel panel, Macro macro, ActionHandler action)
      : base(action.Name)
    {
      this.m_Panel = panel;
      this.m_Macro = macro;
      this.m_Action = action;
      if (this.m_Action.Params != null && this.m_Action.Params.Length > 0)
        this.Tooltip = (ITooltip) new Tooltip("Choose a parameter from the menu to the right, or just click here to add the instruction with a default parameter.", false, 200);
      else
        this.Tooltip = (ITooltip) new Tooltip("Click here to add this instruction.", false, 200);
      this.Tooltip.Delay = 2f;
    }

    public override void OnClick()
    {
      this.m_Macro.Actions.Add(new Action(this.m_Action.Action, this.FindFirst(this.m_Action.Params)));
      GMacroEditorForm gmacroEditorForm = this.m_Panel.Parent.Parent as GMacroEditorForm;
      if (gmacroEditorForm == null)
        return;
      gmacroEditorForm.Current = gmacroEditorForm.Current;
    }

    private string FindFirst(ParamNode[] nodes)
    {
      string str = "";
      for (int index = 0; nodes != null && str == null && index < nodes.Length; ++index)
        str = this.FindFirst(nodes[index]);
      return str;
    }

    private string FindFirst(ParamNode node)
    {
      if (node.Param != null)
        return node.Param;
      return this.FindFirst(node.Nodes);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GParamMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GParamMenu : GMenuItem
  {
    private ParamNode m_Param;
    private Action m_Action;
    private ActionHandler m_Handler;

    public Action Action
    {
      get
      {
        return this.m_Action;
      }
    }

    public ParamNode Param
    {
      get
      {
        return this.m_Param;
      }
    }

    public ActionHandler Handler
    {
      get
      {
        return this.m_Handler;
      }
    }

    public GParamMenu(ParamNode param, ActionHandler handler, Action action)
      : base(param.Name)
    {
      this.m_Param = param;
      this.m_Handler = handler;
      this.m_Action = action;
      if (this.m_Action == null)
        this.Tooltip = (ITooltip) new Tooltip(string.Format("Click here to add the instruction:\n{0} {1}", (object) handler.Name, (object) param.Name), true);
      else
        this.Tooltip = (ITooltip) new Tooltip("Click here to change the parameter", true);
      this.Tooltip.Delay = 3f;
    }

    public override void OnClick()
    {
      string str = this.m_Param.Param;
      if (str == null)
        return;
      Action action = this.m_Action;
      if (action == null)
      {
        Gump gump = this.m_Parent;
        while (gump != null && !(gump is GMacroEditorPanel))
          gump = gump.Parent;
        if (!(gump is GMacroEditorPanel))
          return;
        Macro macro = ((GMacroEditorPanel) gump).Macro;
        macro.AddAction(new Action(this.m_Handler.Action, str));
        ((GMacroEditorForm) gump.Parent.Parent).Current = macro;
      }
      else
      {
        action.Param = str;
        GMenuItem gmenuItem = (GMenuItem) this;
        while (gmenuItem.Parent is GMenuItem)
          gmenuItem = (GMenuItem) gmenuItem.Parent;
        gmenuItem.Text = this.m_Param.Name;
      }
    }
  }
}

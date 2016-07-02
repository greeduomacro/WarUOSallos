// Decompiled with JetBrains decompiler
// Type: PlayUO.GActionMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GActionMenu : GMenuItem
  {
    private GMacroEditorPanel m_Panel;
    private Macro m_Macro;
    private Action m_Action;

    public GActionMenu(GMacroEditorPanel panel, Macro macro, Action action)
      : base(action.Handler.Name)
    {
      this.m_Panel = panel;
      this.m_Macro = macro;
      this.m_Action = action;
      this.Tooltip = (ITooltip) new Tooltip("Click here to edit this action", true);
    }

    public override void OnClick()
    {
      Gumps.Desktop.Children.Add((Gump) new GEditAction(this.m_Panel, this.m_Macro, this.m_Action));
    }
  }
}

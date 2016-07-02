// Decompiled with JetBrains decompiler
// Type: PlayUO.GMacroParamEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GMacroParamEntry : GTextBox
  {
    private Action m_Action;

    public GMacroParamEntry(Action action, string text, int x, int y, int w, int h)
      : base(0, false, x, y, w, h, text, (IFont) Engine.GetUniFont(2), GumpHues.WindowText, GumpHues.WindowText, GumpHues.WindowText)
    {
      this.m_Action = action;
      this.OnTextChange = new OnTextChange(this.UpdateParam);
    }

    private void UpdateParam(string str, Gump g)
    {
      this.m_Action.Param = str;
    }
  }
}

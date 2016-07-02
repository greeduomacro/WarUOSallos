// Decompiled with JetBrains decompiler
// Type: PlayUO.GFlatButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GFlatButton : GAlphaBackground
  {
    protected OnClick m_OnClick;

    public GFlatButton(int X, int Y, int Width, int Height, string Text, OnClick OnClick)
      : base(X, Y, Width, Height)
    {
      this.m_OnClick = OnClick;
      this.m_CanDrag = false;
      GTextButton gtextButton = new GTextButton(Text, (IFont) Engine.GetUniFont(0), Hues.Default, Hues.Load(53), 0, 0, new OnClick(this.Route_OnClick));
      this.m_Children.Add((Gump) gtextButton);
      gtextButton.Center();
      this.m_Children.Add((Gump) new GHotspot(0, 0, Width, Height, (Gump) gtextButton));
    }

    private void Route_OnClick(Gump g)
    {
      if (this.m_OnClick == null)
        return;
      this.m_OnClick((Gump) this);
    }

    public void Click()
    {
      if (this.m_OnClick == null)
        return;
      this.m_OnClick((Gump) this);
    }
  }
}

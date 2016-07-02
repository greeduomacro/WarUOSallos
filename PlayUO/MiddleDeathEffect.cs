// Decompiled with JetBrains decompiler
// Type: PlayUO.MiddleDeathEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class MiddleDeathEffect : Fade
  {
    private GLabel m_Label;

    public MiddleDeathEffect()
      : base(0, 1f, 1f, 1f)
    {
      this.m_Label = new GLabel("You are dead.", (IFont) Engine.GetFont(3), Hues.Default, 0, 0);
      this.m_Label.Center();
      Gumps.Desktop.Children.Add((Gump) this.m_Label);
    }

    protected internal override void OnFadeComplete()
    {
      Cursor.Visible = true;
      Engine.Effects.Add((Fade) new EndDeathEffect(this.m_Label));
    }
  }
}

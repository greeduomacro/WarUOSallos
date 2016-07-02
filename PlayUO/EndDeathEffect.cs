// Decompiled with JetBrains decompiler
// Type: PlayUO.EndDeathEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class EndDeathEffect : Fade
  {
    private GLabel m_Label;

    public EndDeathEffect(GLabel lbl)
      : base(0, 1f, 0.0f, 1f)
    {
      this.m_Label = lbl;
    }

    protected internal override void OnFadeComplete()
    {
      Gumps.Destroy((Gump) this.m_Label);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.DeathEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class DeathEffect : Fade
  {
    public DeathEffect()
      : base(0, 1f, 1f, 2f)
    {
      Cursor.Visible = false;
    }

    protected internal override void OnFadeComplete()
    {
      Engine.Effects.Add((Fade) new MiddleDeathEffect());
    }
  }
}

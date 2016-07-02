// Decompiled with JetBrains decompiler
// Type: PlayUO.ResurrectEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ResurrectEffect : Fade
  {
    public ResurrectEffect()
      : base(16777215, 0.0f, 1f, 0.5f)
    {
      Renderer.m_DeathOverride = true;
    }

    protected internal override void OnFadeComplete()
    {
      Mobile player = World.Player;
      if (player != null)
        player.Animation = (Animation) null;
      Renderer.m_DeathOverride = false;
      Engine.Effects.Add(new Fade(16777215, 1f, 0.0f, 1f));
    }
  }
}

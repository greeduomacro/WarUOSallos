// Decompiled with JetBrains decompiler
// Type: PlayUO.RenderEffects
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public static class RenderEffects
  {
    public static readonly RenderEffect Default = new RenderEffect(1f, DrawBlendType.Normal, (RenderEffectGlow) null);
    public static readonly RenderEffect Additive = new RenderEffect(1f, DrawBlendType.Additive, (RenderEffectGlow) null);
    public static readonly RenderEffect AdditiveGlow = new RenderEffect(0.5f, DrawBlendType.Additive, new RenderEffectGlow(1f, new int?()));
    public static readonly RenderEffect HalfAdditive = new RenderEffect(0.5f, DrawBlendType.Additive, (RenderEffectGlow) null);
  }
}

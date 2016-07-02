// Decompiled with JetBrains decompiler
// Type: PlayUO.RenderEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class RenderEffect
  {
    private float _alpha;
    private DrawBlendType _blendType;
    private RenderEffectGlow _glow;

    public float Alpha
    {
      get
      {
        return this._alpha;
      }
    }

    public DrawBlendType BlendType
    {
      get
      {
        return this._blendType;
      }
    }

    public RenderEffectGlow Glow
    {
      get
      {
        return this._glow;
      }
    }

    public RenderEffect(float alpha, DrawBlendType blendType, RenderEffectGlow glow)
    {
      this._alpha = alpha;
      this._blendType = blendType;
      this._glow = glow;
    }
  }
}

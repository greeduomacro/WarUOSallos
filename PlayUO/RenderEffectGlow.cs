// Decompiled with JetBrains decompiler
// Type: PlayUO.RenderEffectGlow
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class RenderEffectGlow
  {
    private float _alpha;
    private int? _color;

    public float Alpha
    {
      get
      {
        return this._alpha;
      }
    }

    public int? Color
    {
      get
      {
        return this._color;
      }
    }

    public RenderEffectGlow(float alpha, int? color)
    {
      this._alpha = alpha;
      this._color = color;
    }
  }
}

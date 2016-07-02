// Decompiled with JetBrains decompiler
// Type: PlayUO.Interpolator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public static class Interpolator
  {
    public static float Linear(float y1, float y2, float mu)
    {
      return (float) ((double) y1 * (1.0 - (double) mu) + (double) y2 * (double) mu);
    }

    public static float Cosine(float y1, float y2, float mu)
    {
      if (float.IsNaN(mu))
        mu = 0.0f;
      float num = (float) ((1.0 - Math.Cos((double) mu * Math.PI)) / 2.0);
      return (float) ((double) y1 * (1.0 - (double) num) + (double) y2 * (double) num);
    }

    public static float Bezier(float f0, float f1, float f2, float f3, float mu)
    {
      float num1 = f1;
      float num2 = f2;
      float num3 = f1 + (float) (((double) f1 - (double) f0) / 1.0);
      float num4 = f2 + (float) (((double) f2 - (double) f3) / 1.0);
      float num5 = (float) (3.0 * ((double) num3 - (double) num1));
      float num6 = (float) (3.0 * ((double) num4 - (double) num3)) - num5;
      float num7 = num2 - num1 - num5 - num6;
      float num8 = mu * mu;
      float num9 = mu * num8;
      return (float) ((double) num7 * (double) num9 + (double) num6 * (double) num8 + (double) num5 * (double) mu) + num1;
    }

    public static float Cubic(float y0, float y1, float y2, float y3, float mu)
    {
      float num1 = mu * mu;
      float num2 = y3 - y2 - y0 + y1;
      float num3 = y0 - y1 - num2;
      float num4 = y2 - y0;
      float num5 = y1;
      return (float) ((double) num2 * (double) mu * (double) num1 + (double) num3 * (double) num1 + (double) num4 * (double) mu) + num5;
    }
  }
}

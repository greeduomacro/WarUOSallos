// Decompiled with JetBrains decompiler
// Type: PlayUO.OptionRangeAttribute
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class OptionRangeAttribute : Attribute
  {
    private int m_Min;
    private int m_Max;

    public int Min
    {
      get
      {
        return this.m_Min;
      }
    }

    public int Max
    {
      get
      {
        return this.m_Max;
      }
    }

    public OptionRangeAttribute(int min, int max)
    {
      this.m_Min = min;
      this.m_Max = max;
    }
  }
}

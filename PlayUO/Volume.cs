// Decompiled with JetBrains decompiler
// Type: PlayUO.Volume
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class Volume
  {
    public static readonly double MaxDB = 3.25;
    public static readonly double MaxIV = Math.Pow(10.0, Volume.MaxDB / 20.0);
    public static readonly double MaxXP = Math.Log(Volume.MaxIV);
    public const int Minimum = 0;
    public const int Maximum = 10000;
    public const int Range = 10000;
    private int m_Scale;
    private bool m_Mute;
    private Callback m_OnChange;

    public Callback OnChange
    {
      get
      {
        return this.m_OnChange;
      }
      set
      {
        this.m_OnChange = value;
      }
    }

    public int Scale
    {
      get
      {
        return this.m_Scale;
      }
      set
      {
        value = Volume.ApplyBounds(value);
        if (this.m_Scale == value)
          return;
        this.m_Scale = value;
        if (this.m_OnChange == null)
          return;
        this.m_OnChange();
      }
    }

    public bool Mute
    {
      get
      {
        return this.m_Mute;
      }
      set
      {
        if (this.m_Mute == value)
          return;
        this.m_Mute = value;
        if (this.m_OnChange == null)
          return;
        this.m_OnChange();
      }
    }

    public int Value
    {
      get
      {
        return Volume.ApplyBounds(Volume.FromScale(this.m_Scale));
      }
    }

    public bool IsMuted
    {
      get
      {
        if (!this.m_Mute)
          return this.m_Scale <= 0;
        return true;
      }
    }

    public Volume()
    {
      this.m_Mute = false;
      this.m_Scale = 10000;
    }

    public static int ApplyBounds(int value)
    {
      return Math.Min(Math.Max(value, 0), 10000);
    }

    public static int FromScale(int scale)
    {
      double num = (double) Volume.ApplyBounds(scale) / 10000.0;
      if (num == 0.0)
        return 0;
      return (int) Math.Round(Math.Exp(Volume.MaxXP * num) / Volume.MaxIV * 10000.0);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.MobileFlags
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class MobileFlags
  {
    private int m_Value;
    private Mobile m_Target;

    public int Value
    {
      get
      {
        return this.m_Value;
      }
      set
      {
        this.m_Value = value;
        this.m_Target.OnFlagsChanged();
      }
    }

    public bool this[MobileFlag flag]
    {
      get
      {
        return ((MobileFlag) this.m_Value & flag) != MobileFlag.None;
      }
      set
      {
        if (value)
          this.m_Value |= (int) flag;
        else
          this.m_Value &= (int) ~flag;
        this.m_Target.OnFlagsChanged();
      }
    }

    public MobileFlags(Mobile who)
    {
      this.m_Target = who;
    }

    public MobileFlags Clone()
    {
      return new MobileFlags(this.m_Target) { m_Value = this.m_Value };
    }

    public override string ToString()
    {
      if ((this.m_Value & -224) != 0)
        return string.Format("0x{0:X2}", (object) this.m_Value);
      return ((MobileFlag) this.m_Value).ToString();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.ItemFlags
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ItemFlags
  {
    private int m_Value;

    public int Value
    {
      get
      {
        return this.m_Value;
      }
      set
      {
        this.m_Value = value;
        if ((this.m_Value & -161) == 0)
          return;
        string Message = string.Format("Unknown item flags: 0x{0:X2}", (object) this.m_Value);
        Debug.Trace(Message);
        Engine.AddTextMessage(Message);
      }
    }

    public bool this[ItemFlag flag]
    {
      get
      {
        return ((ItemFlag) this.m_Value & flag) != (ItemFlag) 0;
      }
      set
      {
        if (value)
          this.m_Value |= (int) flag;
        else
          this.m_Value &= (int) ~flag;
      }
    }

    public override string ToString()
    {
      if ((this.m_Value & -161) != 0)
        return string.Format("Unknown flags: 0x{0:X2}", (object) this.m_Value);
      return ((ItemFlag) this.m_Value).ToString();
    }
  }
}

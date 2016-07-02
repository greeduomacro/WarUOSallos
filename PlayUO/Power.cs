// Decompiled with JetBrains decompiler
// Type: PlayUO.Power
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public struct Power
  {
    private string m_Name;
    private char m_Symbol;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public char Symbol
    {
      get
      {
        return this.m_Symbol;
      }
    }

    public Power(string Name)
    {
      this.m_Name = Name;
      if (Name.Length > 0)
        this.m_Symbol = Name[0];
      else
        this.m_Symbol = char.MinValue;
    }

    public static Power[] Parse(string Words)
    {
      string[] strArray = Words.Split(' ');
      int length = strArray.Length;
      Power[] powerArray = new Power[length];
      for (int index = 0; index < length; ++index)
        powerArray[index] = new Power(strArray[index]);
      return powerArray;
    }
  }
}

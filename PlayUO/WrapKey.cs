// Decompiled with JetBrains decompiler
// Type: PlayUO.WrapKey
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class WrapKey
  {
    public string m_ToWrap;
    public int m_MaxWidth;
    public int m_HashCode;

    public WrapKey(string toWrap, int maxWidth)
    {
      this.m_ToWrap = toWrap;
      this.m_MaxWidth = maxWidth;
      this.m_HashCode = (toWrap == null ? 0 : toWrap.GetHashCode()) ^ maxWidth;
    }

    public override int GetHashCode()
    {
      return this.m_HashCode;
    }

    public override bool Equals(object x)
    {
      WrapKey wrapKey = (WrapKey) x;
      if (this == wrapKey)
        return true;
      if (this.m_HashCode == wrapKey.m_HashCode && this.m_MaxWidth == wrapKey.m_MaxWidth)
        return this.m_ToWrap == wrapKey.m_ToWrap;
      return false;
    }
  }
}

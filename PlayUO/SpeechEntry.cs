// Decompiled with JetBrains decompiler
// Type: PlayUO.SpeechEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class SpeechEntry : IComparable
  {
    public string[] m_Keywords;
    public short m_KeywordID;

    public SpeechEntry(int idKeyword, string keyword)
    {
      this.m_KeywordID = (short) idKeyword;
      this.m_Keywords = keyword.Split('*');
    }

    public int CompareTo(object x)
    {
      if (x == null || x.GetType() != typeof (SpeechEntry))
        return -1;
      if (x == this)
        return 0;
      SpeechEntry speechEntry = (SpeechEntry) x;
      if ((int) this.m_KeywordID < (int) speechEntry.m_KeywordID)
        return -1;
      return (int) this.m_KeywordID > (int) speechEntry.m_KeywordID ? 1 : 0;
    }
  }
}

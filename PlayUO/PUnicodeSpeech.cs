// Decompiled with JetBrains decompiler
// Type: PlayUO.PUnicodeSpeech
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Text;

namespace PlayUO
{
  internal class PUnicodeSpeech : Packet
  {
    public PUnicodeSpeech(string toSay, bool getKeywords, SpeechFormat speechFormat)
      : base((byte) 173)
    {
      SpeechEntry[] keywords = Speech.GetKeywords(toSay);
      byte messageType = speechFormat.MessageType;
      if (getKeywords && keywords.Length > 0)
        messageType |= (byte) 192;
      this.m_Stream.Write(messageType);
      this.m_Stream.Write((short) speechFormat.Hue);
      this.m_Stream.Write((short) 3);
      this.m_Stream.Write(Localization.Language, 4);
      if (!getKeywords || keywords.Length <= 0)
      {
        this.m_Stream.WriteUnicode(toSay);
        this.m_Stream.Write((short) 0);
      }
      else
      {
        this.m_Stream.Write((byte) (keywords.Length >> 4));
        int num1 = keywords.Length & 15;
        bool flag = false;
        int index = 0;
        while (index < keywords.Length)
        {
          int num2 = (int) keywords[index].m_KeywordID;
          if (flag)
          {
            this.m_Stream.Write((byte) (num2 >> 4));
            num1 = num2 & 15;
          }
          else
          {
            this.m_Stream.Write((byte) (num1 << 4 | num2 >> 8 & 15));
            this.m_Stream.Write((byte) num2);
          }
          ++index;
          flag = !flag;
        }
        if (!flag)
          this.m_Stream.Write((byte) (num1 << 4));
        this.m_Stream.Write(Encoding.UTF8.GetBytes(toSay));
        this.m_Stream.Write((byte) 0);
      }
    }
  }
}

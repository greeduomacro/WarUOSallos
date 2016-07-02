// Decompiled with JetBrains decompiler
// Type: PlayUO.PPE_InvokeRunebook
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PPE_InvokeRunebook : Packet
  {
    public PPE_InvokeRunebook(Item book, RuneInfo rune, bool recall)
      : base((byte) 240)
    {
      this.m_Stream.Write((byte) 3);
      this.m_Stream.Write(book.Serial);
      this.m_Stream.Write(recall ? (byte) 0 : (byte) 1);
      this.m_Stream.Write((short) rune.Point.X);
      this.m_Stream.Write((short) rune.Point.Y);
      this.m_Stream.Write((byte) rune.Facet);
    }
  }
}

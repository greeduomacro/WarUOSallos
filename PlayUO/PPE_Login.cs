// Decompiled with JetBrains decompiler
// Type: PlayUO.PPE_Login
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Reflection;

namespace PlayUO
{
  internal class PPE_Login : Packet
  {
    public PPE_Login(ulong ticket)
      : base((byte) 240)
    {
      this.m_Stream.Write((byte) 221);
      this.m_Stream.Write((int) (ticket >> 32));
      this.m_Stream.Write((int) ticket);
      byte[] toWrite = (byte[]) null;
      try
      {
        toWrite = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
      }
      catch
      {
      }
      if (toWrite == null)
        toWrite = new byte[0];
      this.m_Stream.Write(toWrite.Length);
      this.m_Stream.Write(toWrite);
    }
  }
}

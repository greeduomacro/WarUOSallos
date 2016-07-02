// Decompiled with JetBrains decompiler
// Type: PlayUO.PUseRequest
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PUseRequest : Packet
  {
    private static IEntity m_Last;

    public static IEntity Last
    {
      get
      {
        return PUseRequest.m_Last;
      }
      set
      {
        PUseRequest.m_Last = value;
      }
    }

    public PUseRequest(IEntity e)
      : base((byte) 6, 5)
    {
      this.m_Stream.Write(e.Serial);
    }

    public static void SendLast()
    {
      if (PUseRequest.m_Last == null)
        return;
      if (PUseRequest.m_Last is Item)
        (PUseRequest.m_Last as Item).Use();
      else
        Network.Send((Packet) new PUseRequest(PUseRequest.m_Last));
    }
  }
}

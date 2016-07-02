// Decompiled with JetBrains decompiler
// Type: PlayUO.PacketHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PacketHandler
  {
    private PacketCallback m_Callback;
    private int m_PacketID;
    private int m_Length;
    private int m_Count;

    public PacketCallback Callback
    {
      get
      {
        return this.m_Callback;
      }
    }

    public int PacketID
    {
      get
      {
        return this.m_PacketID;
      }
    }

    public int Length
    {
      get
      {
        return this.m_Length;
      }
      set
      {
        this.m_Length = value;
      }
    }

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public PacketHandler(int packetID, int length, PacketCallback callback)
    {
      this.m_Callback = callback;
      this.m_PacketID = packetID;
      this.m_Length = length;
    }

    public void Handle(PacketReader pvSrc)
    {
      this.m_Callback(pvSrc);
      ++this.m_Count;
    }
  }
}

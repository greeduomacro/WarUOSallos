// Decompiled with JetBrains decompiler
// Type: PlayUO.PUpdateRange
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  internal class PUpdateRange : Packet
  {
    private static Queue m_Queue = new Queue();

    public static Queue Queue
    {
      get
      {
        return PUpdateRange.m_Queue;
      }
    }

    private PUpdateRange(int range)
      : base((byte) 200, 2)
    {
      World.Range = range;
      this.m_Stream.Write((byte) range);
    }

    public static void Dispatch(object state)
    {
      PUpdateRange.m_Queue.Enqueue(state);
      Network.Send((Packet) new PUpdateRange(18));
    }
  }
}

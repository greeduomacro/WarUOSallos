// Decompiled with JetBrains decompiler
// Type: PlayUO.MiniHealthEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class MiniHealthEntry
  {
    public int m_X;
    public int m_Y;
    public Mobile m_Mobile;
    private static Queue m_Pool;

    private MiniHealthEntry(int x, int y, Mobile m)
    {
      this.m_X = x;
      this.m_Y = y;
      this.m_Mobile = m;
    }

    public static MiniHealthEntry PoolInstance(int x, int y, Mobile m)
    {
      if (MiniHealthEntry.m_Pool == null)
        MiniHealthEntry.m_Pool = new Queue();
      if (MiniHealthEntry.m_Pool.Count <= 0)
        return new MiniHealthEntry(x, y, m);
      MiniHealthEntry miniHealthEntry = (MiniHealthEntry) MiniHealthEntry.m_Pool.Dequeue();
      miniHealthEntry.m_X = x;
      miniHealthEntry.m_Y = y;
      miniHealthEntry.m_Mobile = m;
      return miniHealthEntry;
    }

    public void Dispose()
    {
      MiniHealthEntry.m_Pool.Enqueue((object) this);
    }
  }
}

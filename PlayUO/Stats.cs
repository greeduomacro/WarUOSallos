// Decompiled with JetBrains decompiler
// Type: PlayUO.Stats
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Stats
  {
    private static int m_yOffset;

    public static int yOffset
    {
      get
      {
        return Stats.m_yOffset;
      }
    }

    public static void Reset()
    {
      Stats.m_yOffset = 4;
    }

    public static void Add(Gump g)
    {
      Stats.m_yOffset += g.Height + 2;
    }
  }
}

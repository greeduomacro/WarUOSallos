// Decompiled with JetBrains decompiler
// Type: PlayUO.GQuestArrow
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GQuestArrow : GTracker
  {
    private static bool m_Active;
    private static int m_ArrowX;
    private static int m_ArrowY;

    protected internal override void Render(int X, int Y)
    {
      if (!GQuestArrow.m_Active)
        return;
      this.Render(X, Y, GQuestArrow.m_ArrowX, GQuestArrow.m_ArrowY);
    }

    protected override string GetPluralString(string direction, int distance)
    {
      return "Target: " + distance.ToString() + " tiles " + direction;
    }

    protected override string GetSingularString(string direction)
    {
      return "Target: 1 tile " + direction;
    }

    public static void Activate(int x, int y)
    {
      GQuestArrow.m_Active = true;
      GQuestArrow.m_ArrowX = x;
      GQuestArrow.m_ArrowY = y;
    }

    public static void Stop()
    {
      GQuestArrow.m_Active = false;
    }
  }
}

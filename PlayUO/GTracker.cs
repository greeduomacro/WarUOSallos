// Decompiled with JetBrains decompiler
// Type: PlayUO.GTracker
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public abstract class GTracker : GLabel
  {
    private static string[] m_DirectionStrings = new string[8]{ "north-west", "north", "north-east", "east", "south-east", "south", "south-west", "west" };
    private static IHue[] m_Hues = new IHue[7];
    private int m_xLast;
    private int m_yLast;

    public override int Y
    {
      get
      {
        return Stats.yOffset;
      }
    }

    static GTracker()
    {
      GTracker.m_Hues[0] = Hues.Load(68);
      GTracker.m_Hues[1] = Hues.Load(63);
      GTracker.m_Hues[2] = Hues.Load(58);
      GTracker.m_Hues[3] = Hues.Load(53);
      GTracker.m_Hues[4] = Hues.Load(48);
      GTracker.m_Hues[5] = Hues.Load(43);
      GTracker.m_Hues[6] = Hues.Load(38);
    }

    public GTracker()
      : base("", Engine.DefaultFont, Engine.DefaultHue, 4, 4)
    {
      this.m_xLast = this.m_yLast = int.MinValue;
    }

    protected abstract string GetPluralString(string direction, int distance);

    protected abstract string GetSingularString(string direction);

    protected void Render(int X, int Y, int xTarget, int yTarget)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      if (this.m_xLast != xTarget || this.m_yLast != yTarget)
      {
        Direction direction1 = Engine.GetDirection(player.X, player.Y, xTarget, yTarget);
        string direction2 = GTracker.m_DirectionStrings[(int) (Direction.West & direction1)];
        int num1 = Math.Abs(xTarget - player.X);
        int num2 = Math.Abs(yTarget - player.Y);
        int distance = num1 <= num2 ? num1 + (num2 - num1) : num2 + (num1 - num2);
        int index = (distance - 2) / 2;
        if (index < 0)
          index = 0;
        else if (index > 6)
          index = 6;
        string str = distance == 1 ? this.GetSingularString(direction2) : this.GetPluralString(direction2, distance);
        this.Hue = GTracker.m_Hues[index];
        this.Text = str;
      }
      this.Render(X, Y);
      Stats.Add((Gump) this);
    }
  }
}

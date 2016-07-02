// Decompiled with JetBrains decompiler
// Type: PlayUO.GItemCounters
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GItemCounters : GEmpty
  {
    private static GItemCounter[] m_List;
    private static bool m_Valid;
    private static bool m_Active;

    public static bool Active
    {
      get
      {
        return GItemCounters.m_Active;
      }
      set
      {
        GItemCounters.m_Active = value;
      }
    }

    public GItemCounters(params ItemIDValidator[] list)
    {
      this.Y = -1;
      GItemCounters.m_List = new GItemCounter[list.Length];
      for (int index = 0; index < GItemCounters.m_List.Length; ++index)
        this.m_Children.Add((Gump) (GItemCounters.m_List[index] = new GItemCounter(list[index])));
      new Timer(new OnTick(this.Update_OnTick), 250).Start(true);
    }

    protected internal override void Render(int x, int y)
    {
      if (!GItemCounters.m_Active || !GItemCounters.m_Valid)
        return;
      base.Render(x, y);
    }

    private void Update_OnTick(Timer t)
    {
      if (Engine.m_Ingame && GItemCounters.m_Active)
      {
        Mobile player = World.Player;
        if (player != null)
        {
          Item backpack = player.Backpack;
          if (backpack != null)
          {
            GItemCounters.m_Valid = true;
            int num1 = 0;
            int num2 = 0;
            for (int index = 0; index < GItemCounters.m_List.Length; ++index)
            {
              GItemCounters.m_List[index].Update(backpack);
              GItemCounters.m_List[index].Y = num1;
              num1 += GItemCounters.m_List[index].Height - 1;
              if (GItemCounters.m_List[index].Width > num2)
                num2 = GItemCounters.m_List[index].Width;
            }
            for (int index = 0; index < GItemCounters.m_List.Length; ++index)
              GItemCounters.m_List[index].Width = num2;
            this.X = Engine.ScreenWidth - num2 + 1;
            return;
          }
        }
      }
      GItemCounters.m_Valid = false;
    }
  }
}

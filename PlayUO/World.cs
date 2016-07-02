// Decompiled with JetBrains decompiler
// Type: PlayUO.World
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Ultima.Client;

namespace PlayUO
{
  public static class World
  {
    private static readonly Viewport viewport = new Viewport();
    private static WorldAgent _agent = new WorldAgent();
    private static int m_Range = 18;
    private static Dictionary<int, Mobile> mobiles = new Dictionary<int, Mobile>();
    private static Dictionary<int, Item> items = new Dictionary<int, Item>();
    private static ArrayList m_Messages = new ArrayList();
    private static int m_Serial;
    private static bool hasIdentified;
    private static Mobile m_Player;
    private static int m_X;
    private static int m_Y;
    private static int m_Z;
    private static Mobile m_Opponent;

    public static Viewport Viewport
    {
      get
      {
        return World.viewport;
      }
    }

    public static WorldAgent Agent
    {
      get
      {
        return World._agent;
      }
    }

    public static bool HasIdentified
    {
      get
      {
        return World.hasIdentified;
      }
      set
      {
        World.hasIdentified = value;
      }
    }

    public static Mobile Opponent
    {
      get
      {
        return World.m_Opponent;
      }
      set
      {
        World.m_Opponent = value;
      }
    }

    public static int X
    {
      get
      {
        return World.m_X;
      }
      set
      {
        World.SetLocation(value, World.m_Y, World.m_Z);
      }
    }

    public static int Y
    {
      get
      {
        return World.m_Y;
      }
      set
      {
        World.SetLocation(World.m_X, value, World.m_Z);
      }
    }

    public static int Z
    {
      get
      {
        return World.m_Z;
      }
      set
      {
        World.m_Z = value;
      }
    }

    public static int Range
    {
      get
      {
        return World.m_Range;
      }
      set
      {
        World.m_Range = value;
      }
    }

    public static int Serial
    {
      get
      {
        return World.m_Serial;
      }
      set
      {
        World.m_Serial = value;
        World.m_Player = World.FindMobile(World.m_Serial);
        World.hasIdentified = false;
        Renderer.SetText(Engine.m_Text);
      }
    }

    public static Dictionary<int, Mobile> Mobiles
    {
      get
      {
        return World.mobiles;
      }
    }

    public static Dictionary<int, Item> Items
    {
      get
      {
        return World.items;
      }
    }

    public static Mobile Player
    {
      get
      {
        return World.m_Player;
      }
    }

    public static void SetLocation(int x, int y, int z)
    {
      World.m_Z = z;
      if (World.m_X == x && World.m_Y == y)
        return;
      World.m_X = x;
      World.m_Y = y;
      foreach (Mobile m in World.mobiles.Values)
      {
        if (!m.Player && !World.InUpdateRange((IPoint2D) m))
          World.Remove(m);
      }
      foreach (Item obj in World.items.Values)
      {
        if (obj.InWorld && !World.InUpdateRange((IPoint2D) obj))
        {
          if (!obj.IsMulti)
            World.Remove(obj);
          else if (!World.InUpdateRange((IPoint2D) obj, 24))
            obj.Revision = -1;
        }
      }
    }

    public static IEnumerable<Item> GetItems(IItemValidator validator)
    {
      if (validator == null)
        throw new ArgumentNullException("validator");
      return World.GetItems(new Predicate<Item>(validator.IsValid));
    }

    public static IEnumerable<Item> GetItems(Predicate<Item> validator)
    {
      if (validator == null)
        throw new ArgumentNullException("validator");
      foreach (Item obj in World.items.Values)
      {
        if (obj.Visible && obj.InWorld && (!obj.IsMulti && World.InRange((IPoint2D) obj)) && validator(obj))
          yield return obj;
      }
    }

    public static Item[] FindItems(IItemValidator validator)
    {
      return ScratchList<Item>.ToArray(World.GetItems(validator));
    }

    public static Item[] FindItems(Predicate<Item> validator)
    {
      return ScratchList<Item>.ToArray(World.GetItems(validator));
    }

    public static Item FindItem(params IItemValidator[] validators)
    {
      if (validators == null)
        throw new ArgumentNullException("validators");
      return World.FindItem((Predicate<Item>) (item =>
      {
        foreach (IItemValidator validator in validators)
        {
          if (validator.IsValid(item))
            return true;
        }
        return false;
      }));
    }

    public static Item FindItem(IItemValidator validator)
    {
      if (validator == null)
        throw new ArgumentNullException("validator");
      return World.FindItem(new Predicate<Item>(validator.IsValid));
    }

    public static Item FindItem(Predicate<Item> validator)
    {
      if (validator == null)
        throw new ArgumentNullException("validator");
      foreach (Item obj in World.items.Values)
      {
        if (obj.Visible && obj.InWorld && (!obj.IsMulti && World.InRange((IPoint2D) obj)) && validator(obj))
          return obj;
      }
      return (Item) null;
    }

    public static bool InRange(Point3D p1, Point3D p2, int range)
    {
      if (p1.X >= p2.X - range && p1.X <= p2.X + range && p1.Y >= p2.Y - range)
        return p1.Y <= p2.Y + range;
      return false;
    }

    public static bool InUpdateRange(IPoint2D p)
    {
      return World.InUpdateRange(p, World.m_Range);
    }

    public static bool InUpdateRange(IPoint2D p, int range)
    {
      Mobile mobile = p as Mobile;
      int num1;
      int num2;
      if (mobile != null)
      {
        num1 = (int) mobile.XReal;
        num2 = (int) mobile.YReal;
      }
      else
      {
        num1 = p.X;
        num2 = p.Y;
      }
      if (World.m_Player == null)
        return true;
      if (num1 >= World.m_X - range && num1 <= World.m_X + range && num2 >= World.m_Y - range)
        return num2 <= World.m_Y + range;
      return false;
    }

    public static bool InRange(IPoint2D p)
    {
      if (World.m_Player == null)
        return true;
      if (p.X >= World.m_Player.X - World.m_Range && p.X <= World.m_Player.X + World.m_Range && p.Y >= World.m_Player.Y - World.m_Range)
        return p.Y <= World.m_Player.Y + World.m_Range;
      return false;
    }

    public static void Offset(int X, int Y)
    {
      int count = World.m_Messages.Count;
      for (int index = 0; index < count; ++index)
        ((StaticMessage) World.m_Messages[index]).Offset(X, Y);
    }

    public static void AddStaticMessage(int Serial, string Message)
    {
      int count = World.m_Messages.Count;
      for (int index = 0; index < count; ++index)
      {
        if (((StaticMessage) World.m_Messages[index]).Serial == Serial)
          return;
      }
      World.m_Messages.Add((object) new StaticMessage(Engine.m_xClick - Engine.GameX, Engine.m_yClick - Engine.GameY, Serial, Message));
    }

    public static void DrawAllMessages()
    {
      int count = World.m_Messages.Count;
      if (count == 0)
        return;
      int index = 0;
      while (index < count)
      {
        StaticMessage staticMessage = (StaticMessage) World.m_Messages[index];
        if ((double) staticMessage.Alpha <= 0.0)
        {
          World.m_Messages.RemoveAt(index);
          --count;
        }
        else
        {
          if (staticMessage.Elapsed && !staticMessage.Disposing)
            staticMessage.Dispose();
          Renderer.m_TextToDraw.Add((object) staticMessage);
          ++index;
        }
      }
    }

    public static void Clear()
    {
      World.m_Serial = 0;
      World.m_Player = (Mobile) null;
      World.hasIdentified = false;
      if (World.mobiles != null)
        World.mobiles.Clear();
      if (World.items != null)
        World.items.Clear();
      if (World.m_Messages != null)
        World.m_Messages.Clear();
      Engine.Multis.Clear();
      Engine.m_Display.Text = "Ultima Online";
    }

    public static int GetAmount(params Item[] items)
    {
      int num = 0;
      for (int index = 0; index < items.Length; ++index)
        num += (int) (ushort) items[index].Amount;
      return num;
    }

    public static Item WantItem(int serial)
    {
      Item obj;
      if (!World.items.TryGetValue(serial, out obj))
      {
        obj = new Item(serial);
        World.items.Add(serial, obj);
      }
      return obj;
    }

    public static Item WantItem(int serial, ref bool wasFound)
    {
      Item obj;
      if (!World.items.TryGetValue(serial, out obj))
      {
        obj = new Item(serial);
        World.items.Add(serial, obj);
        wasFound = false;
      }
      else
        wasFound = true;
      return obj;
    }

    public static Mobile WantMobile(int serial, ref bool wasFound)
    {
      Mobile mobile;
      if (!World.mobiles.TryGetValue(serial, out mobile))
      {
        mobile = new Mobile(serial);
        World.mobiles.Add(serial, mobile);
        wasFound = false;
      }
      else
        wasFound = true;
      return mobile;
    }

    public static Mobile WantMobile(int serial)
    {
      Mobile mobile;
      if (!World.mobiles.TryGetValue(serial, out mobile))
      {
        mobile = new Mobile(serial);
        World.mobiles.Add(serial, mobile);
      }
      return mobile;
    }

    public static Item FindItem(int serial)
    {
      Item obj;
      World.items.TryGetValue(serial, out obj);
      return obj;
    }

    public static Mobile FindMobile(int serial)
    {
      Mobile mobile;
      World.mobiles.TryGetValue(serial, out mobile);
      return mobile;
    }

    public static void Remove(Item item)
    {
      if (item == null)
        return;
      item.Delete();
    }

    public static void Remove(Mobile m)
    {
      if (m == null || m.Player)
        return;
      m.Delete();
    }
  }
}

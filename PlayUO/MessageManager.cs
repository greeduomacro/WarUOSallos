// Decompiled with JetBrains decompiler
// Type: PlayUO.MessageManager
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class MessageManager
  {
    private static ArrayList m_Messages = new ArrayList();
    private static Queue m_ToRemove = new Queue();
    private static int m_yStack;

    public static int yStack
    {
      get
      {
        return MessageManager.m_yStack;
      }
      set
      {
        MessageManager.m_yStack = value;
      }
    }

    public static void Remove(IMessage m)
    {
      MessageManager.m_ToRemove.Enqueue((object) m);
      Gumps.Invalidated = true;
    }

    public static void AddMessage(IMessage m)
    {
      Gumps.Desktop.Children.Add((Gump) m);
      MessageManager.m_Messages.Insert(0, (object) m);
      Gumps.Invalidated = true;
      if (m is GDynamicMessage)
      {
        int num = 0;
        IMessageOwner owner = ((GDynamicMessage) m).Owner;
        int count = MessageManager.m_Messages.Count;
        for (int index = 0; index < count; ++index)
        {
          if (MessageManager.m_Messages[index] is GDynamicMessage && ((GDynamicMessage) MessageManager.m_Messages[index]).Owner == owner)
          {
            if (num >= 3 && !((GDynamicMessage) MessageManager.m_Messages[index]).Unremovable)
              MessageManager.Remove((IMessage) MessageManager.m_Messages[index]);
            ++num;
          }
        }
      }
      else
      {
        if (!(m is GSystemMessage))
          return;
        GSystemMessage gsystemMessage1 = (GSystemMessage) m;
        DateTime dateTime = DateTime.Now - TimeSpan.FromSeconds(1.0);
        int count = MessageManager.m_Messages.Count;
        for (int index = 1; index < count; ++index)
        {
          if (MessageManager.m_Messages[index] is GSystemMessage)
          {
            GSystemMessage gsystemMessage2 = (GSystemMessage) MessageManager.m_Messages[index];
            if (gsystemMessage2.OrigText == gsystemMessage1.Text && (index == 1 || gsystemMessage2.UpdateTime >= dateTime))
            {
              gsystemMessage1.DupeCount = gsystemMessage2.DupeCount + 1;
              MessageManager.Remove((IMessage) gsystemMessage2);
              break;
            }
          }
        }
      }
    }

    public static void ClearMessages(IMessageOwner owner)
    {
      int count = MessageManager.m_Messages.Count;
      for (int index = 0; index < count; ++index)
      {
        IMessage m = (IMessage) MessageManager.m_Messages[index];
        if (m is GDynamicMessage && ((GDynamicMessage) m).Owner == owner)
          MessageManager.Remove(m);
      }
    }

    private static void RecurseProcessItemGumps(Gump g, int x, int y, bool isItemGump)
    {
      if (isItemGump)
      {
        IItemGump itemGump = (IItemGump) g;
        Item obj = itemGump.Item;
        obj.MessageX = x + itemGump.xOffset;
        obj.MessageY = y + itemGump.yOffset;
        obj.BottomY = y + itemGump.yBottom;
        obj.MessageFrame = Renderer.m_ActFrames;
        Gump desktop = Gumps.Desktop;
        GumpList children = desktop.Children;
        Gump Child = g;
        while (Child.Parent != desktop)
          Child = Child.Parent;
        int num = children.IndexOf(Child);
        for (int index1 = 0; index1 < MessageManager.m_Messages.Count; ++index1)
        {
          if (MessageManager.m_Messages[index1] is GDynamicMessage && ((GDynamicMessage) MessageManager.m_Messages[index1]).Owner == obj)
          {
            int index2 = children.IndexOf((Gump) MessageManager.m_Messages[index1]);
            if (index2 < num && index2 >= 0)
            {
              children.RemoveAt(index2);
              num = children.IndexOf(Child);
              children.Insert(num + 1, (Gump) MessageManager.m_Messages[index1]);
            }
          }
        }
      }
      else
      {
        foreach (Gump g1 in g.Children.ToArray())
        {
          if (g1 is IItemGump)
            MessageManager.RecurseProcessItemGumps(g1, x + g1.X, y + g1.Y, true);
          else if (g1.Children.Count > 0)
            MessageManager.RecurseProcessItemGumps(g1, x + g1.X, y + g1.Y, false);
        }
      }
    }

    public static void BeginRender()
    {
      while (MessageManager.m_ToRemove.Count > 0)
      {
        object obj = MessageManager.m_ToRemove.Dequeue();
        MessageManager.m_Messages.Remove(obj);
        Gumps.Destroy((Gump) obj);
      }
      MessageManager.m_yStack = Engine.GameY + Engine.GameHeight - 22;
      MessageManager.RecurseProcessItemGumps(Gumps.Desktop, 0, 0, false);
      for (int index = 0; index < MessageManager.m_Messages.Count; ++index)
        ((IMessage) MessageManager.m_Messages[index]).OnBeginRender();
      while (MessageManager.m_ToRemove.Count > 0)
      {
        object obj = MessageManager.m_ToRemove.Dequeue();
        MessageManager.m_Messages.Remove(obj);
        Gumps.Destroy((Gump) obj);
      }
      if (!Gumps.Invalidated)
        return;
      if (Engine.m_LastMouseArgs != null)
      {
        Engine.MouseMove((object) Engine.m_Display, Engine.m_LastMouseArgs);
        Engine.MouseMoveQueue();
      }
      Gumps.Invalidated = false;
    }
  }
}

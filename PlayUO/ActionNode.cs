// Decompiled with JetBrains decompiler
// Type: PlayUO.ActionNode
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class ActionNode : IComparable
  {
    private string m_Name;
    private ArrayList m_Nodes;
    private ArrayList m_Handlers;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public ArrayList Nodes
    {
      get
      {
        return this.m_Nodes;
      }
    }

    public ArrayList Handlers
    {
      get
      {
        return this.m_Handlers;
      }
    }

    public ActionNode(string name)
    {
      this.m_Name = name;
      this.m_Nodes = new ArrayList();
      this.m_Handlers = new ArrayList();
    }

    public ActionNode GetNode(string name)
    {
      for (int index = 0; index < this.m_Nodes.Count; ++index)
      {
        ActionNode actionNode = (ActionNode) this.m_Nodes[index];
        if (actionNode.m_Name == name)
          return actionNode;
      }
      return (ActionNode) null;
    }

    public ActionHandler GetHandler(string action)
    {
      for (int index = 0; index < this.m_Handlers.Count; ++index)
      {
        ActionHandler actionHandler = (ActionHandler) this.m_Handlers[index];
        if (actionHandler.Action == action)
          return actionHandler;
      }
      return (ActionHandler) null;
    }

    public int CompareTo(object obj)
    {
      return this.m_Name.CompareTo(((ActionNode) obj).m_Name);
    }
  }
}

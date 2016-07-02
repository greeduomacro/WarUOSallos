// Decompiled with JetBrains decompiler
// Type: PlayUO.ActionHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class ActionHandler : IComparable
  {
    private string m_Action;
    private string m_Name;
    private ParamNode[] m_Params;
    private ActionCallback m_Callback;
    private static Hashtable m_Table;
    private static ArrayList m_List;
    private static ActionNode m_RootNode;

    public string Action
    {
      get
      {
        return this.m_Action;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public ParamNode[] Params
    {
      get
      {
        return this.m_Params;
      }
    }

    public ActionCallback Callback
    {
      get
      {
        return this.m_Callback;
      }
    }

    public static Hashtable Table
    {
      get
      {
        if (ActionHandler.m_Table == null)
          ActionHandler.m_Table = new Hashtable();
        return ActionHandler.m_Table;
      }
    }

    public static ArrayList List
    {
      get
      {
        if (ActionHandler.m_List == null)
          ActionHandler.m_List = new ArrayList();
        return ActionHandler.m_List;
      }
    }

    public static ActionNode Root
    {
      get
      {
        if (ActionHandler.m_RootNode == null)
          ActionHandler.m_RootNode = new ActionNode("-root-");
        return ActionHandler.m_RootNode;
      }
    }

    private ActionHandler(string action, string name, ParamNode[] parms, ActionCallback callback)
    {
      this.m_Action = action;
      this.m_Name = name;
      this.m_Params = parms;
      this.m_Callback = callback;
    }

    public static void Register(string action, ParamNode[] parms, ActionCallback callback)
    {
      if (ActionHandler.m_Table == null)
        ActionHandler.m_Table = new Hashtable();
      if (ActionHandler.m_List == null)
        ActionHandler.m_List = new ArrayList();
      if (ActionHandler.m_RootNode == null)
        ActionHandler.m_RootNode = new ActionNode("-root-");
      string[] strArray = action.Split('|');
      ActionNode actionNode1 = ActionHandler.m_RootNode;
      for (int index = 0; index < strArray.Length - 1; ++index)
      {
        ActionNode actionNode2 = actionNode1.GetNode(strArray[index]);
        if (actionNode2 == null)
        {
          actionNode1.Nodes.Add((object) (actionNode2 = new ActionNode(strArray[index])));
          actionNode1.Nodes.Sort();
        }
        actionNode1 = actionNode2;
      }
      action = strArray[strArray.Length - 1];
      int length = action.IndexOf('@');
      string name;
      if (length >= 0)
      {
        name = action.Substring(length + 1);
        action = action.Substring(0, length);
      }
      else
        name = action;
      ActionHandler actionHandler = new ActionHandler(action, name, parms, callback);
      actionNode1.Handlers.Add((object) actionHandler);
      actionNode1.Handlers.Sort();
      ActionHandler.m_Table[(object) action] = (object) actionHandler;
      ActionHandler.m_List.Add((object) actionHandler);
    }

    public static ActionHandler Find(string action)
    {
      if (ActionHandler.m_Table == null)
        return (ActionHandler) null;
      return ActionHandler.m_Table[(object) action] as ActionHandler;
    }

    public int CompareTo(object obj)
    {
      return this.m_Name.CompareTo(((ActionHandler) obj).m_Name);
    }
  }
}

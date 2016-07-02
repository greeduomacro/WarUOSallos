// Decompiled with JetBrains decompiler
// Type: PlayUO.ActionContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public abstract class ActionContext : IComparable
  {
    private static List<ActionContext> _queue = new List<ActionContext>();
    private static Queue<ActionContext> _pending = new Queue<ActionContext>();
    private static Stack<ActionContext> _active = new Stack<ActionContext>();
    private bool _wasDelayed;

    public static List<ActionContext> Queued
    {
      get
      {
        return ActionContext._queue;
      }
    }

    public static Queue<ActionContext> Pending
    {
      get
      {
        return ActionContext._pending;
      }
    }

    public bool IsPending
    {
      get
      {
        if (!ActionContext._pending.Contains(this))
          return ActionContext._active.Contains(this);
        return true;
      }
    }

    public static ActionContext Active
    {
      get
      {
        if (ActionContext._active.Count <= 0)
          return (ActionContext) null;
        return ActionContext._active.Peek();
      }
    }

    public bool WasDelayed
    {
      get
      {
        return this._wasDelayed;
      }
      set
      {
        this._wasDelayed = value;
      }
    }

    protected virtual bool IsReady
    {
      get
      {
        return true;
      }
    }

    protected virtual bool ShouldRepeat
    {
      get
      {
        return this._wasDelayed;
      }
    }

    public static void Clear()
    {
      ActionContext._pending.Clear();
      ActionContext._active.Clear();
      ActionContext._queue.Clear();
    }

    public static void HandleSignal(bool isBegin)
    {
      if (isBegin)
      {
        if (ActionContext._pending.Count <= 0)
          return;
        ActionContext._pending.Dequeue().Begin();
      }
      else
      {
        if (ActionContext._active.Count <= 0)
          return;
        ActionContext._active.Pop().Finish();
      }
    }

    protected virtual bool CheckDispatch()
    {
      return true;
    }

    protected virtual bool CheckQueue()
    {
      return true;
    }

    public static void InvokeQueue()
    {
      while (ActionContext._queue.Count > 0)
      {
        ActionContext actionContext = ActionContext._queue[0];
        if (ActionContext._pending.Contains(actionContext) || !actionContext.IsReady || actionContext.Dispatch())
          break;
        ActionContext._queue.RemoveAt(0);
      }
    }

    public bool Enqueue()
    {
      if (!this.CheckQueue())
        return false;
      ActionContext._queue.Add(this);
      for (int index = ActionContext._queue.Count - 1; index - 1 > 0 && this.CompareTo(ActionContext._queue[index - 1]) < 0; --index)
      {
        ActionContext._queue[index] = ActionContext._queue[index - 1];
        ActionContext._queue[index - 1] = this;
      }
      this.OnEnqueue();
      ActionContext.InvokeQueue();
      return true;
    }

    protected virtual void OnEnqueue()
    {
    }

    public bool Dispatch()
    {
      if (!this.CheckDispatch())
        return false;
      ActionContext._pending.Enqueue(this);
      Network.Send((Packet) new BeginCriticalRegion());
      this.OnDispatch();
      Network.Send((Packet) new LeaveCriticalRegion());
      Network.Flush();
      return true;
    }

    public virtual void OnDispatch()
    {
    }

    public void Begin()
    {
      this._wasDelayed = false;
      ActionContext._active.Push(this);
      this.OnBegin();
    }

    public virtual void OnBegin()
    {
    }

    public void Finish()
    {
      this.OnFinish();
      if (ActionContext._queue.Count <= 0 || ActionContext._queue[0] != this)
        return;
      if (!this.ShouldRepeat)
        ActionContext._queue.RemoveAt(0);
      ActionContext.InvokeQueue();
    }

    public virtual void OnFinish()
    {
    }

    public virtual bool OnSpeech(string text)
    {
      return true;
    }

    public virtual void OnContextBegin(object owner)
    {
    }

    public virtual bool OnContextItem(object owner, int entryID, int stringID)
    {
      return false;
    }

    public virtual bool OnContextEnd(object owner, bool selected)
    {
      return true;
    }

    int IComparable.CompareTo(object obj)
    {
      return this.CompareTo(obj as ActionContext);
    }

    protected virtual int CompareTo(ActionContext cmp)
    {
      return cmp.IsPending.CompareTo(this.IsPending);
    }
  }
}

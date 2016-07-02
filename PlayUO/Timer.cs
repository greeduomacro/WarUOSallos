// Decompiled with JetBrains decompiler
// Type: PlayUO.Timer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public class Timer
  {
    private OnTick m_OnTick;
    private int m_Delay;
    private int m_MaxExecute;
    private int m_CurExecute;
    private int m_LastExecute;
    private bool m_State;
    private Dictionary<string, object> tags;

    public int Delay
    {
      get
      {
        return this.m_Delay;
      }
      set
      {
        this.m_Delay = value;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.m_State;
      }
    }

    public Timer(OnTick OnTick, int Delay)
    {
      this.m_OnTick = OnTick;
      this.m_Delay = Delay;
      this.m_MaxExecute = -1;
      this.m_CurExecute = -1;
      this.m_LastExecute = 0;
    }

    public Timer(OnTick OnTick, int Delay, int MaxExecute)
    {
      this.m_OnTick = OnTick;
      this.m_Delay = Delay;
      this.m_MaxExecute = MaxExecute;
      this.m_CurExecute = 0;
      this.m_LastExecute = 0;
    }

    public void RemoveTag(string name)
    {
      if (this.tags == null)
        return;
      this.tags.Remove(name);
    }

    public object GetTag(string name)
    {
      object obj = (object) null;
      if (this.tags != null)
        this.tags.TryGetValue(name, out obj);
      return obj;
    }

    public void SetTag(string name, object value)
    {
      if (this.tags == null)
        this.tags = new Dictionary<string, object>();
      this.tags[name] = value;
    }

    public bool HasTag(string name)
    {
      if (this.tags != null)
        return this.tags.ContainsKey(name);
      return false;
    }

    public void Start(bool Now)
    {
      this.m_State = true;
      if (Now)
      {
        this.m_LastExecute = 0;
        this.Tick();
      }
      else
        this.m_LastExecute = Engine.Ticks;
      Engine.AddTimer(this);
    }

    public void Stop()
    {
      this.m_State = false;
    }

    public void Delete()
    {
      this.m_State = false;
    }

    public bool Tick()
    {
      if (!this.m_State)
        return false;
      int ticks = Engine.Ticks;
      if (ticks >= this.m_LastExecute + this.m_Delay)
      {
        if (this.m_MaxExecute != -1 && this.m_CurExecute++ >= this.m_MaxExecute)
        {
          this.m_State = false;
          return false;
        }
        if (this.m_OnTick != null)
          this.m_OnTick(this);
        this.m_LastExecute = ticks;
      }
      return true;
    }

    public override string ToString()
    {
      return string.Format("Delay: {0}ms MaxExecute: {1} Running: {2} Target: {3}", (object) this.m_Delay, (object) this.m_MaxExecute, (object) this.m_State, (object) this.m_OnTick.Method.Name);
    }
  }
}

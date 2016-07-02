// Decompiled with JetBrains decompiler
// Type: PlayUO.Animation
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Animation
  {
    private int m_Action;
    private int m_RepeatCount;
    private int m_Delay;
    private int m_Start;
    private bool m_Repeat;
    private bool m_Forward;
    private bool m_Running;
    private OnAnimationEnd m_AnimEnd;

    public OnAnimationEnd OnAnimationEnd
    {
      get
      {
        return this.m_AnimEnd;
      }
      set
      {
        this.m_AnimEnd = value;
      }
    }

    public int Action
    {
      get
      {
        return this.m_Action;
      }
      set
      {
        this.m_Action = value;
      }
    }

    public int RepeatCount
    {
      get
      {
        return this.m_RepeatCount;
      }
      set
      {
        this.m_RepeatCount = value;
      }
    }

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

    public int Start
    {
      get
      {
        return this.m_Start;
      }
      set
      {
        this.m_Start = value;
      }
    }

    public bool Repeat
    {
      get
      {
        return this.m_Repeat;
      }
      set
      {
        this.m_Repeat = value;
      }
    }

    public bool Forward
    {
      get
      {
        return this.m_Forward;
      }
      set
      {
        this.m_Forward = value;
      }
    }

    public bool Running
    {
      get
      {
        return this.m_Running;
      }
    }

    public void Run()
    {
      this.m_Running = true;
      this.m_Start = Renderer.m_Frames;
    }

    public void Stop()
    {
      this.m_Running = false;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.TextMessage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class TextMessage : IComparable
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    protected Texture m_Image;
    protected bool m_Disposing;
    protected TimeSync m_Sync;
    protected TimeDelay m_Delay;
    protected int m_X;
    protected int m_Y;
    protected int m_Timestamp;
    private VertexCache m_vCache;

    protected VertexCachePool VCPool
    {
      get
      {
        return TextMessage.m_vPool;
      }
    }

    public virtual int X
    {
      get
      {
        return this.m_X;
      }
      set
      {
        this.m_X = value;
      }
    }

    public virtual int Y
    {
      get
      {
        return this.m_Y;
      }
      set
      {
        this.m_Y = value;
      }
    }

    public bool Disposing
    {
      get
      {
        return this.m_Disposing;
      }
    }

    public bool Elapsed
    {
      get
      {
        if (!this.m_Disposing)
          return this.m_Delay.Elapsed;
        return true;
      }
    }

    public float Alpha
    {
      get
      {
        if (!this.m_Disposing)
          return 1f;
        return (float) (1.0 - this.m_Sync.Normalized);
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
    }

    public TextMessage(string Message)
      : this(Message, Engine.SystemDuration, Engine.DefaultFont, Engine.DefaultHue)
    {
    }

    public TextMessage(string Message, float Delay)
      : this(Message, Delay, Engine.DefaultFont, Engine.DefaultHue)
    {
    }

    public TextMessage(string Message, float Delay, IFont Font)
      : this(Message, Delay, Font, Engine.DefaultHue)
    {
    }

    public TextMessage(string Message, float Delay, IFont Font, IHue Hue)
    {
      this.m_Timestamp = Engine.Ticks;
      this.m_Image = Font.GetString(Message, Hue);
      this.m_Delay = new TimeDelay(Delay);
    }

    public int CompareTo(object a)
    {
      if (a == null)
        return -1;
      if (a == this)
        return 0;
      TextMessage textMessage = (TextMessage) a;
      if (this.m_Timestamp < textMessage.m_Timestamp)
        return -1;
      return this.m_Timestamp > textMessage.m_Timestamp ? 1 : 0;
    }

    public void Draw(int x, int y)
    {
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      this.m_vCache.Draw(this.m_Image, x, y);
    }

    public void Dispose()
    {
      this.m_Disposing = true;
      this.m_Sync = new TimeSync(1.0);
    }

    public void OnRemove()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }
  }
}

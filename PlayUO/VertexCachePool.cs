// Decompiled with JetBrains decompiler
// Type: PlayUO.VertexCachePool
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public class VertexCachePool
  {
    private Queue<VertexCache> m_Queue;

    public VertexCachePool()
    {
      this.m_Queue = new Queue<VertexCache>();
    }

    public VertexCache GetInstance()
    {
      VertexCache vertexCache;
      if (this.m_Queue.Count > 0)
      {
        vertexCache = this.m_Queue.Dequeue();
        vertexCache.Invalidate();
      }
      else
        vertexCache = new VertexCache();
      return vertexCache;
    }

    public void ReleaseInstance(VertexCache vc)
    {
      if (vc == null)
        return;
      this.m_Queue.Enqueue(vc);
    }
  }
}

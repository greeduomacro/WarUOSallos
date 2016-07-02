// Decompiled with JetBrains decompiler
// Type: PlayUO.AnimationCache
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public class AnimationCache
  {
    private Dictionary<int, Frames> m_Cache;
    private IHue m_Hue;

    public Frames this[int realID]
    {
      get
      {
        Frames frames;
        if (!this.m_Cache.TryGetValue(realID, out frames))
          this.m_Cache.Add(realID, frames = Engine.m_Animations.Create(realID, this.m_Hue));
        return frames;
      }
    }

    public AnimationCache(IHue hue)
    {
      this.m_Cache = new Dictionary<int, Frames>();
      this.m_Hue = hue;
    }

    public void Dispose()
    {
      foreach (Frames frames in this.m_Cache.Values)
      {
        foreach (Frame frame in frames.FrameList)
        {
          if (frame != null && frame.Image != null)
            frame.Image.Dispose();
        }
      }
      this.m_Cache.Clear();
      this.m_Cache = (Dictionary<int, Frames>) null;
      this.m_Hue = (IHue) null;
    }
  }
}

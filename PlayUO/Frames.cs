// Decompiled with JetBrains decompiler
// Type: PlayUO.Frames
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;

namespace PlayUO
{
  public class Frames
  {
    public int FrameCount;
    public Frame[] FrameList;
    private static Frames m_Empty;

    public bool Disposed
    {
      get
      {
        for (int index = 0; index < this.FrameList.Length; ++index)
        {
          if (this.FrameList[index].Image.m_Surface != null && !FrameList[index].Image.m_Surface.IsDisposed)
            return true;
        }
        return false;
      }
    }

    public int LastAccessTime
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.FrameList.Length; ++index)
        {
          if (this.FrameList[index].Image.m_LastAccess > num)
            num = this.FrameList[index].Image.m_LastAccess;
        }
        return num;
      }
    }

    public static Frames Empty
    {
      get
      {
        if (Frames.m_Empty == null)
        {
          Frames.m_Empty = new Frames();
          Frames.m_Empty.FrameList = new Frame[0];
        }
        return Frames.m_Empty;
      }
    }

    public void Dispose()
    {
      for (int index = 0; index < this.FrameList.Length; ++index)
      {
        if (this.FrameList[index].Image.m_Surface != null && !FrameList[index].Image.m_Surface.IsDisposed)
        {
          ((DisposeBase) this.FrameList[index].Image.m_Surface).Dispose();
          Texture.m_Textures.Remove(this.FrameList[index].Image);
        }
      }
    }

    public static Frames Clone(Frames original, ShaderData shaderData)
    {
      if (original == null || original == Frames.m_Empty)
        return original;
      Frames frames = new Frames();
      frames.FrameCount = original.FrameCount;
      frames.FrameList = new Frame[original.FrameList.Length];
      for (int index = 0; index < frames.FrameList.Length; ++index)
        frames.FrameList[index] = Frame.Clone(original.FrameList[index], shaderData);
      return frames;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.TextureFactory
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections;

namespace PlayUO
{
  public abstract class TextureFactory
  {
    private static ArrayList m_Factories = new ArrayList();
    public static Queue m_Disposing = new Queue();
    private ArrayList m_Textures = new ArrayList();

    public abstract TextureTransparency Transparency { get; }

    public TextureFactory()
    {
      TextureFactory.m_Factories.Add((object) this);
    }

    public static void StippleDispose(int timeNow)
    {
      int num = timeNow - 15000;
      for (int index = 0; index < 5000 && TextureFactory.m_Disposing.Count > 0; ++index)
      {
        object obj = TextureFactory.m_Disposing.Dequeue();
        if (obj is Frames)
        {
          Frames frames = (Frames) obj;
          if (frames.Disposed || frames.LastAccessTime <= num)
          {
            frames.Dispose();
            Engine.m_Animations.m_Frames.Remove((object) frames);
            index += frames.FrameCount;
          }
        }
        else
        {
          Texture texture = (Texture) obj;
          if (!((DisposeBase) texture.m_Surface).IsDisposed)
            ((DisposeBase) texture.m_Surface).Dispose();
          if (texture.m_Factory != null)
            texture.m_Factory.m_Textures.Remove((object) texture);
        }
      }
    }

    public void Remove(Texture tex)
    {
      this.m_Textures.Remove((object) tex);
    }

    public static void FullCleanup(int timeNow)
    {
      for (int index = 0; index < TextureFactory.m_Factories.Count; ++index)
        ((TextureFactory) TextureFactory.m_Factories[index]).Cleanup(timeNow);
    }

    public void Cleanup(int timeNow)
    {
      int num = timeNow - 15000;
      for (int index = 0; index < this.m_Textures.Count; ++index)
      {
        Texture texture = (Texture) this.m_Textures[index];
        if (texture.m_Surface == null || ((DisposeBase) texture.m_Surface).IsDisposed)
          this.m_Textures.RemoveAt(index--);
        else if (texture.m_LastAccess <= num)
          TextureFactory.m_Disposing.Enqueue((object) texture);
      }
    }

    protected unsafe Texture Construct(bool isReconstruct)
    {
      if (!this.CoreLookup())
        return Texture.Empty;
      int width;
      int height;
      this.CoreGetDimensions(out width, out height);
      Texture tex = new Texture(width, height, (Format) 25, (Pool) 1, isReconstruct, this.Transparency);
      if (tex.IsEmpty())
        return Texture.Empty;
      LockData lockData = tex.Lock(LockFlags.WriteOnly);
      this.CoreProcessImage(lockData.Width, lockData.Height, lockData.Pitch, (ushort*) lockData.pvSrc, (ushort*) ((IntPtr) lockData.pvSrc +  lockData.Width * 2), (ushort*) ((IntPtr) lockData.pvSrc +  lockData.Height * lockData.Pitch), (lockData.Pitch >> 1) - lockData.Width, lockData.Pitch >> 1);
      tex.Unlock();
      this.CoreAssignArgs(tex);
      this.m_Textures.Add((object) tex);
      return tex;
    }

    public abstract Texture Reconstruct(object[] args);

    protected abstract void CoreAssignArgs(Texture tex);

    protected abstract bool CoreLookup();

    protected abstract void CoreGetDimensions(out int width, out int height);

    protected abstract unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta);
  }
}

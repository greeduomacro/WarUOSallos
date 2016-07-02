// Decompiled with JetBrains decompiler
// Type: PlayUO.TransparentDraw
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class TransparentDraw
  {
    public Texture m_Texture;
    public int m_X;
    public int m_Y;
    public float m_fAlpha;
    public bool m_Double;
    private static Queue m_Pool;

    private TransparentDraw(Texture tex, int x, int y, float theAlpha, bool xDouble)
    {
      this.m_Texture = tex;
      this.m_X = x;
      this.m_Y = y;
      this.m_fAlpha = theAlpha;
      this.m_Double = xDouble;
    }

    public static TransparentDraw PoolInstance(Texture tex, int x, int y, float theAlpha, bool xDouble)
    {
      if (TransparentDraw.m_Pool == null)
        TransparentDraw.m_Pool = new Queue();
      if (TransparentDraw.m_Pool.Count <= 0)
        return new TransparentDraw(tex, x, y, theAlpha, xDouble);
      TransparentDraw transparentDraw = (TransparentDraw) TransparentDraw.m_Pool.Dequeue();
      transparentDraw.m_Texture = tex;
      transparentDraw.m_X = x;
      transparentDraw.m_Y = y;
      transparentDraw.m_fAlpha = theAlpha;
      transparentDraw.m_Double = xDouble;
      return transparentDraw;
    }

    public void Dispose()
    {
      TransparentDraw.m_Pool.Enqueue((object) this);
    }
  }
}

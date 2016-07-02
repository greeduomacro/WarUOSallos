// Decompiled with JetBrains decompiler
// Type: PlayUO.AnimationVertexCache
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class AnimationVertexCache
  {
    public AnimationVertexCache()
      : this((TransformedColoredTextured[]) null)
    {
    }

    public AnimationVertexCache(TransformedColoredTextured[] v)
    {
    }

    public void Draw(Texture t, int x, int y)
    {
      t.Draw(x, y, 16777215);
    }

    public void DrawGame(Texture t, int x, int y, int color)
    {
      t.DrawGame(x, y, color);
    }

    public void Invalidate()
    {
    }
  }
}

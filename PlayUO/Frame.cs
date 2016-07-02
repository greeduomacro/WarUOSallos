// Decompiled with JetBrains decompiler
// Type: PlayUO.Frame
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Frame
  {
    public Texture Image;
    public int CenterX;
    public int CenterY;
    private static Frame m_Empty;

    public static Frame Empty
    {
      get
      {
        if (Frame.m_Empty == null)
        {
          Frame.m_Empty = new Frame();
          Frame.m_Empty.Image = Texture.Empty;
        }
        return Frame.m_Empty;
      }
    }

    public static Frame Clone(Frame original, ShaderData shaderData)
    {
      if (original == null || original == Frame.m_Empty)
        return original;
      return new Frame() { Image = Texture.Clone(original.Image, shaderData), CenterX = original.CenterX, CenterY = original.CenterY };
    }
  }
}

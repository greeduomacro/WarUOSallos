// Decompiled with JetBrains decompiler
// Type: PlayUO.VertexConstructor
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class VertexConstructor
  {
    public static TransformedColoredTextured[] Create()
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = new TransformedColoredTextured[4];
      transformedColoredTexturedArray[0].Rhw = 1f;
      transformedColoredTexturedArray[1].Rhw = 1f;
      transformedColoredTexturedArray[2].Rhw = 1f;
      transformedColoredTexturedArray[3].Rhw = 1f;
      return transformedColoredTexturedArray;
    }

    public static TransformedColoredTextured[] Create(int n)
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = new TransformedColoredTextured[n];
      for (int index = 0; index < transformedColoredTexturedArray.Length; ++index)
        transformedColoredTexturedArray[index].Rhw = 1f;
      return transformedColoredTexturedArray;
    }
  }
}

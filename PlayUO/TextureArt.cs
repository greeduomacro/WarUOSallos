// Decompiled with JetBrains decompiler
// Type: PlayUO.TextureArt
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.IO;

namespace PlayUO
{
  public class TextureArt
  {
    private int[] m_Lookup;
    private Stream m_Stream;
    private TextureArt.TexMapFactory m_Factory;

    public unsafe TextureArt()
    {
      this.m_Lookup = new int[16384];
      Stream stream = Engine.FileManager.OpenMUL(Files.TexIdx);
      byte[] buffer = new byte[196608];
      UnsafeMethods.ReadFile((FileStream) stream, buffer, 0, buffer.Length);
      stream.Close();
      fixed (byte* numPtr = buffer)
      {
        int index = 0;
        do
        {
          this.m_Lookup[index] = *(int*) numPtr | ((int*) numPtr)[2] << 31;
          (int*) numPtr += 3;
        }
        while (++index < 16384);
      }
      foreach (GraphicTranslation graphicTranslation in GraphicTranslators.Textures.Table.Values)
      {
        if (graphicTranslation.FallbackId < 16384 && graphicTranslation.UpdatedId < 16384)
          this.m_Lookup[graphicTranslation.UpdatedId] = this.m_Lookup[graphicTranslation.FallbackId];
      }
      this.m_Stream = Engine.FileManager.OpenMUL(Files.TexMul);
    }

    public void Dispose()
    {
      this.m_Lookup = (int[]) null;
      this.m_Stream.Close();
      this.m_Stream = (Stream) null;
    }

    public Texture ReadFromDisk(int TextureID, IHue Hue)
    {
      if (this.m_Factory == null)
        this.m_Factory = new TextureArt.TexMapFactory(this);
      return this.m_Factory.Load(TextureID, Hue);
    }

    private class TexMapFactory : TextureFactory
    {
      private byte[] m_Buffer = new byte[32768];
      private int m_TextureID;
      private IHue m_Hue;
      private TextureArt m_Textures;

      public override TextureTransparency Transparency
      {
        get
        {
          return TextureTransparency.None;
        }
      }

      public TexMapFactory(TextureArt textures)
      {
        this.m_Textures = textures;
      }

      public Texture Load(int textureID, IHue hue)
      {
        this.m_TextureID = textureID & 16383;
        this.m_Hue = hue;
        return this.Construct(false);
      }

      public override Texture Reconstruct(object[] args)
      {
        this.m_TextureID = (int) args[0];
        this.m_Hue = (IHue) args[1];
        return this.Construct(true);
      }

      protected override void CoreAssignArgs(Texture tex)
      {
        tex.m_Factory = (TextureFactory) this;
        tex.m_FactoryArgs = new object[2]
        {
          (object) this.m_TextureID,
          (object) this.m_Hue
        };
        tex._shaderData = this.m_Hue.ShaderData;
      }

      protected override bool CoreLookup()
      {
        return this.m_Textures.m_Lookup[this.m_TextureID] != -1;
      }

      protected override void CoreGetDimensions(out int width, out int height)
      {
        if (this.m_Textures.m_Lookup[this.m_TextureID] < 0)
          width = height = 128;
        else
          width = height = 64;
      }

      protected override unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta)
      {
        int num = this.m_Textures.m_Lookup[this.m_TextureID];
        if (num == -1)
          return;
        int size = width * height * 2;
        this.m_Textures.m_Stream.Seek((long) (num & int.MaxValue), SeekOrigin.Begin);
        UnsafeMethods.ReadFile((FileStream) this.m_Textures.m_Stream, this.m_Buffer, 0, size);
        fixed (byte* numPtr = this.m_Buffer)
          this.m_Hue.CopyPixels((void*) numPtr, (void*) pLine, width * height);
      }
    }
  }
}

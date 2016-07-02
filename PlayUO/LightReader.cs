// Decompiled with JetBrains decompiler
// Type: PlayUO.LightReader
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX.Direct3D9;
using System;
using System.IO;

namespace PlayUO
{
  public sealed class LightReader
  {
    private readonly Entry3D[] index;
    private readonly Stream dataStream;

    public unsafe LightReader()
    {
      this.index = new Entry3D[100];
      fixed (Entry3D* entry3DPtr = this.index)
      {
        using (FileStream file = (FileStream) Engine.FileManager.OpenMUL(Files.LightIdx))
          UnsafeMethods.ReadFile(file, (void*) entry3DPtr, 1200);
      }
      this.dataStream = Engine.FileManager.OpenMUL(Files.LightMul);
    }

    public unsafe Texture ReadLight(int lightId)
    {
      if (lightId >= 0 && lightId < this.index.Length)
      {
        Entry3D entry3D = this.index[lightId];
        if (entry3D.m_Lookup >= 0 && entry3D.m_Length > 0)
        {
          ushort num1 = (ushort) (entry3D.m_Extra & (int) ushort.MaxValue);
          ushort num2 = (ushort) (entry3D.m_Extra >> 16 & (int) ushort.MaxValue);
          if ((int) num1 > 0 && (int) num2 > 0 && entry3D.m_Length == (int) num1 * (int) num2)
          {
            byte[] buffer = new byte[entry3D.m_Length];
            int offset = 0;
            int length = buffer.Length;
            this.dataStream.Seek((long) entry3D.m_Lookup, SeekOrigin.Begin);
            int num3;
            while (length > 0 && (num3 = this.dataStream.Read(buffer, offset, length)) > 0)
            {
              offset += num3;
              length -= num3;
            }
            if (length == 0)
            {
              Texture texture = new Texture((int) num1, (int) num2, (Format) 21, TextureTransparency.Complex);
              LockData lockData = texture.Lock(LockFlags.WriteOnly);
              try
              {
                fixed (byte* numPtr1 = buffer)
                {
                  sbyte* numPtr2 = (sbyte*) numPtr1;
                  byte* numPtr3 = (byte*) lockData.pvSrc;
                  int num4 = lockData.Pitch - (int) num1 * 4;
                  for (int index1 = 0; index1 < (int) num2; ++index1)
                  {
                    for (int index2 = 0; index2 < (int) num1; ++index2)
                    {
                      *numPtr3 = (byte) (((int) Math.Abs(*numPtr2++) * (int) byte.MaxValue + 15) / 31);
                      numPtr3[1] = *numPtr3;
                      numPtr3[2] = *numPtr3;
                      numPtr3[3] = *numPtr3;
                      numPtr3 += 4;
                    }
                    numPtr3 += num4;
                  }
                }
                return texture;
              }
              finally
              {
                texture.Unlock();
              }
            }
          }
        }
      }
      return Texture.Empty;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.IHue
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Assets;
using System;

namespace PlayUO
{
  public interface IHue : IGraphicProvider, IDisposable
  {
    Palette Palette { get; }

    ShaderData ShaderData { get; }

    ushort Pixel(ushort input);

    int Pixel32(int input);

    unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels);

    unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd);

    int HueID();
  }
}

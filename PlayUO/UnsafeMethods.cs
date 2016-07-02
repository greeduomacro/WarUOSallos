// Decompiled with JetBrains decompiler
// Type: PlayUO.UnsafeMethods
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.IO;

namespace PlayUO
{
  public static class UnsafeMethods
  {
    private static IUnsafeMethods implementation = (IUnsafeMethods) new KernelUnsafeMethods();

    public static unsafe int ReadFile(FileStream file, byte[] buffer, int offset, int size)
    {
      fixed (byte* numPtr = buffer)
        return UnsafeMethods.ReadFile(file, (void*) (numPtr + offset), size);
    }

    public static unsafe int ReadFile(FileStream file, void* buffer, int size)
    {
      return UnsafeMethods.implementation.ReadFile(file, buffer, size);
    }

    public static unsafe int WriteFile(FileStream file, void* buffer, int size)
    {
      return UnsafeMethods.implementation.WriteFile(file, buffer, size);
    }

    public static unsafe void ZeroMemory(byte* buffer, int size)
    {
      UnsafeMethods.implementation.ZeroMemory((void*) buffer, size);
    }

    public static unsafe void CopyMemory(void* target, void* source, int size)
    {
      UnsafeMethods.implementation.CopyMemory(target, source, size);
    }
  }
}

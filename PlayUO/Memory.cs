// Decompiled with JetBrains decompiler
// Type: PlayUO.Memory
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Runtime.InteropServices;

namespace PlayUO
{
  public class Memory
  {
    public static unsafe void* Alloc(int Size)
    {
      return (void*) Marshal.AllocHGlobal(Size);
    }

    public static unsafe void Free(void* Data)
    {
      Marshal.FreeHGlobal((IntPtr) Data);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.KernelUnsafeMethods
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace PlayUO
{
  [SuppressUnmanagedCodeSecurity]
  public sealed class KernelUnsafeMethods : IUnsafeMethods
  {
    public unsafe int ReadFile(FileStream file, void* buffer, int size)
    {
      int read;
      if (KernelUnsafeMethods.Kernel32.ReadFile((SafeHandle) file.SafeFileHandle, buffer, size, out read, IntPtr.Zero) == 0)
      {
        file.SafeFileHandle.SetHandleAsInvalid();
        read = -1;
      }
      return read;
    }

    public unsafe int WriteFile(FileStream file, void* buffer, int size)
    {
      int written;
      if (KernelUnsafeMethods.Kernel32.WriteFile((SafeHandle) file.SafeFileHandle, buffer, size, out written, IntPtr.Zero) == 0)
      {
        file.SafeFileHandle.SetHandleAsInvalid();
        written = -1;
      }
      return written;
    }

    public unsafe void ZeroMemory(void* buffer, int size)
    {
      KernelUnsafeMethods.Kernel32.ZeroMemory(buffer, size);
    }

    public unsafe void CopyMemory(void* target, void* source, int size)
    {
      KernelUnsafeMethods.Kernel32.CopyMemory(target, source, size);
    }

    private static class Kernel32
    {
      public const string DllName = "kernel32";

      [DllImport("kernel32", SetLastError = true)]
      public static extern unsafe int ReadFile(SafeHandle handle, void* buffer, int size, out int read, IntPtr zero);

      [DllImport("kernel32", SetLastError = true)]
      public static extern unsafe int WriteFile(SafeHandle handle, void* buffer, int size, out int written, IntPtr zero);

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      [DllImport("kernel32", SetLastError = true)]
      public static extern unsafe void ZeroMemory(void* buffer, int size);

      [DllImport("kernel32", EntryPoint = "RtlMoveMemory")]
      public static extern unsafe void CopyMemory(void* target, void* source, int size);
    }
  }
}

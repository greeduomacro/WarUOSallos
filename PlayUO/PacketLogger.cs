// Decompiled with JetBrains decompiler
// Type: PlayUO.PacketLogger
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;

namespace PlayUO
{
  internal sealed class PacketLogger : INetworkDiagnostic
  {
    private TextWriter _output;

    public PacketLogger(TextWriter output)
    {
      if (output == null)
        throw new ArgumentNullException("output");
      this._output = output;
    }

    public void Open()
    {
      this._output.WriteLine("####\tOpened on {0}\t####", (object) DateTime.Now);
      this._output.Flush();
    }

    public void Close()
    {
      this._output.WriteLine("####\tClosed on {0}\t####", (object) DateTime.Now);
      this._output.Flush();
    }

    public void PacketSent(Packet packet, byte[] buffer, int offset, int length)
    {
      this._output.WriteLine("Packet Client->Server '{0}' ( {1:N0} bytes ) @ {2} {3}", (object) packet.GetType().Name, (object) length, (object) DateTime.Now.Date.ToShortDateString(), (object) DateTime.Now.TimeOfDay);
      PacketLogger.WriteBuffer(this._output, buffer, offset, length);
      this._output.WriteLine();
      this._output.WriteLine();
    }

    public void PacketReceived(PacketHandler packetHandler, byte[] buffer, int offset, int length)
    {
      this._output.WriteLine("Packet Server->Client '{0}' ( {1:N0} bytes ) @ {2} {3}", (object) packetHandler.Callback.Method.Name, (object) length, (object) DateTime.Now.Date.ToShortDateString(), (object) DateTime.Now.TimeOfDay);
      PacketLogger.WriteBuffer(this._output, buffer, offset, length);
      this._output.WriteLine();
      this._output.WriteLine();
    }

    public static void WriteBuffer(TextWriter output, byte[] buffer, int offset, int length)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0 || offset >= buffer.Length)
        throw new ArgumentOutOfRangeException("offset", (object) offset, "Offset must be greater than or equal to zero and less than the size of the buffer.");
      if (length < 0 || length > buffer.Length)
        throw new ArgumentOutOfRangeException("length", (object) length, "Length cannot be less than zero or greater than the size of the buffer.");
      if (buffer.Length - offset < length)
        throw new ArgumentException("Offset and length do not point to a valid segment within the buffer.");
      output.WriteLine("        0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F");
      output.WriteLine("       -- -- -- -- -- -- -- --  -- -- -- -- -- -- -- --");
      int num1 = 0;
      while (num1 < length)
      {
        output.Write(num1.ToString("X4"));
        output.Write("   ");
        for (int index = 0; index < 16; ++index)
        {
          if (num1 + index < length)
          {
            output.Write(buffer[offset + num1 + index].ToString("X2"));
            if (index == 7)
              output.Write("  ");
            else
              output.Write(' ');
          }
          else if (index == 7)
            output.Write("    ");
          else
            output.Write("   ");
        }
        output.Write("  ");
        for (int index = 0; index < 16 && num1 < length; ++num1)
        {
          byte num2 = buffer[offset + num1];
          if ((int) num2 >= 32 && (int) num2 < 128)
            output.Write((char) num2);
          else
            output.Write('.');
          ++index;
        }
        output.WriteLine();
      }
      output.Flush();
    }
  }
}

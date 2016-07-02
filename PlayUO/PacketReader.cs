// Decompiled with JetBrains decompiler
// Type: PlayUO.PacketReader
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;
using System.Text;

namespace PlayUO
{
  public class PacketReader
  {
    private byte[] m_Data;
    private int m_Start;
    private int m_Index;
    private int m_Count;
    private int m_Bounds;
    private bool m_FixedSize;
    private byte m_Command;
    private string m_Name;
    private string m_ReturnName;
    private static PacketReader m_Instance;

    public string ReturnName
    {
      get
      {
        return this.m_ReturnName;
      }
      set
      {
        this.m_ReturnName = value;
      }
    }

    public bool Finished
    {
      get
      {
        return this.m_Index >= this.m_Bounds;
      }
    }

    public byte Command
    {
      get
      {
        return this.m_Command;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public bool FixedSize
    {
      get
      {
        return this.m_FixedSize;
      }
    }

    public int Position
    {
      get
      {
        return this.m_Index - this.m_Start;
      }
      set
      {
        this.m_Index = value + this.m_Start;
      }
    }

    public int Start
    {
      get
      {
        return this.m_Start;
      }
    }

    public int Length
    {
      get
      {
        return this.m_Count;
      }
    }

    public PacketReader(byte[] data, int index, int count, bool fixedSize, byte command, string name)
    {
      this.m_Data = data;
      this.m_Start = this.m_Index = index;
      this.m_Count = count;
      this.m_Bounds = this.m_Start + this.m_Count;
      this.m_FixedSize = fixedSize;
      this.m_Command = command;
      this.m_Name = name;
      this.m_ReturnName = name;
      if (!fixedSize)
        this.m_Index += 3;
      else
        ++this.m_Index;
    }

    public static PacketReader Initialize(byte[] data, int index, int count, bool fixedSize, byte command, string name)
    {
      if (PacketReader.m_Instance == null)
      {
        PacketReader.m_Instance = new PacketReader(data, index, count, fixedSize, command, name);
      }
      else
      {
        PacketReader.m_Instance.m_Data = data;
        PacketReader.m_Instance.m_Start = PacketReader.m_Instance.m_Index = index;
        PacketReader.m_Instance.m_Count = count;
        PacketReader.m_Instance.m_Bounds = PacketReader.m_Instance.m_Start + PacketReader.m_Instance.m_Count;
        PacketReader.m_Instance.m_FixedSize = fixedSize;
        PacketReader.m_Instance.m_Command = command;
        PacketReader.m_Instance.m_Name = name;
        PacketReader.m_Instance.m_ReturnName = name;
        if (!fixedSize)
          PacketReader.m_Instance.m_Index += 3;
        else
          ++PacketReader.m_Instance.m_Index;
      }
      return PacketReader.m_Instance;
    }

    internal void Trace(bool silent = false)
    {
      if (!silent)
        Engine.AddTextMessage(string.Format("Tracing packet 0x{0:X2} '{1}' of length {2} ( 0x{2:X} ). (Prior: 0x{3:X2}, 0x{4:X2}, 0x{5:X2})", (object) this.m_Command, (object) this.m_Name, (object) this.m_Count, (object) NetworkContext.prior1, (object) NetworkContext.prior2, (object) NetworkContext.prior3));
      StreamWriter streamWriter = new StreamWriter("PacketTrace.log", true);
      if (this.m_Count < 16)
        streamWriter.WriteLine("Packet Server->Client '{0}' ( {1} bytes )", (object) this.m_ReturnName, (object) this.m_Count);
      else
        streamWriter.WriteLine("Packet Server->Client '{0}' ( {1} [0x{1:X}] bytes )", (object) this.m_ReturnName, (object) this.m_Count);
      streamWriter.WriteLine();
      PacketLogger.WriteBuffer((TextWriter) streamWriter, this.m_Data, this.m_Start, this.m_Count);
      streamWriter.WriteLine();
      streamWriter.Flush();
      streamWriter.Close();
    }

    public void Seek(int offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          this.m_Index = this.m_Start + offset;
          break;
        case SeekOrigin.Current:
          this.m_Index += offset;
          break;
        case SeekOrigin.End:
          this.m_Index = this.m_Bounds + offset;
          break;
      }
    }

    public byte[] ReadBytes(int length)
    {
      byte[] numArray = new byte[length];
      Buffer.BlockCopy((Array) this.m_Data, this.m_Index, (Array) numArray, 0, length);
      this.m_Index += length;
      return numArray;
    }

    public unsafe string ReadString()
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr4 = numPtr2;
        byte* numPtr5;
        do
          ;
        while ((numPtr5 = numPtr4) < numPtr3 && (int) *numPtr4++ != 0);
        this.m_Index = (int) (numPtr5 - numPtr1 + 1L);
        return new string((sbyte*) numPtr2, 0, (int) (numPtr5 - numPtr2));
      }
    }

    public unsafe string ReadUTF8()
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr4 = numPtr2;
        byte* numPtr5;
        do
          ;
        while ((numPtr5 = numPtr4) < numPtr3 && (int) *numPtr4++ != 0);
        int index = this.m_Index;
        this.m_Index = (int) (numPtr5 - numPtr1 + 1L);
        return Encoding.UTF8.GetString(this.m_Data, index, (int) (numPtr5 - numPtr2));
      }
    }

    public unsafe string ReadString(int fixedLength)
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr2 + fixedLength;
        byte* numPtr4 = numPtr2;
        if (numPtr1 + this.m_Bounds < numPtr3)
          numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr5;
        do
          ;
        while ((numPtr5 = numPtr4) < numPtr3 && (int) *numPtr4++ != 0);
        this.m_Index += fixedLength;
        return new string((sbyte*) numPtr2, 0, (int) (numPtr5 - numPtr2));
      }
    }

    public unsafe string ReadUnicodeString()
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr4 = numPtr2;
        byte* numPtr5;
        while ((numPtr5 = numPtr4) < numPtr3)
        {
          byte* numPtr6 = numPtr4;
          IntPtr num1 = new IntPtr(1);
          byte* numPtr7 = numPtr6 + num1.ToInt64();
          int num2 = (int) *numPtr6;
          byte* numPtr8 = numPtr7;
          IntPtr num3 = new IntPtr(1);
          numPtr4 = numPtr8 + num3.ToInt64();
          int num4 = (int) *numPtr8;
          if ((num2 | num4) == 0)
            break;
        }
        this.m_Index = (int) (numPtr5 - numPtr1 + 2L);
        return new string((sbyte*) numPtr2, 0, (int) (numPtr5 - numPtr2), Encoding.BigEndianUnicode);
      }
    }

    public unsafe string ReadUnicodeString(int fixedLength)
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr2 + (fixedLength << 1);
        byte* numPtr4 = numPtr2;
        if (numPtr1 + this.m_Bounds < numPtr3)
          numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr5;
        while ((numPtr5 = numPtr4) < numPtr3)
        {
          byte* numPtr6 = numPtr4;
          IntPtr num1 = new IntPtr(1);
          byte* numPtr7 = numPtr6 + num1.ToInt64();
          int num2 = (int) *numPtr6;
          byte* numPtr8 = numPtr7;
          IntPtr num3 = new IntPtr(1);
          numPtr4 = numPtr8 + num3.ToInt64();
          int num4 = (int) *numPtr8;
          if ((num2 | num4) == 0)
            break;
        }
        this.m_Index += fixedLength << 1;
        return new string((sbyte*) numPtr2, 0, (int) (numPtr5 - numPtr2), Encoding.BigEndianUnicode);
      }
    }

    public unsafe string ReadUnicodeLEString()
    {
      fixed (byte* numPtr1 = this.m_Data)
      {
        byte* numPtr2 = numPtr1 + this.m_Index;
        byte* numPtr3 = numPtr1 + this.m_Bounds;
        byte* numPtr4 = numPtr2;
        byte* numPtr5;
        while ((numPtr5 = numPtr4) < numPtr3)
        {
          byte* numPtr6 = numPtr4;
          IntPtr num1 = new IntPtr(1);
          byte* numPtr7 = numPtr6 + num1.ToInt64();
          int num2 = (int) *numPtr6;
          byte* numPtr8 = numPtr7;
          IntPtr num3 = new IntPtr(1);
          numPtr4 = numPtr8 + num3.ToInt64();
          int num4 = (int) *numPtr8;
          if ((num2 | num4) == 0)
            break;
        }
        this.m_Index = (int) (numPtr5 - numPtr1 + 2L);
        return new string((sbyte*) numPtr2, 0, (int) (numPtr5 - numPtr2), Encoding.Unicode);
      }
    }

    public bool ReadBoolean()
    {
      return (int) this.m_Data[this.m_Index++] != 0;
    }

    public byte ReadByte()
    {
      return this.m_Data[this.m_Index++];
    }

    public sbyte ReadSByte()
    {
      return (sbyte) this.m_Data[this.m_Index++];
    }

    public short ReadInt16()
    {
      return (short) ((int) this.m_Data[this.m_Index++] << 8 | (int) this.m_Data[this.m_Index++]);
    }

    public ushort ReadUInt16()
    {
      return (ushort) ((int) this.m_Data[this.m_Index++] << 8 | (int) this.m_Data[this.m_Index++]);
    }

    public int ReadInt32()
    {
      return (int) this.m_Data[this.m_Index++] << 24 | (int) this.m_Data[this.m_Index++] << 16 | (int) this.m_Data[this.m_Index++] << 8 | (int) this.m_Data[this.m_Index++];
    }

    public uint ReadUInt32()
    {
      return (uint) ((int) this.m_Data[this.m_Index++] << 24 | (int) this.m_Data[this.m_Index++] << 16 | (int) this.m_Data[this.m_Index++] << 8) | (uint) this.m_Data[this.m_Index++];
    }
  }
}

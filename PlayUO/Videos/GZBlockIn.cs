// Decompiled with JetBrains decompiler
// Type: PlayUO.Videos.GZBlockIn
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos.Compression;
using System;
using System.IO;

namespace PlayUO.Videos
{
  public class GZBlockIn : Stream
  {
    private MemoryStream m_Uncomp;
    private BinaryReader m_In;
    private BinaryReader m_Self;
    private bool m_Compressed;
    private static byte[] m_ReadBuff;
    private static byte[] m_CompBuff;

    public Stream RawStream
    {
      get
      {
        return this.m_In.BaseStream;
      }
    }

    public BinaryReader Raw
    {
      get
      {
        return this.m_In;
      }
    }

    public BinaryReader Compressed
    {
      get
      {
        if (!this.m_Compressed)
          return this.m_In;
        return this.m_Self;
      }
    }

    public bool IsCompressed
    {
      get
      {
        return this.m_Compressed;
      }
      set
      {
        this.m_Compressed = value;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return true;
      }
    }

    public override bool CanRead
    {
      get
      {
        return true;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return false;
      }
    }

    public override long Length
    {
      get
      {
        if (!this.m_Compressed)
          return this.RawStream.Length;
        if (this.RawStream.Position >= this.RawStream.Length)
          return this.m_Uncomp.Length;
        return (long) int.MaxValue;
      }
    }

    public override long Position
    {
      get
      {
        if (!this.m_Compressed)
          return this.RawStream.Position;
        return this.m_Uncomp.Position;
      }
      set
      {
        if (this.m_Compressed)
          this.m_Uncomp.Position = value;
        else
          this.RawStream.Position = value;
      }
    }

    public bool EndOfFile
    {
      get
      {
        if (!this.m_Compressed || this.m_Uncomp.Position >= this.m_Uncomp.Length)
          return this.RawStream.Position >= this.RawStream.Length;
        return false;
      }
    }

    public GZBlockIn(Stream underlyingStream)
    {
      this.m_Compressed = true;
      this.m_In = new BinaryReader(underlyingStream);
      this.m_Uncomp = new MemoryStream();
      this.m_Self = new BinaryReader((Stream) this);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override void Flush()
    {
      this.RawStream.Flush();
      this.m_Uncomp.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      if (!this.m_Compressed)
        return this.RawStream.Seek(offset, origin);
      long num = offset;
      if (origin == SeekOrigin.Current)
        num += this.m_Uncomp.Position;
      if (num < 0L)
        throw new Exception("Cannot seek past the begining of the stream.");
      long position = this.m_Uncomp.Position;
      this.m_Uncomp.Seek(0L, SeekOrigin.End);
      while ((origin == SeekOrigin.End || num >= this.m_Uncomp.Length) && this.RawStream.Position < this.RawStream.Length)
      {
        int count1 = this.Raw.ReadInt32();
        int count2 = this.Raw.ReadInt32();
        if (GZBlockIn.m_ReadBuff == null || GZBlockIn.m_ReadBuff.Length < count1)
          GZBlockIn.m_ReadBuff = new byte[count1];
        if (GZBlockIn.m_CompBuff == null || GZBlockIn.m_CompBuff.Length < count2)
          GZBlockIn.m_CompBuff = new byte[count2];
        else
          count2 = GZBlockIn.m_CompBuff.Length;
        this.Raw.Read(GZBlockIn.m_ReadBuff, 0, count1);
        Z_RESULT zResult = ZLib.Decompress(GZBlockIn.m_CompBuff, ref count2, GZBlockIn.m_ReadBuff, count1);
        if (zResult != null)
          throw new Exception("ZLib error uncompressing: " + ((object) zResult).ToString());
        this.m_Uncomp.Write(GZBlockIn.m_CompBuff, 0, count2);
      }
      this.m_Uncomp.Position = position;
      return this.m_Uncomp.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (!this.m_Compressed)
        return this.RawStream.Read(buffer, offset, count);
      long position = this.m_Uncomp.Position;
      this.m_Uncomp.Seek(0L, SeekOrigin.End);
      while (position + (long) count > this.m_Uncomp.Length && this.RawStream.Position + 8L < this.RawStream.Length)
      {
        int count1 = this.Raw.ReadInt32();
        int count2 = this.Raw.ReadInt32();
        if (count1 <= 268435456 && count1 > 0 && (count2 <= 268435456 && count2 > 0) && this.RawStream.Position + (long) count1 <= this.RawStream.Length)
        {
          if (GZBlockIn.m_ReadBuff == null || GZBlockIn.m_ReadBuff.Length < count1)
            GZBlockIn.m_ReadBuff = new byte[count1];
          if (GZBlockIn.m_CompBuff == null || GZBlockIn.m_CompBuff.Length < count2)
            GZBlockIn.m_CompBuff = new byte[count2];
          else
            count2 = GZBlockIn.m_CompBuff.Length;
          this.Raw.Read(GZBlockIn.m_ReadBuff, 0, count1);
          Z_RESULT zResult = ZLib.Decompress(GZBlockIn.m_CompBuff, ref count2, GZBlockIn.m_ReadBuff, count1);
          if (zResult != null)
            throw new Exception("ZLib error uncompressing: " + ((object) zResult).ToString());
          this.m_Uncomp.Write(GZBlockIn.m_CompBuff, 0, count2);
        }
        else
          break;
      }
      this.m_Uncomp.Position = position;
      return this.m_Uncomp.Read(buffer, offset, count);
    }

    public override void Close()
    {
      this.m_In.Close();
      this.m_Uncomp.Close();
      this.m_Self = (BinaryReader) null;
    }
  }
}

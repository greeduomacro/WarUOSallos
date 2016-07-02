// Decompiled with JetBrains decompiler
// Type: PlayUO.LocalizationFile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;
using System.Text;

namespace PlayUO
{
  public class LocalizationFile
  {
    private static byte[] m_Buffer = new byte[4];
    private bool m_Valid;
    private string m_Name;
    private string[] m_Text;

    public int Count
    {
      get
      {
        return this.m_Text.Length;
      }
    }

    public string this[int index]
    {
      get
      {
        if (!this.m_Valid)
          return string.Format("<Invalid localization file: {0}>", (object) this.m_Name);
        if (index < 0 || index >= this.m_Text.Length)
          return string.Format("<Index out of bounds: {0}:{1} ({2})>", (object) this.m_Name, (object) index, (object) this.m_Text.Length);
        return this.m_Text[index];
      }
    }

    public LocalizationFile(string path)
    {
      this.m_Name = Path.GetFileName(path);
      if (!File.Exists(path))
        return;
      this.ReadFromDisk(path);
    }

    private int ReadInt32_BE(Stream stream)
    {
      stream.Read(LocalizationFile.m_Buffer, 0, 4);
      return (int) LocalizationFile.m_Buffer[0] << 24 | (int) LocalizationFile.m_Buffer[1] << 16 | (int) LocalizationFile.m_Buffer[2] << 8 | (int) LocalizationFile.m_Buffer[3];
    }

    private int ReadInt32_LE(Stream stream)
    {
      stream.Read(LocalizationFile.m_Buffer, 0, 4);
      return (int) LocalizationFile.m_Buffer[0] | (int) LocalizationFile.m_Buffer[1] << 8 | (int) LocalizationFile.m_Buffer[2] << 16 | (int) LocalizationFile.m_Buffer[3] << 24;
    }

    private unsafe void ReadFromDisk(string path)
    {
      try
      {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        fileStream.Seek(28L, SeekOrigin.Begin);
        int num1 = this.ReadInt32_BE((Stream) fileStream);
        long position = fileStream.Position;
        do
          ;
        while (fileStream.ReadByte() > 0);
        int num2 = this.ReadInt32_LE((Stream) fileStream);
        fileStream.Seek(4L, SeekOrigin.Current);
        do
          ;
        while (fileStream.ReadByte() > 0);
        fileStream.Seek(4L, SeekOrigin.Current);
        int length = this.ReadInt32_LE((Stream) fileStream);
        fileStream.Seek(position + (long) num1 + (long) (num1 & 1) + 4L, SeekOrigin.Begin);
        int count = this.ReadInt32_BE((Stream) fileStream);
        if (count > LocalizationFile.m_Buffer.Length)
          LocalizationFile.m_Buffer = new byte[count];
        fileStream.Read(LocalizationFile.m_Buffer, 0, count);
        fileStream.Close();
        this.m_Valid = true;
        switch (num2)
        {
          case 1:
            this.m_Text = new string[length];
            fixed (byte* numPtr1 = LocalizationFile.m_Buffer)
            {
              byte* numPtr2 = numPtr1;
              byte* numPtr3 = numPtr2 + count;
              for (int index = 0; index < length; ++index)
              {
                byte* numPtr4 = numPtr2;
                do
                  ;
                while (numPtr2 < numPtr3 && (int) *numPtr2++ != 0);
                this.m_Text[index] = new string((sbyte*) numPtr4, 0, (int) (numPtr2 - numPtr4 - 1L));
              }
              break;
            }
          case 2:
            this.m_Text = new string[length];
            Encoding unicode = Encoding.Unicode;
            fixed (byte* numPtr1 = LocalizationFile.m_Buffer)
            {
              byte* numPtr2 = numPtr1;
              byte* numPtr3 = numPtr2 + count;
              for (int index = 0; index < length; ++index)
              {
                byte* numPtr4 = numPtr2;
                while (numPtr2 < numPtr3)
                {
                  byte* numPtr5 = numPtr2;
                  IntPtr num3 = new IntPtr(1);
                  byte* numPtr6 = numPtr5 + num3.ToInt64();
                  int num4 = (int) *numPtr5;
                  byte* numPtr7 = numPtr6;
                  IntPtr num5 = new IntPtr(1);
                  numPtr2 = numPtr7 + num5.ToInt64();
                  int num6 = (int) *numPtr7;
                  if ((num4 | num6) == 0)
                    break;
                }
                this.m_Text[index] = new string((sbyte*) numPtr4, 0, (int) (numPtr2 - numPtr4 - 2L), unicode);
              }
              break;
            }
          default:
            throw new InvalidOperationException(string.Format("Character size invalid. (charSize={0})", (object) num2));
        }
      }
      catch
      {
        this.m_Valid = false;
      }
    }
  }
}

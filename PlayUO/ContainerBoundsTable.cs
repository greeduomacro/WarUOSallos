// Decompiled with JetBrains decompiler
// Type: PlayUO.ContainerBoundsTable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PlayUO
{
  public class ContainerBoundsTable
  {
    private Rectangle m_Default;
    private Dictionary<int, Rectangle> m_Entries;
    private bool m_Disposed;

    public ContainerBoundsTable()
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/config/container-bounds.cfg");
      if (archivedFile != null)
      {
        using (StreamReader ip = new StreamReader(archivedFile.Download()))
          this.Load(ip);
      }
      else
        this.Default();
    }

    public void Dispose()
    {
      if (this.m_Disposed)
        return;
      this.m_Disposed = true;
      this.m_Entries.Clear();
      this.m_Entries = (Dictionary<int, Rectangle>) null;
    }

    public Rectangle Translate(int gumpID)
    {
      gumpID &= 16383;
      Rectangle rectangle;
      if (!this.m_Entries.TryGetValue(gumpID, out rectangle))
        rectangle = this.m_Default;
      return rectangle;
    }

    private void Load(StreamReader ip)
    {
      this.m_Entries = new Dictionary<int, Rectangle>();
      int num = 0;
      string str1;
      while ((str1 = ip.ReadLine()) != null)
      {
        string str2 = str1.Trim();
        if (str2.Length > 0 && (int) str2[0] != 35)
        {
          string[] strArray = str2.Split('\t');
          Rectangle rectangle = new Rectangle(this.IntConvert(strArray[1]), this.IntConvert(strArray[2]), this.IntConvert(strArray[3]), this.IntConvert(strArray[4]));
          if (strArray[0] == "default")
            this.m_Default = rectangle;
          else
            this.m_Entries[this.IntConvert(strArray[0]) & 16383] = rectangle;
          ++num;
        }
      }
    }

    private int IntConvert(string s)
    {
      if (s.StartsWith("0x"))
        return Convert.ToInt32(s.Substring(2), 16);
      return Convert.ToInt32(s);
    }

    private void Default()
    {
      int[,] numArray = new int[25, 5]{ { 7, 30, 30, 240, 140 }, { 9, 20, 85, 104, 111 }, { 60, 44, 65, 142, 94 }, { 61, 29, 34, 108, 94 }, { 62, 33, 36, 109, 112 }, { 63, 19, 47, 163, 76 }, { 64, 16, 38, 136, 87 }, { 65, 35, 38, 110, 78 }, { 66, 18, 105, 144, 73 }, { 67, 16, 51, 168, 73 }, { 68, 20, 10, 150, 90 }, { 71, 16, 10, 132, 128 }, { 72, 16, 10, 138, 84 }, { 73, 18, 105, 144, 73 }, { 74, 18, 105, 144, 73 }, { 75, 16, 51, 168, 73 }, { 76, 46, 74, 150, 110 }, { 77, 76, 12, 64, 56 }, { 78, 24, 96, 172, 56 }, { 79, 24, 96, 172, 56 }, { 81, 16, 10, 138, 84 }, { 82, 0, 0, 110, 62 }, { 2330, 0, 0, 282, 230 }, { 2350, 0, 0, 282, 210 }, { 10851, 60, 33, 400, 315 } };
      int length = numArray.GetLength(0);
      this.m_Entries = new Dictionary<int, Rectangle>(length);
      for (int index = 0; index < length; ++index)
        this.m_Entries[numArray[index, 0]] = new Rectangle(numArray[index, 1], numArray[index, 2], numArray[index, 3], numArray[index, 4]);
      this.m_Default = new Rectangle(0, 0, 200, 200);
    }
  }
}

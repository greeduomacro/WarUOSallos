// Decompiled with JetBrains decompiler
// Type: PlayUO.MountTable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class MountTable
  {
    private int m_Default;
    private Hashtable m_Entries;
    private bool m_Disposed;

    public MountTable()
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/config/mounts.cfg");
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
      this.m_Entries = (Hashtable) null;
    }

    public int Translate(int itemID)
    {
      itemID &= 16383;
      object obj = this.m_Entries[(object) itemID];
      if (obj != null)
        return (int) obj;
      return this.m_Default;
    }

    public bool IsMount(int body)
    {
      return this.m_Entries.ContainsValue((object) body);
    }

    private void Load(StreamReader ip)
    {
      this.m_Entries = new Hashtable();
      int num1 = 0;
      string str1;
      while ((str1 = ip.ReadLine()) != null)
      {
        string str2 = str1.Trim();
        if (str2.Length > 0 && (int) str2[0] != 35)
        {
          int length = str2.IndexOf('\t');
          string str3 = str2.Substring(0, length);
          string str4 = str2.Substring(length + 1);
          bool flag = false;
          int num2;
          if (str3.StartsWith("0x"))
            num2 = Convert.ToInt32(str3.Substring(2), 16);
          else if (str3 == "default")
          {
            flag = true;
            num2 = 0;
          }
          else
            num2 = Convert.ToInt32(str3);
          int num3 = !str4.StartsWith("0x") ? Convert.ToInt32(str4) : Convert.ToInt32(str4.Substring(2), 16);
          if (!flag)
            this.m_Entries[(object) (num2 & 16383)] = (object) num3;
          else
            this.m_Default = num3;
          ++num1;
        }
      }
    }

    private void Save(string filePath)
    {
      try
      {
        using (StreamWriter streamWriter = new StreamWriter(filePath, false))
        {
          streamWriter.WriteLine("# Defines the table used to translate internal mount items to their corresponding body numbers");
          streamWriter.WriteLine("# All lines are trimmed. Empty lines, and lines starting with '#' are ignored.");
          streamWriter.WriteLine("# Format: <item number><tab><body number>");
          streamWriter.WriteLine("# Parser supports hex or decimal numbers. Any numbers prefixed with \"0x\" are treated as hex.");
          streamWriter.WriteLine("# Any lines improperly formatted, the parser will ignore.");
          streamWriter.WriteLine("# The \"default\" item number is a special case, item numbers which are not in the table fall into this category.");
          streamWriter.WriteLine("# Generated on {0}", (object) DateTime.Now);
          streamWriter.WriteLine();
          foreach (DictionaryEntry mEntry in this.m_Entries)
          {
            streamWriter.Write("0x");
            streamWriter.Write(((int) mEntry.Key).ToString("X"));
            streamWriter.Write("\t0x");
            streamWriter.WriteLine(((int) mEntry.Value).ToString("X"));
          }
          streamWriter.WriteLine("default\t0x{0:X}", (object) this.m_Default);
        }
      }
      catch (Exception ex)
      {
        Debug.Trace("Error saving '{0}':", (object) filePath);
        Debug.Error(ex);
      }
    }

    private void Default()
    {
      int[,] numArray = new int[29, 2]{ { 16032, 226 }, { 16033, 228 }, { 16034, 204 }, { 16035, 210 }, { 16036, 218 }, { 16037, 219 }, { 16038, 220 }, { 16039, 116 }, { 16040, 117 }, { 16041, 114 }, { 16042, 115 }, { 16043, 170 }, { 16044, 171 }, { 16045, 132 }, { 16047, 120 }, { 16048, 121 }, { 16049, 119 }, { 16050, 118 }, { 16051, 144 }, { 16052, 122 }, { 16053, 177 }, { 16054, 178 }, { 16055, 179 }, { 16056, 188 }, { 16058, 187 }, { 16059, 793 }, { 16060, 791 }, { 16061, 794 }, { 16062, 799 } };
      int length = numArray.GetLength(0);
      this.m_Entries = new Hashtable(length);
      for (int index = 0; index < length; ++index)
        this.m_Entries[(object) numArray[index, 0]] = (object) numArray[index, 1];
      this.m_Default = 200;
    }
  }
}

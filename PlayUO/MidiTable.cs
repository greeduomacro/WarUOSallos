// Decompiled with JetBrains decompiler
// Type: PlayUO.MidiTable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class MidiTable
  {
    private Hashtable m_Entries;
    private bool m_Disposed;
    private Hashtable m_Overwrite;

    public MidiTable()
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/config/music.cfg");
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

    public string Translate(int midiID)
    {
      if (this.m_Overwrite == null)
        this.LoadMP3Table();
      return (string) this.m_Overwrite[(object) midiID] ?? (string) this.m_Entries[(object) midiID];
    }

    public void LoadMP3Table()
    {
      this.m_Overwrite = new Hashtable();
      string path = Engine.FileManager.ResolveMUL("music/digital/config.txt");
      if (!File.Exists(path))
        return;
      using (StreamReader streamReader = new StreamReader(path))
      {
        string str1;
        while ((str1 = streamReader.ReadLine()) != null)
        {
          string str2 = str1.Trim();
          if (str2.Length == 0)
            break;
          string[] strArray = str2.Split(' ');
          if (strArray.Length < 2)
            break;
          try
          {
            int num = int.Parse(strArray[0]);
            string str3 = strArray[1];
            int length = str3.IndexOf(',');
            if (length >= 0)
              str3 = str3.Substring(0, length);
            this.m_Overwrite[(object) num] = (object) ("digital/" + str3 + ".mp3");
          }
          catch
          {
          }
        }
      }
    }

    private void Load(StreamReader ip)
    {
      this.m_Entries = new Hashtable();
      int num = 0;
      string str1;
      while ((str1 = ip.ReadLine()) != null)
      {
        string str2 = str1.Trim();
        if (str2.Length > 0 && (int) str2[0] != 35)
        {
          int length = str2.IndexOf('\t');
          string str3 = str2.Substring(0, length);
          string str4 = str2.Substring(length + 1);
          this.m_Entries[(object) (!str3.StartsWith("0x") ? Convert.ToInt32(str3) : Convert.ToInt32(str3.Substring(2), 16))] = (object) str4;
          ++num;
        }
      }
    }

    private void Default()
    {
      object[,] objArray = new object[49, 2]{ { (object) 0, (object) "oldult01.mid" }, { (object) 1, (object) "create1.mid" }, { (object) 2, (object) "draglift.mid" }, { (object) 3, (object) "oldult02.mid" }, { (object) 4, (object) "oldult03.mid" }, { (object) 5, (object) "oldult04.mid" }, { (object) 6, (object) "oldult05.mid" }, { (object) 7, (object) "oldult06.mid" }, { (object) 8, (object) "stones2.mid" }, { (object) 9, (object) "britain1.mid" }, { (object) 10, (object) "britain2.mid" }, { (object) 11, (object) "bucsden.mid" }, { (object) 12, (object) "jhelom.mid" }, { (object) 13, (object) "lbcastle.mid" }, { (object) 14, (object) "linelle.mid" }, { (object) 15, (object) "magincia.mid" }, { (object) 16, (object) "minoc.mid" }, { (object) 17, (object) "ocllo.mid" }, { (object) 18, (object) "samlethe.mid" }, { (object) 19, (object) "serpents.mid" }, { (object) 20, (object) "skarabra.mid" }, { (object) 21, (object) "trinsic.mid" }, { (object) 22, (object) "vesper.mid" }, { (object) 23, (object) "wind.mid" }, { (object) 24, (object) "yew.mid" }, { (object) 25, (object) "cave01.mid" }, { (object) 26, (object) "dungeon9.mid" }, { (object) 27, (object) "forest_a.mid" }, { (object) 28, (object) "intown01.mid" }, { (object) 29, (object) "jungle_a.mid" }, { (object) 30, (object) "mountn_a.mid" }, { (object) 31, (object) "plains_a.mid" }, { (object) 32, (object) "sailing.mid" }, { (object) 33, (object) "swamp_a.mid" }, { (object) 34, (object) "tavern01.mid" }, { (object) 35, (object) "tavern02.mid" }, { (object) 36, (object) "tavern03.mid" }, { (object) 37, (object) "tavern04.mid" }, { (object) 38, (object) "combat1.mid" }, { (object) 39, (object) "combat2.mid" }, { (object) 40, (object) "combat3.mid" }, { (object) 41, (object) "approach.mid" }, { (object) 42, (object) "death.mid" }, { (object) 43, (object) "victory.mid" }, { (object) 44, (object) "btcastle.mid" }, { (object) 45, (object) "nujelm.mid" }, { (object) 46, (object) "dungeon2.mid" }, { (object) 47, (object) "cove.mid" }, { (object) 48, (object) "moonglow.mid" } };
      int length = objArray.GetLength(0);
      this.m_Entries = new Hashtable(length);
      for (int index = 0; index < length; ++index)
        this.m_Entries[objArray[index, 0]] = objArray[index, 1];
    }
  }
}

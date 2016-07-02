// Decompiled with JetBrains decompiler
// Type: PlayUO.Config
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class Config
  {
    private static string[] m_FileNames = new string[27]{ "Skills.idx", "Skills.mul", "SoundIdx.mul", "Sound.mul", "LightIdx.mul", "Light.mul", "Fonts.mul", "TileData.mul", "Anim.idx", "Anim.mul", "ArtIdx.mul", "Art.mul", "TexIdx.mul", "TexMaps.mul", "Hues.mul", "Multi.idx", "Multi.mul", "Map0.mul", "Map2.mul", "Statics0.mul", "Statics2.mul", "StaIdx0.mul", "StaIdx2.mul", "AnimData.mul", "VerData.mul", "GumpIdx.mul", "GumpArt.mul" };
    private static ArrayList m_PaperdollCFG;

    static Config()
    {
      if (!File.Exists(Engine.FileManager.ResolveMUL(Files.Verdata)))
      {
        string path = Engine.FileManager.BasePath("data/ultima/empty-verdata.mul");
        Config.m_FileNames[24] = path;
        if (!File.Exists(path))
        {
          using (Stream stream = (Stream) File.Create(path, 4))
          {
            stream.Write(new byte[4], 0, 4);
            stream.Flush();
          }
        }
      }
      Config.m_PaperdollCFG = new ArrayList();
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/config/paperdoll.cfg");
      if (archivedFile == null)
        return;
      using (StreamReader streamReader = new StreamReader(archivedFile.Download()))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
          string[] strArray = str.Split('\t');
          if (strArray.Length >= 2)
          {
            try
            {
              Config.m_PaperdollCFG.Add((object) new PaperdollEntry(Convert.ToInt32(strArray[0], 16), Convert.ToInt32(strArray[1], 16)));
            }
            catch
            {
            }
          }
        }
      }
    }

    public static string GetFile(int FileID)
    {
      return Config.m_FileNames[FileID];
    }

    public static int GetPaperdollGump(int BodyID)
    {
      int count = Config.m_PaperdollCFG.Count;
      for (int index = 0; index < count; ++index)
      {
        PaperdollEntry paperdollEntry = (PaperdollEntry) Config.m_PaperdollCFG[index];
        if (paperdollEntry.BodyID == BodyID)
          return paperdollEntry.GumpID;
      }
      return 0;
    }
  }
}

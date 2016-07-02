// Decompiled with JetBrains decompiler
// Type: PlayUO.Localization
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace PlayUO
{
  public class Localization
  {
    private static byte[] m_Buffer = new byte[1024];
    private static string m_Language = CultureInfo.CurrentUICulture.ThreeLetterWindowsLanguageName.ToUpper();
    private static string m_Extension = !File.Exists(Engine.FileManager.ResolveMUL("cliloc-1." + Localization.m_Language)) ? ".ENU" : "." + Localization.m_Language;
    private static string m_Cliloc1 = "cliloc-1" + Localization.m_Extension;
    private static Hashtable m_Files = new Hashtable();
    private static Dictionary<int, string> m_Strings = new Dictionary<int, string>(50000);

    public static string Language
    {
      get
      {
        return Localization.m_Language;
      }
    }

    static Localization()
    {
      Localization.LoadCompiledDatabase();
    }

    private static void LoadCompiledDatabase()
    {
      string path = Engine.FileManager.ResolveMUL("cliloc" + Localization.m_Extension);
      if (!File.Exists(path))
        path = Engine.FileManager.ResolveMUL("cliloc.enu");
      if (!File.Exists(path))
        return;
      using (BinaryReader binaryReader = new BinaryReader((Stream) new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
      {
        binaryReader.ReadInt32();
        int num1 = (int) binaryReader.ReadInt16();
        while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
        {
          int index = binaryReader.ReadInt32();
          int num2 = (int) binaryReader.ReadByte();
          int count = (int) binaryReader.ReadInt16();
          if (count > Localization.m_Buffer.Length)
            Localization.m_Buffer = new byte[count + 1023 & -1024];
          if (count == 0)
          {
            Localization.m_Strings[index] = "";
          }
          else
          {
            binaryReader.Read(Localization.m_Buffer, 0, count);
            Localization.m_Strings[index] = Encoding.UTF8.GetString(Localization.m_Buffer, 0, count);
          }
        }
      }
    }

    public static string GetString(int number)
    {
      string str;
      if (!Localization.m_Strings.TryGetValue(number, out str))
      {
        int index1 = number;
        string Path;
        int index2;
        if (number >= 3000000)
        {
          number -= 3000000;
          Path = "intloc" + (number / 1000).ToString("D2") + Localization.m_Extension;
          index2 = number % 1000;
        }
        else if (number >= 1000000)
        {
          number -= 1000000;
          Path = "cliloc" + (number / 1000).ToString("D2") + Localization.m_Extension;
          index2 = number % 1000;
        }
        else
        {
          if (number < 500000)
            return string.Format("<Localization number invalid: {0}>", (object) index1);
          Path = Localization.m_Cliloc1;
          index2 = number - 500000;
        }
        LocalizationFile file = Localization.GetFile(Engine.FileManager.ResolveMUL(Path));
        Localization.m_Strings[index1] = str = file[index2];
      }
      return str;
    }

    public static LocalizationFile GetFile(string path)
    {
      LocalizationFile localizationFile = (LocalizationFile) Localization.m_Files[(object) path];
      if (localizationFile == null)
        Localization.m_Files[(object) path] = (object) (localizationFile = new LocalizationFile(path));
      return localizationFile;
    }
  }
}

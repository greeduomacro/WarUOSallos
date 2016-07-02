// Decompiled with JetBrains decompiler
// Type: PlayUO.Macros
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.IO;
using System.Windows.Forms;

namespace PlayUO
{
  public class Macros
  {
    private static MacroCollection m_Running = new MacroCollection();
    private const string RelativeUserDataPath = "Sallos/Ultima Online/Macros.xml";
    private const string RelativeLegacyPath = "data/ultima/macros/macros.xml";
    private const string RelativeArchivePath = "play/macros/macros.xml";
    private static MacroConfig m_Config;
    private static MacroSet m_Current;

    public static MacroConfig Config
    {
      get
      {
        if (Macros.m_Config == null)
          Macros.m_Config = Macros.LoadConfig();
        return Macros.m_Config;
      }
    }

    public static MacroSet Current
    {
      get
      {
        if (Macros.m_Current == null)
          Macros.m_Current = Macros.FindCurrent();
        return Macros.m_Current;
      }
    }

    public static MacroCollection List
    {
      get
      {
        return Macros.Current.Macros;
      }
    }

    public static MacroCollection Running
    {
      get
      {
        return Macros.m_Running;
      }
    }

    public static void Reset()
    {
      Macros.StopAll();
      Macros.m_Current = (MacroSet) null;
      Macros.m_Current = Macros.Current;
    }

    private static MacroSet FindCurrent(MacroConfig config, Mobile mob)
    {
      int index1 = mob == null ? 0 : mob.Serial;
      int index2 = mob == null ? 0 : (Engine.m_ServerName == null ? 0 : Engine.m_ServerName.GetHashCode());
      MacroSet macroSet = config[index1, index2];
      if (macroSet == null && (mob == null || Macros.Exists(Macros.GetMobilePath(mob))))
      {
        macroSet = Macros.LoadTextMacroSet(mob);
        macroSet.Serial = index1;
        macroSet.Server = index2;
        config.MacroSets.Add(macroSet);
        Macros.Save();
      }
      return macroSet;
    }

    private static MacroSet FindCurrent()
    {
      MacroConfig config = Macros.Config;
      Mobile player = World.Player;
      MacroSet current = Macros.FindCurrent(config, player);
      if (current == null && player != null)
        current = Macros.FindCurrent(config, (Mobile) null);
      return current;
    }

    private static string GetConfigurationPath()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sallos/Ultima Online/Macros.xml");
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      return path;
    }

    private static MacroConfig LoadConfig()
    {
      MacroConfig macroConfig = new MacroConfig();
      string configurationPath = Macros.GetConfigurationPath();
      if (!File.Exists(configurationPath))
      {
        string str = Engine.FileManager.BasePath("data/ultima/macros/macros.xml");
        if (File.Exists(str))
        {
          try
          {
            File.Move(str, configurationPath);
          }
          catch
          {
            File.Copy(str, configurationPath, false);
          }
        }
        else
        {
          ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/macros/macros.xml");
          if (archivedFile != null)
          {
            using (FileStream fileStream = new FileStream(configurationPath, FileMode.Create, FileAccess.Write, FileShare.None))
              archivedFile.Download((Stream) fileStream);
          }
        }
      }
      if (File.Exists(configurationPath))
      {
        using (FileStream fileStream = new FileStream(configurationPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          XmlPersistanceReader persistanceReader = new XmlPersistanceReader((Stream) fileStream);
          ((PersistanceReader) persistanceReader).ReadDocument((PersistableObject) macroConfig);
          ((PersistanceReader) persistanceReader).Close();
        }
      }
      return macroConfig;
    }

    public static bool Start(Keys key)
    {
      foreach (Macro macro in Macros.Current.Macros)
      {
        if (macro.CheckKey(key))
        {
          if (macro.Running)
            macro.Stop();
          macro.Start();
          return true;
        }
      }
      return false;
    }

    public static void StopAll()
    {
      while (Macros.m_Running.Count > 0)
        Macros.m_Running[0].Stop();
    }

    public static void Slice()
    {
      if (Macros.m_Running == null)
        return;
      for (int index = 0; index < Macros.m_Running.Count; ++index)
      {
        if (!Macros.m_Running[index].Slice())
          Macros.m_Running.RemoveAt(index--);
      }
    }

    public static string ReadLine(StreamReader ip)
    {
      string str;
      while ((str = ip.ReadLine()) != null)
      {
        str = str.Trim();
        if (str.Length != 0 && !str.StartsWith(";"))
          break;
      }
      return str;
    }

    public static void Skip(string line, StreamReader ip)
    {
      Debug.Trace("Skipping improperly formatted line in macros.txt: {0}", (object) line);
      do
      {
        line = line.Trim();
      }
      while (!line.StartsWith("#") && (line = ip.ReadLine()) != null);
    }

    public static void Cleanup()
    {
      MacroCollection macros = Macros.Current.Macros;
      for (int index = macros.Count - 1; index >= 0; --index)
      {
        if (macros[index].Actions.Count == 0)
          macros.RemoveAt(index);
      }
    }

    public static void Save()
    {
      XmlPersistanceWriter.SaveObject((PersistableObject) Macros.Config, Macros.GetConfigurationPath());
    }

    public static string GetMobilePath(Mobile mob)
    {
      return ((Engine.m_ServerName ?? "").GetHashCode() ^ mob.Serial).ToString("X8");
    }

    public static bool Exists(string filename)
    {
      return File.Exists(Engine.FileManager.BasePath(string.Format("data/ultima/macros/{0}.txt", (object) filename)));
    }

    public static MacroSet LoadTextMacroSet(Mobile mob)
    {
      if (mob != null)
      {
        string mobilePath = Macros.GetMobilePath(mob);
        if (Macros.Exists(mobilePath))
          return Macros.LoadTextMacroSet(mobilePath);
      }
      return Macros.LoadTextMacroSet("Macros");
    }

    public static MacroSet LoadTextMacroSet(string fileName)
    {
      MacroSet macroSet = new MacroSet();
      string path = Engine.FileManager.BasePath(string.Format("data/ultima/macros/{0}.txt", (object) fileName));
      if (File.Exists(path))
      {
        using (StreamReader ip = new StreamReader(path))
        {
          string line1;
          string line2;
          while (true)
          {
            line1 = Macros.ReadLine(ip);
            if (line1 != null)
            {
              if (line1.Length == 5)
              {
                string[] strArray = line1.Split(' ');
                if (strArray.Length == 3)
                {
                  bool flag = true;
                  for (int index = 0; flag && index < strArray.Length; ++index)
                    flag = strArray[index] == "0" || strArray[index] == "1";
                  if (flag)
                  {
                    Keys keys1 = Keys.None;
                    if (strArray[0] != "0")
                      keys1 |= Keys.Control;
                    if (strArray[1] != "0")
                      keys1 |= Keys.Alt;
                    if (strArray[2] != "0")
                      keys1 |= Keys.Shift;
                    line2 = Macros.ReadLine(ip);
                    if (line2 != null)
                    {
                      Keys keys2 = Keys.KeyCode | Keys.Modifiers;
                      switch (line2)
                      {
                        case "WheelUp":
                        case "Wheel Up":
                          keys2 = (Keys) 69632;
                          break;
                        case "WheelDown":
                        case "Wheel Down":
                          keys2 = (Keys) 69633;
                          break;
                        case "WheelPress":
                        case "Wheel Press":
                          keys2 = (Keys) 69634;
                          break;
                        default:
                          try
                          {
                            keys2 = (Keys) Enum.Parse(typeof (Keys), line2, true);
                            break;
                          }
                          catch
                          {
                            break;
                          }
                      }
                      if (keys2 != (Keys.KeyCode | Keys.Modifiers))
                      {
                        MacroData data = new MacroData();
                        data.Key = keys2;
                        data.Mods = keys1;
                        string command;
                        while ((command = Macros.ReadLine(ip)) != null && !command.StartsWith("#"))
                        {
                          int length = command.IndexOf(' ');
                          Action action = length < 0 ? new Action(command, "") : new Action(command.Substring(0, length), command.Substring(length + 1));
                          if (action.Handler == null)
                            Debug.Trace("Bad macro action: {0}", (object) command);
                          data.Actions.Add(action);
                        }
                        macroSet.Macros.Add(new Macro(data));
                      }
                      else
                        goto label_26;
                    }
                    else
                      goto label_36;
                  }
                  else
                    goto label_11;
                }
                else
                  goto label_6;
              }
              else
                break;
            }
            else
              goto label_36;
          }
          Macros.Skip(line1, ip);
          goto label_36;
label_6:
          Macros.Skip(line1, ip);
          goto label_36;
label_11:
          Macros.Skip(line1, ip);
          goto label_36;
label_26:
          Macros.Skip(line2, ip);
        }
      }
label_36:
      return macroSet;
    }
  }
}

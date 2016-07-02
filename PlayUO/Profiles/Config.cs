// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Config
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.IO;

namespace PlayUO.Profiles
{
  public class Config : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("config", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[2]{ ProfileList.TypeCode, ServerList.TypeCode });
    public static readonly Config Current = new Config();
    private const string RelativeUserDataPath = "Sallos/Ultima Online/Configuration.xml";
    private const string RelativeLegacyPath = "config.xml";
    private ProfileList m_Profiles;
    private ServerList m_Servers;

    public virtual PersistableType TypeID
    {
      get
      {
        return Config.TypeCode;
      }
    }

    public ProfileList Profiles
    {
      get
      {
        return this.m_Profiles;
      }
    }

    public ServerList Servers
    {
      get
      {
        return this.m_Servers;
      }
    }

    public Config()
    {
      this.m_Profiles = new ProfileList();
      this.m_Servers = new ServerList();
      this.Load();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Config();
    }

    private static string GetConfigurationPath()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sallos/Ultima Online/Configuration.xml");
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      return path;
    }

    public void Load()
    {
      string configurationPath = Config.GetConfigurationPath();
      if (!File.Exists(configurationPath))
      {
        string str = Engine.FileManager.BasePath("config.xml");
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
      }
      if (!File.Exists(configurationPath))
        return;
      using (FileStream fileStream = new FileStream(configurationPath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        XmlPersistanceReader persistanceReader = new XmlPersistanceReader((Stream) fileStream);
        ((PersistanceReader) persistanceReader).ReadDocument((PersistableObject) this);
        ((PersistanceReader) persistanceReader).Close();
      }
    }

    public void Save()
    {
      XmlPersistanceWriter persistanceWriter = new XmlPersistanceWriter(Config.GetConfigurationPath());
      ((PersistanceWriter) persistanceWriter).WriteDocument((PersistableObject) this);
      ((PersistanceWriter) persistanceWriter).Close();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.m_Profiles.Serialize(op);
      this.m_Servers.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
      {
        object obj = (object) ip.GetChild();
        if (obj is ProfileList)
          this.m_Profiles = obj as ProfileList;
        else if (obj is ServerList)
          this.m_Servers = obj as ServerList;
      }
    }
  }
}

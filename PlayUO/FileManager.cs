// Decompiled with JetBrains decompiler
// Type: PlayUO.FileManager
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Microsoft.Win32;
using Sallos;
using System;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PlayUO
{
  public class FileManager
  {
    private static readonly string[] RootKeyNames = new string[8]{ "Software\\Electronic Arts\\EA Games\\Ultima Online Classic", "Software\\Electronic Arts\\EA Games\\Ultima Online Stygian Abyss Classic", "Software\\EA Games", "Software\\Origin Worlds Online", "SOFTWARE\\Wow6432Node\\Electronic Arts\\EA Games\\Ultima Online Classic", "SOFTWARE\\Wow6432Node\\Electronic Arts\\EA Games\\Ultima Online Stygian Abyss Classic", "Software\\Wow6432Node\\EA Games", "Software\\Wow6432Node\\Origin Worlds Online" };
    private static readonly Regex RegistryKeyRegex = new Regex("^HKEY_LOCAL_MACHINE\\\\Software(?:\\\\Wow6432Node)?\\\\(?:EA Games|Origin Worlds Online|Electronic Arts)\\\\(Ultima Online|EA Games)[^\\\\]*(?:\\\\(KR Legacy Beta|Ultima Online Classic|Ultima Online Stygian Abyss Classic))?(?:\\\\[23]d)?(?:\\\\1\\.0+(?:\\.0+)?)?$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private string m_BasePath = "";
    private string m_FilePath = "";
    private const string RegistryKeyPattern = "^HKEY_LOCAL_MACHINE\\\\Software(?:\\\\Wow6432Node)?\\\\(?:EA Games|Origin Worlds Online|Electronic Arts)\\\\(Ultima Online|EA Games)[^\\\\]*(?:\\\\(KR Legacy Beta|Ultima Online Classic|Ultima Online Stygian Abyss Classic))?(?:\\\\[23]d)?(?:\\\\1\\.0+(?:\\.0+)?)?$";
    private Archive _archive;
    private bool m_Error;

    public string FilePath
    {
      get
      {
        return this.m_FilePath;
      }
    }

    public bool Error
    {
      get
      {
        return this.m_Error;
      }
    }

    public FileManager()
    {
      this._archive = Archive.AcquireArchive("ultima");
      this.m_BasePath = Directory.GetCurrentDirectory();
      this.m_FilePath = FileManager.GetPathFromRegistry() ?? this.QueryPathFromUser();
      this.m_Error = this.m_FilePath == null;
    }

    public string ResolveMUL(Files File)
    {
      return Path.Combine(this.m_FilePath, Config.GetFile((int) File));
    }

    public string ResolveMUL(string Path)
    {
      return Path.Combine(this.m_FilePath, Path);
    }

    public FileStream CreateUnique(string basePath, string extension)
    {
      string path = this.BasePath(string.Format("{0}{1}", (object) basePath, (object) extension));
      int num = 0;
      do
      {
        try
        {
          return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
        }
        catch
        {
          path = string.Format("{0}{1}{2}", (object) basePath, (object) ++num, (object) extension);
        }
      }
      while (num < 1000);
      throw new Exception(string.Format("Unable to create unique file (basePath='{0}', extension='{0}')", (object) basePath, (object) extension));
    }

    internal ArchivedFile GetArchivedFile(string path)
    {
      return this._archive.FindFile(path);
    }

    private static string GetPathFromRegistry()
    {
      foreach (string rootKeyName in FileManager.RootKeyNames)
      {
        try
        {
          using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(rootKeyName))
          {
            if (registryKey != null)
            {
              string path1 = registryKey.GetValue("InstallDir") as string;
              if (!string.IsNullOrEmpty(path1))
              {
                string fullPath = Path.GetFullPath(path1);
                if (Directory.Exists(fullPath))
                  return fullPath;
              }
              string path2 = registryKey.GetValue("ExePath") as string;
              if (!string.IsNullOrEmpty(path2))
              {
                path2 = Path.GetDirectoryName(path2);
                if (Directory.Exists(path2))
                  return path2;
              }
              if (FileManager.FindRegistryPathAux(registryKey, out path2))
                return path2;
            }
          }
        }
        catch (SecurityException ex)
        {
        }
      }
      return (string) null;
    }

    private static bool FindRegistryPathAux(RegistryKey registryKey, out string path)
    {
      if (registryKey != null)
      {
        foreach (string subKeyName in registryKey.GetSubKeyNames())
        {
          try
          {
            using (RegistryKey registryKey1 = registryKey.OpenSubKey(subKeyName))
            {
              if (registryKey1 != null)
              {
                if (FileManager.RegistryKeyRegex.IsMatch(registryKey1.Name))
                {
                  path = registryKey1.GetValue("InstallDir") as string;
                  if (!string.IsNullOrEmpty(path))
                  {
                    path = Path.GetFullPath(path);
                    if (Directory.Exists(path))
                      return true;
                  }
                  path = registryKey1.GetValue("ExePath") as string;
                  if (!string.IsNullOrEmpty(path))
                  {
                    path = Path.GetDirectoryName(path);
                    if (Directory.Exists(path))
                      return true;
                  }
                  if (FileManager.FindRegistryPathAux(registryKey1, out path))
                    return true;
                }
              }
            }
          }
          catch (SecurityException ex)
          {
          }
        }
      }
      path = (string) null;
      return false;
    }

    private string QueryPathFromUser()
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.CheckPathExists = true;
        openFileDialog.CheckFileExists = false;
        openFileDialog.FileName = "Client.exe";
        openFileDialog.Filter = "Client.exe|Client.exe";
        openFileDialog.Title = "Find your UO directory";
        openFileDialog.InitialDirectory = Path.GetPathRoot(this.m_BasePath);
        if (openFileDialog.ShowDialog() == DialogResult.OK)
          return Path.GetDirectoryName(openFileDialog.FileName);
        return (string) null;
      }
    }

    public void Dispose()
    {
    }

    public string BasePath(string Path)
    {
      return Path.Combine(this.m_BasePath, Path);
    }

    public Stream OpenMUL(Files File)
    {
      return this.OpenRead(Path.Combine(this.m_FilePath, Config.GetFile((int) File)));
    }

    public Stream OpenMUL(string Path)
    {
      return this.OpenRead(Path.Combine(this.m_FilePath, Path));
    }

    protected Stream OpenRead(string Path)
    {
      return (Stream) File.OpenRead(Path);
    }
  }
}

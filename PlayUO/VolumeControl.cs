// Decompiled with JetBrains decompiler
// Type: PlayUO.VolumeControl
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Microsoft.Win32;

namespace PlayUO
{
  public class VolumeControl
  {
    private static int m_Music = int.MinValue;
    private static int m_Sound = int.MinValue;

    public static int Sound
    {
      get
      {
        if (VolumeControl.m_Sound == int.MinValue)
        {
          VolumeControl.m_Sound = 100;
          try
          {
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KUOC"))
            {
              if (registryKey != null)
                VolumeControl.m_Sound = (int) registryKey.GetValue("Sound Volume", (object) 100);
            }
          }
          catch
          {
          }
        }
        return VolumeControl.m_Sound;
      }
      set
      {
        if (VolumeControl.m_Sound == value)
          return;
        VolumeControl.m_Sound = value;
        try
        {
          RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KUOC", true) ?? Registry.LocalMachine.CreateSubKey("SOFTWARE\\KUOC");
          registryKey.SetValue("Sound Volume", (object) value);
          registryKey.Close();
        }
        catch
        {
        }
      }
    }

    public static int Music
    {
      get
      {
        if (VolumeControl.m_Music == int.MinValue)
        {
          VolumeControl.m_Music = 100;
          try
          {
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KUOC"))
            {
              if (registryKey != null)
                VolumeControl.m_Music = (int) registryKey.GetValue("Music Volume", (object) 100);
            }
          }
          catch
          {
          }
        }
        return VolumeControl.m_Music;
      }
      set
      {
        if (VolumeControl.m_Music == value)
          return;
        VolumeControl.m_Music = value;
        try
        {
          RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KUOC", true) ?? Registry.LocalMachine.CreateSubKey("SOFTWARE\\KUOC");
          registryKey.SetValue("Music Volume", (object) value);
          registryKey.Close();
        }
        catch
        {
        }
      }
    }
  }
}

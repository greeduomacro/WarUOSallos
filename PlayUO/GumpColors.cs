// Decompiled with JetBrains decompiler
// Type: PlayUO.GumpColors
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Microsoft.Win32;
using System.Drawing;

namespace PlayUO
{
  public class GumpColors
  {
    private static int m_ControlAlternate = -1;
    private static int m_ActiveCaptionGradient = -1;
    private static int m_InactiveCaptionGradient = -1;

    public static int Info
    {
      get
      {
        return SystemColors.Info.ToArgb() & 16777215;
      }
    }

    public static int Menu
    {
      get
      {
        return SystemColors.Menu.ToArgb() & 16777215;
      }
    }

    public static int Window
    {
      get
      {
        return SystemColors.Window.ToArgb() & 16777215;
      }
    }

    public static int Control
    {
      get
      {
        return SystemColors.Control.ToArgb() & 16777215;
      }
    }

    public static int Desktop
    {
      get
      {
        return SystemColors.Desktop.ToArgb() & 16777215;
      }
    }

    public static int GrayText
    {
      get
      {
        return SystemColors.GrayText.ToArgb() & 16777215;
      }
    }

    public static int HotTrack
    {
      get
      {
        return SystemColors.HotTrack.ToArgb() & 16777215;
      }
    }

    public static int InfoText
    {
      get
      {
        return SystemColors.InfoText.ToArgb() & 16777215;
      }
    }

    public static int MenuText
    {
      get
      {
        return SystemColors.MenuText.ToArgb() & 16777215;
      }
    }

    public static int Highlight
    {
      get
      {
        return SystemColors.Highlight.ToArgb() & 16777215;
      }
    }

    public static int ScrollBar
    {
      get
      {
        return SystemColors.ScrollBar.ToArgb() & 16777215;
      }
    }

    public static int WindowText
    {
      get
      {
        return SystemColors.WindowText.ToArgb() & 16777215;
      }
    }

    public static int ControlDark
    {
      get
      {
        return SystemColors.ControlDark.ToArgb() & 16777215;
      }
    }

    public static int ControlText
    {
      get
      {
        return SystemColors.ControlText.ToArgb() & 16777215;
      }
    }

    public static int WindowFrame
    {
      get
      {
        return SystemColors.WindowFrame.ToArgb() & 16777215;
      }
    }

    public static int ActiveBorder
    {
      get
      {
        return SystemColors.ActiveBorder.ToArgb() & 16777215;
      }
    }

    public static int AppWorkspace
    {
      get
      {
        return SystemColors.AppWorkspace.ToArgb() & 16777215;
      }
    }

    public static int ControlLight
    {
      get
      {
        return SystemColors.ControlLight.ToArgb() & 16777215;
      }
    }

    public static int ActiveCaption
    {
      get
      {
        return SystemColors.ActiveCaption.ToArgb() & 16777215;
      }
    }

    public static int HighlightText
    {
      get
      {
        return SystemColors.HighlightText.ToArgb() & 16777215;
      }
    }

    public static int InactiveBorder
    {
      get
      {
        return SystemColors.InactiveBorder.ToArgb() & 16777215;
      }
    }

    public static int ControlDarkDark
    {
      get
      {
        return SystemColors.ControlDarkDark.ToArgb() & 16777215;
      }
    }

    public static int InactiveCaption
    {
      get
      {
        return SystemColors.InactiveCaption.ToArgb() & 16777215;
      }
    }

    public static int ActiveCaptionText
    {
      get
      {
        return SystemColors.ActiveCaptionText.ToArgb() & 16777215;
      }
    }

    public static int ControlLightLight
    {
      get
      {
        return SystemColors.ControlLightLight.ToArgb() & 16777215;
      }
    }

    public static int InactiveCaptionText
    {
      get
      {
        return SystemColors.InactiveCaptionText.ToArgb() & 16777215;
      }
    }

    public static int ControlAlternate
    {
      get
      {
        if (GumpColors.m_ControlAlternate >= 0)
          return GumpColors.m_ControlAlternate;
        System.Drawing.Color color = GumpColors.ReadRegistryColor("ButtonAlternateFace");
        return GumpColors.m_ControlAlternate = color.ToArgb() & 16777215;
      }
    }

    public static int ActiveCaptionGradient
    {
      get
      {
        if (GumpColors.m_ActiveCaptionGradient >= 0)
          return GumpColors.m_ActiveCaptionGradient;
        System.Drawing.Color color = GumpColors.ReadRegistryColor("GradientActiveTitle");
        return GumpColors.m_ActiveCaptionGradient = color.ToArgb() & 16777215;
      }
    }

    public static int InactiveCaptionGradient
    {
      get
      {
        if (GumpColors.m_InactiveCaptionGradient >= 0)
          return GumpColors.m_InactiveCaptionGradient;
        System.Drawing.Color color = GumpColors.ReadRegistryColor("GradientInactiveTitle");
        return GumpColors.m_InactiveCaptionGradient = color.ToArgb() & 16777215;
      }
    }

    public static void Invalidate()
    {
      GumpColors.m_ControlAlternate = -1;
      GumpColors.m_ActiveCaptionGradient = -1;
      GumpColors.m_InactiveCaptionGradient = -1;
    }

    private static System.Drawing.Color ReadRegistryColor(string name)
    {
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Colors", false))
        {
          string[] strArray = (registryKey.GetValue(name) as string).Split(' ');
          return System.Drawing.Color.FromArgb(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]));
        }
      }
      catch
      {
      }
      return System.Drawing.Color.White;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GumpHues
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GumpHues
  {
    private static IHue[] m_Hues = new IHue[27];

    public static IHue Info
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Info, 0);
      }
    }

    public static IHue Menu
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Menu, 1);
      }
    }

    public static IHue Window
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Window, 2);
      }
    }

    public static IHue Control
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Control, 3);
      }
    }

    public static IHue Desktop
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Desktop, 4);
      }
    }

    public static IHue GrayText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.GrayText, 5);
      }
    }

    public static IHue HotTrack
    {
      get
      {
        return GumpHues.GetHue(GumpColors.HotTrack, 6);
      }
    }

    public static IHue InfoText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.InfoText, 7);
      }
    }

    public static IHue MenuText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.MenuText, 8);
      }
    }

    public static IHue Highlight
    {
      get
      {
        return GumpHues.GetHue(GumpColors.Highlight, 9);
      }
    }

    public static IHue ScrollBar
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ScrollBar, 10);
      }
    }

    public static IHue WindowText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.WindowText, 11);
      }
    }

    public static IHue ControlDark
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlDark, 12);
      }
    }

    public static IHue ControlText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlText, 13);
      }
    }

    public static IHue WindowFrame
    {
      get
      {
        return GumpHues.GetHue(GumpColors.WindowFrame, 14);
      }
    }

    public static IHue ActiveBorder
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ActiveBorder, 15);
      }
    }

    public static IHue AppWorkspace
    {
      get
      {
        return GumpHues.GetHue(GumpColors.AppWorkspace, 16);
      }
    }

    public static IHue ControlLight
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlLight, 17);
      }
    }

    public static IHue ActiveCaption
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ActiveCaption, 18);
      }
    }

    public static IHue HighlightText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.HighlightText, 19);
      }
    }

    public static IHue InactiveBorder
    {
      get
      {
        return GumpHues.GetHue(GumpColors.InactiveBorder, 20);
      }
    }

    public static IHue ControlDarkDark
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlDarkDark, 21);
      }
    }

    public static IHue InactiveCaption
    {
      get
      {
        return GumpHues.GetHue(GumpColors.InactiveCaption, 22);
      }
    }

    public static IHue ActiveCaptionText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ActiveCaptionText, 23);
      }
    }

    public static IHue ControlLightLight
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlLightLight, 24);
      }
    }

    public static IHue InactiveCaptionText
    {
      get
      {
        return GumpHues.GetHue(GumpColors.InactiveCaptionText, 25);
      }
    }

    public static IHue ControlAlternate
    {
      get
      {
        return GumpHues.GetHue(GumpColors.ControlAlternate, 26);
      }
    }

    public static void Invalidate()
    {
      for (int index = 0; index < GumpHues.m_Hues.Length; ++index)
        GumpHues.m_Hues[index] = (IHue) null;
    }

    public static IHue GetHue(int c, int idx)
    {
      if (GumpHues.m_Hues[idx] == null)
        GumpHues.m_Hues[idx] = (IHue) new Hues.ColorFillHue(c);
      return GumpHues.m_Hues[idx];
    }
  }
}

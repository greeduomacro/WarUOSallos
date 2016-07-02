// Decompiled with JetBrains decompiler
// Type: PlayUO.GHyperLink
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class GHyperLink : GTextButton
  {
    private static IHue RegularHue = (IHue) new Hues.ColorFillHue((int) byte.MaxValue);
    private static IHue VisitedHue = (IHue) new Hues.ColorFillHue(16711680);
    private static Hashtable m_Visited = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
    private string m_Url;

    public GHyperLink(string url, string text, IFont font, int x, int y)
      : base(text, font, GHyperLink.m_Visited.Contains((object) url) ? GHyperLink.VisitedHue : GHyperLink.RegularHue, GHyperLink.m_Visited.Contains((object) url) ? GHyperLink.VisitedHue : GHyperLink.RegularHue, x, y, (OnClick) null)
    {
      this.Underline = true;
      this.m_Url = url;
      this.OnClick = new OnClick(this.Button_OnClick);
    }

    private void Button_OnClick(Gump g)
    {
      Engine.OpenBrowser(this.m_Url);
      GHyperLink.m_Visited[(object) this.m_Url] = (object) true;
      this.DefaultHue = this.FocusHue = GHyperLink.VisitedHue;
    }
  }
}

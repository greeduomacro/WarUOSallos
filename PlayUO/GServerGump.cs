// Decompiled with JetBrains decompiler
// Type: PlayUO.GServerGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace PlayUO
{
  public class GServerGump : Gump
  {
    private static Hashtable m_LocationCache = new Hashtable();
    private static char[] m_FirstChars = new char[2]{ '°', 'o' };
    private static char[] m_SecondChars = new char[1]{ '\'' };
    private ArrayList m_AlphaRegions = new ArrayList();
    private bool m_CanClose;
    private bool m_CanMove;
    private Hashtable m_Pages;
    private int m_Page;
    private int m_Serial;
    private int m_DialogID;
    private LayoutEntry[] m_Entries;
    private string[] m_Text;

    public bool CanClose
    {
      get
      {
        return this.m_CanClose;
      }
    }

    public bool CanMove
    {
      get
      {
        return this.m_CanMove;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public int DialogID
    {
      get
      {
        return this.m_DialogID;
      }
    }

    public int Page
    {
      get
      {
        return this.m_Page;
      }
      set
      {
        if (this.m_Page == value)
          return;
        this.m_Page = value;
        this.m_Children.Set(this.Pages(0));
        if (this.m_Page != 0)
          this.m_Children.Add(this.Pages(this.m_Page));
        this.m_AlphaRegions.Clear();
        for (int index = 0; index < this.m_Children.Count; ++index)
        {
          if (this.m_Children[index] is GTransparentRegion)
            this.m_AlphaRegions.Add((object) this.m_Children[index]);
        }
      }
    }

    public GServerGump(int serial, int dialogID, int x, int y, string layout, string[] text)
      : base(x, y)
    {
      this.m_Serial = serial;
      this.m_DialogID = dialogID;
      this.m_CanClose = true;
      this.m_CanMove = true;
      this.m_NonRestrictivePicking = true;
      this.m_Pages = new Hashtable();
      this.m_Page = -1;
      LayoutEntry[] layout1 = this.ParseLayout(layout);
      this.ProcessLayout(layout1, text);
      this.m_Text = text;
      this.m_Entries = layout1;
    }

    public static void GetCachedLocation(int dialogID, ref int x, ref int y)
    {
      GServerGump.LocationCacheEntry locationCacheEntry = GServerGump.m_LocationCache[(object) dialogID] as GServerGump.LocationCacheEntry;
      if (locationCacheEntry == null)
        return;
      x = locationCacheEntry.m_xOffset;
      y = locationCacheEntry.m_yOffset;
      GServerGump.m_LocationCache.Remove((object) dialogID);
    }

    public static void SetCachedLocation(int dialogID, int x, int y)
    {
      GServerGump.LocationCacheEntry locationCacheEntry = GServerGump.m_LocationCache[(object) dialogID] as GServerGump.LocationCacheEntry;
      if (locationCacheEntry == null)
        GServerGump.m_LocationCache[(object) dialogID] = (object) (locationCacheEntry = new GServerGump.LocationCacheEntry(dialogID));
      locationCacheEntry.m_xOffset = x;
      locationCacheEntry.m_yOffset = y;
    }

    public static void ClearCachedLocation(int dialogID)
    {
      GServerGump.m_LocationCache.Remove((object) dialogID);
    }

    protected internal override void Render(int X, int Y)
    {
      if (this.m_AlphaRegions.Count == 0)
      {
        base.Render(X, Y);
      }
      else
      {
        if (!this.m_Visible)
          return;
        int X1 = X + this.X;
        int Y1 = Y + this.Y;
        this.Draw(X1, Y1);
        Gump[] array = this.m_Children.ToArray();
        RectangleList rectangleList1 = new RectangleList();
        RectangleList rectangleList2 = new RectangleList();
        int num = 0;
        for (int index1 = 0; index1 < array.Length; ++index1)
        {
          Gump gump1 = array[index1];
          if (gump1 is GTransparentRegion)
            ++num;
          else if (num >= this.m_AlphaRegions.Count)
          {
            gump1.Render(X1, Y1);
          }
          else
          {
            RectangleList rectangleList3 = rectangleList1;
            Rectangle b = new Rectangle(gump1.X, gump1.Y, gump1.Width, gump1.Height);
            Rectangle rect1 = b;
            rectangleList3.Add(rect1);
            for (int index2 = num; index2 < this.m_AlphaRegions.Count; ++index2)
            {
              Gump gump2 = (Gump) this.m_AlphaRegions[index2];
              if (gump2 is GTransparentRegion)
              {
                Rectangle rect2 = Rectangle.Intersect(new Rectangle(gump2.X, gump2.Y, gump2.Width, gump2.Height), b);
                rectangleList1.Remove(rect2);
                rectangleList2.Add(rect2);
              }
            }
            if (rectangleList2.Count > 0)
            {
              for (int index2 = index1 + 1; index2 < array.Length; ++index2)
              {
                Gump gump2 = array[index2];
                if (gump2 is GServerBackground)
                {
                  GServerBackground gserverBackground = (GServerBackground) gump2;
                  Rectangle rect2 = new Rectangle(gserverBackground.X + gserverBackground.OffsetX, gserverBackground.Y + gserverBackground.OffsetY, gserverBackground.UseWidth, gserverBackground.UseHeight);
                  rectangleList1.Remove(rect2);
                  rectangleList2.Remove(rect2);
                }
                else if (gump2 == this.m_AlphaRegions[this.m_AlphaRegions.Count - 1])
                  break;
              }
              if (rectangleList2.Count == 1 && rectangleList1.Count == 0)
              {
                Renderer.PushAlpha(0.5f);
                gump1.Render(X1, Y1);
                Renderer.PopAlpha();
              }
              else
              {
                for (int index2 = 0; index2 < rectangleList1.Count; ++index2)
                {
                  Rectangle rectangle = rectangleList1[index2];
                  if (Renderer.SetViewport(X1 + rectangle.X, Y1 + rectangle.Y, rectangle.Width, rectangle.Height))
                    gump1.Render(X1, Y1);
                }
                for (int index2 = 0; index2 < rectangleList2.Count; ++index2)
                {
                  Rectangle rectangle = rectangleList2[index2];
                  if (Renderer.SetViewport(X1 + rectangle.X, Y1 + rectangle.Y, rectangle.Width, rectangle.Height))
                  {
                    Renderer.PushAlpha(0.5f);
                    gump1.Render(X1, Y1);
                    Renderer.PopAlpha();
                  }
                }
                if (rectangleList1.Count > 0 || rectangleList2.Count > 0)
                  Renderer.SetViewport(0, 0, Engine.ScreenWidth, Engine.ScreenHeight);
              }
              rectangleList1.Clear();
              rectangleList2.Clear();
            }
            else
              gump1.Render(X1, Y1);
          }
        }
      }
    }

    public string GetValue(int index)
    {
      if (index < 0 || index >= this.m_Entries.Length)
        return (string) null;
      LayoutEntry layoutEntry = this.m_Entries[index];
      switch (layoutEntry.Name)
      {
        case "text":
          return this.m_Text[layoutEntry[3]];
        case "croppedtext":
          return this.m_Text[layoutEntry[5]];
        case "xmfhtmlgump":
        case "xmfhtmlgumpcolor":
          return "#" + (object) layoutEntry[4];
        case "htmlgump":
          return this.m_Text[layoutEntry[4]];
        default:
          string str = layoutEntry.Name;
          for (int index1 = 0; index1 < layoutEntry.Parameters.Length; ++index1)
            str = str + " " + (object) layoutEntry.Parameters[index1];
          return str;
      }
    }

    public GumpList Pages(int page)
    {
      return (GumpList) (this.m_Pages[(object) page] ?? (this.m_Pages[(object) page] = (object) new GumpList((Gump) this)));
    }

    public static Point3D ReverseLookup(int f, int xLong, int yLat, int xMins, int yMins, bool xEast, bool ySouth)
    {
      int xCenter;
      int yCenter;
      int xWidth;
      int yHeight;
      if (!GServerGump.ComputeMapDetails(f, 0, 0, out xCenter, out yCenter, out xWidth, out yHeight))
        return new Point3D(0, 0, 0);
      double num1 = (double) xLong + (double) xMins / 60.0;
      double num2 = (double) yLat + (double) yMins / 60.0;
      if (!xEast)
        num1 = 360.0 - num1;
      if (!ySouth)
        num2 = 360.0 - num2;
      int x = xCenter + (int) (num1 * (double) xWidth / 360.0);
      int y = yCenter + (int) (num2 * (double) yHeight / 360.0);
      if (x < 0)
        x += xWidth;
      else if (x >= xWidth)
        x -= xWidth;
      if (y < 0)
        y += yHeight;
      else if (y >= yHeight)
        y -= yHeight;
      int z = 0;
      return new Point3D(x, y, z);
    }

    public static bool ComputeMapDetails(int facet, int x, int y, out int xCenter, out int yCenter, out int xWidth, out int yHeight)
    {
      xWidth = 5120;
      yHeight = 4096;
      if (facet < 2)
      {
        if (x >= 0 && y >= 0 && (x < 5120 && y < 4096))
        {
          xCenter = 1323;
          yCenter = 1624;
        }
        else if (x >= 5120 && y >= 2304 && (x < 6144 && y < 4096))
        {
          xCenter = 5936;
          yCenter = 3112;
        }
        else
        {
          xCenter = 0;
          yCenter = 0;
          return false;
        }
      }
      else
      {
        xCenter = 1323;
        yCenter = 1624;
      }
      return true;
    }

    private bool ParseCoord(string format, out int degrees, out int minutes, out bool direction)
    {
      try
      {
        int startIndex1 = 0;
        int num1 = format.IndexOfAny(GServerGump.m_FirstChars, startIndex1);
        degrees = int.Parse(format.Substring(startIndex1, num1 - startIndex1));
        int startIndex2 = num1 + 1;
        int num2 = format.IndexOfAny(GServerGump.m_SecondChars, startIndex2);
        minutes = int.Parse(format.Substring(startIndex2, num2 - startIndex2));
        char ch = format[format.Length - 1];
        direction = (int) ch == 83 || (int) ch == 69;
        return true;
      }
      catch
      {
        degrees = 0;
        minutes = 0;
        direction = false;
        Debug.Trace("failed to parse -> {0}", (object) format);
        return false;
      }
    }

    private LayoutEntry[] ParseLayout(string layout)
    {
      using (ScratchList<LayoutEntry> scratchList = new ScratchList<LayoutEntry>())
      {
        List<LayoutEntry> layoutEntryList = scratchList.Value;
        int startIndex1 = 0;
        int num;
        while ((num = layout.IndexOf('{', startIndex1)) >= 0)
        {
          int startIndex2 = num + 1;
          startIndex1 = layout.IndexOf('}', startIndex2);
          layoutEntryList.Add(new LayoutEntry(layout.Substring(startIndex2, startIndex1 - startIndex2).Trim()));
        }
        return layoutEntryList.ToArray();
      }
    }

    private void ProcessLayout(LayoutEntry[] list, string[] text)
    {
      int num1 = 0;
      int num2 = 0;
      bool flag = false;
      for (int index = 0; index < list.Length; ++index)
      {
        LayoutEntry le = list[index];
        switch (le.Name)
        {
          case "nodispose":
          case "noresize":
            goto case "nodispose";
          case "checkertrans":
            this.Pages(num1).Add((Gump) new GTransparentRegion(this, le[0], le[1], le[2], le[3]));
            goto case "nodispose";
          case "page":
            num1 = le[0];
            if (num1 == 0)
            {
              flag = false;
              num2 = 0;
              goto case "nodispose";
            }
            else if (!flag || num1 < num2)
            {
              num2 = num1;
              flag = true;
              goto case "nodispose";
            }
            else
              goto case "nodispose";
          case "noclose":
            this.m_CanClose = false;
            goto case "nodispose";
          case "nomove":
            this.m_CanMove = false;
            goto case "nodispose";
          case "resizepic":
            this.Pages(num1).Add((Gump) new GServerBackground(this, le[0], le[1], le[3], le[4], le[2] + 4, true));
            goto case "nodispose";
          case "gumppictiled":
            this.Pages(num1).Add((Gump) new GServerBackground(this, le[0], le[1], le[2], le[3], le[4], false));
            goto case "nodispose";
          case "gumppic":
            string attribute = le.GetAttribute("hue");
            IHue hue;
            if (attribute != null)
            {
              try
              {
                hue = Hues.Load(Convert.ToInt32(attribute) + 1);
              }
              catch
              {
                hue = Hues.Default;
              }
            }
            else
              hue = Hues.Default;
            if (le.GetAttribute("class") == "VirtueGumpItem")
            {
              this.Pages(num1).Add((Gump) new GVirtueItem(this, le[0], le[1], le[2], hue));
              goto case "nodispose";
            }
            else
            {
              this.Pages(num1).Add((Gump) new GServerImage(this, le[0], le[1], le[2], hue));
              goto case "nodispose";
            }
          case "textentry":
            this.Pages(num1).Add((Gump) new GServerTextBox(text[le[6]], le));
            goto case "nodispose";
          case "tilepic":
            this.Pages(num1).Add((Gump) new GItemArt(le[0], le[1], le[2]));
            goto case "nodispose";
          case "tilepichue":
            this.Pages(num1).Add((Gump) new GItemArt(le[0], le[1], le[2], Hues.GetItemHue(le[2], le[3] + 1)));
            goto case "nodispose";
          case "button":
            this.Pages(num1).Add((Gump) new GServerButton(this, le));
            goto case "nodispose";
          case "radio":
            this.Pages(num1).Add((Gump) new GServerRadio(this, le));
            goto case "nodispose";
          case "checkbox":
            this.Pages(num1).Add((Gump) new GServerCheckBox(this, le));
            goto case "nodispose";
          case "text":
            int num3 = le[2];
            this.Pages(num1).Add((Gump) new GLabel(text[le[3]], (IFont) Engine.GetUniFont(1), Hues.Load(num3 + 1), le[0] - 1, le[1]));
            goto case "nodispose";
          case "croppedtext":
            int num4 = le[4];
            string str = text[le[5]];
            int xWidth = le[2];
            IFont Font = (IFont) Engine.GetUniFont(1);
            if (Font.GetStringWidth(str) > xWidth)
            {
              while (str.Length > 0 && Font.GetStringWidth(str + "...") > xWidth)
                str = str.Substring(0, str.Length - 1);
              str += "...";
            }
            GLabel glabel = new GLabel(str, Font, Hues.Load(num4 + 1), le[0] - 1, le[1]);
            glabel.Scissor(0, 0, xWidth, le[3]);
            this.Pages(num1).Add((Gump) glabel);
            goto case "nodispose";
          case "xmfhtmlgump":
            this.ProcessHtmlGump(num1, le[0], le[1], le[2], le[3], Localization.GetString(le[4]), le[5] != 0, le[6] != 0, le[5] != 0 || le[6] == 0 ? 0 : 16777215);
            goto case "nodispose";
          case "xmfhtmlgumpcolor":
            this.ProcessHtmlGump(num1, le[0], le[1], le[2], le[3], Localization.GetString(le[4]), le[5] != 0, le[6] != 0, Engine.C16232(le[7]));
            goto case "nodispose";
          case "htmlgump":
            this.ProcessHtmlGump(num1, le[0], le[1], le[2], le[3], text[le[4]], le[5] != 0, le[6] != 0, le[5] != 0 || le[6] == 0 ? 0 : 16777215);
            goto case "nodispose";
          default:
            Engine.AddTextMessage(le.Name);
            goto case "nodispose";
        }
      }
      this.Page = num2 == 0 ? 1 : num2;
    }

    private void OnScroll(double vNew, double vOld, Gump g)
    {
      int y = (int) vNew;
      Gump gump = (Gump) g.GetTag("toScroll");
      int num = (int) g.GetTag("yBase");
      int h = (int) g.GetTag("viewHeight");
      gump.Y = num - y;
      ((GHtmlLabel) gump).Scissor(0, y, gump.Width, h);
    }

    private void ProcessHtmlGump(int thisPage, int x, int y, int width, int height, string text, bool hasBack, bool hasScroll, int color)
    {
      UnicodeFont uniFont = Engine.GetUniFont(1);
      if (!hasScroll)
      {
        if (hasBack)
        {
          GServerBackground gserverBackground = new GServerBackground(this, x, y, width, height, 3004, true);
          GHtmlLabel ghtmlLabel = new GHtmlLabel(text, uniFont, color, gserverBackground.OffsetX - 2, gserverBackground.OffsetY - 1, gserverBackground.UseWidth);
          ghtmlLabel.Scissor(0, 0, ghtmlLabel.Width, gserverBackground.UseHeight);
          gserverBackground.Children.Add((Gump) ghtmlLabel);
          this.Pages(thisPage).Add((Gump) gserverBackground);
        }
        else
        {
          GHtmlLabel ghtmlLabel = new GHtmlLabel(text, uniFont, color, x - 2, y - 1, width);
          ghtmlLabel.Scissor(0, 0, ghtmlLabel.Width, height);
          this.Pages(thisPage).Add((Gump) ghtmlLabel);
        }
      }
      else
      {
        width -= 15;
        GHtmlLabel ghtmlLabel;
        int num;
        if (hasBack)
        {
          GServerBackground gserverBackground = new GServerBackground(this, x, y, width, height, 3004, true);
          ghtmlLabel = new GHtmlLabel(text, uniFont, color, gserverBackground.OffsetX - 2, gserverBackground.OffsetY - 1, gserverBackground.UseWidth);
          ghtmlLabel.Scissor(0, 0, ghtmlLabel.Width, num = gserverBackground.UseHeight);
          gserverBackground.Children.Add((Gump) ghtmlLabel);
          this.Pages(thisPage).Add((Gump) gserverBackground);
        }
        else
        {
          ghtmlLabel = new GHtmlLabel(text, uniFont, color, x - 2, y - 1, width);
          ghtmlLabel.Scissor(0, 0, ghtmlLabel.Width, num = height);
          this.Pages(thisPage).Add((Gump) ghtmlLabel);
        }
        if (height >= 27 && ghtmlLabel.Height > num)
        {
          this.Pages(thisPage).Add((Gump) new GImage(257, x + width, y));
          this.Pages(thisPage).Add((Gump) new GImage((int) byte.MaxValue, x + width, y + height - 32));
          int y1 = y + 30;
          while (y1 + 32 < y + height)
          {
            this.Pages(thisPage).Add((Gump) new GImage(256, x + width, y1));
            y1 += 30;
          }
          GVSlider gvSlider = new GVSlider(254, x + width + 1, y + 1 + 12, 13, height - 2 - 24, 0.0, 0.0, (double) (ghtmlLabel.Height - num), 1.0);
          gvSlider.SetTag("yBase", (object) ghtmlLabel.Y);
          gvSlider.SetTag("toScroll", (object) ghtmlLabel);
          gvSlider.SetTag("viewHeight", (object) num);
          gvSlider.OnValueChange = new OnValueChange(this.OnScroll);
          this.Pages(thisPage).Add((Gump) gvSlider);
          this.Pages(thisPage).Add((Gump) new GHotspot(x + width, y, 15, height, (Gump) gvSlider));
        }
        else
        {
          this.Pages(thisPage).Add((Gump) new GImage(257, x + width, y));
          this.Pages(thisPage).Add((Gump) new GImage((int) byte.MaxValue, x + width, y + height - 32));
          int y1 = y + 30;
          while (y1 + 32 < y + height)
          {
            this.Pages(thisPage).Add((Gump) new GImage(256, x + width, y1));
            y1 += 30;
          }
          this.Pages(thisPage).Add((Gump) new GImage(254, Hues.Grayscale, x + width + 1, y + 1));
        }
      }
    }

    private class LocationCacheEntry
    {
      public int m_DialogID;
      public int m_xOffset;
      public int m_yOffset;

      public LocationCacheEntry(int dialogID)
      {
        this.m_DialogID = dialogID;
      }
    }
  }
}

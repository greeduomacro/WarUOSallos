// Decompiled with JetBrains decompiler
// Type: PlayUO.GHtmlLabel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class GHtmlLabel : Gump
  {
    private static object[,] m_ColorTable = new object[24, 2]{ { (object) "black", (object) 0 }, { (object) "red", (object) 16711680 }, { (object) "lime", (object) 65280 }, { (object) "blue", (object) (int) byte.MaxValue }, { (object) "yellow", (object) 16776960 }, { (object) "magenta", (object) 16711935 }, { (object) "fuchsia", (object) 16711935 }, { (object) "cyan", (object) (int) ushort.MaxValue }, { (object) "aqua", (object) (int) ushort.MaxValue }, { (object) "white", (object) 16777215 }, { (object) "darkred", (object) 8323072 }, { (object) "maroon", (object) 8323072 }, { (object) "green", (object) 32512 }, { (object) "darkgreen", (object) 23040 }, { (object) "darkblue", (object) 32512 }, { (object) "navy", (object) 32512 }, { (object) "darkyellow", (object) 8355584 }, { (object) "olive", (object) 8355584 }, { (object) "darkmagenta", (object) 8323199 }, { (object) "purple", (object) 8323199 }, { (object) "darkcyan", (object) 32639 }, { (object) "teal", (object) 32639 }, { (object) "gray", (object) 8355711 }, { (object) "grey", (object) 8355711 } };
    private int m_Width;
    private int m_Height;

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
      }
    }

    public GHtmlLabel(string text, UnicodeFont initialFont, int initialColor, int x, int y, int width)
      : base(x, y)
    {
      this.m_Width = width;
      this.Update(text, initialFont, initialColor);
    }

    public void Update(string text, UnicodeFont initialFont, int initialColor)
    {
      int width1 = this.m_Width;
      Stack stack1 = new Stack();
      FontInfo fontInfo1 = new FontInfo(initialFont, initialColor);
      int num1 = 0;
      int num2 = 0;
      Stack stack2 = new Stack();
      Stack stack3 = new Stack();
      text = text.Replace("\r", "");
      text = text.Replace("\n", "<br>");
      HtmlElement[] elements = HtmlElement.GetElements(text);
      int num3 = 0;
      int num4 = 0;
      for (int index1 = 0; index1 < elements.Length; ++index1)
      {
        HtmlElement htmlElement = elements[index1];
        FontInfo fontInfo2 = stack1.Count > 0 ? (FontInfo) stack1.Peek() : fontInfo1;
        HtmlAlignment htmlAlignment = stack2.Count > 0 ? (HtmlAlignment) stack2.Peek() : HtmlAlignment.Normal;
        string url = stack3.Count > 0 ? (string) stack3.Peek() : (string) null;
        switch (htmlElement.Type)
        {
          case ElementType.Text:
            string text1 = htmlElement.Name;
            bool flag = false;
            while (text1.Length > 0)
            {
              int num5 = num3;
              switch (htmlAlignment & (HtmlAlignment) 255)
              {
                case HtmlAlignment.Center:
                  int stringWidth1 = fontInfo2.Font.GetStringWidth(text1);
                  if (stringWidth1 > width1)
                  {
                    string[] strArray = Engine.WrapText(text1, width1, (IFont) fontInfo2.Font).Split('\n');
                    stringWidth1 = fontInfo2.Font.GetStringWidth(strArray[0]);
                  }
                  num5 = (width1 - (stringWidth1 - 1) + 1) / 2;
                  break;
                case HtmlAlignment.Left:
                  num5 = (int) htmlAlignment >> 8;
                  break;
                case HtmlAlignment.Right:
                  int stringWidth2 = fontInfo2.Font.GetStringWidth(text1);
                  if (stringWidth2 > width1)
                  {
                    string[] strArray = Engine.WrapText(text1, width1, (IFont) fontInfo2.Font).Split('\n');
                    stringWidth2 = fontInfo2.Font.GetStringWidth(strArray[0]);
                  }
                  num5 = ((int) htmlAlignment >> 8) - stringWidth2;
                  break;
              }
              string[] strArray1 = text1.Split(' ');
              int width2 = width1 - num5;
              if (!flag && fontInfo2.Font.GetStringWidth(strArray1[0]) > width2)
              {
                flag = true;
                num3 = 0;
                num4 += 18;
              }
              else
              {
                flag = false;
                string[] strArray2 = Engine.WrapText(text1, width2, (IFont) fontInfo2.Font).Split('\n');
                string str = strArray2[0];
                if (strArray2.Length > 1)
                  str = str.TrimEnd();
                GLabel glabel = url == null ? new GLabel(str, (IFont) fontInfo2.Font, fontInfo2.Hue, num5, num4) : (GLabel) new GHyperLink(url, str, (IFont) fontInfo2.Font, num5, num4);
                if (url == null)
                  glabel.Underline = num1 > 0;
                this.m_Children.Add((Gump) glabel);
                if (strArray2.Length > 1)
                {
                  text1 = text1.Remove(0, strArray2[0].Length);
                  num3 = 0;
                  num4 += 18;
                }
                else
                {
                  num3 = glabel.X + glabel.Width - 1;
                  break;
                }
              }
            }
            break;
          case ElementType.Start:
            switch (htmlElement.Name.ToLower())
            {
              case "br":
                num3 = 0;
                num4 += 18;
                continue;
              case "u":
                ++num1;
                continue;
              case "i":
                ++num2;
                continue;
              case "a":
                string attribute1 = htmlElement.GetAttribute("href");
                if (attribute1 != null && !attribute1.StartsWith("?"))
                {
                  stack3.Push((object) attribute1);
                  continue;
                }
                continue;
              case "basefont":
                string attribute2 = htmlElement.GetAttribute("color");
                if (attribute2 != null)
                {
                  int color = 0;
                  if (attribute2.StartsWith("#"))
                  {
                    color = Convert.ToInt32(attribute2.Substring(1), 16);
                  }
                  else
                  {
                    for (int index2 = 0; index2 < GHtmlLabel.m_ColorTable.GetLength(0); ++index2)
                    {
                      if (attribute2.ToLower() == (string) GHtmlLabel.m_ColorTable[index2, 0])
                      {
                        color = (int) GHtmlLabel.m_ColorTable[index2, 1];
                        break;
                      }
                    }
                  }
                  stack1.Push((object) new FontInfo(fontInfo2.Font, color));
                  continue;
                }
                continue;
              case "center":
                stack2.Push((object) HtmlAlignment.Center);
                continue;
              case "div":
                string attribute3 = htmlElement.GetAttribute("align");
                if (attribute3 == null)
                {
                  string attribute4 = htmlElement.GetAttribute("alignleft");
                  if (attribute4 != null)
                  {
                    try
                    {
                      int num5 = int.Parse(attribute4);
                      stack2.Push((object) (HtmlAlignment) (2 | num5 << 8));
                    }
                    catch
                    {
                    }
                  }
                  string attribute5 = htmlElement.GetAttribute("alignright");
                  if (attribute5 != null)
                  {
                    try
                    {
                      int num5 = int.Parse(attribute5);
                      stack2.Push((object) (HtmlAlignment) (3 | num5 << 8));
                      continue;
                    }
                    catch
                    {
                      continue;
                    }
                  }
                  else
                    continue;
                }
                else
                {
                  switch (attribute3.ToLower())
                  {
                    case "center":
                      stack2.Push((object) HtmlAlignment.Center);
                      continue;
                    case "right":
                      stack2.Push((object) (HtmlAlignment) (3 | width1 << 8));
                      continue;
                    case "left":
                      stack2.Push((object) HtmlAlignment.Left);
                      continue;
                    default:
                      continue;
                  }
                }
              default:
                continue;
            }
          case ElementType.End:
            switch (htmlElement.Name.ToLower())
            {
              case "u":
                --num1;
                if (num1 < 0)
                {
                  num1 = 0;
                  continue;
                }
                continue;
              case "i":
                --num2;
                if (num2 < 0)
                {
                  num2 = 0;
                  continue;
                }
                continue;
              case "a":
                if (stack3.Count > 0)
                {
                  stack3.Pop();
                  continue;
                }
                continue;
              case "basefont":
                if (stack1.Count > 0)
                {
                  stack1.Pop();
                  continue;
                }
                continue;
              case "div":
              case "center":
                if (stack2.Count > 0)
                {
                  stack2.Pop();
                  continue;
                }
                continue;
              default:
                continue;
            }
        }
      }
      this.m_Height = num4 + 18;
    }

    public void Scissor(int x, int y, int w, int h)
    {
      foreach (GLabel glabel in this.m_Children.ToArray())
        glabel.Scissor(x - glabel.X, y - glabel.Y, w, h);
    }
  }
}

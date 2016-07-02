// Decompiled with JetBrains decompiler
// Type: PlayUO.GVirtueItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GVirtueItem : GServerImage
  {
    private static int[] m_Table = new int[32]{ 108, 1153, 2403, 2405, 110, 1546, 1551, 42, 105, 2212, 2215, 52, 111, 2405, 2301, 1152, 112, 234, 2117, 32, 107, 17, 617, 317, 109, 2209, 2211, 66, 106, 1347, 1351, 97 };
    private GAlphaBackground m_Title;

    public GVirtueItem(GServerGump owner, int x, int y, int gumpID, IHue hue)
      : base(owner, x, y, gumpID, hue)
    {
      this.m_QuickDrag = false;
      int num1 = hue.HueID() - 1;
      int num2 = -1;
      int num3 = 0;
      int index1 = 0;
      while (index1 < GVirtueItem.m_Table.Length)
      {
        if (GVirtueItem.m_Table[index1] == gumpID)
        {
          num2 = index1 / 4;
          for (int index2 = 1; index2 < 4; ++index2)
          {
            if (GVirtueItem.m_Table[index1 + index2] == num1)
            {
              num3 = index2;
              goto label_9;
            }
          }
        }
        index1 += 4;
      }
label_9:
      if (num2 < 0)
        return;
      this.m_Title = new GAlphaBackground(30 - x, 40 - y, 0, 0);
      GLabel glabel = new GLabel(Localization.GetString(1051000 + num3 * 8 + num2), (IFont) Engine.GetUniFont(0), hue, 3, 3);
      this.m_Title.Children.Add((Gump) glabel);
      glabel.X -= glabel.Image.xMin;
      glabel.Y -= glabel.Image.yMin;
      this.m_Title.Width = glabel.Image.xMax - glabel.Image.xMin + 7;
      this.m_Title.Height = glabel.Image.yMax - glabel.Image.yMin + 7;
      Size size = Engine.m_Gumps.Measure(104);
      this.m_Title.X += (size.Width - this.m_Title.Width) / 2;
      this.m_Title.Y += (size.Height - this.m_Title.Height) / 2;
      this.m_Title.Visible = false;
      this.m_Children.Add((Gump) this.m_Title);
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      return this.m_Draw;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (this.m_Title == null)
        return;
      this.m_Title.Visible = true;
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      Network.Send((Packet) new PVirtueItemTrigger(this.m_Owner, this.m_GumpID));
    }

    protected internal override void OnMouseLeave()
    {
      if (this.m_Title == null)
        return;
      this.m_Title.Visible = false;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GJournal
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GJournal : GAlphaBackground, IResizable
  {
    private static VertexCache m_vCache = new VertexCache();
    protected int m_CropWidth;
    protected bool m_ToClose;
    protected GAlphaVSlider m_Scroller;
    protected GHotspot m_Hotspot;

    public int MinWidth
    {
      get
      {
        return 100;
      }
    }

    public int MaxWidth
    {
      get
      {
        return Engine.ScreenWidth;
      }
    }

    public int MinHeight
    {
      get
      {
        return 100;
      }
    }

    public int MaxHeight
    {
      get
      {
        return Engine.ScreenHeight;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
        this.m_CropWidth = this.m_Width - 24;
        this.m_Scroller.X = this.m_Hotspot.X = this.m_Width - 19;
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
        double num = this.m_Scroller.GetValue();
        this.m_Hotspot.Height = this.m_Height - 8;
        this.m_Scroller.Height = this.m_Height - 19;
        this.m_Scroller.SetValue(num, false);
      }
    }

    public GJournal()
      : base(50, 50, 300, 188)
    {
      int num = Engine.m_Journal.Count - 1;
      if (num < 0)
        num = 0;
      this.m_Children.Add((Gump) new GVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLResizer((IResizable) this));
      this.m_Children.Add((Gump) new GTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLVResizer((IResizable) this));
      this.m_Scroller = new GAlphaVSlider(0, 10, 16, 169, (double) num, 0.0, (double) num, 1.0);
      this.m_Hotspot = new GHotspot(0, 4, 16, 180, (Gump) this.m_Scroller);
      this.m_Hotspot.NormalHit = false;
      this.m_Children.Add((Gump) this.m_Scroller);
      this.m_Children.Add((Gump) this.m_Hotspot);
      this.Width = 300;
      this.Height = 188;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.BringToTop();
      if (mb != MouseButtons.Right)
        return;
      this.m_ToClose = true;
    }

    protected internal override void OnDispose()
    {
      Engine.m_JournalOpen = false;
      Engine.m_JournalGump = (GJournal) null;
      base.OnDispose();
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (!this.m_ToClose)
        return;
      Gumps.Destroy((Gump) this);
      Engine.m_JournalOpen = false;
      Engine.m_JournalGump = (GJournal) null;
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      this.BringToTop();
      this.m_Scroller.SetValue(this.m_Scroller.GetValue() + (double) -Math.Sign(Delta) * 5.0 * this.m_Scroller.Increase, true);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (!this.m_ToClose || mb == MouseButtons.Right)
        return;
      this.m_ToClose = false;
    }

    public void OnEntryAdded()
    {
      double num = this.m_Scroller.GetValue();
      this.m_Scroller.End = (double) Engine.m_Journal.Count;
      if (num == (double) (Engine.m_Journal.Count - 1))
        return;
      this.m_Scroller.SetValue(num, false);
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      int num1 = X + 2;
      int num2 = this.m_Height - 2;
      int count = Engine.m_Journal.Count;
      int num3 = (int) this.m_Scroller.GetValue();
      if (num3 >= count)
        num3 = count - 1;
      UnicodeFont uniFont = Engine.GetUniFont(3);
      for (int index = num3; index >= 0 && index < count; --index)
      {
        JournalEntry journalEntry = (JournalEntry) Engine.m_Journal[index];
        Texture t;
        if (journalEntry.Width != this.m_CropWidth)
        {
          string String = Engine.WrapText(journalEntry.Text, this.m_CropWidth, (IFont) uniFont);
          t = uniFont.GetString(String, journalEntry.Hue);
          journalEntry.Width = this.m_CropWidth;
          journalEntry.Image = t;
        }
        else
          t = journalEntry.Image;
        if (t != null && !t.IsEmpty())
        {
          int num4 = num2 - t.Height;
          if (num4 < 3)
          {
            t.DrawClipped(num1, Y + num4, Clipper.TemporaryInstance(X, Y + 1, this.Width, this.Height));
            break;
          }
          GJournal.m_vCache.Draw(t, num1, Y + num4);
          num2 = num4 - 4;
        }
      }
    }
  }
}

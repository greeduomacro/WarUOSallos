// Decompiled with JetBrains decompiler
// Type: PlayUO.GMainMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GMainMenu : Gump
  {
    private bool m_LeftToRight;

    public bool LeftToRight
    {
      get
      {
        return this.m_LeftToRight;
      }
      set
      {
        this.m_LeftToRight = value;
        this.Layout();
      }
    }

    public override int Width
    {
      get
      {
        if (!this.m_LeftToRight)
          return 120;
        return 1 + this.m_Children.Count * 23;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        if (!this.m_LeftToRight)
          return 1 + this.m_Children.Count * 119;
        return 24;
      }
      set
      {
      }
    }

    public GMainMenu(int x, int y)
      : base(x, y)
    {
      this.m_NonRestrictivePicking = true;
    }

    public void Add(GMenuItem child)
    {
      this.m_Children.Remove((Gump) child);
      if (this.m_LeftToRight)
      {
        child.X = this.m_Children.Count * 119;
        child.Y = 0;
      }
      else
      {
        child.X = 0;
        child.Y = this.m_Children.Count * 23;
      }
      this.m_Children.Add((Gump) child);
    }

    public void Remove(GMenuItem child)
    {
      this.m_Children.Remove((Gump) child);
      this.Layout();
    }

    public bool Contains(GMenuItem child)
    {
      return this.m_Children.IndexOf((Gump) child) >= 0;
    }

    public void Layout()
    {
      int num = 0;
      foreach (Gump gump in this.m_Children.ToArray())
      {
        GMenuItem gmenuItem = gump as GMenuItem;
        if (gmenuItem != null)
        {
          if (this.m_LeftToRight)
          {
            gmenuItem.X = num++ * 119;
            gmenuItem.Y = 0;
          }
          else
          {
            gmenuItem.X = 0;
            gmenuItem.Y = num++ * 23;
          }
        }
      }
    }
  }
}

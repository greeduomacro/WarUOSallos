// Decompiled with JetBrains decompiler
// Type: PlayUO.GListBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GListBox : GBackground
  {
    private static Type tGListItem = typeof (GListItem);
    protected IFont m_Font;
    protected IHue m_HRegular;
    protected IHue m_HOver;
    protected int m_Index;
    protected int m_ItemCount;
    protected int m_Count;
    protected OnClick m_OnClick;

    public OnClick OnClick
    {
      get
      {
        return this.m_OnClick;
      }
      set
      {
        this.m_OnClick = value;
      }
    }

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public int StartIndex
    {
      get
      {
        return this.m_Index;
      }
      set
      {
        if (this.m_Index == value)
          return;
        this.m_Index = value;
        Gump[] array = this.m_Children.ToArray();
        for (int index = 0; index < array.Length; ++index)
        {
          if (array[index].GetType() == GListBox.tGListItem)
            ((GListItem) this.m_Children[index]).Layout();
        }
      }
    }

    public int ItemCount
    {
      get
      {
        return this.m_ItemCount;
      }
    }

    public IFont Font
    {
      get
      {
        return this.m_Font;
      }
    }

    public IHue HRegular
    {
      get
      {
        return this.m_HRegular;
      }
    }

    public IHue HOver
    {
      get
      {
        return this.m_HOver;
      }
    }

    public GListBox(IFont Font, IHue HRegular, IHue HOver, int BackID, int X, int Y, int Width, int Height, bool HasBorder)
      : base(BackID, Width, Height, X, Y, HasBorder)
    {
      this.m_Font = Font;
      this.m_HRegular = HRegular;
      this.m_HOver = HOver;
      this.m_ItemCount = this.UseHeight / 18;
    }

    protected internal void OnListItemClick(GListItem who)
    {
      if (this.m_OnClick == null)
        return;
      this.SetTag("Clicked", (object) who);
      this.m_OnClick((Gump) this);
    }

    public void AddItem(string Text)
    {
      this.m_Children.Add((Gump) new GListItem(Text, this.m_Count++, this));
    }

    public void AddItem(string Text, Tooltip t)
    {
      GListItem glistItem = new GListItem(Text, this.m_Count++, this);
      glistItem.Tooltip = (ITooltip) t;
      this.m_Children.Add((Gump) glistItem);
    }
  }
}

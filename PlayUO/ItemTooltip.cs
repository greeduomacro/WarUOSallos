// Decompiled with JetBrains decompiler
// Type: PlayUO.ItemTooltip
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ItemTooltip : ITooltip
  {
    private Item m_Item;
    private Gump m_Gump;

    public Gump Gump
    {
      get
      {
        return this.m_Gump;
      }
      set
      {
        this.m_Gump = value;
      }
    }

    public float Delay
    {
      get
      {
        return 0.25f;
      }
      set
      {
      }
    }

    public ItemTooltip(Item item)
    {
      this.m_Item = item;
    }

    public Gump GetGump()
    {
      if (this.m_Gump != null)
        return this.m_Gump;
      if (this.m_Item.PropertyList != null)
        return this.m_Gump = (Gump) new GObjectProperties(1020000 + (this.m_Item.ID & 16383), (object) this.m_Item, this.m_Item.PropertyList);
      this.m_Item.QueryProperties();
      return (Gump) null;
    }
  }
}

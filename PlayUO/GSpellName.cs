// Decompiled with JetBrains decompiler
// Type: PlayUO.GSpellName
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GSpellName : GTextButton
  {
    private int m_SpellID;

    public GSpellName(int SpellID, string Name, IFont Font, IHue HRegular, IHue HOver, int X, int Y)
      : base(Name, Font, HRegular, HOver, X, Y, (OnClick) null)
    {
      this.m_SpellID = SpellID;
      this.m_CanDrag = true;
      this.m_QuickDrag = false;
    }

    protected internal override void OnDragStart()
    {
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      GSpellIcon gspellIcon = new GSpellIcon(this.m_SpellID);
      gspellIcon.m_OffsetX = gspellIcon.Width / 2;
      gspellIcon.m_OffsetY = gspellIcon.Height / 2;
      gspellIcon.X = Engine.m_xMouse - gspellIcon.m_OffsetX;
      gspellIcon.Y = Engine.m_yMouse - gspellIcon.m_OffsetY;
      gspellIcon.m_IsDragging = true;
      Gumps.Desktop.Children.Add((Gump) gspellIcon);
      Gumps.Drag = (Gump) gspellIcon;
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      Spell spellById = Spells.GetSpellByID(this.m_SpellID);
      if (spellById != null)
        spellById.Cast();
      Item container = (Item) this.m_Parent.GetTag("Container");
      container.LastSpell = this.m_SpellID;
      this.m_Parent.Visible = false;
      Gumps.Desktop.Children.Add((Gump) new GSpellbookIcon(this.m_Parent, container));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GSpellbookIcon
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSpellbookIcon : GClickable
  {
    private Item m_Container;
    private Gump m_Book;

    public GSpellbookIcon(Gump book, Item container)
      : base(container.BookIconX, container.BookIconY, Spells.GetBookIcon(container.ID))
    {
      this.m_Book = book;
      this.m_Container = container;
      this.m_Container.OpenSB = true;
      this.m_CanDrag = true;
      this.m_QuickDrag = false;
      this.m_GUID = string.Format("Spellbook Icon #{0}", (object) container.Serial);
    }

    protected override void OnDoubleClicked()
    {
      this.m_Container.BookIconX = this.m_X;
      this.m_Container.BookIconY = this.m_Y;
      int x = this.m_Book.X;
      int y = this.m_Book.Y;
      Gumps.Destroy((Gump) this);
      Gumps.Destroy(this.m_Book);
      Gump gump = Spells.OpenSpellbook(this.m_Container);
      gump.X = this.m_Book.X;
      gump.Y = this.m_Book.Y;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      this.m_Container.BookIconX = this.m_X;
      this.m_Container.BookIconY = this.m_Y;
      this.m_Container.OpenSB = false;
      Gumps.Destroy((Gump) this);
      Gumps.Destroy(this.m_Book);
      Engine.CancelClick();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.GSpellIcon
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System.Windows.Forms;

namespace PlayUO
{
  public class GSpellIcon : GClickable
  {
    public int m_SpellID;

    public GSpellIcon(int spellID)
      : base(0, 0, GSpellIcon.GetGumpIDFor(spellID))
    {
      this.m_SpellID = spellID;
      this.m_CanDrag = true;
      this.m_QuickDrag = false;
    }

    public static int GetGumpIDFor(int spellID)
    {
      if (spellID >= 1 && spellID <= 64)
      {
        --spellID;
        return 2240 + spellID;
      }
      if (spellID >= 101 && spellID <= 116)
      {
        spellID -= 101;
        return 20480 + spellID;
      }
      if (spellID < 201 || spellID > 210)
        return 0;
      spellID -= 201;
      return 20736 + spellID;
    }

    public override GumpLayout CreateLayout()
    {
      return (GumpLayout) new SpellIconLayout();
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      if (Gumps.LastOver != this)
        return;
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.1f);
      Renderer.SolidRect(16777215, X - 1, Y - 1, this.Width + 2, this.Height + 2);
      Renderer.SetAlpha(0.6f);
      Renderer.TransparentRect(16777215, X - 1, Y - 1, this.Width + 2, this.Height + 2);
      Renderer.PopAlpha();
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None || !Engine.amMoving)
        return;
      Point screen = this.PointToScreen(new Point(X, Y));
      int distance = 0;
      Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) != MouseButtons.None && (Control.ModifierKeys & Keys.Shift) == Keys.None)
      {
        Point screen = this.PointToScreen(new Point(x, y));
        int distance = 0;
        Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
        Engine.amMoving = true;
      }
      else
        this.BringToTop();
    }

    protected internal override void OnDragStart()
    {
      if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
    }

    protected override void OnDoubleClicked()
    {
      Spell spellById = Spells.GetSpellByID(this.m_SpellID);
      if (spellById == null)
        return;
      spellById.Cast();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
      {
        this.ManualClose();
        Engine.CancelClick();
      }
      else
        Engine.amMoving = false;
    }
  }
}

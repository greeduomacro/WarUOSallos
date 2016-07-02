// Decompiled with JetBrains decompiler
// Type: PlayUO.GSpellPlaceholder
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSpellPlaceholder : Gump
  {
    public const int Seperator = 4;
    private int m_GameOffsetX;
    private int m_GameOffsetY;

    public override int Width
    {
      get
      {
        return 48;
      }
    }

    public override int Height
    {
      get
      {
        return 48;
      }
    }

    public GSpellPlaceholder(int xOffset, int yOffset)
      : base(xOffset, yOffset)
    {
      this.m_GameOffsetX = xOffset - 2;
      this.m_GameOffsetY = yOffset - 2;
      this.m_CanDrop = true;
    }

    protected internal override void Render(int X, int Y)
    {
      this.X = Engine.GameX + this.m_GameOffsetX;
      this.Y = Engine.GameY + this.m_GameOffsetY;
      base.Render(X, Y);
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (!(g is GSpellIcon))
        return;
      g.X = Engine.GameX + this.m_GameOffsetX + 2;
      g.Y = Engine.GameY + this.m_GameOffsetY + 2;
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
      if ((mb & MouseButtons.Right) == MouseButtons.None || (Control.ModifierKeys & Keys.Shift) != Keys.None)
        return;
      Point screen = this.PointToScreen(new Point(x, y));
      int distance = 0;
      Engine.movingDir = Engine.GetDirection(screen.X, screen.Y, ref distance);
      Engine.amMoving = true;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None || (Control.ModifierKeys & Keys.Shift) != Keys.None)
        return;
      Engine.amMoving = false;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return !Engine.amMoving;
    }

    protected internal override void Draw(int X, int Y)
    {
      if (Gumps.LastOver != this)
        return;
      ++X;
      ++Y;
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.1f);
      Renderer.SolidRect(16777215, X, Y, this.Width - 4 + 2, this.Height - 4 + 2);
      Renderer.SetAlpha(0.6f);
      Renderer.TransparentRect(16777215, X, Y, this.Width - 4 + 2, this.Height - 4 + 2);
      Renderer.PopAlpha();
    }
  }
}

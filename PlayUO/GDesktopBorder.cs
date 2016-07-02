// Decompiled with JetBrains decompiler
// Type: PlayUO.GDesktopBorder
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System.Windows.Forms;

namespace PlayUO
{
  public class GDesktopBorder : GBackground
  {
    public static GDesktopBorder Instance;

    public GDesktopBorder()
      : base(Engine.GameX - 4, Engine.GameY - 4, Engine.GameWidth + 8, Engine.GameHeight + 8, 2700, 2700, 2700, 2701, 0, 2701, 2700, 2700, 2700)
    {
      GDesktopBorder.Instance = this;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_DragClipX = Engine.GameWidth + 4;
      this.m_DragClipY = Engine.GameHeight + 4;
      this.CanClose = false;
    }

    protected internal override void OnDragStart()
    {
      if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (Engine.amMoving)
        return false;
      if (X >= 4 && Y >= 4 && X < Engine.GameWidth + 4)
        return Y >= Engine.GameHeight + 4;
      return true;
    }

    public void DoRender()
    {
      int num1 = this.X + 4;
      int num2 = this.Y + 4;
      if (num1 != Engine.GameX || num2 != Engine.GameY)
      {
        Engine.GameX = num1;
        Engine.GameY = num2;
        Preferences.Current.Layout.Update();
      }
      base.Render(Engine.GameX - 4 - this.X, Engine.GameY - 4 - this.Y);
    }

    protected internal override void Render(int X, int Y)
    {
    }
  }
}

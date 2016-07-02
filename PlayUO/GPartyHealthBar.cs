// Decompiled with JetBrains decompiler
// Type: PlayUO.GPartyHealthBar
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GPartyHealthBar : Gump, IMobileStatus
  {
    public Mobile m_Mobile;
    private string m_xName;
    private GLabel m_Name;
    private int m_Width;
    private int m_Height;

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public Gump Gump
    {
      get
      {
        return (Gump) this;
      }
    }

    public GPartyHealthBar(Mobile m, int x, int y)
      : base(x, y)
    {
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_CanDrop = true;
      this.m_Mobile = m;
      this.m_Name = new GLabel("", (IFont) Engine.GetUniFont(0), m.Visible ? Hues.GetNotoriety(m.Notoriety) : Hues.Grayscale, 4, 4);
      this.SetName(m.Name);
      this.m_Children.Add((Gump) this.m_Name);
    }

    public void OnEnergyChange()
    {
    }

    public void OnFireChange()
    {
    }

    public void OnColdChange()
    {
    }

    public void OnLuckChange()
    {
    }

    public void OnPoisonChange()
    {
    }

    public void OnDamageChange()
    {
    }

    public void OnWeightChange(int weight)
    {
    }

    public void OnArmorChange(int armor)
    {
    }

    public void OnFollCurChange(int current)
    {
    }

    public void OnFollMaxChange(int maximum)
    {
    }

    public void OnStatCapChange(int statCap)
    {
    }

    public void OnNotorietyChange(Notoriety n)
    {
      if (this.m_Mobile.Visible)
        this.m_Name.Hue = Hues.GetNotoriety(n);
      else
        this.m_Name.Hue = Hues.Grayscale;
    }

    public void OnGenderChange(int gender)
    {
    }

    public void OnGoldChange(int gold)
    {
    }

    public void OnStrChange(int str)
    {
    }

    public void OnHPCurChange(int cur)
    {
    }

    public void OnHPMaxChange(int max)
    {
    }

    public void OnDexChange(int dex)
    {
    }

    public void OnStamCurChange(int cur)
    {
    }

    public void OnStamMaxChange(int max)
    {
    }

    public void OnIntChange(int intel)
    {
    }

    public void OnManaCurChange(int cur)
    {
    }

    public void OnManaMaxChange(int max)
    {
    }

    public void OnFlagsChange(MobileFlags flags)
    {
      if (this.m_Mobile.Visible)
        this.m_Name.Hue = Hues.GetNotoriety(this.m_Mobile.Notoriety);
      else
        this.m_Name.Hue = Hues.Grayscale;
    }

    public void OnNameChange(string name)
    {
      if (!(this.m_xName != name))
        return;
      this.SetName(name);
    }

    protected internal override void OnDispose()
    {
      base.OnDispose();
      if (Engine.m_Highlight == this.m_Mobile && !this.m_Mobile.Player)
        Engine.m_Highlight = (object) null;
      this.m_Mobile.StatusBar = (IMobileStatus) null;
    }

    public void Close()
    {
      Gumps.Destroy((Gump) this);
    }

    public void OnRefresh()
    {
      this.OnNameChange(this.m_Mobile.Name);
      this.OnNotorietyChange(this.m_Mobile.Notoriety);
      this.m_Height = 26 + (this.m_Mobile.MaximumStamina > 0 ? 6 : 0) + (this.m_Mobile.MaximumMana > 0 ? 6 : 0);
    }

    protected internal override void OnDragStart()
    {
      if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (!(g is GDraggedItem))
        return;
      Item obj = ((GDraggedItem) g).Item;
      if (obj == null)
        return;
      Network.Send((Packet) new PDropItem(obj.Serial, -1, -1, 0, this.m_Mobile.Serial));
      Gumps.Destroy(g);
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      if (TargetManager.IsActive)
        return;
      if (this.m_Mobile.Player)
      {
        this.Close();
        this.m_Mobile.BigStatus = true;
        this.m_Mobile.OpenStatus(false);
      }
      else
        this.m_Mobile.OnDoubleClick();
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (this.m_Mobile.Player)
        return;
      Engine.m_Highlight = (object) this.m_Mobile;
    }

    protected internal override void OnMouseLeave()
    {
      if (Engine.m_Highlight != this.m_Mobile || this.m_Mobile.Player)
        return;
      Engine.m_Highlight = (object) null;
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

    protected internal override void OnSingleClick(int x, int y)
    {
      if (TargetManager.IsActive)
        return;
      this.m_Mobile.OnSingleClick();
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) != MouseButtons.None)
      {
        if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        {
          this.Close();
          Engine.CancelClick();
        }
        else
          Engine.amMoving = false;
      }
      else if (this.m_Mobile.IsInParty && (mb & MouseButtons.Left) != MouseButtons.None && (Control.ModifierKeys & Keys.Control) != Keys.None)
      {
        GRadar.m_FocusMob = this.m_Mobile;
      }
      else
      {
        if (!TargetManager.IsActive || (mb & MouseButtons.Left) == MouseButtons.None)
          return;
        this.m_Mobile.OnTarget();
        Engine.CancelClick();
      }
    }

    protected internal override bool HitTest(int x, int y)
    {
      return true;
    }

    protected internal override void Render(int x, int y)
    {
      base.Render(x, y);
    }

    protected internal override void Draw(int x, int y)
    {
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.4f);
      Renderer.SolidRect(0, x + 1, y + 1, this.m_Width - 2, this.m_Height - 2);
      Renderer.PopAlpha();
      Renderer.TransparentRect(0, x, y, this.m_Width, this.m_Height);
      bool flag1 = this.m_Mobile.MaximumStamina > 0;
      bool flag2 = this.m_Mobile.MaximumMana > 0;
      int num = 6 + (flag1 ? 6 : 0) + (flag2 ? 6 : 0);
      y += this.m_Height;
      y -= num + 1;
      Renderer.SolidRect(0, x, y, this.m_Width, num + 1);
      ++x;
      ++y;
      int Width1 = this.m_Width - 2;
      if (this.m_Mobile.Ghost || this.m_Mobile.IsDeadPet)
      {
        Renderer.GradientRect(12632256, 6316128, x, y, Width1, 5);
        if (flag1)
        {
          y += 6;
          Renderer.GradientRect(12632256, 6316128, x, y, Width1, 5);
        }
        if (!flag2)
          return;
        y += 6;
        Renderer.GradientRect(12632256, 6316128, x, y, Width1, 5);
      }
      else
      {
        MobileFlags flags = this.m_Mobile.Flags;
        int Color;
        int Color2;
        if (this.m_Mobile.IsPoisoned)
        {
          Color = 65280;
          Color2 = 32768;
        }
        else if (flags[MobileFlag.YellowHits])
        {
          Color = 16760832;
          Color2 = 8413184;
        }
        else
        {
          Color = 2146559;
          Color2 = 1073280;
        }
        int Width2 = this.m_Mobile.CurrentHitPoints * Width1 / Math.Max(1, this.m_Mobile.MaximumHitPoints);
        if (Width2 > Width1)
          Width2 = Width1;
        else if (Width2 < 0)
          Width2 = 0;
        Renderer.GradientRect(Color, Color2, x, y, Width2, 5);
        Renderer.GradientRect(16711680, 8388608, x + Width2, y, Width1 - Width2, 5);
        if (flag1)
        {
          y += 6;
          int Width3 = this.m_Mobile.CurrentMana * Width1 / Math.Max(1, this.m_Mobile.MaximumMana);
          if (Width3 > Width1)
            Width3 = Width1;
          else if (Width3 < 0)
            Width3 = 0;
          Renderer.GradientRect(2146559, 1073280, x, y, Width3, 5);
          Renderer.GradientRect(16711680, 8388608, x + Width3, y, Width1 - Width3, 5);
        }
        if (!flag2)
          return;
        y += 6;
        int Width4 = this.m_Mobile.CurrentStamina * Width1 / Math.Max(1, this.m_Mobile.MaximumStamina);
        if (Width4 > Width1)
          Width4 = Width1;
        else if (Width4 < 0)
          Width4 = 0;
        Renderer.GradientRect(2146559, 1073280, x, y, Width4, 5);
        Renderer.GradientRect(16711680, 8388608, x + Width4, y, Width1 - Width4, 5);
      }
    }

    private void SetName(string name)
    {
      this.m_xName = name;
      if (this.m_Name.Font.GetStringWidth(name) > 70)
      {
        while (name.Length > 0 && this.m_Name.Font.GetStringWidth(name + "...") > 70)
          name = name.Substring(0, name.Length - 1);
        name += "...";
      }
      this.m_Name.Text = name;
      int num = this.m_Name.Image.xMax - this.m_Name.Image.xMin + 1 + 6;
      if (num < 80)
        num = 80;
      this.m_Name.Y = 3 - this.m_Name.Image.yMin;
      this.m_Name.X = (num - (this.m_Name.Image.xMax - this.m_Name.Image.xMin + 1)) / 2 - this.m_Name.Image.xMin;
      this.m_Width = num;
      this.m_Height = 26 + (this.m_Mobile.MaximumStamina > 0 ? 6 : 0) + (this.m_Mobile.MaximumMana > 0 ? 6 : 0);
      this.m_DragClipX = this.m_Width - 1;
      this.m_DragClipY = this.m_Height - 1;
    }
  }
}

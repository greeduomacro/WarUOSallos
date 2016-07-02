// Decompiled with JetBrains decompiler
// Type: PlayUO.GHealthBar
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GHealthBar : GDragable, IMobileStatus
  {
    private string m_xName = "";
    private Mobile m_Mobile;
    private bool m_Player;
    private GLabel m_Name;
    private int m_xHPCur;
    private int m_xHPMax;
    private GImageClip m_HP;
    private int m_xStamCur;
    private int m_xStamMax;
    private GImageClip m_Stam;
    private int m_xManaCur;
    private int m_xManaMax;
    private GImageClip m_Mana;

    public Gump Gump
    {
      get
      {
        return (Gump) this;
      }
    }

    public GHealthBar(Mobile m, int X, int Y)
      : base(m.Player ? (m.Flags[MobileFlag.Warmode] ? 2055 : 2051) : 2052, X, Y)
    {
      this.m_Mobile = m;
      this.m_CanDrop = true;
      this.m_QuickDrag = false;
      this.m_Player = m.Player;
      if (!this.m_Player)
        this.Hue = Hues.GetNotoriety(m.Notoriety);
      if (Engine.ServerFeatures.AOS)
        this.Tooltip = (ITooltip) new MobileTooltip(m);
      if (this.m_Player)
      {
        this.m_HP = new GImageClip(this.GetHealthGumpID(m.Flags), 34, 12, m.CurrentHitPoints, m.MaximumHitPoints);
        this.m_Mana = new GImageClip(2054, 34, 25, m.CurrentMana, m.MaximumMana);
        this.m_Stam = new GImageClip(2054, 34, 38, m.CurrentStamina, m.MaximumStamina);
        this.m_Children.Add((Gump) new GImage(2053, 34, 12));
        this.m_Children.Add((Gump) new GImage(2053, 34, 25));
        this.m_Children.Add((Gump) new GImage(2053, 34, 38));
        this.m_Children.Add((Gump) this.m_HP);
        this.m_Children.Add((Gump) this.m_Mana);
        this.m_Children.Add((Gump) this.m_Stam);
        this.m_xHPCur = m.CurrentHitPoints;
        this.m_xHPMax = m.MaximumHitPoints;
        this.m_xStamCur = m.CurrentStamina;
        this.m_xStamMax = m.MaximumStamina;
        this.m_xManaCur = m.CurrentMana;
        this.m_xManaMax = m.MaximumMana;
      }
      else
      {
        this.m_Name = new GLabel(m.Name, (IFont) Engine.GetFont(1), Hues.Load(1109), 16, 0);
        this.m_Name.Y = 11 + (24 - this.m_Name.Height) / 2;
        this.m_Name.Scissor(0, 0, 122, this.m_Name.Height);
        this.m_HP = new GImageClip(this.GetHealthGumpID(m.Flags), 34, 38, m.CurrentHitPoints, m.MaximumHitPoints);
        this.m_Children.Add((Gump) new GImage(2053, 34, 38));
        this.m_Children.Add((Gump) this.m_Name);
        this.m_Children.Add((Gump) this.m_HP);
        this.m_xName = m.Name;
        this.m_xHPCur = m.CurrentHitPoints;
        this.m_xHPMax = m.MaximumHitPoints;
      }
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

    public void OnWeightChange(int Weight)
    {
    }

    public void OnArmorChange(int Armor)
    {
    }

    public void OnDexChange(int Dex)
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

    public void OnFlagsChange(MobileFlags Flags)
    {
      this.GumpID = Flags[MobileFlag.Warmode] ? 2055 : 2051;
      this.m_HP.GumpID = this.GetHealthGumpID(Flags);
    }

    public void OnNotorietyChange(Notoriety n)
    {
      if (this.m_Player)
        return;
      this.Hue = Hues.GetNotoriety(n);
    }

    public void OnGenderChange(int Gender)
    {
    }

    public void OnGoldChange(int Gold)
    {
    }

    public void OnHPCurChange(int HPCur)
    {
      if (this.m_xHPCur == HPCur && this.m_xHPMax == this.m_Mobile.MaximumHitPoints)
        return;
      this.m_HP.Resize(HPCur, this.m_Mobile.MaximumHitPoints);
      this.m_xHPCur = HPCur;
      this.m_xHPMax = this.m_Mobile.MaximumHitPoints;
    }

    public void OnHPMaxChange(int HPMax)
    {
      if (this.m_xHPMax == HPMax && this.m_xHPCur == this.m_Mobile.CurrentHitPoints)
        return;
      this.m_HP.Resize(this.m_Mobile.CurrentHitPoints, HPMax);
      this.m_xHPCur = this.m_Mobile.CurrentHitPoints;
      this.m_xHPMax = HPMax;
    }

    public void OnIntChange(int Int)
    {
    }

    public void OnManaCurChange(int ManaCur)
    {
      if (!this.m_Player || this.m_xManaCur == ManaCur && this.m_xManaMax == this.m_Mobile.MaximumMana)
        return;
      this.m_Mana.Resize(ManaCur, this.m_Mobile.MaximumMana);
      this.m_xManaCur = ManaCur;
      this.m_xManaMax = this.m_Mobile.MaximumMana;
    }

    public void OnManaMaxChange(int ManaMax)
    {
      if (!this.m_Player || this.m_xManaMax == ManaMax && this.m_xManaCur == this.m_Mobile.CurrentMana)
        return;
      this.m_Mana.Resize(this.m_Mobile.CurrentMana, ManaMax);
      this.m_xManaCur = this.m_Mobile.CurrentMana;
      this.m_xManaMax = ManaMax;
    }

    public void OnNameChange(string Name)
    {
      if (this.m_Player || !(this.m_xName != Name))
        return;
      this.m_Name.Text = Name;
      this.m_Name.Y = 11 + (24 - this.m_Name.Height) / 2;
      this.m_Name.Scissor(0, 0, 122, this.m_Name.Height);
      this.m_xName = Name;
    }

    public void OnStamCurChange(int StamCur)
    {
      if ((!this.m_Player || this.m_xStamCur == StamCur) && this.m_xStamMax == this.m_Mobile.MaximumStamina)
        return;
      this.m_Stam.Resize(StamCur, this.m_Mobile.MaximumStamina);
      this.m_xStamCur = StamCur;
      this.m_xStamMax = this.m_Mobile.MaximumStamina;
    }

    public void OnStamMaxChange(int StamMax)
    {
      if ((!this.m_Player || this.m_xStamMax == StamMax) && this.m_xStamCur == this.m_Mobile.CurrentStamina)
        return;
      this.m_Stam.Resize(this.m_Mobile.CurrentStamina, StamMax);
      this.m_xStamCur = this.m_Mobile.CurrentStamina;
      this.m_xStamMax = StamMax;
    }

    public void OnStrChange(int Str)
    {
    }

    public void Close()
    {
      if (Engine.m_Highlight == this.m_Mobile && !this.m_Mobile.Player)
        Engine.m_Highlight = (object) null;
      Gumps.Destroy((Gump) this);
      if (this.m_Mobile == null)
        return;
      this.m_Mobile.StatusBar = (IMobileStatus) null;
    }

    public void OnRefresh()
    {
      Mobile mobile = this.m_Mobile;
      if (mobile == null)
        this.Close();
      this.OnHPCurChange(mobile.CurrentHitPoints);
      this.OnFlagsChange(mobile.Flags);
      if (this.m_Player)
      {
        this.OnStamCurChange(mobile.CurrentStamina);
        this.OnManaCurChange(mobile.CurrentMana);
      }
      else
      {
        this.OnNotorietyChange(mobile.Notoriety);
        this.OnNameChange(mobile.Name);
      }
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (!(g is GDraggedItem))
        return;
      Item obj = ((GDraggedItem) g).Item;
      if (obj == null || this.m_Mobile == null)
        return;
      Network.Send((Packet) new PDropItem(obj.Serial, -1, -1, 0, this.m_Mobile.Serial));
      Gumps.Destroy(g);
    }

    private int GetHealthGumpID(MobileFlags flags)
    {
      if (this.m_Mobile.IsPoisoned)
        return 2056;
      return this.m_Mobile.IsInvulnerable ? 2057 : 2054;
    }

    protected internal override void OnDoubleClick(int X, int Y)
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

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
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
        this.Close();
        Engine.CancelClick();
      }
      else
      {
        if (!TargetManager.IsActive || (mb & MouseButtons.Left) == MouseButtons.None)
          return;
        this.m_Mobile.OnTarget();
        Engine.CancelClick();
      }
    }
  }
}

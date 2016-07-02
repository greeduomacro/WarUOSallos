// Decompiled with JetBrains decompiler
// Type: PlayUO.GStatusBar
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GStatusBar : GFader, IMobileStatus
  {
    private string m_xName = "";
    private Mobile m_Mobile;
    private GLabel m_Name;
    private int m_xStr;
    private GLabel m_Str;
    private int m_xDex;
    private GLabel m_Dex;
    private int m_xInt;
    private GLabel m_Int;
    private int m_xArmor;
    private GLabel m_Armor;
    private int m_xGold;
    private GLabel m_Gold;
    private int m_xCold;
    private int m_xFire;
    private int m_xEnergy;
    private int m_xPoison;
    private int m_xLuck;
    private int m_xDamageMin;
    private int m_xDamageMax;
    private GLabel m_Cold;
    private GLabel m_Fire;
    private GLabel m_Energy;
    private GLabel m_Poison;
    private GLabel m_Luck;
    private GLabel m_Damages;
    private int m_xFollCur;
    private int m_xFollMax;
    private GLabel m_Followers;
    private int m_xStatCap;
    private GLabel m_StatCap;
    private GAttributeCurMax m_Hits;
    private GAttributeCurMax m_Stam;
    private GAttributeCurMax m_Mana;
    private GAttributeCurMax m_Weight;

    public Gump Gump
    {
      get
      {
        return (Gump) this;
      }
    }

    public GStatusBar(Mobile m, int X, int Y)
      : base(0.25f, 0.25f, 0.6f, 10860, X, Y)
    {
      this.m_Mobile = m;
      this.m_CanDrop = true;
      this.m_QuickDrag = true;
      IFont font = (IFont) Engine.GetFont(9);
      IHue hue = Hues.Load(1109);
      this.m_Name = new GLabel("", font, hue, 38, 50);
      this.m_Str = new GLabel("0", font, hue, 88, 77);
      this.m_Hits = new GAttributeCurMax(146, 72, 34, 21, this.m_Mobile.CurrentHitPoints, this.m_Mobile.MaximumHitPoints, font, hue);
      this.m_Dex = new GLabel("0", font, hue, 88, 105);
      this.m_Stam = new GAttributeCurMax(146, 100, 34, 21, this.m_Mobile.CurrentStamina, this.m_Mobile.MaximumStamina, font, hue);
      this.m_Int = new GLabel("0", font, hue, 88, 133);
      this.m_Mana = new GAttributeCurMax(146, 128, 34, 21, this.m_Mobile.CurrentMana, this.m_Mobile.MaximumMana, font, hue);
      this.m_Armor = new GLabel("0", font, hue, 355, 74);
      this.m_Fire = new GLabel("0", font, hue, 355, 91);
      this.m_Cold = new GLabel("0", font, hue, 355, 106);
      this.m_Poison = new GLabel("0", font, hue, 355, 119);
      this.m_Energy = new GLabel("0", font, hue, 355, 133);
      this.m_Luck = new GLabel("0", font, hue, 220, 105);
      this.m_Damages = new GLabel("0-0", font, hue, 280, 77);
      this.m_Gold = new GLabel("0", font, hue, 280, 105);
      this.m_Weight = new GAttributeCurMax(216, 128, 34, 21, this.m_Mobile.Weight, this.GetMaxWeight(this.m_Mobile.Strength), font, hue);
      this.m_StatCap = new GLabel("0", font, hue, 220, 77);
      this.m_Followers = new GLabel("0/0", font, hue, 285, 133);
      this.m_Name.X = 39 + (352 - (this.m_Name.Image.xMax - this.m_Name.Image.xMin + 1)) / 2;
      this.m_Name.X -= this.m_Name.Image.xMin;
      this.m_Children.Add((Gump) this.m_Name);
      this.m_Children.Add((Gump) this.m_Str);
      this.m_Children.Add((Gump) this.m_Hits);
      this.m_Children.Add((Gump) this.m_Dex);
      this.m_Children.Add((Gump) this.m_Stam);
      this.m_Children.Add((Gump) this.m_Int);
      this.m_Children.Add((Gump) this.m_Mana);
      this.m_Children.Add((Gump) this.m_Armor);
      this.m_Children.Add((Gump) this.m_Fire);
      this.m_Children.Add((Gump) this.m_Cold);
      this.m_Children.Add((Gump) this.m_Poison);
      this.m_Children.Add((Gump) this.m_Energy);
      this.m_Children.Add((Gump) this.m_Luck);
      this.m_Children.Add((Gump) this.m_Damages);
      this.m_Children.Add((Gump) this.m_Gold);
      this.m_Children.Add((Gump) this.m_Weight);
      this.m_Children.Add((Gump) this.m_StatCap);
      this.m_Children.Add((Gump) this.m_Followers);
      this.m_Children.Add((Gump) new GStatusBar.GMinimizer(this));
      this.AddTooltip(55, 70, 64, 26, 1061146);
      this.AddTooltip(55, 98, 64, 26, 1061147);
      this.AddTooltip(55, 126, 64, 26, 1061148);
      this.AddTooltip(121, 70, 63, 26, 1061149);
      this.AddTooltip(121, 98, 63, 26, 1061150);
      this.AddTooltip(121, 126, 63, 26, 1061151);
      this.AddTooltip(186, 70, 69, 26, 1061152);
      this.AddTooltip(186, 98, 69, 26, 1061153);
      this.AddTooltip(186, 126, 69, 26, 1061154);
      this.AddTooltip(257, 70, 72, 26, 1061155);
      this.AddTooltip(257, 98, 72, 26, 1061156);
      this.AddTooltip(257, 126, 72, 26, 1061157);
      this.AddTooltip(333, 74, 46, 14, 1061158);
      this.AddTooltip(333, 91, 46, 14, 1061159);
      this.AddTooltip(333, 106, 46, 13, 1061160);
      this.AddTooltip(333, 120, 46, 11, 1061161);
      this.AddTooltip(333, 133, 46, 16, 1061162);
      this.OnRefresh();
      if (!Engine.ServerFeatures.AOS)
        return;
      this.Tooltip = (ITooltip) new MobileTooltip(m);
    }

    protected internal override void OnDispose()
    {
      if (this.m_Mobile == null)
        return;
      this.m_Mobile.StatusBar = (IMobileStatus) null;
      this.m_Mobile = (Mobile) null;
    }

    private string FormatMinMax(int min, int max)
    {
      return min.ToString() + "/" + max.ToString();
    }

    public void OnEnergyChange()
    {
      if (this.m_xEnergy == this.m_Mobile.EnergyResist)
        return;
      this.m_xEnergy = this.m_Mobile.EnergyResist;
      this.m_Energy.Text = this.m_xEnergy.ToString();
    }

    public void OnFireChange()
    {
      if (this.m_xFire == this.m_Mobile.FireResist)
        return;
      this.m_xFire = this.m_Mobile.FireResist;
      this.m_Fire.Text = this.m_xFire.ToString();
    }

    public void OnColdChange()
    {
      if (this.m_xCold == this.m_Mobile.ColdResist)
        return;
      this.m_xCold = this.m_Mobile.ColdResist;
      this.m_Cold.Text = this.m_xCold.ToString();
    }

    public void OnLuckChange()
    {
      if (this.m_xLuck == this.m_Mobile.Luck)
        return;
      this.m_xLuck = this.m_Mobile.Luck;
      this.m_Luck.Text = this.m_xLuck.ToString();
    }

    public void OnPoisonChange()
    {
      if (this.m_xPoison == this.m_Mobile.PoisonResist)
        return;
      this.m_xPoison = this.m_Mobile.PoisonResist;
      this.m_Poison.Text = this.m_xPoison.ToString();
    }

    public void OnDamageChange()
    {
      if (this.m_xDamageMin == this.m_Mobile.DamageMin && this.m_xDamageMax == this.m_Mobile.DamageMax)
        return;
      this.m_xDamageMin = this.m_Mobile.DamageMin;
      this.m_xDamageMax = this.m_Mobile.DamageMax;
      this.m_Damages.Text = string.Format("{0}-{1}", (object) this.m_xDamageMin, (object) this.m_xDamageMax);
    }

    public void OnFollCurChange(int current)
    {
      if (this.m_Followers == null || this.m_xFollCur == current && this.m_xFollMax == this.m_Mobile.FollowersMax)
        return;
      this.m_Followers.Text = this.FormatMinMax(current, this.m_Mobile.FollowersMax);
      this.m_xFollCur = current;
      this.m_xFollMax = this.m_Mobile.FollowersMax;
    }

    public void OnFollMaxChange(int maximum)
    {
      if (this.m_Followers == null || this.m_xFollMax == maximum && this.m_xFollCur == this.m_Mobile.FollowersCur)
        return;
      this.m_Followers.Text = this.FormatMinMax(this.m_Mobile.FollowersCur, maximum);
      this.m_xFollCur = this.m_Mobile.FollowersCur;
      this.m_xFollMax = this.m_Mobile.FollowersMax;
    }

    public void OnStatCapChange(int statCap)
    {
      if (this.m_StatCap == null || this.m_xStatCap == statCap)
        return;
      this.m_StatCap.Text = statCap.ToString();
      this.m_xStatCap = statCap;
    }

    public void OnNotorietyChange(Notoriety n)
    {
    }

    public void OnWeightChange(int Weight)
    {
      this.m_Weight.SetValues(Weight, this.GetMaxWeight(this.m_Mobile.Strength));
    }

    private int GetMaxWeight(int str)
    {
      return 40 + (int) (3.5 * (double) str);
    }

    public void OnArmorChange(int Armor)
    {
      if (this.m_xArmor == Armor)
        return;
      this.m_Armor.Text = Armor.ToString();
      this.m_xArmor = Armor;
    }

    public void OnDexChange(int Dex)
    {
      if (this.m_xDex == Dex)
        return;
      this.m_Dex.Text = Dex.ToString();
      this.m_xDex = Dex;
    }

    public void OnFlagsChange(MobileFlags flags)
    {
    }

    public void OnGenderChange(int Gender)
    {
    }

    public void OnGoldChange(int Gold)
    {
      if (this.m_xGold == Gold)
        return;
      this.m_Gold.Text = Gold.ToString();
      this.m_xGold = Gold;
    }

    public void OnHPCurChange(int HPCur)
    {
      this.m_Hits.SetValues(HPCur, this.m_Mobile.MaximumHitPoints);
    }

    public void OnHPMaxChange(int HPMax)
    {
      this.m_Hits.SetValues(this.m_Mobile.CurrentHitPoints, HPMax);
    }

    public void OnIntChange(int Int)
    {
      if (this.m_xInt == Int)
        return;
      this.m_Int.Text = Int.ToString();
      this.m_xInt = Int;
    }

    public void OnManaCurChange(int ManaCur)
    {
      this.m_Mana.SetValues(ManaCur, this.m_Mobile.MaximumMana);
    }

    public void OnManaMaxChange(int ManaMax)
    {
      this.m_Mana.SetValues(this.m_Mobile.CurrentMana, ManaMax);
    }

    public void OnNameChange(string Name)
    {
      if (!(this.m_xName != Name))
        return;
      this.m_Name.Text = Name;
      this.m_xName = Name;
      this.m_Name.X = 39 + (352 - (this.m_Name.Image.xMax - this.m_Name.Image.xMin + 1)) / 2;
      this.m_Name.X -= this.m_Name.Image.xMin;
    }

    public void OnStamCurChange(int StamCur)
    {
      this.m_Stam.SetValues(StamCur, this.m_Mobile.MaximumStamina);
    }

    public void OnStamMaxChange(int StamMax)
    {
      this.m_Stam.SetValues(this.m_Mobile.CurrentStamina, StamMax);
    }

    public void OnStrChange(int Str)
    {
      if (this.m_xStr == Str)
        return;
      this.m_Str.Text = Str.ToString();
      this.m_xStr = Str;
      this.m_Weight.SetValues(this.m_Mobile.Weight, this.GetMaxWeight(Str));
    }

    public void Close()
    {
      if (Engine.m_Highlight == this.m_Mobile && !this.m_Mobile.Player)
        Engine.m_Highlight = (object) null;
      Gumps.Destroy((Gump) this);
    }

    public void OnRefresh()
    {
      Mobile mobile = this.m_Mobile;
      if (mobile == null)
        this.Close();
      this.OnNameChange(mobile.Name);
      this.OnStrChange(mobile.Strength);
      this.OnHPCurChange(mobile.CurrentHitPoints);
      this.OnDexChange(mobile.Dexterity);
      this.OnStamCurChange(mobile.CurrentStamina);
      this.OnIntChange(mobile.Intelligence);
      this.OnManaCurChange(mobile.CurrentMana);
      this.OnArmorChange(mobile.Armor);
      this.OnFireChange();
      this.OnColdChange();
      this.OnPoisonChange();
      this.OnEnergyChange();
      this.OnLuckChange();
      this.OnDamageChange();
      this.OnGoldChange(mobile.Gold);
      this.OnWeightChange(mobile.Weight);
      this.OnNotorietyChange(mobile.Notoriety);
      this.OnStatCapChange(mobile.StatCap);
      this.OnFollCurChange(mobile.FollowersCur);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      this.m_QuickDrag = !TargetManager.IsActive;
      return base.HitTest(X, Y);
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

    private void AddTooltip(int x, int y, int w, int h, int num)
    {
      GHotspot ghotspot = new GHotspot(x, y, w, h, (Gump) this);
      ghotspot.m_CanDrag = true;
      ghotspot.m_QuickDrag = false;
      ghotspot.Tooltip = (ITooltip) new Tooltip(Localization.GetString(num));
      this.m_Children.Add((Gump) ghotspot);
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      if (TargetManager.IsActive)
        return;
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

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) != MouseButtons.None)
      {
        Mobile mobile = this.m_Mobile;
        this.Close();
        mobile.BigStatus = false;
        Engine.CancelClick();
      }
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      if (TargetManager.IsActive)
      {
        this.m_Mobile.OnTarget();
        Engine.CancelClick();
      }
      else
        this.m_Mobile.Look();
    }

    private class GMinimizer : GRegion
    {
      private GStatusBar m_Owner;

      public GMinimizer(GStatusBar owner)
        : base(384, 146, 24, 25)
      {
        this.m_Owner = owner;
        this.m_Tooltip = (ITooltip) new Tooltip("Minimize");
      }

      protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
      {
        if ((mb & MouseButtons.Left) == MouseButtons.None)
          return;
        Mobile mobile = this.m_Owner.m_Mobile;
        this.m_Owner.Close();
        mobile.BigStatus = false;
        mobile.OpenStatus(false);
      }
    }
  }
}

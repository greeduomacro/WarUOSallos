// Decompiled with JetBrains decompiler
// Type: PlayUO.AbilityInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class AbilityInfo
  {
    private static int[] HatchetID = new int[2]{ 3907, 3908 };
    private static int[] LongSwordID = new int[2]{ 3936, 3937 };
    private static int[] BroadswordID = new int[2]{ 3934, 3935 };
    private static int[] KatanaID = new int[2]{ 5118, 5119 };
    private static int[] BladedStaffID = new int[2]{ 9917, 9927 };
    private static int[] HammerPickID = new int[2]{ 5180, 5181 };
    private static int[] WarAxeID = new int[2]{ 5039, 5040 };
    private static int[] KryssID = new int[2]{ 5120, 5121 };
    private static int[] SpearID = new int[2]{ 3938, 3939 };
    private static int[] CompositeBowID = new int[2]{ 9922, 9932 };
    private static int[] CleaverID = new int[2]{ 3778, 3779 };
    private static int[] LargeBattleAxeID = new int[2]{ 5114, 5115 };
    private static int[] BattleAxeID = new int[2]{ 3911, 3912 };
    private static int[] ExecAxeID = new int[2]{ 3909, 3910 };
    private static int[] CutlassID = new int[2]{ 5184, 5185 };
    private static int[] ScytheID = new int[2]{ 9914, 9924 };
    private static int[] WarMaceID = new int[2]{ 5126, 5127 };
    private static int[] PitchforkID = new int[2]{ 3719, 3720 };
    private static int[] WarForkID = new int[2]{ 5124, 5125 };
    private static int[] HalberdID = new int[2]{ 5182, 5183 };
    private static int[] MaulID = new int[2]{ 5178, 5179 };
    private static int[] MaceID = new int[2]{ 3932, 1117 };
    private static int[] GnarledStaffID = new int[2]{ 5112, 5113 };
    private static int[] QuarterStaffID = new int[2]{ 3721, 3722 };
    private static int[] LanceID = new int[2]{ 9920, 9930 };
    private static int[] CrossbowID = new int[2]{ 3919, 3920 };
    private static int[] VikingSwordID = new int[2]{ 5049, 5050 };
    private static int[] AxeID = new int[2]{ 3913, 3914 };
    private static int[] ShepherdsCrookID = new int[2]{ 3713, 3714 };
    private static int[] SmithsHammerID = new int[2]{ 5100, 5092 };
    private static int[] WarHammerID = new int[2]{ 5176, 5177 };
    private static int[] ScepterID = new int[2]{ 9916, 9926 };
    private static int[] SledgeHammerID = new int[2]{ 4020, 4021 };
    private static int[] ButcherKnifeID = new int[2]{ 5110, 5111 };
    private static int[] PickaxeID = new int[2]{ 3717, 3718 };
    private static int[] SkinningKnifeID = new int[2]{ 3780, 3781 };
    private static int[] WandID = new int[4]{ 3570, 3571, 3572, 3573 };
    private static int[] BardicheID = new int[2]{ 3917, 3918 };
    private static int[] ClubID = new int[2]{ 5043, 5044 };
    private static int[] ScimitarID = new int[2]{ 5045, 5046 };
    private static int[] HeavyCrossbowID = new int[2]{ 5116, 5117 };
    private static int[] TwoHandedAxeID = new int[2]{ 5186, 5187 };
    private static int[] DoubleAxeID = new int[2]{ 3915, 3916 };
    private static int[] CrescentBladeID = new int[2]{ 9921, 9922 };
    private static int[] DoubleBladedStaffID = new int[2]{ 9919, 9929 };
    private static int[] RepeatingCrossbowID = new int[2]{ 9923, 9933 };
    private static int[] DaggerID = new int[2]{ 3921, 3922 };
    private static int[] PikeID = new int[2]{ 9918, 9928 };
    private static int[] BoneHarvesterID = new int[2]{ 9915, 9925 };
    private static int[] ShortSpearID = new int[2]{ 5122, 5123 };
    private static int[] BowID = new int[2]{ 5041, 5042 };
    private static int[] BlackStaffID = new int[2]{ 3568, 3569 };
    private static AbilityInfo[] m_Abilities = new AbilityInfo[13]{ new AbilityInfo(0, new int[10][]{ AbilityInfo.HatchetID, AbilityInfo.LongSwordID, AbilityInfo.BroadswordID, AbilityInfo.KatanaID, AbilityInfo.BladedStaffID, AbilityInfo.HammerPickID, AbilityInfo.WarAxeID, AbilityInfo.KryssID, AbilityInfo.SpearID, AbilityInfo.CompositeBowID }), new AbilityInfo(1, new int[10][]{ AbilityInfo.CleaverID, AbilityInfo.LargeBattleAxeID, AbilityInfo.BattleAxeID, AbilityInfo.ExecAxeID, AbilityInfo.CutlassID, AbilityInfo.ScytheID, AbilityInfo.WarMaceID, AbilityInfo.WarAxeID, AbilityInfo.PitchforkID, AbilityInfo.WarForkID }), new AbilityInfo(2, new int[9][]{ AbilityInfo.LongSwordID, AbilityInfo.BattleAxeID, AbilityInfo.HalberdID, AbilityInfo.MaulID, AbilityInfo.MaceID, AbilityInfo.GnarledStaffID, AbilityInfo.QuarterStaffID, AbilityInfo.LanceID, AbilityInfo.CrossbowID }), new AbilityInfo(3, new int[10][]{ AbilityInfo.VikingSwordID, AbilityInfo.AxeID, AbilityInfo.BroadswordID, AbilityInfo.ShepherdsCrookID, AbilityInfo.SmithsHammerID, AbilityInfo.MaulID, AbilityInfo.WarMaceID, AbilityInfo.WarHammerID, AbilityInfo.ScepterID, AbilityInfo.SledgeHammerID }), new AbilityInfo(4, new int[8][]{ AbilityInfo.ButcherKnifeID, AbilityInfo.PickaxeID, AbilityInfo.SkinningKnifeID, AbilityInfo.HatchetID, AbilityInfo.WandID, AbilityInfo.ShepherdsCrookID, AbilityInfo.MaceID, AbilityInfo.WarForkID }), new AbilityInfo(5, new int[8][]{ AbilityInfo.BardicheID, AbilityInfo.AxeID, AbilityInfo.BladedStaffID, AbilityInfo.WandID, AbilityInfo.ClubID, AbilityInfo.PitchforkID, AbilityInfo.LanceID, AbilityInfo.HeavyCrossbowID }), new AbilityInfo(6, new int[9][]{ AbilityInfo.PickaxeID, AbilityInfo.TwoHandedAxeID, AbilityInfo.DoubleAxeID, AbilityInfo.ScimitarID, AbilityInfo.KatanaID, AbilityInfo.CrescentBladeID, AbilityInfo.QuarterStaffID, AbilityInfo.DoubleBladedStaffID, AbilityInfo.RepeatingCrossbowID }), new AbilityInfo(7, new int[6][]{ AbilityInfo.ButcherKnifeID, AbilityInfo.CleaverID, AbilityInfo.DaggerID, AbilityInfo.PikeID, AbilityInfo.KryssID, AbilityInfo.DoubleBladedStaffID }), new AbilityInfo(8, new int[8][]{ AbilityInfo.ExecAxeID, AbilityInfo.BoneHarvesterID, AbilityInfo.CrescentBladeID, AbilityInfo.HammerPickID, AbilityInfo.ScepterID, AbilityInfo.ShortSpearID, AbilityInfo.CrossbowID, AbilityInfo.BowID }), new AbilityInfo(9, new int[3][]{ AbilityInfo.HeavyCrossbowID, AbilityInfo.CompositeBowID, AbilityInfo.RepeatingCrossbowID }), new AbilityInfo(10, new int[10][]{ AbilityInfo.VikingSwordID, AbilityInfo.BardicheID, AbilityInfo.ScimitarID, AbilityInfo.ScytheID, AbilityInfo.BoneHarvesterID, AbilityInfo.GnarledStaffID, AbilityInfo.BlackStaffID, AbilityInfo.PikeID, AbilityInfo.SpearID, AbilityInfo.BowID }), new AbilityInfo(11, new int[8][]{ AbilityInfo.SkinningKnifeID, AbilityInfo.TwoHandedAxeID, AbilityInfo.CutlassID, AbilityInfo.SmithsHammerID, AbilityInfo.ClubID, AbilityInfo.DaggerID, AbilityInfo.ShortSpearID, AbilityInfo.SledgeHammerID }), new AbilityInfo(12, new int[5][]{ AbilityInfo.LargeBattleAxeID, AbilityInfo.HalberdID, AbilityInfo.DoubleAxeID, AbilityInfo.WarHammerID, AbilityInfo.BlackStaffID }) };
    private static Hashtable m_Table;
    private static AbilityInfo m_Active;
    private int m_Index;
    private int m_Tooltip;
    private int m_Icon;
    private int m_Name;
    private int[] m_Weapons;
    private GTextButton m_NameLabel;

    public static AbilityInfo Active
    {
      get
      {
        return AbilityInfo.m_Active;
      }
      set
      {
        AbilityInfo abilityInfo1 = value;
        AbilityInfo abilityInfo2 = AbilityInfo.m_Active;
        if (abilityInfo1 == abilityInfo2)
          return;
        AbilityInfo.m_Active = abilityInfo1;
        if (abilityInfo1 == null)
          Network.Send((Packet) new PSetActiveAbility(0));
        else
          Network.Send((Packet) new PSetActiveAbility(abilityInfo1.Index + 1));
        GCombatGump.Update();
      }
    }

    public static AbilityInfo Primary
    {
      get
      {
        return AbilityInfo.GetAbilityFor(World.Player, true);
      }
    }

    public static AbilityInfo Secondary
    {
      get
      {
        return AbilityInfo.GetAbilityFor(World.Player, false);
      }
    }

    public static AbilityInfo[] Abilities
    {
      get
      {
        return AbilityInfo.m_Abilities;
      }
    }

    public GTextButton NameLabel
    {
      get
      {
        return this.m_NameLabel;
      }
      set
      {
        this.m_NameLabel = value;
      }
    }

    public int Icon
    {
      get
      {
        return this.m_Icon;
      }
    }

    public int Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public int Index
    {
      get
      {
        return this.m_Index;
      }
    }

    public int Tooltip
    {
      get
      {
        return this.m_Tooltip;
      }
    }

    public AbilityInfo(int index, params int[][] weapons)
    {
      if (AbilityInfo.m_Table == null)
        AbilityInfo.m_Table = new Hashtable();
      this.m_Index = index;
      this.m_Name = 1028838 + index;
      this.m_Tooltip = 1061693 + index;
      this.m_Icon = 20992 + index;
      int length = 0;
      for (int index1 = 0; index1 < weapons.Length; ++index1)
        length += weapons[index1].Length;
      this.m_Weapons = new int[length];
      int index2 = 0;
      int index3 = 0;
      for (; index2 < weapons.Length; ++index2)
      {
        int index1 = 0;
        while (index1 < weapons[index2].Length)
        {
          this.m_Weapons[index3] = weapons[index2][index1];
          if (this.m_Weapons[index3] == 3921)
          {
            int num = 0 + 1;
          }
          ArrayList arrayList = (ArrayList) AbilityInfo.m_Table[(object) this.m_Weapons[index3]];
          if (arrayList == null)
            AbilityInfo.m_Table[(object) this.m_Weapons[index3]] = (object) (arrayList = new ArrayList());
          arrayList.Add((object) this);
          ++index1;
          ++index3;
        }
      }
    }

    public static void ClearActive()
    {
      AbilityInfo.m_Active = (AbilityInfo) null;
      GCombatGump.Update();
    }

    public static AbilityInfo GetAbilityFor(Mobile m, bool primary)
    {
      if (m == null)
        return AbilityInfo.m_Abilities[primary ? 4 : 10];
      Item equip1 = m.FindEquip(Layer.TwoHanded);
      if (equip1 != null)
      {
        int num = equip1.ID & 16383;
        ArrayList arrayList = (ArrayList) AbilityInfo.m_Table[(object) num];
        if (arrayList != null && arrayList.Count > 0)
          return (AbilityInfo) arrayList[primary ? 0 : arrayList.Count - 1];
      }
      Item equip2 = m.FindEquip(Layer.OneHanded);
      if (equip2 != null)
      {
        int num = equip2.ID & 16383;
        ArrayList arrayList = (ArrayList) AbilityInfo.m_Table[(object) num];
        if (arrayList != null && arrayList.Count > 0)
          return (AbilityInfo) arrayList[primary ? 0 : arrayList.Count - 1];
      }
      return AbilityInfo.m_Abilities[primary ? 4 : 10];
    }
  }
}

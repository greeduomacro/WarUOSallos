// Decompiled with JetBrains decompiler
// Type: PlayUO.Spells
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Text;
using System.Windows.Forms;

namespace PlayUO
{
  public class Spells
  {
    private static Reagent[] m_Reagents;
    private static SpellList m_RegularList;
    private static SpellList m_PaladinList;
    private static SpellList m_NecromancerList;
    private static string[] m_Numbers;

    public static SpellList RegularList
    {
      get
      {
        if (Spells.m_RegularList == null)
          Spells.m_RegularList = new SpellList("magery");
        return Spells.m_RegularList;
      }
    }

    public static SpellList PaladinList
    {
      get
      {
        if (Spells.m_PaladinList == null)
          Spells.m_PaladinList = new SpellList("chivalry");
        return Spells.m_PaladinList;
      }
    }

    public static SpellList NecromancerList
    {
      get
      {
        if (Spells.m_NecromancerList == null)
          Spells.m_NecromancerList = new SpellList("necromancy");
        return Spells.m_NecromancerList;
      }
    }

    public static Reagent[] Reagents
    {
      get
      {
        return Spells.m_Reagents;
      }
    }

    static Spells()
    {
      Debug.TimeBlock("Initializing Spells");
      Spells.m_Reagents = new Reagent[13]
      {
        new Reagent("Black pearl", 3962),
        new Reagent("Bloodmoss", 3963),
        new Reagent("Garlic", 3972),
        new Reagent("Ginseng", 3973),
        new Reagent("Mandrake root", 3974),
        new Reagent("Nightshade", 3976),
        new Reagent("Sulfurous ash", 3980),
        new Reagent("Spiders' silk", 3981),
        new Reagent("Bat wing", 3960),
        new Reagent("Grave dust", 3983),
        new Reagent("Daemon blood", 3965),
        new Reagent("Nox crystal", 3982),
        new Reagent("Pig iron", 3978)
      };
      Spells.m_Numbers = new string[8]
      {
        "First",
        "Second",
        "Third",
        "Fourth",
        "Fifth",
        "Sixth",
        "Seventh",
        "Eighth"
      };
      Debug.EndBlock();
    }

    public static Spell GetSpellByPower(string power)
    {
      return (Spells.GetSpellByPower(Spells.RegularList, power) ?? Spells.GetSpellByPower(Spells.PaladinList, power)) ?? Spells.GetSpellByPower(Spells.NecromancerList, power);
    }

    public static Spell GetSpellByPower(SpellList list, string power)
    {
      for (int index = 0; index < list.Spells.Length; ++index)
      {
        if (list.Spells[index].FullPower == power)
          return list.Spells[index];
      }
      return (Spell) null;
    }

    public static Spell GetSpellByID(int spellID)
    {
      return (Spells.GetSpellByID(Spells.RegularList, spellID) ?? Spells.GetSpellByID(Spells.PaladinList, spellID)) ?? Spells.GetSpellByID(Spells.NecromancerList, spellID);
    }

    public static Spell GetSpellByID(SpellList list, int spellID)
    {
      if (spellID < list.Start)
        return (Spell) null;
      spellID -= list.Start;
      if (spellID >= list.Spells.Length)
        return (Spell) null;
      return list.Spells[spellID];
    }

    public static Spell GetSpellByName(string name)
    {
      return (Spells.GetSpellByName(Spells.RegularList, name) ?? Spells.GetSpellByName(Spells.PaladinList, name)) ?? Spells.GetSpellByName(Spells.NecromancerList, name);
    }

    public static Spell GetSpellByName(SpellList list, string name)
    {
      for (int index = 0; index < list.Spells.Length; ++index)
      {
        if (list.Spells[index].Name == name)
          return list.Spells[index];
      }
      return (Spell) null;
    }

    public static Reagent GetReagent(string Name)
    {
      switch (Name)
      {
        case "Black pearl":
          return Spells.m_Reagents[0];
        case "Bloodmoss":
          return Spells.m_Reagents[1];
        case "Garlic":
          return Spells.m_Reagents[2];
        case "Ginseng":
          return Spells.m_Reagents[3];
        case "Mandrake root":
          return Spells.m_Reagents[4];
        case "Nightshade":
          return Spells.m_Reagents[5];
        case "Sulfurous ash":
          return Spells.m_Reagents[6];
        case "Spiders' silk":
          return Spells.m_Reagents[7];
        case "Bat wing":
          return Spells.m_Reagents[8];
        case "Grave dust":
          return Spells.m_Reagents[9];
        case "Daemon blood":
          return Spells.m_Reagents[10];
        case "Nox crystal":
          return Spells.m_Reagents[11];
        case "Pig iron":
          return Spells.m_Reagents[12];
        default:
          throw new ArgumentException("Invalid reagent name. Valid values are: [\"Black pearl\", \"Bloodmoss\", \"Garlic\", \"Ginseng\", \"Mandrake root\", \"Nightshade\", \"Sulfurous ash\", \"Spiders' silk\"].", Name);
      }
    }

    public static Gump OpenSpellbook(Item container)
    {
      Gump ToAdd = Spells.OpenSpellbook(container.Circle, container.LastSpell, container);
      ToAdd.X = (Engine.ScreenWidth - ToAdd.Width) / 2;
      ToAdd.Y = (Engine.ScreenHeight - ToAdd.Height) / 2;
      Gumps.Desktop.Children.Add(ToAdd);
      return ToAdd;
    }

    private static void ChangeCircle_OnClick(Gump sender)
    {
      Gump parent = sender.Parent;
      Item container = (Item) parent.GetTag("Container");
      object tag = sender.GetTag("Circle");
      if (container == null)
        return;
      int circle = tag == null ? 0 : (int) tag;
      int x = parent.X;
      int y = parent.Y;
      Gumps.Destroy(parent);
      Gump ToAdd = Spells.OpenSpellbook(circle, container.LastSpell, container);
      ToAdd.X = x;
      ToAdd.Y = y;
      Gumps.Desktop.Children.Add(ToAdd);
    }

    public static int GetBookType(int itemID)
    {
      switch (itemID)
      {
        case 3643:
        case 3834:
          return 1;
        case 8786:
          return 3;
        case 8787:
          return 2;
        default:
          return 0;
      }
    }

    public static int GetBookIndex(int itemID)
    {
      switch (Spells.GetBookType(itemID))
      {
        case 2:
          return 11008;
        case 3:
          return 11009;
        default:
          return 2220;
      }
    }

    public static int GetBookOffset(int itemID)
    {
      switch (Spells.GetBookType(itemID))
      {
        case 2:
          return 101;
        case 3:
          return 201;
        default:
          return 1;
      }
    }

    public static int GetBookIcon(int itemID)
    {
      switch (Spells.GetBookType(itemID))
      {
        case 2:
          return 11011;
        case 3:
          return 11012;
        default:
          return 2234;
      }
    }

    public static Gump OpenSpellbook(int circle, int lastSpell, Item container)
    {
      container.OpenSB = true;
      container.Circle = circle;
      container.LastSpell = lastSpell;
      circle &= -2;
      Engine.Sounds.PlaySound(85, -1, -1, -1, 0.75f, 0.0f);
      Engine.DoEvents();
      GDragable gdragable = new GDragable(Spells.GetBookIndex(container.ID), 0, 0);
      gdragable.SetTag("Container", (object) container);
      gdragable.SetTag("Dispose", (object) "Spellbook");
      gdragable.Children.Add((Gump) new Spells.GMinimizer());
      SpellList spellList = container.SpellbookOffset != 101 ? (container.SpellbookOffset != 201 ? Spells.RegularList : Spells.PaladinList) : Spells.NecromancerList;
      if (lastSpell >= spellList.Start && lastSpell < spellList.Start + spellList.Spells.Length)
      {
        int num1 = (lastSpell - spellList.Start) / spellList.SpellsPerCircle;
        int num2 = (lastSpell - spellList.Start) % spellList.SpellsPerCircle;
        if (num1 >= 0 && num1 < spellList.Circles)
        {
          if (num1 == circle)
          {
            gdragable.Children.Add((Gump) new GImage(2221, 184, 2));
            gdragable.Children.Add((Gump) new GImage(2223, 183, 52 + num2 * 15));
          }
          else if (num1 == circle + 1)
          {
            gdragable.Children.Add((Gump) new GImage(2222, 204, 3));
            gdragable.Children.Add((Gump) new GImage(2224, 204, 52 + num2 * 15));
          }
        }
      }
      gdragable.Children.Add((Gump) new GLabel("INDEX", (IFont) Engine.GetFont(6), Hues.Default, 106, 10));
      gdragable.Children.Add((Gump) new GLabel("INDEX", (IFont) Engine.GetFont(6), Hues.Default, 269, 10));
      OnClick ClickHandler = new OnClick(Spells.ChangeCircle_OnClick);
      int[] numArray1 = new int[8]{ 58, 93, 130, 164, 227, 260, 297, 332 };
      int[] numArray2 = new int[2]{ 52, 52 };
      if (spellList.DisplayIndex)
      {
        for (int index = 0; index < spellList.Circles; ++index)
        {
          GButton gbutton = new GButton(2225 + index, 2225 + index, 2225 + index, numArray1[index], 175, ClickHandler);
          gbutton.SetTag("Circle", (object) index);
          gdragable.Children.Add((Gump) gbutton);
        }
      }
      if (spellList.DisplayCircles)
      {
        if (circle > 0)
        {
          GButton gbutton = new GButton(2235, 2235, 2235, 50, 8, ClickHandler);
          gbutton.SetTag("Circle", (object) (circle - 1));
          gdragable.Children.Add((Gump) gbutton);
        }
        if (circle < (spellList.Circles - 1 & -2))
        {
          GButton gbutton = new GButton(2236, 2236, 2236, 321, 8, ClickHandler);
          gbutton.SetTag("Circle", (object) (circle + 2));
          gdragable.Children.Add((Gump) gbutton);
        }
        for (int index = circle; index < circle + 2; ++index)
        {
          int X = (index & 1) == 0 ? 62 : 225;
          string str = index < 0 || index >= Spells.m_Numbers.Length ? "Bad" : Spells.m_Numbers[index];
          gdragable.Children.Add((Gump) new GLabel(string.Format("{0} Circle", (object) str), (IFont) Engine.GetFont(6), Hues.Default, X, 30));
        }
      }
      int num3 = circle * spellList.SpellsPerCircle;
      int num4 = (circle + 2) * spellList.SpellsPerCircle;
      for (int index1 = num3; index1 < num4; ++index1)
      {
        if (index1 >= num3 && index1 < num4 && container.GetSpellContained(index1))
        {
          int num1 = index1 / spellList.SpellsPerCircle;
          Spell spellById = Spells.GetSpellByID(container.SpellbookOffset + index1);
          if (spellById != null)
          {
            GSpellName gspellName = new GSpellName(container.SpellbookOffset + index1, spellById.Name, (IFont) Engine.GetFont(9), Hues.Load(648), Hues.Load(651), 62 + (num1 & 1) * 163, numArray2[num1 & 1]);
            numArray2[num1 & 1] += 15;
            string.Format("{0}\n", (object) spellById.Name);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(spellById.Name);
            stringBuilder.Append('\n');
            for (int index2 = 0; index2 < spellById.Power.Length; ++index2)
            {
              stringBuilder.Append(spellById.Power[index2].Name);
              stringBuilder.Append(' ');
            }
            for (int index2 = 0; index2 < spellById.Reagents.Count; ++index2)
            {
              stringBuilder.Append('\n');
              stringBuilder.Append(((Reagent) spellById.Reagents[index2]).Name);
            }
            if (spellById.Tithing > 0)
            {
              stringBuilder.Append('\n');
              stringBuilder.AppendFormat("Tithing: {0}", (object) spellById.Tithing);
            }
            if (spellById.Mana > 0)
            {
              stringBuilder.Append('\n');
              stringBuilder.AppendFormat("Mana: {0}", (object) spellById.Mana);
            }
            if (spellById.Skill > 0)
            {
              stringBuilder.Append('\n');
              stringBuilder.AppendFormat("Skill: {0}", (object) spellById.Skill);
            }
            Tooltip tooltip = new Tooltip(stringBuilder.ToString(), true);
            gspellName.Tooltip = (ITooltip) tooltip;
            gdragable.Children.Add((Gump) gspellName);
          }
        }
      }
      return (Gump) gdragable;
    }

    private class GMinimizer : GRegion
    {
      public GMinimizer()
        : base(4, 101, 17, 17)
      {
        this.m_Tooltip = (ITooltip) new Tooltip("Minimize");
      }

      protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
      {
        if ((mb & MouseButtons.Left) == MouseButtons.None)
          return;
        this.m_Parent.Visible = false;
        Gumps.Desktop.Children.Add((Gump) new GSpellbookIcon(this.m_Parent, (Item) this.m_Parent.GetTag("Container")));
      }
    }
  }
}

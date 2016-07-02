// Decompiled with JetBrains decompiler
// Type: PlayUO.Spell
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System.Collections;

namespace PlayUO
{
  public class Spell
  {
    private string m_Name;
    private int m_SpellID;
    private PlayUO.Power[] m_Power;
    private ArrayList m_Reagents;
    private string m_FullPower;
    private int m_Tithing;
    private int m_Skill;
    private int m_Mana;

    public SpellID SpellID
    {
      get
      {
        return (SpellID) this.m_SpellID;
      }
    }

    public int Circle
    {
      get
      {
        return 1 + (this.m_SpellID - 1) / 8;
      }
    }

    public ArrayList Reagents
    {
      get
      {
        return this.m_Reagents;
      }
    }

    public PlayUO.Power[] Power
    {
      get
      {
        return this.m_Power;
      }
    }

    public string FullPower
    {
      get
      {
        return this.m_FullPower;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public int Tithing
    {
      get
      {
        return this.m_Tithing;
      }
      set
      {
        this.m_Tithing = value;
      }
    }

    public int Skill
    {
      get
      {
        return this.m_Skill;
      }
      set
      {
        this.m_Skill = value;
      }
    }

    public int Mana
    {
      get
      {
        return this.m_Mana;
      }
      set
      {
        this.m_Mana = value;
      }
    }

    public Spell(string name, string power, int spellID)
    {
      this.m_Name = name;
      this.m_Power = PlayUO.Power.Parse(power);
      this.m_FullPower = power;
      this.m_SpellID = spellID;
      this.m_Reagents = new ArrayList();
    }

    public void Cast()
    {
      if (Preferences.Current.Options.ClearHandsBeforeCast)
        Engine.Dequip(false);
      Network.Send((Packet) new PCastSpell(this.m_SpellID));
    }
  }
}

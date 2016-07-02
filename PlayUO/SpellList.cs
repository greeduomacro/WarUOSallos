// Decompiled with JetBrains decompiler
// Type: PlayUO.SpellList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.IO;
using System.Xml;

namespace PlayUO
{
  public class SpellList
  {
    private int m_Circles;
    private bool m_DisplayCircles;
    private bool m_DisplayIndex;
    private Spell[] m_Spells;
    private int m_SpellID;
    private int m_Start;
    private int m_SpellsPerCircle;

    public int Circles
    {
      get
      {
        return this.m_Circles;
      }
    }

    public int Start
    {
      get
      {
        return this.m_Start;
      }
    }

    public int SpellsPerCircle
    {
      get
      {
        return this.m_SpellsPerCircle;
      }
    }

    public bool DisplayCircles
    {
      get
      {
        return this.m_DisplayCircles;
      }
    }

    public bool DisplayIndex
    {
      get
      {
        return this.m_DisplayIndex;
      }
    }

    public Spell[] Spells
    {
      get
      {
        return this.m_Spells;
      }
    }

    public SpellList(string name)
    {
      bool flag = true;
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile(string.Format("play/config/spells/{0}.xml", (object) name));
      if (archivedFile != null)
      {
        using (Stream input = archivedFile.Download())
        {
          XmlTextReader xml = new XmlTextReader(input);
          xml.WhitespaceHandling = WhitespaceHandling.None;
          flag = !this.Parse(xml);
          xml.Close();
        }
      }
      if (!flag)
        return;
      this.m_Circles = 0;
      this.m_DisplayCircles = false;
      this.m_DisplayIndex = false;
      this.m_Spells = new Spell[0];
      this.m_SpellID = 0;
    }

    private bool Parse_Reagent(XmlTextReader xml, Spell spell)
    {
      string Name = (string) null;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "name":
            Name = xml.Value;
            continue;
          default:
            return false;
        }
      }
      if (Name == null)
        return false;
      spell.Reagents.Add((object) new Reagent(Name));
      return true;
    }

    private bool Parse_Tithing(XmlTextReader xml, Spell spell)
    {
      string str = (string) null;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "value":
            str = xml.Value;
            continue;
          default:
            return false;
        }
      }
      if (str == null)
        return false;
      spell.Tithing = Convert.ToInt32(str);
      return true;
    }

    private bool Parse_Skill(XmlTextReader xml, Spell spell)
    {
      string str = (string) null;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "value":
            str = xml.Value;
            continue;
          default:
            return false;
        }
      }
      if (str == null)
        return false;
      spell.Skill = Convert.ToInt32(str);
      return true;
    }

    private bool Parse_Mana(XmlTextReader xml, Spell spell)
    {
      string str = (string) null;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "value":
            str = xml.Value;
            continue;
          default:
            return false;
        }
      }
      if (str == null)
        return false;
      spell.Mana = Convert.ToInt32(str);
      return true;
    }

    private bool Parse_Spell(XmlTextReader xml)
    {
      string name = (string) null;
      string power = (string) null;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "name":
            name = xml.Value;
            continue;
          case "power":
            power = xml.Value;
            continue;
          default:
            return false;
        }
      }
      if (name == null)
        return false;
      if (power == null)
        power = "";
      Spell spell = new Spell(name, power, this.m_Start + this.m_SpellID);
      this.m_Spells[this.m_SpellID++] = spell;
      while (xml.Read())
      {
        switch (xml.NodeType)
        {
          case XmlNodeType.Element:
            switch (xml.Name)
            {
              case "reagent":
                if (!this.Parse_Reagent(xml, spell))
                  return false;
                continue;
              case "tithing":
                if (!this.Parse_Tithing(xml, spell))
                  return false;
                continue;
              case "skill":
                if (!this.Parse_Skill(xml, spell))
                  return false;
                continue;
              case "mana":
                if (!this.Parse_Mana(xml, spell))
                  return false;
                continue;
              default:
                return false;
            }
          case XmlNodeType.Comment:
            continue;
          case XmlNodeType.EndElement:
            return true;
          default:
            return false;
        }
      }
      return false;
    }

    private bool Parse_Spells(XmlTextReader xml)
    {
      int num1 = 0;
      int length = 0;
      int num2 = 0;
      int num3 = 0;
      bool flag1 = false;
      bool flag2 = false;
      while (xml.MoveToNextAttribute())
      {
        switch (xml.Name)
        {
          case "circles":
            num1 = Convert.ToInt32(xml.Value);
            continue;
          case "count":
            length = Convert.ToInt32(xml.Value);
            continue;
          case "start":
            num2 = Convert.ToInt32(xml.Value);
            continue;
          case "spellsPerCircle":
            num3 = Convert.ToInt32(xml.Value);
            continue;
          case "displayCircles":
            flag1 = Convert.ToBoolean(xml.Value);
            continue;
          case "displayIndex":
            flag2 = Convert.ToBoolean(xml.Value);
            continue;
          default:
            return false;
        }
      }
      this.m_Circles = num1;
      this.m_DisplayCircles = flag1;
      this.m_DisplayIndex = flag2;
      this.m_Start = num2;
      this.m_SpellsPerCircle = num3;
      this.m_Spells = new Spell[length];
      while (xml.Read())
      {
        switch (xml.NodeType)
        {
          case XmlNodeType.Element:
            switch (xml.Name)
            {
              case "spell":
                if (!this.Parse_Spell(xml))
                  return false;
                continue;
              default:
                return false;
            }
          case XmlNodeType.Comment:
            continue;
          case XmlNodeType.EndElement:
            return true;
          default:
            return false;
        }
      }
      return false;
    }

    private bool Parse(XmlTextReader xml)
    {
      while (xml.Read())
      {
        switch (xml.NodeType)
        {
          case XmlNodeType.Element:
            switch (xml.Name)
            {
              case "spells":
                if (!this.Parse_Spells(xml))
                  return false;
                continue;
              default:
                return false;
            }
          case XmlNodeType.Comment:
          case XmlNodeType.XmlDeclaration:
            continue;
          default:
            return false;
        }
      }
      return true;
    }
  }
}

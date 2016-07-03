// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.SpellIconLayout
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class SpellIconLayout : GumpLayout
  {
    public static readonly PersistableType TypeCode = new PersistableType("spellIcon", Construct);
    private int m_SpellID;

    public override PersistableType TypeID
    {
      get
      {
        return SpellIconLayout.TypeCode;
      }
    }

    public int SpellID
    {
      get
      {
        return this.m_SpellID;
      }
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new SpellIconLayout();
    }

    public override Gump CreateGump()
    {
      return (Gump) new GSpellIcon(this.m_SpellID);
    }

    public override void Update(Gump g)
    {
      base.Update(g);
      this.m_SpellID = (g as GSpellIcon).m_SpellID;
    }

    protected override void SerializeAttributes(PersistanceWriter op)
    {
      base.SerializeAttributes(op);
      op.SetInt32("id", this.m_SpellID);
    }

    protected override void DeserializeAttributes(PersistanceReader ip)
    {
      base.DeserializeAttributes(ip);
      this.m_SpellID = ip.GetInt32("id");
    }
  }
}

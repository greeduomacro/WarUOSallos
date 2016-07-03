// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.SkillIconLayout
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class SkillIconLayout : GumpLayout
  {
    public static readonly PersistableType TypeCode = new PersistableType("skillIcon", Construct);
    private int m_SkillID;

    public override PersistableType TypeID
    {
      get
      {
        return SkillIconLayout.TypeCode;
      }
    }

    public int SkillID
    {
      get
      {
        return this.m_SkillID;
      }
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new SkillIconLayout();
    }

    public override void Update(Gump g)
    {
      base.Update(g);
      this.m_SkillID = (g as GSkillIcon).SkillID;
    }

    public override Gump CreateGump()
    {
      return (Gump) new GSkillIcon(Engine.Skills[this.m_SkillID]);
    }

    protected override void SerializeAttributes(PersistanceWriter op)
    {
      base.SerializeAttributes(op);
      op.SetInt32("id", this.m_SkillID);
    }

    protected override void DeserializeAttributes(PersistanceReader ip)
    {
      base.DeserializeAttributes(ip);
      this.m_SkillID = ip.GetInt32("id");
    }
  }
}

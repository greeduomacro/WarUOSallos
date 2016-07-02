// Decompiled with JetBrains decompiler
// Type: PlayUO.Skill
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Skill
  {
    public string Name;
    public float Value;
    public float Real;
    public int ID;
    public SkillGroup Group;
    public SkillLock Lock;
    public bool Action;

    public Skill(int id, bool action, string name)
    {
      this.ID = id;
      this.Action = action;
      this.Name = name;
      this.Value = 0.0f;
      this.Real = 0.0f;
      this.Group = (SkillGroup) null;
      this.Lock = SkillLock.Up;
    }

    public void Use()
    {
      Network.Send((Packet) new PUseSkill(this));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PlayUO.SkillGroup
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class SkillGroup
  {
    public string Name;
    public int GroupID;
    public ArrayList Skills;

    public SkillGroup(string name, int groupID)
    {
      this.Name = name;
      this.GroupID = groupID;
      this.Skills = new ArrayList();
    }
  }
}

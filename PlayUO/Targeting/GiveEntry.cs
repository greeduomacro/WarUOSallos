// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.GiveEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  public abstract class GiveEntry
  {
    protected string m_Name;
    protected IItemValidator[] m_Validators;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public IItemValidator[] Validators
    {
      get
      {
        return this.m_Validators;
      }
    }

    public GiveEntry(string name, params IItemValidator[] validators)
    {
      this.m_Name = name;
      this.m_Validators = validators;
    }

    public abstract int GetAmount(int currentAmount);
  }
}

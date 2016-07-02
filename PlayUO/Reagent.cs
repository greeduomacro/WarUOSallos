// Decompiled with JetBrains decompiler
// Type: PlayUO.Reagent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public struct Reagent
  {
    private string m_Name;
    private int m_ItemID;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public int ItemID
    {
      get
      {
        return this.m_ItemID;
      }
    }

    public Reagent(string Name)
    {
      this.m_Name = Name;
      this.m_ItemID = Spells.GetReagent(Name).ItemID;
    }

    public Reagent(string Name, int ItemID)
    {
      this.m_Name = Name;
      this.m_ItemID = ItemID;
    }
  }
}

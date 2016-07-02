// Decompiled with JetBrains decompiler
// Type: PlayUO.ResistInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ResistInfo
  {
    public static ResistInfo[] m_Armor = new ResistInfo[13]{ new ResistInfo(2, 4, 3, 3, 3, new string[2]{ "leather ", "female leather " }), new ResistInfo(2, 4, 3, 3, 4, new string[3]{ "studded ", "female studded ", " ranger armor" }), new ResistInfo(3, 3, 1, 5, 3, new string[1]{ "ringmail " }), new ResistInfo(5, 3, 2, 3, 2, new string[1]{ "platemail " }), new ResistInfo(3, 3, 3, 3, 3, new string[1]{ "dragon " }), new ResistInfo(6, 6, 7, 5, 7, new string[1]{ "daemon bone " }), new ResistInfo(4, 4, 4, 1, 2, new string[1]{ "chainmail " }), new ResistInfo(3, 3, 4, 2, 4, new string[1]{ "bone " }), new ResistInfo(7, 2, 2, 2, 2, new string[1]{ "bascinet" }), new ResistInfo(3, 3, 3, 3, 3, new string[1]{ "close helm" }), new ResistInfo(2, 4, 4, 3, 2, new string[1]{ "helmet" }), new ResistInfo(4, 1, 4, 4, 2, new string[1]{ "norse helm" }), new ResistInfo(3, 1, 3, 3, 4, new string[1]{ "orc helm" }) };
    public static ResistInfo[] m_Materials = new ResistInfo[17]{ new ResistInfo(6, 0, 0, 0, 0, new string[1]{ "dull copper " }), new ResistInfo(2, 1, 0, 0, 5, new string[1]{ "shadow iron " }), new ResistInfo(1, 1, 0, 5, 2, new string[1]{ "copper " }), new ResistInfo(3, 0, 5, 1, 1, new string[1]{ "bronze " }), new ResistInfo(1, 1, 2, 0, 2, new string[1]{ "golden " }), new ResistInfo(2, 3, 2, 2, 2, new string[1]{ "agapite " }), new ResistInfo(3, 3, 2, 3, 1, new string[1]{ "verite " }), new ResistInfo(4, 0, 3, 3, 3, new string[1]{ "valorite " }), new ResistInfo(5, 0, 0, 0, 0, new string[1]{ "spined " }), new ResistInfo(2, 3, 2, 2, 2, new string[1]{ "horned " }), new ResistInfo(2, 1, 2, 3, 4, new string[1]{ "barbed " }), new ResistInfo(0, 10, -3, 0, 0, new string[1]{ "red dragon " }), new ResistInfo(-3, 0, 0, 0, 0, new string[1]{ "yellow dragon " }), new ResistInfo(10, 0, 0, 0, -3, new string[1]{ "black dragon " }), new ResistInfo(0, -3, 0, 10, 0, new string[1]{ "green dragon " }), new ResistInfo(-3, 0, 10, 0, 0, new string[1]{ "white dragon " }), new ResistInfo(0, 0, 0, -3, 10, new string[1]{ "blue dragon " }) };
    public int m_Physical;
    public int m_Fire;
    public int m_Cold;
    public int m_Poison;
    public int m_Energy;
    public string[] m_Names;

    public ResistInfo(int physical, int fire, int cold, int poison, int energy, params string[] names)
    {
      this.m_Physical = physical;
      this.m_Fire = fire;
      this.m_Cold = cold;
      this.m_Poison = poison;
      this.m_Energy = energy;
      this.m_Names = names;
    }

    public static ResistInfo Find(string text, ResistInfo[] list)
    {
      for (int index1 = 0; index1 < list.Length; ++index1)
      {
        for (int index2 = 0; index2 < list[index1].m_Names.Length; ++index2)
        {
          if (text.IndexOf(list[index1].m_Names[index2]) >= 0)
            return list[index1];
        }
      }
      return (ResistInfo) null;
    }
  }
}

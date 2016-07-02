// Decompiled with JetBrains decompiler
// Type: PlayUO.CustomMultiLoader
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class CustomMultiLoader
  {
    private static Hashtable m_Hashtable = new Hashtable();

    public static CustomMultiEntry GetCustomMulti(int serial, int revision)
    {
      ArrayList arrayList = (ArrayList) CustomMultiLoader.m_Hashtable[(object) serial];
      if (arrayList == null)
        return (CustomMultiEntry) null;
      for (int index = 0; index < arrayList.Count; ++index)
      {
        CustomMultiEntry customMultiEntry = (CustomMultiEntry) arrayList[index];
        if (customMultiEntry.Revision == revision)
          return customMultiEntry;
      }
      return (CustomMultiEntry) null;
    }

    public static void SetCustomMulti(int serial, int revision, Multi baseMulti, int compressionType, byte[] buffer)
    {
      ArrayList arrayList = (ArrayList) CustomMultiLoader.m_Hashtable[(object) serial];
      if (arrayList == null)
        CustomMultiLoader.m_Hashtable[(object) serial] = (object) (arrayList = new ArrayList());
      CustomMultiEntry customMultiEntry = new CustomMultiEntry(serial, revision, baseMulti, compressionType, buffer);
      for (int index = 0; index < arrayList.Count; ++index)
      {
        if (((CustomMultiEntry) arrayList[index]).Revision == revision)
        {
          arrayList[index] = (object) customMultiEntry;
          return;
        }
      }
      arrayList.Add((object) customMultiEntry);
      Map.Invalidate();
      GRadar.Invalidate();
    }
  }
}

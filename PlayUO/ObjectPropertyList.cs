// Decompiled with JetBrains decompiler
// Type: PlayUO.ObjectPropertyList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class ObjectPropertyList
  {
    private static Hashtable m_Table = new Hashtable();
    private int m_Serial;
    private int m_Number;
    private ObjectProperty[] m_Props;

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public int Number
    {
      get
      {
        return this.m_Number;
      }
    }

    public ObjectProperty[] Properties
    {
      get
      {
        return this.m_Props;
      }
    }

    public ObjectPropertyList(int serial, int number, ObjectProperty[] props)
    {
      this.m_Serial = serial;
      this.m_Number = number;
      this.m_Props = props;
      ObjectPropertyList.m_Table[(object) ObjectPropertyList.GetKey(serial, number)] = (object) this;
    }

    public static long GetKey(int serial, int number)
    {
      return (long) serial << 32 | (long) (uint) number;
    }

    public static ObjectPropertyList Find(int serial, int number)
    {
      return (ObjectPropertyList) ObjectPropertyList.m_Table[(object) ObjectPropertyList.GetKey(serial, number)];
    }
  }
}

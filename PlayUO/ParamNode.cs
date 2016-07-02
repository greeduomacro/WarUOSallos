// Decompiled with JetBrains decompiler
// Type: PlayUO.ParamNode
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ParamNode
  {
    private string m_Name;
    private string m_Param;
    private ParamNode[] m_Nodes;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
      set
      {
        this.m_Name = value;
      }
    }

    public string Param
    {
      get
      {
        return this.m_Param;
      }
      set
      {
        this.m_Param = value;
      }
    }

    public ParamNode[] Nodes
    {
      get
      {
        return this.m_Nodes;
      }
      set
      {
        this.m_Nodes = value;
      }
    }

    public static ParamNode[] Toggle
    {
      get
      {
        return new ParamNode[3]{ new ParamNode("Toggle", ""), new ParamNode("On", "On"), new ParamNode("Off", "Off") };
      }
    }

    public static ParamNode[] Empty
    {
      get
      {
        return new ParamNode[0];
      }
    }

    public ParamNode(string name, string param)
      : this(name, param, (ParamNode[]) null)
    {
    }

    public ParamNode(string name, ParamNode[] nodes)
      : this(name, (string) null, nodes)
    {
    }

    public ParamNode(string name, string[] nodes)
      : this(name, (string) null, new ParamNode[nodes.Length])
    {
      for (int index = 0; index < this.m_Nodes.Length; ++index)
        this.m_Nodes[index] = new ParamNode(nodes[index], nodes[index]);
    }

    private ParamNode(string name, string param, ParamNode[] nodes)
    {
      this.m_Name = name;
      this.m_Param = param;
      this.m_Nodes = nodes;
    }

    public static ParamNode[] Count(int start, int count, string format)
    {
      ParamNode[] paramNodeArray = new ParamNode[count];
      for (int index = 0; index < count; ++index)
        paramNodeArray[index] = new ParamNode(string.Format(format, (object) (1 + index)), index.ToString());
      return paramNodeArray;
    }
  }
}

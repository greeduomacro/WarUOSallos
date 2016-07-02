// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.SizableLayout
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System.Drawing;

namespace PlayUO.Profiles
{
  public abstract class SizableLayout : GumpLayout
  {
    protected Size m_Size;

    public Size Size
    {
      get
      {
        return this.m_Size;
      }
      set
      {
        this.m_Size = value;
      }
    }

    public override void Setup(Gump g)
    {
      base.Setup(g);
      g.Width = this.m_Size.Width;
      g.Height = this.m_Size.Height;
    }

    protected override void SerializeAttributes(PersistanceWriter op)
    {
      base.SerializeAttributes(op);
      op.SetSize("gump", this.m_Size);
    }

    protected override void DeserializeAttributes(PersistanceReader ip)
    {
      base.DeserializeAttributes(ip);
      this.m_Size = ip.GetSize("gump");
    }
  }
}

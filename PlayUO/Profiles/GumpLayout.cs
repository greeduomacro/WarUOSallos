// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.GumpLayout
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public abstract class GumpLayout : PersistableObject
  {
    protected PlayUO.Point m_Offset;

    public PlayUO.Point Offset
    {
      get
      {
        return this.m_Offset;
      }
      set
      {
        this.m_Offset = value;
      }
    }

    public GumpLayout()
    {
      base.\u002Ector();
    }

    public virtual void Remove()
    {
      Preferences.Current.Layout.Gumps.Remove(this);
    }

    public virtual void BringToTop()
    {
      Preferences.Current.Layout.Gumps.Remove(this);
      Preferences.Current.Layout.Gumps.Add(this);
    }

    public virtual void Update(Gump g)
    {
      this.m_Offset = new PlayUO.Point(g.X, g.Y);
    }

    public virtual void Setup(Gump g)
    {
      g.X = this.m_Offset.X;
      g.Y = this.m_Offset.Y;
    }

    public abstract Gump CreateGump();

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetPoint("gump", (System.Drawing.Point) this.m_Offset);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Offset = (PlayUO.Point) ip.GetPoint("gump");
    }
  }
}

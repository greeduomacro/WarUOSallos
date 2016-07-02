// Decompiled with JetBrains decompiler
// Type: PlayUO.Faction
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class Faction
  {
    public static readonly Faction Minax = new Faction("Minax", "Minax");
    public static readonly Faction CouncilOfMages = new Faction("Council of Mages", "CoM");
    public static readonly Faction Shadowlords = new Faction("Shadowlords", "SL");
    public static readonly Faction TrueBritannians = new Faction("True Britannians", "TB");
    private string friendlyName;
    private string abbreviation;

    public string FriendlyName
    {
      get
      {
        return this.friendlyName;
      }
    }

    public string Abbreviation
    {
      get
      {
        return this.abbreviation;
      }
    }

    public Faction(string friendlyName, string abbreviation)
    {
      this.friendlyName = friendlyName;
      this.abbreviation = abbreviation;
    }
  }
}

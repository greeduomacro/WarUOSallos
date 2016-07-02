// Decompiled with JetBrains decompiler
// Type: PlayUO.GMapTracker
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GMapTracker : GTracker
  {
    private static GenericRadarTrackable _trackable = new GenericRadarTrackable(TimeSpan.FromMinutes(5.0));

    public static GenericRadarTrackable Trackable
    {
      get
      {
        return GMapTracker._trackable;
      }
    }

    protected internal override void Render(int X, int Y)
    {
      if (GMapTracker._trackable.HasExpired)
        return;
      this.Render(X, Y, GMapTracker._trackable.X, GMapTracker._trackable.Y);
    }

    protected override string GetPluralString(string direction, int distance)
    {
      return "Treasure: " + distance.ToString() + " tiles " + direction;
    }

    protected override string GetSingularString(string direction)
    {
      return "Treasure: 1 tile " + direction;
    }
  }
}

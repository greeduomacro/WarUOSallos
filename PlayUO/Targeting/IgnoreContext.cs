// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.IgnoreContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO.Targeting
{
  internal class IgnoreContext : ActionContext
  {
    private Mobile m_Mobile;

    public IgnoreContext(Mobile mob)
    {
      this.m_Mobile = mob;
    }

    public override void OnDispatch()
    {
      this.m_Mobile.QueryStats();
    }

    public override void OnFinish()
    {
      IgnoreList ignoreList = Player.Current.IgnoreList;
      Character character = ignoreList[this.m_Mobile];
      if (character == null)
      {
        if (this.m_Mobile.HasName)
        {
          ignoreList.Characters.Add(new Character(this.m_Mobile));
          this.m_Mobile.IsIgnored = true;
          this.m_Mobile.AddTextMessage("", "- ignored -", Engine.DefaultFont, Hues.Load(144), true);
          Engine.AddTextMessage("They have been added to the ignore list.", Engine.DefaultFont, Hues.Load(144));
        }
        else
          Engine.AddTextMessage("Unable to friend that person.");
      }
      else
      {
        ignoreList.Characters.Remove(character);
        this.m_Mobile.IsIgnored = false;
        this.m_Mobile.AddTextMessage("", "- no longer ignored -", Engine.DefaultFont, Hues.Load(63), true);
        Engine.AddTextMessage("They have been removed from the ignore list.", Engine.DefaultFont, Hues.Load(63));
      }
    }
  }
}

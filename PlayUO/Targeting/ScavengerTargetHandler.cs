// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.ScavengerTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO.Targeting
{
  internal class ScavengerTargetHandler : ClientTargetHandler
  {
    private bool m_IsAdd;
    private bool m_ByType;

    public ScavengerTargetHandler(bool isAdd, bool byType)
    {
      this.m_IsAdd = isAdd;
      this.m_ByType = byType;
    }

    protected override bool OnTarget(Item item)
    {
      ScavengerAgent scavenger = Preferences.Current.Scavenger;
      ItemRef itemRef1 = scavenger[item];
      if (itemRef1 == null)
      {
        foreach (ItemRef itemRef2 in scavenger.Items)
        {
          if (itemRef2.Serial == 0 && itemRef2.ItemID == item.ID)
          {
            itemRef1 = itemRef2;
            break;
          }
        }
      }
      if (this.m_IsAdd)
      {
        if (itemRef1 == null)
        {
          if (this.m_ByType)
          {
            scavenger.Items.Add(new ItemRef(item.ID));
            Engine.AddTextMessage("Item type added to the scavenger list.");
          }
          else
          {
            scavenger.Items.Add(new ItemRef(item));
            Engine.AddTextMessage("Item instance added to the scavenger list.");
          }
        }
        else if (itemRef1.Serial == 0)
          Engine.AddTextMessage("Item type is already in the scavenger list.");
        else if (this.m_ByType)
        {
          scavenger.Items.Remove(itemRef1);
          scavenger.Items.Add(new ItemRef(item.ID));
          Engine.AddTextMessage("Scavenger entry changed to by-type.");
        }
        else
          Engine.AddTextMessage("Item instance is already in the scavenger list.");
      }
      else if (itemRef1 != null)
      {
        scavenger.Items.Remove(itemRef1);
        if (itemRef1.Serial == 0)
          Engine.AddTextMessage("Item type removed from the scavenger list.");
        else
          Engine.AddTextMessage("Item instance removed from the scavenger list.");
      }
      else
        Engine.AddTextMessage("That's not even in the scavenger list.");
      return true;
    }
  }
}

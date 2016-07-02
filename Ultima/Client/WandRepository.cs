// Decompiled with JetBrains decompiler
// Type: Ultima.Client.WandRepository
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO;
using System.Collections.Generic;

namespace Ultima.Client
{
  internal sealed class WandRepository
  {
    private static readonly Dictionary<Item, WandInformation> table = new Dictionary<Item, WandInformation>();

    public static void Set(Item item, WandInformation? value)
    {
      if (value.HasValue)
        WandRepository.Store(item, value.Value);
      else
        WandRepository.Delete(item);
    }

    public static void Store(Item item, WandInformation value)
    {
      WandRepository.table[item] = value;
    }

    public static void Delete(Item item)
    {
      WandRepository.table.Remove(item);
    }

    public static bool Retrieve(Item item, out WandInformation value)
    {
      return WandRepository.table.TryGetValue(item, out value);
    }

    public static Item Find(WandEffect effect)
    {
      Mobile player = World.Player;
      if (player == null)
        return (Item) null;
      Item equip = player.FindEquip(Layer.OneHanded);
      WandInformation wandInformation;
      if (equip != null && WandRepository.Retrieve(equip, out wandInformation) && (wandInformation.Effect == effect && wandInformation.Charges > 0))
      {
        equip.Look();
        return equip;
      }
      foreach (KeyValuePair<Item, WandInformation> keyValuePair in WandRepository.table)
      {
        if (keyValuePair.Value.Effect == effect && (keyValuePair.Value.Charges > 0 && keyValuePair.Key.IsChildOf((Agent) player)))
        {
          keyValuePair.Key.Look();
          return keyValuePair.Key;
        }
      }
      return (Item) null;
    }
  }
}

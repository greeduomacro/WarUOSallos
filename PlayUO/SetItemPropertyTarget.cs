// Decompiled with JetBrains decompiler
// Type: PlayUO.SetItemPropertyTarget
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;

namespace PlayUO
{
  internal class SetItemPropertyTarget : ClientTargetHandler
  {
    private GPropertyEntry m_Entry;

    public SetItemPropertyTarget(GPropertyEntry entry)
    {
      this.m_Entry = entry;
    }

    protected override bool OnTarget(Item item)
    {
      this.m_Entry.SetValue((object) item);
      return true;
    }
  }
}

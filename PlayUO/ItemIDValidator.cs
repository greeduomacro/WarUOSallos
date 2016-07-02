// Decompiled with JetBrains decompiler
// Type: PlayUO.ItemIDValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ItemIDValidator : IItemValidator
  {
    private int[] m_List;
    private IItemValidator m_Parent;

    public int[] List
    {
      get
      {
        return this.m_List;
      }
    }

    public ItemIDValidator(params int[] list)
      : this((IItemValidator) null, list)
    {
    }

    public ItemIDValidator(IItemValidator parent, params int[] list)
    {
      this.m_Parent = parent;
      this.m_List = list;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent != null && !this.m_Parent.IsValid(check) || (this.m_List == null || this.m_List.Length <= 0))
        return false;
      int num = check.ID & 16383;
      for (int index = 0; index < this.m_List.Length; ++index)
      {
        if (this.m_List[index] == num)
          return true;
      }
      return false;
    }
  }
}

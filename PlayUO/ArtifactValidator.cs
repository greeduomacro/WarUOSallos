// Decompiled with JetBrains decompiler
// Type: PlayUO.ArtifactValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ArtifactValidator : IItemValidator
  {
    public static readonly ArtifactValidator Default = new ArtifactValidator();
    private IItemValidator m_Parent;

    public ArtifactValidator()
      : this((IItemValidator) null)
    {
    }

    public ArtifactValidator(IItemValidator parent)
    {
      this.m_Parent = parent;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent != null && !this.m_Parent.IsValid(check))
        return false;
      ObjectPropertyList propertyList = check.PropertyList;
      if (propertyList == null)
        return false;
      for (int index = 0; index < propertyList.Properties.Length; ++index)
      {
        if (propertyList.Properties[index].Number == 1061078)
          return true;
      }
      return false;
    }
  }
}

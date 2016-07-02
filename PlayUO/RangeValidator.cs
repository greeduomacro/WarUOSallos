// Decompiled with JetBrains decompiler
// Type: PlayUO.RangeValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class RangeValidator : IItemValidator
  {
    private IPoint2D m_Point;
    private int m_xyRange;
    private IItemValidator m_Parent;

    public RangeValidator(IPoint2D point, int xyRange)
      : this((IItemValidator) null, point, xyRange)
    {
    }

    public RangeValidator(IItemValidator parent, IPoint2D point, int xyRange)
    {
      this.m_Point = point;
      this.m_Parent = parent;
      this.m_xyRange = xyRange;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent == null || this.m_Parent.IsValid(check))
        return check.InRange(this.m_Point, this.m_xyRange);
      return false;
    }
  }
}

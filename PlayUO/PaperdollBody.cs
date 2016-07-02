// Decompiled with JetBrains decompiler
// Type: PlayUO.PaperdollBody
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class PaperdollBody
  {
    public static readonly PaperdollBody[] Configuration = new PaperdollBody[6]{ new PaperdollBody(400, new int?(), PaperdollType.Regular, new PaperdollImage[1]{ new PaperdollImage(12, new int?(), 1f) }), new PaperdollBody(401, new int?(), PaperdollType.Regular, new PaperdollImage[1]{ new PaperdollImage(13, new int?(), 1f) }), new PaperdollBody(402, new int?(), PaperdollType.Ghost, new PaperdollImage[1]{ new PaperdollImage(12, new int?(), 0.25f) }), new PaperdollBody(403, new int?(), PaperdollType.Ghost, new PaperdollImage[1]{ new PaperdollImage(13, new int?(), 0.25f) }), new PaperdollBody(987, new int?(0), PaperdollType.Regular, new PaperdollImage[2]{ new PaperdollImage(12, new int?(33770), 1f), new PaperdollImage(50987, new int?(), 1f) }), new PaperdollBody(987, new int?(1), PaperdollType.Regular, new PaperdollImage[2]{ new PaperdollImage(13, new int?(33770), 1f), new PaperdollImage(60987, new int?(), 1f) }) };
    private int _bodyId;
    private int? _gender;
    private PaperdollType _type;
    private PaperdollImage[] _images;

    public int BodyId
    {
      get
      {
        return this._bodyId;
      }
    }

    public int? Gender
    {
      get
      {
        return this._gender;
      }
    }

    public PaperdollType Type
    {
      get
      {
        return this._type;
      }
    }

    public PaperdollImage[] Images
    {
      get
      {
        return this._images;
      }
    }

    public PaperdollBody(int bodyId, int? gender, PaperdollType type, params PaperdollImage[] images)
    {
      this._bodyId = bodyId;
      this._gender = gender;
      this._type = type;
      this._images = images;
    }

    public static PaperdollBody FromMobile(Mobile mob)
    {
      if (mob == null)
        throw new ArgumentNullException("mob");
      foreach (PaperdollBody paperdollBody in PaperdollBody.Configuration)
      {
        if (paperdollBody.IsMatch(mob))
          return paperdollBody;
      }
      return (PaperdollBody) null;
    }

    public bool IsMatch(Mobile mob)
    {
      return (int) mob.Body == this._bodyId && (!this._gender.HasValue || mob.Gender == this._gender.Value);
    }
  }
}

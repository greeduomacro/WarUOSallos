// Decompiled with JetBrains decompiler
// Type: PlayUO.IMobileStatus
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public interface IMobileStatus
  {
    Gump Gump { get; }

    void OnStrChange(int Str);

    void OnHPCurChange(int HPCur);

    void OnHPMaxChange(int HPMax);

    void OnDexChange(int Dex);

    void OnStamCurChange(int StamCur);

    void OnStamMaxChange(int StamMax);

    void OnIntChange(int Int);

    void OnManaCurChange(int ManaCur);

    void OnManaMaxChange(int ManaMax);

    void OnFollCurChange(int current);

    void OnFollMaxChange(int maximum);

    void OnNameChange(string Name);

    void OnArmorChange(int Armor);

    void OnGoldChange(int Gold);

    void OnWeightChange(int Weight);

    void OnFlagsChange(MobileFlags Flags);

    void OnGenderChange(int Gender);

    void OnNotorietyChange(Notoriety n);

    void OnLuckChange();

    void OnDamageChange();

    void OnFireChange();

    void OnColdChange();

    void OnEnergyChange();

    void OnPoisonChange();

    void OnStatCapChange(int statCap);

    void OnRefresh();

    void Close();
  }
}

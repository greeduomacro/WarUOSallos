// Decompiled with JetBrains decompiler
// Type: PlayUO.BaseCrypto
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public abstract class BaseCrypto
  {
    private uint m_Seed;

    public uint Seed
    {
      get
      {
        return this.m_Seed;
      }
    }

    public BaseCrypto(uint seed)
    {
      this.m_Seed = seed;
      this.InitKeys(seed);
    }

    protected abstract void InitKeys(uint seed);

    public abstract int Decrypt(byte[] input, int inputStart, int count, byte[] output, int outputStart);

    public abstract void Encrypt(byte[] buffer, int start, int count);

    public abstract void Decrypt(byte[] buffer, int offset, int length, IConsolidator output);

    public abstract void Encrypt(byte[] buffer, int offset, int length, IConsolidator output);
  }
}

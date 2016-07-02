// Decompiled with JetBrains decompiler
// Type: PlayUO.LoginCrypto
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class LoginCrypto : BaseCrypto
  {
    public LoginCrypto(uint seed)
      : base(seed)
    {
    }

    protected override void InitKeys(uint seed)
    {
    }

    public override int Decrypt(byte[] input, int inputStart, int count, byte[] output, int outputStart)
    {
      for (int index = 0; index < count; ++index)
        output[index + outputStart] = input[index + inputStart];
      return count;
    }

    public override void Encrypt(byte[] buffer, int start, int count)
    {
    }

    public override void Decrypt(byte[] buffer, int offset, int length, IConsolidator output)
    {
      output.Enqueue(buffer, offset, length);
    }

    public override void Encrypt(byte[] buffer, int offset, int length, IConsolidator output)
    {
      output.Enqueue(buffer, offset, length);
    }
  }
}

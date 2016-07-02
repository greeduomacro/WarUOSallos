// Decompiled with JetBrains decompiler
// Type: PlayUO.GameCrypto
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.IO;

namespace PlayUO
{
  public sealed class GameCrypto : BaseCrypto
  {
    private UnpackLeaf m_Leaf;
    private byte[] _unpackBuffer;

    public GameCrypto(uint seed)
      : base(seed)
    {
    }

    protected override void InitKeys(uint seed)
    {
      this.m_Leaf = Compression.m_Tree;
    }

    public override unsafe int Decrypt(byte[] input, int inputStart, int count, byte[] output, int outputStart)
    {
      fixed (byte* numPtr1 = output)
      {
        byte* numPtr2 = numPtr1 + outputStart;
        byte* numPtr3 = numPtr1 + output.Length;
        fixed (UnpackCacheEntry* unpackCacheEntryPtr = Compression.m_CacheEntries)
          fixed (byte* numPtr4 = Compression.m_OutputBuffer)
            fixed (byte* numPtr5 = input)
            {
              UnpackLeaf unpackLeaf = this.m_Leaf;
              UnpackLeaf[] unpackLeafArray = Compression.m_Leaves;
              byte* numPtr6 = numPtr5;
              byte* numPtr7 = numPtr6 + count;
              while (numPtr6 < numPtr7)
              {
                UnpackCacheEntry unpackCacheEntry = unpackCacheEntryPtr[unpackLeaf.m_Cache[(int) *numPtr6++]];
                byte* numPtr8 = numPtr4 + unpackCacheEntry.m_ByteIndex;
                byte* numPtr9 = numPtr2 + unpackCacheEntry.m_ByteCount;
                if (numPtr9 > numPtr3)
                  throw new InternalBufferOverflowException("Network::Decompress(): Buffer overflow.");
                while (numPtr2 < numPtr9)
                  *numPtr2++ = *numPtr8++;
                unpackLeaf = unpackLeafArray[unpackCacheEntry.m_NextIndex];
              }
              this.m_Leaf = unpackLeaf;
            }
        return (int) (numPtr2 - outputStart - numPtr1);
      }
    }

    public override void Encrypt(byte[] buffer, int start, int count)
    {
    }

    public override void Decrypt(byte[] buffer, int offset, int length, IConsolidator output)
    {
      if (this._unpackBuffer == null)
        this._unpackBuffer = new byte[65536];
      int length1 = this.Decrypt(buffer, offset, length, this._unpackBuffer, 0);
      output.Enqueue(this._unpackBuffer, 0, length1);
    }

    public override void Encrypt(byte[] buffer, int offset, int length, IConsolidator output)
    {
      output.Enqueue(buffer, offset, length);
    }
  }
}

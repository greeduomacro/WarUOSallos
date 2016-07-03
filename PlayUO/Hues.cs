// Decompiled with JetBrains decompiler
// Type: PlayUO.Hues
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Assets;
using PlayUO.Profiles;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.IO;
using Microsoft.DirectX;
using SharpDX.Mathematics.Interop;

namespace PlayUO
{
  public class Hues
  {
    private static IHue[,] m_NotorietyHues = new IHue[7, 2];
    private const string RelativeApplicationDataPath = "Sallos/Ultima Online/Cache/Hues";
    private const string RelativeLegacyPath = "data/ultima/cache/hues.uoi";
    private static HueData[] m_HueData;
    private static Hues.DefaultHue _default;
    private static Hues.EtherealHue _ethereal;
    private static Hues.GrayscaleHue m_Grayscale;
    private static Hues.PartialHue[] m_Partial;
    private static Hues.RegularHue[] m_Regular;
    private static Hues.ShadowHue _shadow;
    private static Texture _hueTexture;

    public static IHue Grayscale
    {
      get
      {
        if (Hues.m_Grayscale == null)
          Hues.m_Grayscale = new Hues.GrayscaleHue();
        return (IHue) Hues.m_Grayscale;
      }
    }

    public static Hues.ShadowHue Shadow
    {
      get
      {
        if (Hues._shadow == null)
          Hues._shadow = new Hues.ShadowHue();
        return Hues._shadow;
      }
    }

    public static Hues.EtherealHue Ethereal
    {
      get
      {
        if (Hues._ethereal == null)
          Hues._ethereal = new Hues.EtherealHue();
        return Hues._ethereal;
      }
    }

    public static IHue Bright
    {
      get
      {
        return Hues.Default;
      }
    }

    public static IHue Default
    {
      get
      {
        return (IHue) Hues._default;
      }
    }

    static unsafe Hues()
    {
      Debug.TimeBlock("Initializing Hues");
      Hues._default = new Hues.DefaultHue();
      Hues.m_HueData = new HueData[3000];
      Hues.m_Partial = new Hues.PartialHue[3000];
      Hues.m_Regular = new Hues.RegularHue[3000];
      string cachePath = Hues.GetCachePath();
      if (!File.Exists(cachePath))
      {
        string str = Engine.FileManager.BasePath("data/ultima/cache/hues.uoi");
        if (File.Exists(str))
        {
          try
          {
            File.Move(str, cachePath);
          }
          catch
          {
            File.Copy(str, cachePath, false);
          }
        }
      }
      FileInfo fileInfo1 = new FileInfo(Engine.FileManager.ResolveMUL(Files.Hues));
      FileInfo fileInfo2 = new FileInfo(Engine.FileManager.ResolveMUL(Files.Verdata));
      if (File.Exists(cachePath))
      {
        using (FileStream fileStream = new FileStream(cachePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
          {
            DateTime dateTime1 = DateTime.FromFileTime(binaryReader.ReadInt64());
            DateTime dateTime2 = DateTime.FromFileTime(binaryReader.ReadInt64());
            if (fileInfo1.LastWriteTime == dateTime1)
            {
              if (fileInfo2.LastWriteTime == dateTime2)
              {
                int num1 = 3000;
                int num2 = 0;
                byte[] numArray = binaryReader.ReadBytes(num1 * 68);
                int srcOffset = 0;
                HueData hueData;
                for (; num2 < num1; Hues.m_HueData[num2++] = hueData)
                {
                  hueData = new HueData();
                  hueData.colors = new ushort[64];
                  Buffer.BlockCopy((Array) numArray, srcOffset, (Array) hueData.colors, 64, 64);
                  srcOffset += 68;
                }
                Hues.Patch();
                Debug.EndBlock();
                return;
              }
            }
          }
        }
      }
      int count = 265500;
      byte[] buffer1 = new byte[count];
      Stream stream1 = Engine.FileManager.OpenMUL(Files.Hues);
      stream1.Read(buffer1, 0, count);
      stream1.Close();
      fixed (byte* numPtr1 = buffer1)
      {
        int num1 = 0;
        int num2 = 0;
        short* numPtr2 = (short*) numPtr1;
        do
        {
          numPtr2 += 2;
          int num3 = 0;
          do
          {
            HueData hueData = new HueData();
            hueData.colors = new ushort[64];
            for (int index = 0; index < 32; ++index)
              hueData.colors[32 + index] = (ushort) *numPtr2++;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            HueData local1 = hueData;
            short* numPtr3 = numPtr2;
            short num4 = 2;
            short* numPtr4 = num4 + numPtr3;
            int num5 = (int) *numPtr3;
            // ISSUE: explicit reference operation
            local1.tableStart = (short) num5;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            HueData local2 = hueData;
            short* numPtr5 = numPtr4;
            short num6 =  2;
            short* numPtr6 = numPtr5 + num6;
            int num7 = (int) *numPtr5;
            // ISSUE: explicit reference operation
            local2.tableEnd = (short) num7;
            Hues.m_HueData[num1++] = hueData;
            numPtr2 = numPtr6 + 10;
          }
          while (++num3 < 8);
        }
        while (++num2 < 375);
      }
      Stream stream2 = Engine.FileManager.OpenMUL(Files.Verdata);
      byte[] buffer2 = new byte[stream2.Length];
      stream2.Read(buffer2, 0, buffer2.Length);
      stream2.Close();
      fixed (byte* numPtr1 = buffer2)
      {
        IntPtr num1 = new IntPtr(4);
        int* numPtr2 = (int*) ( numPtr1 + *(int*)num1);
        int num2 = *(int*) numPtr1;
        int num3 = 0;
        while (num3++ < num2)
        {
          int* numPtr3 = numPtr2;
          IntPtr num4 = new IntPtr(4);
          int* numPtr4 = (int*) ((IntPtr) numPtr3 + *(int*)num4);
          if (*numPtr3 == 32)
          {
            int* numPtr5 = numPtr4;
            IntPtr num5 = new IntPtr(4);
            int* numPtr6 = (int*) ((IntPtr) numPtr5 + *(int*)num5);
            int num6 = *numPtr5;
            int* numPtr7 = numPtr6;
            IntPtr num7 = new IntPtr(4);
            int* numPtr8 = (int*) ((IntPtr) numPtr7 + *(int*)num7);
            int num8 = *numPtr7;
            int* numPtr9 = numPtr8;
            IntPtr num9 = new IntPtr(4);
            int* numPtr10 = (int*) ((IntPtr) numPtr9 + *(int*)num9);
            int num10 = *numPtr9;
            int* numPtr11 = numPtr10;
            IntPtr num11 = new IntPtr(4);
            numPtr2 = (int*) ((IntPtr) numPtr11 + *(int*)num11);
            int num12 = *numPtr11;
            short* numPtr12 = (short*) (numPtr1 + num8 + 4);
            for (int index1 = 0; index1 < 8; ++index1)
            {
              HueData hueData = new HueData();
              hueData.colors = new ushort[64];
              for (int index2 = 0; index2 < 32; ++index2)
                hueData.colors[32 + index2] = (ushort) *numPtr12++;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              HueData local1 = hueData;
              short* numPtr13 = numPtr12;
              short num13 = 2;
              short* numPtr14 = numPtr13 + num13;
              int num14 = (int) *numPtr13;
              // ISSUE: explicit reference operation
              local1.tableStart = (short) num14;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              HueData local2 = hueData;
              short* numPtr15 = numPtr14;
              short num15 = 2;
              short* numPtr16 = numPtr15 + num15;
              int num16 = *numPtr15;
              // ISSUE: explicit reference operation
              local2.tableEnd = (short) num16;
              Hues.m_HueData[(num6 << 3) + index1] = hueData;
              numPtr12 = numPtr16 + 10;
            }
          }
          else
            numPtr2 = numPtr4 + 4;
        }
      }
      using (FileStream fileStream = new FileStream(cachePath, FileMode.Create, FileAccess.Write, FileShare.None))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
        {
          binaryWriter.Write(fileInfo1.LastWriteTime.ToFileTime());
          binaryWriter.Write(fileInfo2.LastWriteTime.ToFileTime());
          int num = 3000;
          for (int index1 = 0; index1 < num; ++index1)
          {
            HueData hueData = Hues.m_HueData[index1];
            for (int index2 = 0; index2 < 32; ++index2)
            {
              hueData.colors[32 + index2] |= (ushort) 32768;
              binaryWriter.Write(hueData.colors[32 + index2]);
            }
            binaryWriter.Write(hueData.tableStart);
            binaryWriter.Write(hueData.tableEnd);
          }
        }
      }
      Hues.Patch();
      Debug.EndBlock();
    }

    public static void ClearNotos()
    {
      for (int index1 = 0; index1 < 7; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
          Hues.m_NotorietyHues[index1, index2] = (IHue) null;
      }
    }

    public static IHue GetNotoriety(Notoriety n)
    {
      return Hues.GetNotoriety(n, true);
    }

    public static IHue GetNotoriety(Notoriety n, bool full)
    {
      if (n < Notoriety.Innocent || n > Notoriety.Vendor)
        return Hues.Default;
      int index1 = (int) (n - (byte) 1);
      int index2 = full ? 1 : 0;
      return Hues.m_NotorietyHues[index1, index2] ?? (Hues.m_NotorietyHues[index1, index2] = Hues.Load(Preferences.Current.NotorietyHues[n] | index2 << 15));
    }

    public static HueData GetNotorietyData(Notoriety n)
    {
      if (n >= Notoriety.Innocent && n <= Notoriety.Vendor)
        return Hues.m_HueData[Preferences.Current.NotorietyHues[n]];
      return new HueData();
    }

    public static unsafe Texture GetHueTexture()
    {
      if (Hues._hueTexture == null)
      {
        Hues._hueTexture = new Texture(512, 256, (Format) 21, TextureTransparency.None);
        LockData lockData = Hues._hueTexture.Lock(LockFlags.WriteOnly);
        for (int index1 = 0; index1 < Hues.m_HueData.Length; ++index1)
        {
          HueData hueData = Hues.m_HueData[index1];
          fixed (ushort* numPtr1 = hueData.colors)
          {
            ushort* numPtr2 = numPtr1 + 32;
            int a32 = Engine.C16232((int) *numPtr2);
            int b32 = Engine.C16232((int) numPtr2[31]);
            Engine.C16232((int) numPtr2[16]);
            int* numPtr3 = (int*) lockData.pvSrc;
            int num1 = (index1 & 15) * 32;
            int num2 = index1 >> 4;
            int* numPtr4 = numPtr3 + num2 * (lockData.Pitch >> 2) + num1;
            for (int index2 = 0; index2 < 32; ++index2)
            {
              Engine.Blend32(a32, b32, (index2 * (int) byte.MaxValue + 15) / 31);
              int num3 = Engine.C16232((int) numPtr2[index2]);
              numPtr4[index2] = num3 | -16777216;
            }
          }
        }
        Hues._hueTexture.Unlock();
      }
      return Hues._hueTexture;
    }

    public static HueData GetData(int HueIndex)
    {
      return Hues.m_HueData[HueIndex];
    }

    public static IHue LoadByRgb(int rgbColor)
    {
      int num1 = rgbColor >> 16 & (int) byte.MaxValue;
      int num2 = rgbColor >> 8 & (int) byte.MaxValue;
      int num3 = rgbColor & (int) byte.MaxValue;
      int num4 = num1 >> 3;
      int num5 = num2 >> 3;
      int num6 = num3 >> 3;
      int num7 = 1000;
      int hueId = 0;
      for (int index = 0; index < 3000; ++index)
      {
        int num8 = (int) Hues.m_HueData[index].colors[56];
        int num9 = num8 >> 10 & 31;
        int num10 = num8 >> 5 & 31;
        int num11 = num8 & 31;
        int num12 = Math.Abs(num9 - num4) + Math.Abs(num10 - num5) + Math.Abs(num11 - num6);
        if (num12 < num7)
        {
          num7 = num12;
          hueId = index + 1;
        }
      }
      return Hues.Load(hueId);
    }

    private static string GetCachePath()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Sallos/Ultima Online/Cache/Hues");
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      return path;
    }

    public static void Patch()
    {
      HueData hueData1 = Hues.m_HueData[2999];
      for (int index = 0; index < 32; ++index)
      {
        int num = 8 + index * 7 / 8;
        if (num > 31)
          num = 31;
        hueData1.colors[32 + index] = (ushort) (32768 | num << 10 | num << 5 | num);
      }
      HueData hueData2 = Hues.m_HueData[2801];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = 0;
        int num2 = index < 16 ? 0 : (index < 20 ? 1 + (index - 16) : 6 + (index - 20) * 2);
        int num3 = 0;
        hueData2.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData3 = Hues.m_HueData[2802];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = (index + 1) / 2;
        int num2 = (index + 1) / 2;
        int num3 = index;
        hueData3.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData4 = Hues.m_HueData[2803];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 16 ? 0 : (index < 20 ? 1 + (index - 16) : 6 + (index - 20) * 2);
        int num2 = num1 / 4;
        int num3 = num1 / 8;
        hueData4.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData5 = Hues.m_HueData[2806];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 24 ? 0 : 1 + (index - 24);
        int num2 = 0;
        int num3 = index < 16 ? 0 : (index < 20 ? 1 + (index - 16) : 6 + (index - 20) * 2);
        hueData5.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData6 = Hues.m_HueData[2830];
      int[] numArray = new int[32]{ 0, 1, 2, 3, 4, 6, 8, 11, 14, 17, 20, 23, 26, 29, 30, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31 };
      for (int index = 0; index < 32; ++index)
      {
        int num1 = numArray[index];
        int num2 = (num1 + 1) / 2;
        int num3 = 0;
        hueData6.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData7 = Hues.m_HueData[2831];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 16 ? 0 : (index < 20 ? 1 + (index - 16) : 6 + (index - 20) * 2);
        int num2 = (num1 + 1) / 2;
        int num3 = 0;
        hueData7.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      HueData hueData8 = Hues.m_HueData[2840];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index;
        int num2 = 0;
        int num3 = 0;
        hueData8.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      hueData8 = Hues.m_HueData[2860];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 16 ? 0 : (index < 20 ? 1 + (index - 16) : 6 + (index - 20) * 2);
        int num2 = num1;
        int num3 = 0;
        hueData8.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      hueData8 = Hues.m_HueData[2861];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 8 ? 0 : (index < 16 ? 1 + (index - 8) / 2 : (index < 25 ? 5 + (index - 16) : 15 + (index - 25) * 2));
        int num2 = num1;
        int num3 = 0;
        hueData8.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
      hueData8 = Hues.m_HueData[2862];
      for (int index = 0; index < 32; ++index)
      {
        int num1 = index < 8 ? 0 : (index < 16 ? 1 + (index - 8) / 2 : (index < 25 ? 5 + (index - 16) : 15 + (index - 25) * 2));
        int num2 = num1;
        int num3 = num1;
        hueData8.colors[32 + index] = (ushort) (32768 | num1 << 10 | num2 << 5 | num3);
      }
    }

    public static void Dispose()
    {
      if (Hues._default != null)
      {
        Hues._default.Dispose();
        Hues._default = (Hues.DefaultHue) null;
      }
      if (Hues.m_Grayscale != null)
      {
        Hues.m_Grayscale.Dispose();
        Hues.m_Grayscale = (Hues.GrayscaleHue) null;
      }
      if (Hues._shadow != null)
      {
        Hues._shadow.Dispose();
        Hues._shadow = (Hues.ShadowHue) null;
      }
      if (Hues._ethereal != null)
      {
        Hues._ethereal.Dispose();
        Hues._ethereal = (Hues.EtherealHue) null;
      }
      for (int index = 0; index < 3000; ++index)
      {
        if (Hues.m_Partial[index] != null)
        {
          Hues.m_Partial[index].Dispose();
          Hues.m_Partial[index] = (Hues.PartialHue) null;
        }
        if (Hues.m_Regular[index] != null)
        {
          Hues.m_Regular[index].Dispose();
          Hues.m_Regular[index] = (Hues.RegularHue) null;
        }
      }
      if (Hues._hueTexture != null)
      {
        Hues._hueTexture.Dispose();
        Hues._hueTexture = (Texture) null;
      }
      Hues.m_Partial = (Hues.PartialHue[]) null;
      Hues.m_Regular = (Hues.RegularHue[]) null;
      Hues.m_HueData = (HueData[]) null;
    }

    public static IHue GetItemHue(int itemID, int hue)
    {
      hue ^= (int) ((long) (Map.m_ItemFlags[itemID & 16383].Value64 >> 3) & 32768L ^ 32768L);
      return Hues.Load(hue);
    }

    public static IHue GetMobileHue(int hue)
    {
      return Hues.Load(hue ^ 32768);
    }

    public static IHue Load(int hueId)
    {
      IHue hue;
      if ((hueId & 16384) == 0)
      {
        int hueIndex = (hueId & 16383) - 1;
        hue = hueIndex < 0 || hueIndex >= 3000 ? Hues.Default : ((hueId & 32768) != 0 ? (IHue) Hues.m_Regular[hueIndex] ?? (IHue) (Hues.m_Regular[hueIndex] = new Hues.RegularHue(Hues.m_HueData[hueIndex], hueId, hueIndex)) : (IHue) Hues.m_Partial[hueIndex] ?? (IHue) (Hues.m_Partial[hueIndex] = new Hues.PartialHue(Hues.m_HueData[hueIndex], hueId, hueIndex)));
      }
      else
        hue = (IHue) Hues.Ethereal;
      return hue;
    }

    public class ShadowHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _shaderData = new ShaderData("ShadowShader.cso", (Texture) null, TextureTransparency.Complex);
      private IGraphicProvider _provider;

      public ShaderData ShaderData
      {
        get
        {
          return Hues.ShadowHue._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public ShadowHue()
      {
        this._provider = (IGraphicProvider) new DynamicCacheGraphicProvider((IGraphicProvider) new PhysicalGraphicProvider((IHue) this));
      }

      public override string ToString()
      {
        return "{ shadow }";
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int count)
      {
        throw new InvalidOperationException();
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        throw new InvalidOperationException();
      }

      public unsafe void FillLine(void* pSrc, void* pDest, int Count)
      {
        throw new InvalidOperationException();
      }

      public ushort Pixel(ushort input)
      {
        return input;
      }

      public int Pixel32(int input)
      {
        return input;
      }

      public int HueID()
      {
        return (int) ushort.MaxValue;
      }
    }

    public class ColorFillHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _baseShaderData = new ShaderData("ColorFillShader.cso", (Texture) null, TextureTransparency.NotSpecified);
      private IGraphicProvider _provider;
      private ShaderData _shaderData;
      private Vector4 _colorData;
      private int _color;

      public ShaderData ShaderData
      {
        get
        {
          return this._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public ColorFillHue(int rgb32)
      {
        this._color = rgb32;
        this._colorData = new Vector4((float) (rgb32 >> 16 & (int) byte.MaxValue) / (float) byte.MaxValue, (float) (rgb32 >> 8 & (int) byte.MaxValue) / (float) byte.MaxValue, (float) (rgb32 & (int) byte.MaxValue) / (float) byte.MaxValue, 1f);
        this._shaderData = new ShaderData(Hues.ColorFillHue._baseShaderData.PixelShader, (Texture) null, TextureTransparency.NotSpecified);
        this._shaderData.RenderCallback = new Callback(this.RenderCallback);
        this._provider = (IGraphicProvider) new DynamicCacheGraphicProvider((IGraphicProvider) new ShadedGraphicProvider(this._shaderData, (IGraphicProvider) Hues.Default));
      }

      private void RenderCallback()
      {
        // ISSUE: explicit reference operation
          Engine.m_Device.SetPixelShaderConstant(0, new[] { _colorData.W, _colorData.X, _colorData.Y, _colorData.Z });
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int count)
      {
        throw new InvalidOperationException();
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        throw new InvalidOperationException();
      }

      public unsafe void FillLine(void* pSrc, void* pDest, int Count)
      {
        throw new InvalidOperationException();
      }

      public ushort Pixel(ushort input)
      {
        throw new InvalidOperationException();
      }

      public int Pixel32(int input)
      {
        return this._color;
      }

      public int HueID()
      {
        return (int) ushort.MaxValue;
      }
    }

    public class DefaultHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _shaderData = new ShaderData("DefaultShader.cso", (Texture) null, TextureTransparency.NotSpecified);
      private const int DoubleOpaque = -2147450880;
      private IGraphicProvider _provider;

      public ShaderData ShaderData
      {
        get
        {
          return Hues.DefaultHue._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public DefaultHue()
      {
        this._provider = (IGraphicProvider) new StaticCacheGraphicProvider((IGraphicProvider) new PhysicalGraphicProvider((IHue) this));
      }

      public bool HintItem(int itemId)
      {
        return false;
      }

      public bool HintLand(int landId)
      {
        return false;
      }

      public override string ToString()
      {
        return "{ default }";
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        ushort* numPtr = pDest;
        while (pDest < pEnd && pSrc + 1 < pSrcEnd)
        {
          ushort num1 = *pSrc;
          ushort num2 = pSrc[1];
          pSrc += 2;
          numPtr += num2;
          if ((int) num1 != 0)
          {
            ushort num3 = (ushort) ((uint) num1 | 32768U);
            while (pDest < numPtr)
              *pDest++ = num3;
          }
          else
            pDest += num2;
        }
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels)
      {
        int* numPtr1 = (int*) pvSrc;
        int* numPtr2 = (int*) pvDest;
        int* numPtr3 = numPtr1 + (Pixels >> 1 & -4);
        while (numPtr1 < numPtr3)
        {
          *numPtr2 = *numPtr1 | -2147450880;
          numPtr2[1] = numPtr1[1] | -2147450880;
          numPtr2[2] = numPtr1[2] | -2147450880;
          numPtr2[3] = numPtr1[3] | -2147450880;
          numPtr2 += 4;
          numPtr1 += 4;
        }
        int num = Pixels >> 1 & 3;
        switch (num)
        {
          case 1:
            *numPtr2 = *numPtr1 | -2147450880;
            break;
          case 2:
            numPtr2[1] = numPtr1[1] | -2147450880;
            goto case 1;
          case 3:
            numPtr2[2] = numPtr1[2] | -2147450880;
            goto case 2;
        }
        int* numPtr4 = numPtr2 + num;
        int* numPtr5 = numPtr1 + num;
        if ((Pixels & 1) == 0)
          return;
        *(short*) numPtr4 = (short) (32768 | (int) *(ushort*) numPtr5);
      }

      public ushort Pixel(ushort input)
      {
        return input;
      }

      public int Pixel32(int input)
      {
        return input;
      }

      public int HueID()
      {
        return 0;
      }
    }

    private class GrayscaleHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _shaderData = new ShaderData("GrayscaleShader.cso", (Texture) null, TextureTransparency.NotSpecified);
      private IGraphicProvider _provider;

      public ShaderData ShaderData
      {
        get
        {
          return Hues.GrayscaleHue._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public GrayscaleHue()
      {
        this._provider = (IGraphicProvider) new StaticCacheGraphicProvider((IGraphicProvider) new ShadedGraphicProvider(Hues.GrayscaleHue._shaderData, (IGraphicProvider) Hues.Default));
      }

      public override string ToString()
      {
        return "{ grayscale }";
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels)
      {
        throw new NotImplementedException();
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        throw new NotImplementedException();
      }

      public ushort Pixel(ushort input)
      {
        return (ushort) (1057 * (Engine.GrayScale((int) Engine.C32216((int) input)) * (int) byte.MaxValue / 31));
      }

      public int Pixel32(int input)
      {
        return 65793 * (Engine.GrayScale((int) Engine.C32216(input)) * (int) byte.MaxValue / 31);
      }

      public int HueID()
      {
        return (int) ushort.MaxValue;
      }
    }

    private class RegularHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _baseShaderData = new ShaderData("HueShader.cso", (Texture) null, TextureTransparency.NotSpecified);
      private HueData m_Data;
      private int hue;
      private IGraphicProvider _provider;
      private ShaderData _shaderData;
      private Vector4 _offset;

      public ShaderData ShaderData
      {
        get
        {
          return this._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public RegularHue(HueData hd, int hueId, int hueIndex)
      {
        this.hue = hueId;
        this.m_Data = hd;
        this._shaderData = new ShaderData(Hues.RegularHue._baseShaderData.PixelShader, Hues.GetHueTexture(), TextureTransparency.NotSpecified);
        this._shaderData.RenderCallback = new Callback(this.RenderCallback);
        this._offset = new Vector4((float) (1 + (hueIndex & 15) * 32 * 2) / 1024f, (float) (1 + (hueIndex >> 4) * 2) / 512f, 0.0f, 0.0f);
        this._provider = (IGraphicProvider) new DynamicCacheGraphicProvider((IGraphicProvider) new ShadedGraphicProvider(this._shaderData, (IGraphicProvider) Hues.Default));
      }

      public override string ToString()
      {
        return string.Format("{{ regular 0x{0:X4} }}", (object) this.hue);
      }

      public ushort Pixel(ushort c)
      {
        return this.m_Data.colors[(int) c >> 10];
      }

      public int Pixel32(int input)
      {
        return (int) this.Pixel(Engine.C32216(input));
      }

      public int HueID()
      {
        return this.hue;
      }

      private void RenderCallback()
      {
        // ISSUE: explicit reference operation
          Engine.m_Device.SetPixelShaderConstant(1, new float[] { _offset.W, _offset.X, _offset.Y, _offset.Z });
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels)
      {
        fixed (ushort* numPtr1 = this.m_Data.colors)
        {
          ushort* numPtr2 = (ushort*) pvSrc;
          ushort* numPtr3 = (ushort*) pvDest;
          ushort* numPtr4 = numPtr2 + (Pixels & -4);
          while (numPtr2 < numPtr4)
          {
            *numPtr3 = numPtr1[(int) *numPtr2 >> 10 | 32];
            numPtr3[1] = numPtr1[(int) numPtr2[1] >> 10 | 32];
            numPtr3[2] = numPtr1[(int) numPtr2[2] >> 10 | 32];
            numPtr3[3] = numPtr1[(int) numPtr2[3] >> 10 | 32];
            numPtr3 += 4;
            numPtr2 += 4;
          }
          switch (Pixels & 3)
          {
            case 1:
              *numPtr3 = numPtr1[(int) *numPtr2 >> 10 | 32];
              break;
            case 2:
              numPtr3[1] = numPtr1[(int) numPtr2[1] >> 10 | 32];
              goto case 1;
            case 3:
              numPtr3[2] = numPtr1[(int) numPtr2[2] >> 10 | 32];
              goto case 2;
          }
        }
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        ushort* numPtr = pDest;
        while (pDest < pEnd)
        {
          ushort num1 = *pSrc;
          ushort num2 = pSrc[1];
          pSrc += 2;
          numPtr += num2;
          if ((int) num1 != 0)
          {
            ushort num3 = this.m_Data.colors[(int) num1 >> 10 | 32];
            while (pDest < numPtr)
              *pDest++ = num3;
          }
          else
            pDest += num2;
        }
      }
    }

    public class EtherealHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _shaderData = new ShaderData("EtherealShader.cso", (Texture) null, TextureTransparency.Complex);
      private IGraphicProvider _provider;

      public ShaderData ShaderData
      {
        get
        {
          return Hues.EtherealHue._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public EtherealHue()
      {
        this._provider = (IGraphicProvider) new DynamicCacheGraphicProvider((IGraphicProvider) new ShadedGraphicProvider(Hues.EtherealHue._shaderData, (IGraphicProvider) Hues.Default));
      }

      public override string ToString()
      {
        return "{ ethereal }";
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int count)
      {
        throw new InvalidOperationException();
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        throw new InvalidOperationException();
      }

      public unsafe void FillLine(void* pSrc, void* pDest, int Count)
      {
        throw new InvalidOperationException();
      }

      public ushort Pixel(ushort input)
      {
        return input;
      }

      public int Pixel32(int input)
      {
        return input;
      }

      public int HueID()
      {
        return 16385;
      }
    }

    private class PartialHue : IHue, IGraphicProvider, IDisposable
    {
      private static ShaderData _baseShaderData = new ShaderData("PartialHueShader.cso", (Texture) null, TextureTransparency.NotSpecified);
      private HueData m_Data;
      private int hue;
      private IGraphicProvider _provider;
      private ShaderData _shaderData;
      private Vector4 _offset;

      public ShaderData ShaderData
      {
        get
        {
          return this._shaderData;
        }
      }

      public Palette Palette
      {
        get
        {
          return (Palette) null;
        }
      }

      public PartialHue(HueData hd, int hueId, int hueIndex)
      {
        this.hue = hueId;
        this.m_Data = hd;
        this._shaderData = new ShaderData(Hues.PartialHue._baseShaderData.PixelShader, Hues.GetHueTexture(), TextureTransparency.NotSpecified);
        this._shaderData.RenderCallback = new Callback(this.RenderCallback);
        this._offset = new Vector4((float) (1 + (hueIndex & 15) * 32 * 2) / 1024f, (float) (1 + (hueIndex >> 4) * 2) / 512f, 0.0f, 0.0f);
        this._provider = (IGraphicProvider) new DynamicCacheGraphicProvider((IGraphicProvider) new ShadedGraphicProvider(this._shaderData, (IGraphicProvider) Hues.Default));
      }

      private void RenderCallback()
      {
        // ISSUE: explicit reference operation
          Engine.m_Device.SetPixelShaderConstant(1, new float[]{ _offset.W, _offset.X, _offset.Y, _offset.Z});
      }

      public override string ToString()
      {
        return string.Format("{{ partial 0x{0:X4} }}", (object) this.hue);
      }

      public int HueID()
      {
        return this.hue;
      }

      public ushort Pixel(ushort c)
      {
        if (((int) c & 31) == ((int) c >> 10 & 31) && ((int) c & 31) == ((int) c >> 5 & 31))
          return this.m_Data.colors[(int) c >> 10];
        return c;
      }

      public int Pixel32(int input)
      {
        input = (int) Engine.C32216(input);
        return (int) this.m_Data.colors[32 + (input >> 10 & 31)];
      }

      public Frames GetAnimation(int RealID)
      {
        return this._provider.GetAnimation(RealID);
      }

      public Texture GetTerrainTexture(int TextureID)
      {
        return this._provider.GetTerrainTexture(TextureID);
      }

      public Texture GetTerrainIsometric(int LandID)
      {
        return this._provider.GetTerrainIsometric(LandID);
      }

      public Texture GetGump(int GumpID)
      {
        return this._provider.GetGump(GumpID);
      }

      public Texture GetItem(int ItemID)
      {
        return this._provider.GetItem(ItemID);
      }

      public Texture GetLight(int lightId)
      {
        return this._provider.GetLight(lightId);
      }

      public void Dispose()
      {
        if (this._provider == null)
          return;
        this._provider.Dispose();
        this._provider = (IGraphicProvider) null;
      }

      public unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels)
      {
        fixed (ushort* numPtr1 = this.m_Data.colors)
        {
          ushort* numPtr2 = (ushort*) pvSrc;
          ushort* numPtr3 = (ushort*) pvDest;
          ushort* numPtr4 = numPtr2 + Pixels;
          while (numPtr2 < numPtr4)
          {
            int num = (int) *numPtr2++;
            *numPtr3++ = (num & 31) != (num >> 5 & 31) || (num & 31) != (num >> 10 & 31) ? (ushort) (num | 32768) : numPtr1[num >> 10 | 32];
          }
        }
      }

      public unsafe void CopyEncodedLine(ushort* pSrc, ushort* pSrcEnd, ushort* pDest, ushort* pEnd)
      {
        ushort* numPtr = pDest;
        while (pDest < pEnd)
        {
          ushort num1 = *pSrc;
          ushort num2 = pSrc[1];
          pSrc += 2;
          numPtr += num2;
          if ((int) num1 != 0)
          {
            ushort num3 = ((int) num1 & 31) != ((int) num1 >> 5 & 31) || ((int) num1 & 31) != ((int) num1 >> 10 & 31) ? (ushort) ((uint) num1 | 32768U) : this.m_Data.colors[(int) num1 >> 10 | 32];
            while (pDest < numPtr)
              *pDest++ = num3;
          }
          else
            pDest += num2;
        }
      }
    }
  }
}

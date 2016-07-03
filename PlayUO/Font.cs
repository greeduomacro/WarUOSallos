// Decompiled with JetBrains decompiler
// Type: PlayUO.Font
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class Font : IFont, IFontFactory
  {
    private Hashtable m_WrapCache = new Hashtable();
    private const string RelativeApplicationDataPath = "Sallos/Ultima Online/Cache/Fonts";
    private const string RelativeLegacyPath = "data/ultima/cache/fonts.uoi";
    private int m_FontID;
    private FontCache m_Cache;
    private short[] m_Palette;
    public FontImage[] m_Images;
    private static byte[] m_Buffer;

    public Hashtable WrapCache
    {
      get
      {
        return this.m_WrapCache;
      }
    }

    public string Name
    {
      get
      {
        return string.Format("Font[{0}]", (object) this.m_FontID);
      }
    }

    public unsafe Font(int fid)
    {
      this.m_FontID = fid;
      this.m_Cache = new FontCache((IFontFactory) this);
      this.m_Images = new FontImage[224];
      string cachePath = Font.GetCachePath();
      if (!File.Exists(cachePath))
      {
        string str = Engine.FileManager.BasePath("data/ultima/cache/fonts.uoi");
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
        else
          Font.Reformat();
      }
      FileStream file = new FileStream(cachePath, FileMode.Open, FileAccess.Read, FileShare.Read);
      BinaryReader binaryReader = new BinaryReader((Stream) file);
      if (DateTime.FromFileTime(binaryReader.ReadInt64()) != new FileInfo(Engine.FileManager.ResolveMUL(Files.Fonts)).LastWriteTime)
      {
        binaryReader.Close();
        Font.Reformat();
        file = new FileStream(cachePath, FileMode.Open, FileAccess.Read, FileShare.None);
        binaryReader = new BinaryReader((Stream) file);
      }
      file.Seek((long) (12 + fid * 8), SeekOrigin.Begin);
      int num1 = binaryReader.ReadInt32();
      int size = binaryReader.ReadInt32();
      file.Seek((long) num1, SeekOrigin.Begin);
      if (Font.m_Buffer == null || size > Font.m_Buffer.Length)
        Font.m_Buffer = new byte[size];
      fixed (byte* numPtr1 = Font.m_Buffer)
      {
        UnsafeMethods.ReadFile(file, (void*) numPtr1, size);
        byte* numPtr2 = numPtr1;
        for (int index = 0; index < 224; ++index)
        {
          int xWidth = (int) *numPtr2;
          int yHeight = (int) numPtr2[1];
          numPtr2 += 3;
          FontImage fontImage = new FontImage(xWidth, yHeight);
          int num2 = fontImage.xDelta;
          fixed (byte* numPtr3 = fontImage.xyPixels)
          {
              IntPtr localPtr = (IntPtr)numPtr3;
            int num3 = 0;
            while (num3 < yHeight)
            {
              int num4 = 0;
              byte* numPtr4 = (byte*)localPtr;
              for (; num4 < xWidth; ++num4)
                *numPtr4++ = *numPtr2++;
              ++num3;
              localPtr += num2;
            }
          }
          this.m_Images[index] = fontImage;
        }
        int length = *(int*) numPtr2;
        short* numPtr5 = (short*) (numPtr2 + 4);
        this.m_Palette = new short[length];
        for (int index = 0; index < length; ++index)
          this.m_Palette[index] = *numPtr5++;
      }
      binaryReader.Close();
    }

    public override string ToString()
    {
      return string.Format("<ASCII Font #{0}>", (object) this.m_FontID);
    }

    private static string GetCachePath()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Sallos/Ultima Online/Cache/Fonts");
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      return path;
    }

    public static void Reformat()
    {
      string str = Engine.FileManager.ResolveMUL(Files.Fonts);
      if (!File.Exists(str))
        throw new InvalidOperationException(string.Format("Unable to reformat the font file, it doesn't exist. (inputPath={0})", (object) str));
      string cachePath = Font.GetCachePath();
      using (FileStream fileStream1 = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream1))
        {
          using (FileStream fileStream2 = new FileStream(cachePath, FileMode.Create, FileAccess.Write, FileShare.None))
          {
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream2))
            {
              FileInfo fileInfo = new FileInfo(str);
              binaryWriter.Write(fileInfo.LastWriteTime.ToFileTime());
              binaryWriter.Write(10);
              binaryWriter.Write(new byte[80]);
              for (int index1 = 0; index1 < 10; ++index1)
              {
                binaryWriter.Flush();
                long length1 = fileStream2.Length;
                fileStream2.Seek((long) (12 + index1 * 8), SeekOrigin.Begin);
                binaryWriter.Write((int) length1);
                fileStream2.Seek(length1, SeekOrigin.Begin);
                int num1 = (int) binaryReader.ReadByte();
                int num2 = 0;
                ArrayList arrayList = new ArrayList();
                arrayList.Add((object) (short) 0);
                for (int index2 = 0; index2 < 224; ++index2)
                {
                  int length2 = (int) binaryReader.ReadByte();
                  int length3 = (int) binaryReader.ReadByte();
                  int num3 = (int) binaryReader.ReadByte();
                  byte[,] numArray = new byte[length2, length3];
                  for (int index3 = 0; index3 < length3; ++index3)
                  {
                    for (int index4 = 0; index4 < length2; ++index4)
                    {
                      int num4 = (int) binaryReader.ReadInt16() & (int) short.MaxValue;
                      int num5 = -1;
                      if (num4 != 0)
                        num4 |= 32768;
                      for (int index5 = 0; index5 < arrayList.Count; ++index5)
                      {
                        if ((int) (short) arrayList[index5] == (int) (short) num4)
                        {
                          num5 = index5;
                          break;
                        }
                      }
                      if (num5 == -1)
                      {
                        num5 = arrayList.Count;
                        arrayList.Add((object) (short) num4);
                      }
                      numArray[index4, index3] = (byte) num5;
                    }
                  }
                  binaryWriter.Write((byte) length2);
                  binaryWriter.Write((byte) length3);
                  binaryWriter.Write((byte) num3);
                  int num6 = num2 + 3;
                  for (int index3 = 0; index3 < length3; ++index3)
                  {
                    for (int index4 = 0; index4 < length2; ++index4)
                      binaryWriter.Write(numArray[index4, index3]);
                  }
                  num2 = num6 + length2 * length3;
                }
                binaryWriter.Write(arrayList.Count);
                int num7 = num2 + 4;
                for (int index2 = 0; index2 < arrayList.Count; ++index2)
                  binaryWriter.Write((short) arrayList[index2]);
                int num8 = num7 + arrayList.Count * 2;
                long length4 = fileStream2.Length;
                fileStream2.Seek((long) (12 + index1 * 8 + 4), SeekOrigin.Begin);
                binaryWriter.Write(num8);
                fileStream2.Seek(length4, SeekOrigin.Begin);
              }
            }
          }
        }
      }
    }

    public int GetStringWidth(string text)
    {
      if (text == null || text.Length <= 0)
        return 0;
      char[] charArray = text.ToCharArray();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < charArray.Length; ++index)
      {
        char ch = charArray[index];
        if ((int) ch >= 32 && (int) ch < 256)
        {
          FontImage fontImage = this.m_Images[(int) ch - 32];
          num1 += fontImage.xWidth;
          if (num1 > num2)
            num2 = num1;
        }
        else if ((int) ch == 10)
          num1 = 0;
      }
      return num2;
    }

    public Texture GetString(string String, IHue Hue)
    {
      return this.m_Cache[String, Hue];
    }

    unsafe Texture IFontFactory.CreateInstance(string text)
    {
      if (string.IsNullOrEmpty(text))
        return Texture.Empty;
      int num1 = 0;
      int num2 = 0;
      int Width = 0;
      int num3 = 1;
      char[] charArray = text.ToCharArray();
      for (int index = 0; index < charArray.Length; ++index)
      {
        char ch = charArray[index];
        if ((int) ch >= 32 && (int) ch < 256)
        {
          FontImage fontImage = this.m_Images[(int) ch - 32];
          num2 += fontImage.xWidth;
          if (num2 > Width)
            Width = num2;
          if (fontImage.yHeight > num1)
            num1 = fontImage.yHeight;
        }
        else if ((int) ch == 10)
        {
          num2 = 0;
          ++num3;
        }
      }
      int Height = num3 * num1;
      if (Width <= 0 || Height <= 0)
        return Texture.Empty;
      Texture texture = new Texture(Width, Height, TextureTransparency.Simple);
      if (texture.IsEmpty())
        return Texture.Empty;
      fixed (short* numPtr1 = new short[this.m_Palette.Length])
      {
        fixed (short* numPtr2 = this.m_Palette)
          Hues.Default.CopyPixels((void*) (numPtr2 + 1), (void*) (numPtr1 + 1), this.m_Palette.Length - 1);
        LockData lockData = texture.Lock(LockFlags.WriteOnly);
        short* numPtr3 = (short*) lockData.pvSrc;
        short* numPtr4 = numPtr3;
        int num4 = lockData.Pitch >> 1;
        int num5 = num4 * num1;
        for (int index = 0; index < charArray.Length; ++index)
        {
          char ch = charArray[index];
          if ((int) ch >= 32 && (int) ch < 256)
          {
            FontImage fontImage = this.m_Images[(int) ch - 32];
            int num6 = fontImage.xWidth;
            int num7 = fontImage.yHeight;
            short* numPtr2 = numPtr4 + (num1 - num7) * num4;
            int num8 = num4 - num6;
            int num9 = fontImage.xDelta - num6;
            fixed (byte* numPtr5 = fontImage.xyPixels)
            {
              byte* numPtr6 = numPtr5;
              int num10 = 0;
              while (num10 < num7)
              {
                int num11 = num6 >> 2;
                int num12 = num6 & 3;
                while (--num11 >= 0)
                {
                  *numPtr2 = numPtr1[*numPtr6];
                  numPtr2[1] = numPtr1[numPtr6[1]];
                  numPtr2[2] = numPtr1[numPtr6[2]];
                  numPtr2[3] = numPtr1[numPtr6[3]];
                  numPtr2 += 4;
                  numPtr6 += 4;
                }
                while (--num12 >= 0)
                  *numPtr2++ = numPtr1[*numPtr6++];
                ++num10;
                numPtr2 += num8;
                numPtr6 += num9;
              }
            }
            numPtr4 += fontImage.xWidth;
          }
          else if ((int) ch == 10)
          {
            numPtr3 += num5;
            numPtr4 = numPtr3;
          }
        }
        texture.Unlock();
        return texture;
      }
    }

    public void Dispose()
    {
      this.m_Cache.Dispose();
      this.m_Cache = (FontCache) null;
      this.m_Palette = (short[]) null;
      this.m_Images = (FontImage[]) null;
      Font.m_Buffer = (byte[]) null;
      this.m_WrapCache.Clear();
      this.m_WrapCache = (Hashtable) null;
    }
  }
}

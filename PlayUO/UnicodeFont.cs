// Decompiled with JetBrains decompiler
// Type: PlayUO.UnicodeFont
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class UnicodeFont : IFont
  {
    private string m_FileName = "";
    private int m_SpaceWidth = 8;
    private Hashtable m_WrapCache = new Hashtable();
    private static short[] m_Colors = new short[256];
    private static short[] m_HuedColors = new short[256];
    private static byte[] m_NullChar = new byte[10];
    private const short Border = -32768;
    private int[] m_Lookup;
    private int m_FontID;
    private FontCache[] m_Cache;
    private Stream m_Stream;
    private bool m_Underline;
    private bool m_Bold;
    private bool m_Italic;
    private byte[] m_4Bytes;
    private UnicodeFont.UnicodeFontFactory[] m_Factories;

    public bool Underline
    {
      get
      {
        return this.m_Underline;
      }
      set
      {
        this.m_Underline = value;
      }
    }

    public bool Bold
    {
      get
      {
        return this.m_Bold;
      }
      set
      {
        this.m_Bold = value;
      }
    }

    public bool Italic
    {
      get
      {
        return this.m_Italic;
      }
      set
      {
        this.m_Italic = value;
      }
    }

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
        return string.Format("UniFont[{0}]", (object) this.m_FontID);
      }
    }

    public int SpaceWidth
    {
      get
      {
        return this.m_SpaceWidth;
      }
      set
      {
        this.m_SpaceWidth = value;
      }
    }

    static UnicodeFont()
    {
      UnicodeFont.m_NullChar[0] = byte.MaxValue;
      UnicodeFont.m_NullChar[1] = (byte) 129;
      UnicodeFont.m_NullChar[2] = (byte) 129;
      UnicodeFont.m_NullChar[3] = (byte) 129;
      UnicodeFont.m_NullChar[4] = (byte) 129;
      UnicodeFont.m_NullChar[5] = (byte) 129;
      UnicodeFont.m_NullChar[6] = (byte) 129;
      UnicodeFont.m_NullChar[7] = (byte) 129;
      UnicodeFont.m_NullChar[8] = (byte) 129;
      UnicodeFont.m_NullChar[9] = byte.MaxValue;
      UnicodeFont.m_HuedColors[0] = (short) 0;
      UnicodeFont.m_HuedColors[128] = (short) -32767;
      short num1 = -1;
      int num2 = 0;
      int index1 = 1;
      int index2 = 129;
      while (num2 < 32)
      {
        UnicodeFont.m_Colors[index1] = UnicodeFont.m_Colors[index2] = num1;
        ++num2;
        ++index1;
        ++index2;
        num1 -= (short) 1057;
      }
    }

    public unsafe UnicodeFont(int FontID)
    {
      this.m_FileName = string.Format("UniFont{0}.mul", FontID != 0 ? (object) FontID.ToString() : (object) "");
      this.m_FontID = FontID;
      this.m_Stream = Engine.FileManager.OpenMUL(this.m_FileName);
      this.m_Lookup = new int[65536];
      fixed (int* numPtr = this.m_Lookup)
        UnsafeMethods.ReadFile((FileStream) this.m_Stream, (void*) numPtr, 262144);
      this.m_4Bytes = new byte[4];
      this.m_Factories = new UnicodeFont.UnicodeFontFactory[16];
      this.m_Cache = new FontCache[this.m_Factories.Length];
      for (int flags = 0; flags < this.m_Factories.Length; ++flags)
      {
        this.m_Factories[flags] = new UnicodeFont.UnicodeFontFactory(this, flags);
        this.m_Cache[flags] = new FontCache((IFontFactory) this.m_Factories[flags]);
      }
    }

    public override string ToString()
    {
      return string.Format("<Unicode Font #{0}>", (object) this.m_FontID);
    }

    public void Dispose()
    {
      for (int index = 0; index < this.m_Cache.Length; ++index)
      {
        this.m_Cache[index].Dispose();
        this.m_Cache[index] = (FontCache) null;
      }
      this.m_Cache = (FontCache[]) null;
      this.m_Stream.Close();
      this.m_Stream = (Stream) null;
      UnicodeFont.m_Colors = (short[]) null;
      UnicodeFont.m_HuedColors = (short[]) null;
      this.m_FileName = (string) null;
      this.m_Lookup = (int[]) null;
      this.m_WrapCache.Clear();
      this.m_WrapCache = (Hashtable) null;
      this.m_4Bytes = (byte[]) null;
      UnicodeFont.m_NullChar = (byte[]) null;
    }

    public int GetStringWidth(string text)
    {
      if (text == null || text.Length <= 0)
        return 2;
      return this.m_Factories[this.GetFlags(Hues.Default)].MeasureWidth(text);
    }

    private int GetFlags(IHue hue)
    {
      int num = 0;
      if (this.m_Underline)
        num |= 1;
      if (this.m_Bold)
        num |= 2;
      if (this.m_Italic)
        num |= 4;
      if (!(hue is Hues.ColorFillHue) && (hue.HueID() & 16383) != 1)
        num |= 8;
      return num;
    }

    public Texture GetString(string String, IHue Hue)
    {
      return this.m_Cache[this.GetFlags(Hue)][String, Hue];
    }

    private class UnicodeFontFactory : TextureFactory, IFontFactory
    {
      private UnicodeFont m_Font;
      private int m_Flags;
      private string m_String;
      private int m_xMin;
      private int m_yMin;
      private int m_xMax;
      private int m_yMax;
      private UnicodeFont.UnicodeFontFactory.CharacterBits[] m_LowCharacters;
      private static byte[] m_Buffer;

      public override TextureTransparency Transparency
      {
        get
        {
          return TextureTransparency.Simple;
        }
      }

      public UnicodeFontFactory(UnicodeFont font, int flags)
      {
        this.m_Font = font;
        this.m_Flags = flags;
      }

      public Texture CreateInstance(string text)
      {
        this.m_String = text;
        return this.Construct(false);
      }

      public override Texture Reconstruct(object[] args)
      {
        this.m_String = (string) args[0];
        return this.Construct(true);
      }

      protected override void CoreAssignArgs(Texture tex)
      {
        tex.m_Factory = (TextureFactory) this;
        tex.m_FactoryArgs = new object[1]
        {
          (object) this.m_String
        };
        tex.xMin = this.m_xMin;
        tex.yMin = this.m_yMin;
        tex.xMax = this.m_xMax;
        tex.yMax = this.m_yMax;
      }

      protected override bool CoreLookup()
      {
        if (this.m_String != null)
          return this.m_String.Length > 0;
        return false;
      }

      public int MeasureWidth(string text)
      {
        this.m_String = text;
        if (!this.CoreLookup())
          return 2;
        int width;
        int height;
        this.CoreGetDimensions(out width, out height);
        return width;
      }

      private UnicodeFont.UnicodeFontFactory.CharacterBits GetCharacter(char c, bool needBits)
      {
        int index = (int) c;
        if (index < 0 || index >= 256)
          return this.LoadCharacter(index, needBits);
        if (this.m_LowCharacters == null)
          this.m_LowCharacters = new UnicodeFont.UnicodeFontFactory.CharacterBits[256];
        UnicodeFont.UnicodeFontFactory.CharacterBits characterBits = this.m_LowCharacters[index];
        if (characterBits == null)
          this.m_LowCharacters[index] = characterBits = this.LoadCharacter(index, true);
        return characterBits;
      }

      private UnicodeFont.UnicodeFontFactory.CharacterBits LoadCharacter(int index, bool needBits)
      {
        this.m_Font.m_Stream.Seek((long) this.m_Font.m_Lookup[index], SeekOrigin.Begin);
        return new UnicodeFont.UnicodeFontFactory.CharacterBits((FileStream) this.m_Font.m_Stream, needBits);
      }

      protected override void CoreGetDimensions(out int width, out int height)
      {
        string str = this.m_String;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 18;
        int spaceWidth = this.m_Font.SpaceWidth;
        bool flag = false;
        for (int index = 0; index < str.Length; ++index)
        {
          char c = str[index];
          switch (c)
          {
            case '\t':
              flag = false;
              num1 += 24;
              if (num1 > num3)
              {
                num3 = num1;
                break;
              }
              break;
            case '\n':
            case '\r':
              if (!flag)
              {
                num1 = 0;
                num2 += 18;
                num4 += 18;
                flag = true;
                break;
              }
              break;
            case ' ':
              flag = false;
              num1 += spaceWidth;
              if (num1 > num3)
              {
                num3 = num1;
                break;
              }
              break;
            default:
              flag = false;
              UnicodeFont.UnicodeFontFactory.CharacterBits character = this.GetCharacter(c, false);
              if (num1 > 0)
                ++num1;
              num1 += character.m_xOffset + character.m_xWidth;
              if (num1 > num3)
                num3 = num1;
              if (num2 + character.m_yOffset + character.m_yHeight > num4)
              {
                num4 = num2 + character.m_yOffset + character.m_yHeight;
                break;
              }
              break;
          }
        }
        width = num3 + 2;
        height = num4 + 2;
      }

      protected override unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta)
      {
        string str = this.m_String;
        int num1 = 0;
        int num2 = 0;
        int spaceWidth = this.m_Font.SpaceWidth;
        bool flag1 = false;
        int size = width * height;
        byte[] numArray = UnicodeFont.UnicodeFontFactory.m_Buffer;
        if (numArray == null || size > numArray.Length)
          UnicodeFont.UnicodeFontFactory.m_Buffer = numArray = new byte[size];
        bool flag2 = (this.m_Flags & 8) != 0;
        fixed (byte* buffer = numArray)
        {
          UnsafeMethods.ZeroMemory(buffer, size);
          for (int index1 = 0; index1 < str.Length; ++index1)
          {
            char c = str[index1];
            switch (c)
            {
              case '\t':
                flag1 = false;
                num1 += 24;
                break;
              case '\n':
              case '\r':
                if (!flag1)
                {
                  num1 = 0;
                  num2 += 18;
                  flag1 = true;
                  break;
                }
                break;
              case ' ':
                flag1 = false;
                num1 += spaceWidth;
                break;
              default:
                flag1 = false;
                UnicodeFont.UnicodeFontFactory.CharacterBits character = this.GetCharacter(c, true);
                if (num1 > 0)
                  ++num1;
                int num3 = num1 + character.m_xOffset;
                fixed (byte* numPtr1 = character.m_Bits)
                {
                  byte* numPtr2 = buffer + ((num2 + 1 + character.m_yOffset) * width) + (num3 + 1 + character.m_xWidth - 1);
                  int num4 = 32 - character.m_xWidth;
                  int num5 = character.m_xWidth + 7 >> 3;
                  int num6 = (num2 + 1 + character.m_yOffset) * width + (num3 + 1 + character.m_xWidth - 1);
                  int num7 = 0;
                  while (num7 < character.m_yHeight)
                  {
                    uint num8 = *(uint*) numPtr1;
                    uint num9 = (uint) (((int) num8 & (int) byte.MaxValue) << 24 | ((int) num8 & 65280) << 8 | (int) ((num8 & 16711680U) >> 8) | (int) (num8 >> 24) & (int) byte.MaxValue) >> num4;
                    byte* numPtr3 = numPtr2;
                    int index2 = num6;
                    int num10 = num3 + 1 + character.m_xWidth - 1;
                    int num11 = num2 + 1 + character.m_yOffset + num7;
                    if (num11 > 0 && num11 + 1 < height)
                    {
                      if (flag2)
                      {
                        if (num10 + 1 < width)
                        {
                          while ((int) num9 != 0 && num10 > 0)
                          {
                            if (((int) num9 & 1) != 0)
                            {
                              IntPtr num12 = (IntPtr) (numPtr3 - width);
                              int num13 = (int) (byte) ((uint) *(byte*) num12 | 128U);
                              *(sbyte*) num12 = (sbyte) num13;
                              IntPtr num14 = (IntPtr) (numPtr3 - 1);
                              int num15 = (int) (byte) ((uint) *(byte*) num14 | 128U);
                              *(sbyte*) num14 = (sbyte) num15;
                              *numPtr3 = (byte) 2;
                              IntPtr num16 = (IntPtr) (numPtr3 + 1);
                              int num17 = (int) (byte) ((uint) *(byte*) num16 | 128U);
                              *(sbyte*) num16 = (sbyte) num17;
                              IntPtr num18 = (IntPtr) (numPtr3 + width);
                              int num19 = (int) (byte) ((uint) *(byte*) num18 | 128U);
                              *(sbyte*) num18 = (sbyte) num19;
                              numArray[index2 - width] |= (byte) 128;
                              numArray[index2 - 1] |= (byte) 128;
                              numArray[index2] = (byte) 2;
                              numArray[index2 + 1] |= (byte) 128;
                              numArray[index2 + width] |= (byte) 128;
                            }
                            --numPtr3;
                            --index2;
                            --num10;
                            num9 >>= 1;
                          }
                        }
                      }
                      else if (num10 + 1 < width)
                      {
                        while ((int) num9 != 0 && num10 > 0)
                        {
                          if (((int) num9 & 1) != 0)
                          {
                            *numPtr3 = (byte) 2;
                            numArray[index2] = (byte) 2;
                          }
                          --numPtr3;
                          --index2;
                          --num10;
                          num9 >>= 1;
                        }
                      }
                    }
                    ++num7;
                    numPtr2 += width;
                    num6 += width;
                    numPtr1 += num5;
                  }
                }
                num1 = num3 + character.m_xWidth;
                break;
            }
          }
          int num20 = width;
          int num21 = height;
          int num22 = 0;
          int num23 = 0;
          bool flag3 = (this.m_Flags & 1) != 0;
          byte* numPtr4 = buffer;
          fixed (short* numPtr1 = UnicodeFont.m_Colors)
            fixed (short* numPtr2 = UnicodeFont.m_HuedColors)
            {
              Hues.Default.CopyPixels((void*) (numPtr1 + 1), (void*) ((ushort*) numPtr2 + 1), 32);
              Hues.Default.CopyPixels((void*) (numPtr1 + 129), (void*) ((ushort*) numPtr2 + 129), 32);
              for (int index1 = 0; index1 < height; ++index1)
              {
                for (int index2 = 0; index2 < width; ++index2)
                {
                  if ((int) *numPtr4 != 0)
                  {
                    if (index2 < num20)
                      num20 = index2;
                    if (index2 > num22)
                      num22 = index2;
                    if (index1 < num21)
                      num21 = index1;
                    if (index1 > num23)
                      num23 = index1;
                  }
                  if (flag3 && index1 % 18 == 15)
                    *numPtr4 = (byte) 16;
                  *pLine++ = ((ushort*) numPtr2)[*numPtr4++];
                }
                pLine += lineDelta;
              }
            }
          this.m_xMin = num20;
          this.m_yMin = num21;
          this.m_xMax = num22;
          this.m_yMax = num23;
        }
      }

      private class CharacterBits
      {
        private static byte[] m_Header = new byte[4];
        public int m_xOffset;
        public int m_yOffset;
        public int m_xWidth;
        public int m_yHeight;
        public byte[] m_Bits;

        public CharacterBits(FileStream stream, bool needBits)
        {
          if (stream.Read(UnicodeFont.UnicodeFontFactory.CharacterBits.m_Header, 0, 4) == 4)
          {
            this.m_xOffset = (int) (sbyte) UnicodeFont.UnicodeFontFactory.CharacterBits.m_Header[0];
            this.m_yOffset = (int) (sbyte) UnicodeFont.UnicodeFontFactory.CharacterBits.m_Header[1];
            this.m_xWidth = (int) (sbyte) UnicodeFont.UnicodeFontFactory.CharacterBits.m_Header[2];
            this.m_yHeight = (int) (sbyte) UnicodeFont.UnicodeFontFactory.CharacterBits.m_Header[3];
            int count = (this.m_xWidth + 7 >> 3) * this.m_yHeight;
            if (count > 0)
            {
              if (!needBits)
                return;
              this.m_Bits = new byte[count + 3 & -4];
              if (stream.Read(this.m_Bits, 0, count) == count)
                return;
            }
          }
          this.m_xOffset = 0;
          this.m_yOffset = 4;
          this.m_xWidth = 8;
          this.m_yHeight = 10;
          this.m_Bits = UnicodeFont.m_NullChar;
        }
      }
    }
  }
}

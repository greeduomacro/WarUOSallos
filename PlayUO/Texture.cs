// Decompiled with JetBrains decompiler
// Type: PlayUO.Texture
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Rectangle = System.Drawing.Rectangle;

namespace PlayUO
{
  public class Texture
  {
    private static TransformedColoredTextured[] m_BadClipperPool = VertexConstructor.Create();
    public const ushort Opaque = 32768;
    public const ushort Transparent = 0;
    private const int DoubleOpaque = -2147450880;
    protected TextureTransparency _transparency;
    protected bool m_Flip;
    protected int m_TexWidth;
    protected int m_TexHeight;
    public int Width;
    public int Height;
    public int _averageColor;
    public ShaderData _shaderData;
    protected float m_fWidth;
    protected float m_fHeight;
    public int xMin;
    public int yMin;
    public int xMax;
    public int yMax;
    protected float _minTu;
    protected float _maxTu;
    protected float _minTv;
    protected float _maxTv;
    public SharpDX.Direct3D9.Texture m_Surface;
    protected static int m_MaxTextureWidth;
    protected static int m_MaxTextureHeight;
    protected static int m_MinTextureWidth;
    protected static int m_MinTextureHeight;
    protected static bool m_Pow2;
    protected static bool m_Square;
    protected static bool m_CanSysMem;
    protected static bool m_CanVidMem;
    protected static int m_MaxAspect;
    protected static int[] m_2Pow;
    private static Texture m_Empty;
    public TextureFactory m_Factory;
    public object[] m_FactoryArgs;
    public static List<Texture> m_Textures;
    private bool m_FourBPP;
    private bool m_Disposed;
    private DataStream m_LockStream;
    public int m_LastAccess;
    private static TransformedColoredTextured[] m_PoolXYWH;
    private static TransformedColoredTextured[] m_PoolClipped;
    public TextureVB[] m_VBs;

    public static int MaxAspect
    {
      get
      {
        return Texture.m_MaxAspect;
      }
      set
      {
        Texture.m_MaxAspect = value;
      }
    }

    public static int MinTextureWidth
    {
      get
      {
        return Texture.m_MinTextureWidth;
      }
      set
      {
        Texture.m_MinTextureWidth = value;
      }
    }

    public static int MinTextureHeight
    {
      get
      {
        return Texture.m_MinTextureHeight;
      }
      set
      {
        Texture.m_MinTextureHeight = value;
      }
    }

    public static int MaxTextureWidth
    {
      get
      {
        return Texture.m_MaxTextureWidth;
      }
      set
      {
        Texture.m_MaxTextureWidth = value;
      }
    }

    public static int MaxTextureHeight
    {
      get
      {
        return Texture.m_MaxTextureHeight;
      }
      set
      {
        Texture.m_MaxTextureHeight = value;
      }
    }

    public static bool CanSysMem
    {
      get
      {
        return Texture.m_CanSysMem;
      }
      set
      {
        Texture.m_CanSysMem = value;
      }
    }

    public static bool CanVidMem
    {
      get
      {
        return Texture.m_CanVidMem;
      }
      set
      {
        Texture.m_CanVidMem = value;
      }
    }

    public static bool Pow2
    {
      get
      {
        return Texture.m_Pow2;
      }
      set
      {
        Texture.m_Pow2 = value;
      }
    }

    public static bool Square
    {
      get
      {
        return Texture.m_Square;
      }
      set
      {
        Texture.m_Square = value;
      }
    }

    public TextureTransparency Transparency
    {
      get
      {
        if (this._shaderData != null && this._shaderData.Transparency > this._transparency)
          return this._shaderData.Transparency;
        return this._transparency;
      }
      set
      {
        this._transparency = value;
      }
    }

    public SharpDX.Direct3D9.Texture Surface
    {
      get
      {
        return this.CoreGetSurface();
      }
    }

    public static Texture Empty
    {
      get
      {
        if (Texture.m_Empty == null)
          Texture.m_Empty = new Texture();
        return Texture.m_Empty;
      }
    }

    public float MaxTU
    {
      get
      {
        return this._maxTu;
      }
    }

    public float MaxTV
    {
      get
      {
        return this._maxTv;
      }
    }

    public bool Flip
    {
      get
      {
        return this.m_Flip;
      }
      set
      {
        this.m_Flip = value;
      }
    }

    static Texture()
    {
      int length = 24;
      Texture.m_Textures = new List<Texture>(1024);
      Texture.m_2Pow = new int[length];
      for (int index = 0; index < length; ++index)
        Texture.m_2Pow[index] = 1 << index;
      Texture.m_PoolXYWH = VertexConstructor.Create();
      Texture.m_PoolClipped = VertexConstructor.Create();
    }

    protected Texture()
    {
      this._transparency = TextureTransparency.None;
    }

    private Texture(Bitmap bmp)
    {
      this.Width = bmp.Width;
      this.Height = bmp.Height;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        bmp.Save((Stream) memoryStream, ImageFormat.Bmp);
        this.m_Surface = SharpDX.Direct3D9.Texture.FromMemory(Engine.m_Device, memoryStream.ToArray(), (Usage) 0, (Pool) 1);
        memoryStream.Close();
      }
      SurfaceDescription levelDescription = m_Surface.GetLevelDescription(0);
      this.m_FourBPP = levelDescription.Format.HasFlag(Format.A8R8G8B8);
      this._transparency = this.m_FourBPP ? TextureTransparency.Complex : TextureTransparency.Simple;
      this.m_TexWidth = (int) levelDescription.Width;
      this.m_TexHeight = (int) levelDescription.Height;
      this._maxTu = (float) this.Width / (float) this.m_TexWidth;
      this._maxTv = (float) this.Height / (float) this.m_TexHeight;
      this.m_fWidth = (float) this.Width;
      this.m_fHeight = (float) this.Height;
      this.xMax = this.Width - 1;
      this.yMax = this.Height - 1;
      Texture.m_Textures.Add(this);
    }

    public Texture(int Width, int Height, TextureTransparency transparency)
      : this(Width, Height, (Format) 25, transparency)
    {
    }

    public Texture(int Width, int Height, Format fmt, TextureTransparency transparency)
      : this(Width, Height, fmt, (Pool) 1, transparency)
    {
    }

    public Texture(int Width, int Height, Format fmt, Pool pool, TextureTransparency transparency)
      : this(Width, Height, fmt, pool, false, transparency)
    {
    }

    public Texture(int Width, int Height, Format fmt, Pool pool, bool isReconstruct, TextureTransparency transparency)
      : this(Width, Height, fmt, pool, isReconstruct, transparency, (Usage) 0)
    {
    }

    public Texture(int Width, int Height, Format fmt, Pool pool, bool isReconstruct, TextureTransparency transparency, Usage usage)
    {
      this._transparency = transparency;
      int num1 = 0;
      int num2 = 0;
      if (Texture.m_Pow2)
      {
        int num3 = 0;
        while (num1 < Width)
          num1 = Texture.m_2Pow[num3++];
        int num4 = 0;
        while (num2 < Height)
          num2 = Texture.m_2Pow[num4++];
      }
      else
      {
        num1 = Width;
        num2 = Height;
      }
      if (Texture.m_MaxAspect != 0)
      {
        if ((num1 <= num2 ? (double) num2 / (double) num1 : (double) num1 / (double) num2) > (double) Texture.m_MaxAspect)
        {
          if (num1 > num2)
            num2 = num1 / Texture.m_MaxAspect;
          else
            num1 = num2 / Texture.m_MaxAspect;
        }
      }
      if (num1 < Texture.m_MinTextureWidth)
        num1 = Texture.m_MinTextureWidth;
      if (num2 < Texture.m_MinTextureHeight)
        num2 = Texture.m_MinTextureHeight;
      if (Texture.m_Square)
      {
        if (num1 > num2)
          num2 = num1;
        else if (num1 < num2)
          num1 = num2;
      }
      if (num1 > Texture.m_MaxTextureWidth || num2 > Texture.m_MaxTextureHeight)
        return;
      this.Width = Width;
      this.Height = Height;
      this.m_TexWidth = num1;
      this.m_TexHeight = num2;
      this.m_FourBPP = fmt.HasFlag(Format.A8R8G8B8);
      this._minTu = 1f / (float) (this.m_TexWidth * 2);
      this._minTv = 1f / (float) (this.m_TexHeight * 2);
      this._maxTu = (float) (Width * 2 - 1) / (float) (this.m_TexWidth * 2);
      this._maxTv = (float) (Height * 2 - 1) / (float) (this.m_TexHeight * 2);
      this.m_fWidth = (float) Width;
      this.m_fHeight = (float) Height;
      this.m_Surface = new SharpDX.Direct3D9.Texture(Engine.m_Device, this.m_TexWidth, this.m_TexHeight, 1, usage, fmt, pool);
      this.xMax = Width - 1;
      this.yMax = Height - 1;
      if (isReconstruct)
        return;
      Texture.m_Textures.Add(this);
    }

    public static unsafe explicit operator Texture(Bitmap bmp)
    {
      int width = bmp.Width;
      int height = bmp.Height;
      Texture texture = new Texture(width, height, TextureTransparency.Simple);
      LockData lockData = texture.Lock(LockFlags.WriteOnly);
      BitmapData bitmapdata = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format16bppArgb1555);
      short* numPtr1 = (short*) bitmapdata.Scan0.ToPointer();
      short* numPtr2 = (short*) lockData.pvSrc;
      int num1 = (bitmapdata.Stride >> 1) - width;
      int num2 = (lockData.Pitch >> 1) - width;
      while (--height >= 0)
      {
        int num3 = width;
        while (--num3 >= 0)
          *numPtr2++ = *numPtr1++;
        numPtr1 += num1;
        numPtr2 += num2;
      }
      bmp.UnlockBits(bitmapdata);
      texture.Unlock();
      return texture;
    }

    public static unsafe void FillPixels(void* pvDest, int Color, int Pixels)
    {
      int num1 = Pixels >> 1;
      int* numPtr = (int*) pvDest;
      int num2 = Color << 16 | Color | -2147450880;
      while (--num1 >= 0)
        *numPtr++ = num2;
      if ((Pixels & 1) == 0)
        return;
      *(short*) numPtr = (short) (Color | 32768);
    }

    public static unsafe void ClearPixels(void* pvClear, int Pixels)
    {
      int num = Pixels >> 1;
      int* numPtr = (int*) pvClear;
      while (--num >= 0)
        *numPtr++ = 0;
      if ((Pixels & 1) == 0)
        return;
      *(short*) numPtr = (short) 0;
    }

    public static unsafe void CopyPixels(void* pvSrc, void* pvDest, int Pixels)
    {
      int num1 = Pixels >> 1;
      int* numPtr1 = (int*) pvSrc;
      int* numPtr2 = (int*) pvDest;
      int num2 = num1 & 7;
      int num3 = num1 >> 3;
      while (--num3 >= 0)
      {
        *numPtr2 = *numPtr1 | -2147450880;
        numPtr2[1] = numPtr1[1] | -2147450880;
        numPtr2[2] = numPtr1[2] | -2147450880;
        numPtr2[3] = numPtr1[3] | -2147450880;
        numPtr2[4] = numPtr1[4] | -2147450880;
        numPtr2[5] = numPtr1[5] | -2147450880;
        numPtr2[6] = numPtr1[6] | -2147450880;
        numPtr2[7] = numPtr1[7] | -2147450880;
        numPtr2 += 8;
        numPtr1 += 8;
      }
      while (--num2 >= 0)
        *numPtr2++ = *numPtr1++ | -2147450880;
      if ((Pixels & 1) == 0)
        return;
      *(short*) numPtr2 = (short) (32768 | (int) *(ushort*) numPtr1);
    }

    public unsafe Bitmap ToBitmap()
    {
      Bitmap bitmap;
      if (this.m_FourBPP)
      {
        bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
        LockData lockData = this.Lock(LockFlags.ReadOnly);
        BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        for (int index = 0; index < this.Height; ++index)
        {
          int* numPtr1 = (int*) ((int) lockData.pvSrc + index * lockData.Pitch);
          int* numPtr2 = (int*) (bitmapdata.Scan0.ToInt32() + index * bitmapdata.Stride);
          int num1 = 0;
          while (num1++ < this.Width)
          {
            int num2 = *numPtr1++;
            *numPtr2++ = num2;
          }
        }
        this.Unlock();
        bitmap.UnlockBits(bitmapdata);
      }
      else
      {
        bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format16bppArgb1555);
        LockData lockData = this.Lock(LockFlags.ReadOnly);
        BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
        for (int index = 0; index < this.Height; ++index)
        {
          ushort* numPtr1 = (ushort*) ((int) lockData.pvSrc + index * lockData.Pitch);
          ushort* numPtr2 = (ushort*) (bitmapdata.Scan0.ToInt32() + index * bitmapdata.Stride);
          int num1 = 0;
          while (num1++ < this.Width)
          {
            ushort num2 = *numPtr1++;
            *numPtr2++ = num2;
          }
        }
        this.Unlock();
        bitmap.UnlockBits(bitmapdata);
      }
      return bitmap;
    }

    public static void DisposeAll()
    {
      StreamWriter streamWriter = (StreamWriter) null;
      Texture[] array = Texture.m_Textures.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        Texture texture = array[index];
        if (texture != null)
        {
          if (texture.m_Surface != null)
          {
            string Message = "Texture leak found";
            Debug.Trace(Message);
            if (streamWriter == null)
              streamWriter = new StreamWriter((Stream) Engine.FileManager.CreateUnique("data/ultima/logs/textures", ".log"));
            streamWriter.WriteLine(Message);
            streamWriter.Flush();
          }
          if (!texture.m_Disposed)
            texture.Dispose();
          array[index] = (Texture) null;
        }
      }
      Texture.m_Textures.Clear();
      Texture.m_Textures = (List<Texture>) null;
      Texture.m_2Pow = (int[]) null;
      if (streamWriter == null)
        return;
      streamWriter.Close();
    }

    public static Texture Clone(Texture original, ShaderData shaderData)
    {
      if (original == null || original.IsEmpty())
        return original;
      return new Texture() { _shaderData = shaderData, _transparency = original._transparency, m_Surface = original.m_Surface, m_TexWidth = original.m_TexWidth, m_TexHeight = original.m_TexHeight, Width = original.Width, Height = original.Height, m_fWidth = original.m_fWidth, m_fHeight = original.m_fHeight, _minTu = original._minTu, _maxTu = original._maxTu, _minTv = original._minTv, _maxTv = original._maxTv, xMin = original.xMin, yMin = original.yMin, xMax = original.xMax, yMax = original.yMax, m_Disposed = original.m_Disposed, m_Flip = original.m_Flip, m_FourBPP = original.m_FourBPP, m_LastAccess = original.m_LastAccess };
    }

    public bool IsEmpty()
    {
      return this.m_Surface == null;
    }

    public virtual unsafe bool HitTest(int x, int y)
    {
      if (this.CoreGetSurface() == null)
        return false;
      if (x < 0)
        x = this.Width - 1 - -x % this.Width;
      else
        x %= this.Width;
      if (y < 0)
        y = this.Height - 1 - -y % this.Height;
      else
        y %= this.Height;
      if (x < this.xMin || x > this.xMax || (y < this.yMin || y > this.yMax))
        return false;
      LockData lockData = this.Lock(LockFlags.ReadOnly);
      bool flag = !this.m_FourBPP ? ((int) *(short*) ((int) lockData.pvSrc + y * lockData.Pitch + (x << 1)) & 32768) != 0 : (*(int*) ((int) lockData.pvSrc + y * lockData.Pitch + (x << 2)) >> 24 & (int) byte.MaxValue) != 0;
      this.Unlock();
      return flag;
    }

    public void Clear()
    {
      this.Clear(this.Lock(LockFlags.WriteOnly));
      this.Unlock();
    }

    public unsafe void Clear(LockData ld)
    {
      int num1 = ld.Pitch * ld.Height;
      int num2 = num1 >> 2;
      int num3 = num1 & 3;
      int* numPtr1 = (int*) ld.pvSrc;
      while (--num2 >= 0)
        *numPtr1++ = 0;
      if (num3 == 0)
        return;
      byte* numPtr2 = (byte*) ld.pvSrc;
      while (--num3 != 0)
        *numPtr2++ = (byte) 0;
    }

    public unsafe void Clear(ushort Color)
    {
      LockData lockData = this.Lock(LockFlags.WriteOnly);
      ushort* numPtr = (ushort*) lockData.pvSrc;
      int num = this.m_TexHeight * (lockData.Pitch >> 1);
      while (num-- != 0)
        *numPtr++ = Color;
      this.Unlock();
    }

    public static Texture[] FromImageSet(string path, int width, int height, int columns, int rows)
    {
      List<Texture> textureList = new List<Texture>();
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile(path);
      if (archivedFile != null)
      {
        using (Stream stream = archivedFile.Download())
        {
          using (Bitmap bitmap = new Bitmap(stream))
          {
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width * columns, rows * height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int num1 = 0;
            int y = 0;
            while (num1 < rows)
            {
              int num2 = 0;
              int x = 0;
              while (num2 < columns)
              {
                textureList.Add(Texture.FromBitmap(bitmapData, x, y, width, height));
                ++num2;
                x += width;
              }
              ++num1;
              y += height;
            }
            bitmap.UnlockBits(bitmapData);
          }
        }
      }
      return textureList.ToArray();
    }

    public static Texture FromBitmap(Bitmap bitmap)
    {
      return new Texture(bitmap);
    }

    public static Texture FromBitmap(BitmapData bitmapData, int x, int y, int width, int height)
    {
      if (bitmapData == null)
        throw new ArgumentNullException("bitmapData");
      if (x < 0 || y < 0 || (width < 0 || height < 0))
        throw new ArgumentException("Position and size must be greater than or equal to zero.");
      if (x + width > bitmapData.Width || y + height > bitmapData.Height)
        throw new ArgumentException("Specified region must be contained entirely within the bitmap data bounds.");
      return FromBitmapAux(bitmapData, x, y, width, height);
    }

    private static unsafe Texture FromBitmapAux(BitmapData bitmapData, int x, int y, int width, int height)
    {
      Texture texture = new Texture(width, height, (Format) 21, TextureTransparency.Complex);
      LockData lockData = texture.Lock(LockFlags.WriteOnly);
      int num = 0;
      while (num < height)
      {
        int* numPtr1 = (int*)(bitmapData.Scan0 + y * bitmapData.Stride +  x * 4);
        int* numPtr2 = (int*)((IntPtr)lockData.pvSrc + num * lockData.Pitch);
        int* numPtr3 = numPtr1 + width;
        while (numPtr1 < numPtr3)
          *numPtr2++ = *numPtr1++;
        ++num;
        ++y;
      }
      texture.Unlock();
      return texture;
    }

    public void SetPriority(int newPriority)
    {
    }

    public void Dispose()
    {
      if (this.m_Disposed)
        return;
      this.m_Disposed = true;
      if (this.m_Surface != null)
        ((DisposeBase) this.m_Surface).Dispose();
      this.m_Surface = null;
    }

    protected SharpDX.Direct3D9.Texture CoreGetSurface()
    {
      this.m_LastAccess = Engine.Ticks;
      if (this.m_Surface == null)
        return  null;
      if (((DisposeBase) this.m_Surface).IsDisposed)
        return this.m_Surface = CoreReconstruct();
      return this.m_Surface;
    }

    protected SharpDX.Direct3D9.Texture CoreReconstruct()
    {
      if (this.m_Factory == null)
        return null;
      return this.m_Factory.Reconstruct(this.m_FactoryArgs).m_Surface;
    }

    public virtual unsafe LockData Lock(LockFlags flags)
    {
        SharpDX.Direct3D9.Texture surface = this.CoreGetSurface();
      if (surface == null)
        return new LockData();
      LockFlags lockFlags = (LockFlags) 2048;
      if (flags == LockFlags.ReadOnly)
        lockFlags = lockFlags | (LockFlags) 16;
      DataStream dataStream = (DataStream) null;
      int num = -1;
      do
      {
        try
        {
          num = (int) surface.LockRectangle(0, (SharpDX.Direct3D9.LockFlags) lockFlags, out dataStream).Pitch;
        }
        catch
        {
        }
      }
      while (dataStream == null);
      LockData lockData = new LockData();
      lockData.Pitch = num;
      lockData.pvSrc = (void*) dataStream.DataPointer;
      lockData.Height = this.Height;
      lockData.Width = this.Width;
      this.m_LockStream = dataStream;
      return lockData;
    }

    public void Unlock()
    {
        SharpDX.Direct3D9.Texture surface = this.CoreGetSurface();
      if (surface == null)
        return;
      if (this.m_LockStream != null)
        ((Stream) this.m_LockStream).Close();
      this.m_LockStream = (DataStream) null;
      surface.UnlockRectangle(0);
    }

    public void Draw(int X, int Y, int Width, int Height)
    {
      this.Draw(X, Y, Width, Height, 16777215);
    }

    public void Draw(int xScreen, int yScreen, int xWidth, int yHeight, int vColor)
    {
      if (this.m_Surface == null || xWidth <= 0 || yHeight <= 0)
        return;
      TransformedColoredTextured[] transformedColoredTexturedArray = Texture.m_PoolXYWH;
      transformedColoredTexturedArray[0].Color = transformedColoredTexturedArray[1].Color = transformedColoredTexturedArray[2].Color = transformedColoredTexturedArray[3].Color = Renderer.GetQuadColor(vColor);
      float num1 = (float) xScreen - 0.5f;
      float num2 = (float) yScreen - 0.5f;
      int num3 = xWidth / this.Width;
      int num4 = yHeight / this.Height;
      int num5 = xWidth % this.Width;
      int num6 = yHeight % this.Height;
      int num7 = Engine.ScreenWidth;
      int num8 = Engine.ScreenHeight;
      float num9 = (float) (num5 * 2 - 1) / (float) (this.m_TexWidth * 2);
      float num10 = (float) (num6 * 2 - 1) / (float) (this.m_TexHeight * 2);
      Renderer.SetTexture(this);
      if (num3 > 0 && num4 > 0)
      {
        int num11 = xScreen;
        int num12 = xScreen + this.Width;
        transformedColoredTexturedArray[0].X = transformedColoredTexturedArray[1].X = num1 + this.m_fWidth;
        transformedColoredTexturedArray[2].X = transformedColoredTexturedArray[3].X = num1;
        this.ApplyQuadTuTv(transformedColoredTexturedArray);
        int num13 = 0;
        while (num13 < num3)
        {
          transformedColoredTexturedArray[0].Y = transformedColoredTexturedArray[2].Y = num2 + this.m_fHeight;
          transformedColoredTexturedArray[1].Y = transformedColoredTexturedArray[3].Y = num2;
          int num14 = yScreen;
          int num15 = yScreen + this.Height;
          int num16 = 0;
          while (num16 < num4)
          {
            if (num12 > 0 && num11 <= num7 && (num15 > 0 && num14 <= num8))
              Renderer.DrawQuadPrecalc(transformedColoredTexturedArray);
            ++num16;
            transformedColoredTexturedArray[0].Y += this.m_fHeight;
            transformedColoredTexturedArray[1].Y += this.m_fHeight;
            transformedColoredTexturedArray[2].Y += this.m_fHeight;
            transformedColoredTexturedArray[3].Y += this.m_fHeight;
            num14 += this.Height;
            num15 += this.Height;
          }
          ++num13;
          transformedColoredTexturedArray[0].X += this.m_fWidth;
          transformedColoredTexturedArray[1].X += this.m_fWidth;
          transformedColoredTexturedArray[2].X += this.m_fWidth;
          transformedColoredTexturedArray[3].X += this.m_fWidth;
          num11 += this.Width;
          num12 += this.Width;
        }
      }
      if (num3 > 0 && num6 > 0)
      {
        int num11 = xScreen;
        int num12 = xScreen + this.Width;
        int num13 = yScreen + num4 * this.Height;
        int num14 = num13 + num6;
        transformedColoredTexturedArray[0].X = transformedColoredTexturedArray[1].X = num1 + this.m_fWidth;
        transformedColoredTexturedArray[0].Y = transformedColoredTexturedArray[2].Y = (float) num14 - 0.5f;
        transformedColoredTexturedArray[1].Y = transformedColoredTexturedArray[3].Y = (float) num13 - 0.5f;
        transformedColoredTexturedArray[2].X = transformedColoredTexturedArray[3].X = num1;
        transformedColoredTexturedArray[0].Tu = transformedColoredTexturedArray[1].Tu = this._maxTu;
        transformedColoredTexturedArray[0].Tv = transformedColoredTexturedArray[2].Tv = num10;
        int num15 = 0;
        while (num15 < num3)
        {
          if (num12 > 0 && num11 <= num7 && (num14 > 0 && num13 <= num8))
            Renderer.DrawQuadPrecalc(transformedColoredTexturedArray);
          ++num15;
          transformedColoredTexturedArray[0].X += this.m_fWidth;
          transformedColoredTexturedArray[1].X += this.m_fWidth;
          transformedColoredTexturedArray[2].X += this.m_fWidth;
          transformedColoredTexturedArray[3].X += this.m_fWidth;
          num11 += this.Width;
          num12 += this.Width;
        }
      }
      if (num4 > 0 && num5 > 0)
      {
        int num11 = xScreen + num3 * this.Width;
        int num12 = num11 + num5;
        int num13 = yScreen;
        int num14 = yScreen + this.Height;
        transformedColoredTexturedArray[0].X = transformedColoredTexturedArray[1].X = (float) num12 - 0.5f;
        transformedColoredTexturedArray[0].Y = transformedColoredTexturedArray[2].Y = num2 + this.m_fHeight;
        transformedColoredTexturedArray[1].Y = transformedColoredTexturedArray[3].Y = num2;
        transformedColoredTexturedArray[2].X = transformedColoredTexturedArray[3].X = (float) num11 - 0.5f;
        transformedColoredTexturedArray[0].Tu = transformedColoredTexturedArray[1].Tu = num9;
        transformedColoredTexturedArray[0].Tv = transformedColoredTexturedArray[2].Tv = this._maxTv;
        int num15 = 0;
        while (num15 < num4)
        {
          if (num12 > 0 && num11 <= num7 && (num14 > 0 && num13 <= num8))
            Renderer.DrawQuadPrecalc(transformedColoredTexturedArray);
          ++num15;
          transformedColoredTexturedArray[0].Y += this.m_fHeight;
          transformedColoredTexturedArray[1].Y += this.m_fHeight;
          transformedColoredTexturedArray[2].Y += this.m_fHeight;
          transformedColoredTexturedArray[3].Y += this.m_fHeight;
          num13 += this.Height;
          num14 += this.Height;
        }
      }
      if (num5 <= 0 || num6 <= 0)
        return;
      int num17 = xScreen + num3 * this.Width;
      int num18 = num17 + num5;
      int num19 = yScreen + num4 * this.Height;
      int num20 = num19 + num6;
      if (num18 <= 0 || num17 > num7 || (num20 <= 0 || num19 > num8))
        return;
      transformedColoredTexturedArray[0].X = transformedColoredTexturedArray[1].X = (float) num18 - 0.5f;
      transformedColoredTexturedArray[0].Y = transformedColoredTexturedArray[2].Y = (float) num20 - 0.5f;
      transformedColoredTexturedArray[1].Y = transformedColoredTexturedArray[3].Y = (float) num19 - 0.5f;
      transformedColoredTexturedArray[2].X = transformedColoredTexturedArray[3].X = (float) num17 - 0.5f;
      transformedColoredTexturedArray[0].Tu = transformedColoredTexturedArray[1].Tu = num9;
      transformedColoredTexturedArray[0].Tv = transformedColoredTexturedArray[2].Tv = num10;
      Renderer.DrawQuadPrecalc(transformedColoredTexturedArray);
    }

    public void DrawRotated(int x, int y, double angle, int color)
    {
      this.DrawRotated(x, y, angle, color, (double) (this.xMin + this.xMax) * 0.5, (double) (this.yMin + this.yMax) * 0.5);
    }

    public unsafe void DrawRotated(int x, int y, double angle, int color, double xCenter, double yCenter)
    {
      if (this.m_Surface == null)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      color = Renderer.GetQuadColor(color);
      Renderer.SetTexture(this);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* pVertex = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        this.ApplyQuadTuTv(pVertex);
        double num1 = (double) x - 0.5;
        double num2 = (double) y - 0.5;
        double num3 = (double) this.Width;
        double num4 = (double) this.Height;
        double x1 = num3 - xCenter;
        double y1 = num4 - yCenter;
        double num5 = Math.Atan2(y1, x1);
        double num6 = Math.Sqrt(x1 * x1 + y1 * y1);
        pVertex->Color = color;
        pVertex->Rhw = 1f;
        pVertex->X = (float) (num1 + num6 * Math.Cos(angle + num5));
        pVertex->Y = (float) (num2 + num6 * Math.Sin(angle + num5));
        pVertex->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr1 = pVertex + 1;
        double x2 = num3 - xCenter;
        double y2 = 0.0 - yCenter;
        double num7 = Math.Atan2(y2, x2);
        double num8 = Math.Sqrt(x2 * x2 + y2 * y2);
        transformedColoredTexturedPtr1->Color = color;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = (float) (num1 + num8 * Math.Cos(angle + num7));
        transformedColoredTexturedPtr1->Y = (float) (num2 + num8 * Math.Sin(angle + num7));
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        double x3 = 0.0 - xCenter;
        double y3 = num4 - yCenter;
        double num9 = Math.Atan2(y3, x3);
        double num10 = Math.Sqrt(x3 * x3 + y3 * y3);
        transformedColoredTexturedPtr2->Color = color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = (float) (num1 + num10 * Math.Cos(angle + num9));
        transformedColoredTexturedPtr2->Y = (float) (num2 + num10 * Math.Sin(angle + num9));
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        double x4 = 0.0 - xCenter;
        double y4 = 0.0 - yCenter;
        double num11 = Math.Atan2(y4, x4);
        double num12 = Math.Sqrt(x4 * x4 + y4 * y4);
        transformedColoredTexturedPtr3->Color = color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = (float) (num1 + num12 * Math.Cos(angle + num11));
        transformedColoredTexturedPtr3->Y = (float) (num2 + num12 * Math.Sin(angle + num11));
        transformedColoredTexturedPtr3->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    private unsafe void ApplyQuadTuTv(TransformedColoredTextured[] vertex)
    {
      fixed (TransformedColoredTextured* pVertex = vertex)
        this.ApplyQuadTuTv(pVertex);
    }

    private unsafe void ApplyQuadTuTv(TransformedColoredTextured* pVertex)
    {
      if (!this.m_Flip)
      {
        pVertex->Tu = this._maxTu;
        pVertex->Tv = this._maxTv;
        pVertex[1].Tu = this._maxTu;
        pVertex[1].Tv = this._minTv;
        pVertex[2].Tu = this._minTu;
        pVertex[2].Tv = this._maxTv;
        pVertex[3].Tu = this._minTu;
        pVertex[3].Tv = this._minTv;
      }
      else
      {
        pVertex->Tu = this._minTu;
        pVertex->Tv = this._maxTv;
        pVertex[1].Tu = this._minTu;
        pVertex[1].Tv = this._minTv;
        pVertex[2].Tu = this._maxTu;
        pVertex[2].Tv = this._maxTv;
        pVertex[3].Tu = this._maxTu;
        pVertex[3].Tv = this._minTv;
      }
    }

    public void DrawShadow(int x, int y, float xCenter, float yCenter)
    {
      this.DrawStretchSkew(x, y, 0, xCenter, yCenter, 0.5f, 0.3f);
    }

    public unsafe void DrawStretchSkew(int x, int y, int color, float xCenter, float yCenter, float skew, float scale)
    {
      if (this.m_Surface == null)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      color = Renderer.GetQuadColor(color);
      float num1 = (float) x - 0.5f;
      float num2 = (float) y - 0.5f;
      float num3 = (float) this.Width;
      float num4 = (float) this.Height;
      Renderer.SetTexture(this);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* pVertex = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        this.ApplyQuadTuTv(pVertex);
        pVertex->Color = color;
        pVertex->Rhw = 1f;
        pVertex->X = (float) ((double) num1 + (double) num3 + ((double) yCenter - (double) num4) * (double) skew);
        pVertex->Y = (float) ((double) num2 + (double) num4 + ((double) yCenter - (double) num4) * (double) scale);
        pVertex->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr1 = pVertex + 1;
        transformedColoredTexturedPtr1->Color = color;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = (float) ((double) num1 + (double) num3 + (double) yCenter * (double) skew);
        transformedColoredTexturedPtr1->Y = num2 + yCenter * scale;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num1 + (yCenter - num4) * skew;
        transformedColoredTexturedPtr2->Y = (float) ((double) num2 + (double) num4 + ((double) yCenter - (double) num4) * (double) scale);
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1 + yCenter * skew;
        transformedColoredTexturedPtr3->Y = num2 + yCenter * scale;
        transformedColoredTexturedPtr3->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public unsafe void DrawScaled(int x, int y, int color, float xCenter, float yCenter, float xScale, float yScale)
    {
      if (this.m_Surface == null)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      color = Renderer.GetQuadColor(color);
      float num1 = (float) this.Width;
      float num2 = (float) this.Height;
      Renderer.SetTexture(this);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* pVertex = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        this.ApplyQuadTuTv(pVertex);
        pVertex->Color = color;
        pVertex->Rhw = 1f;
        pVertex->X = (float) x + (num1 - xCenter) * xScale;
        pVertex->Y = (float) y + (num2 - yCenter) * yScale;
        pVertex->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr1 = pVertex + 1;
        transformedColoredTexturedPtr1->Color = color;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = (float) x + (num1 - xCenter) * xScale;
        transformedColoredTexturedPtr1->Y = (float) y - yCenter * yScale;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = (float) x - xCenter * xScale;
        transformedColoredTexturedPtr2->Y = (float) y + (num2 - yCenter) * yScale;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = (float) x - xCenter * xScale;
        transformedColoredTexturedPtr3->Y = (float) y - yCenter * yScale;
        transformedColoredTexturedPtr3->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public void DrawRotated(int x, int y, double angle)
    {
      this.DrawRotated(x, y, angle, 16777215);
    }

    public void DrawClipped(int X, int Y, Clipper Clipper)
    {
      if (Clipper == null)
      {
        this.Draw(X, Y);
      }
      else
      {
        if (this.m_Surface == null)
          return;
        TransformedColoredTextured[] transformedColoredTexturedArray = Texture.m_PoolClipped;
        if (!Clipper.Clip(X, Y, this.Width, this.Height, transformedColoredTexturedArray))
          return;
        transformedColoredTexturedArray[0].Color = transformedColoredTexturedArray[1].Color = transformedColoredTexturedArray[2].Color = transformedColoredTexturedArray[3].Color = Renderer.GetQuadColor(16777215);
        if (this.m_Flip)
        {
          transformedColoredTexturedArray[3].Tu = transformedColoredTexturedArray[2].Tu = 1f - transformedColoredTexturedArray[3].Tu;
          transformedColoredTexturedArray[1].Tu = transformedColoredTexturedArray[0].Tu = 1f - transformedColoredTexturedArray[1].Tu;
        }
        transformedColoredTexturedArray[0].Tu *= this._maxTu;
        transformedColoredTexturedArray[1].Tu *= this._maxTu;
        transformedColoredTexturedArray[2].Tu *= this._maxTu;
        transformedColoredTexturedArray[3].Tu *= this._maxTu;
        transformedColoredTexturedArray[0].Tv *= this._maxTv;
        transformedColoredTexturedArray[1].Tv *= this._maxTv;
        transformedColoredTexturedArray[2].Tv *= this._maxTv;
        transformedColoredTexturedArray[3].Tv *= this._maxTv;
        Renderer.SetTexture(this);
        Renderer.DrawQuadPrecalc(transformedColoredTexturedArray);
      }
    }

    public unsafe void Draw(int x, int y, int color)
    {
      if (this.m_Surface == null || (x >= Engine.ScreenWidth || x + this.Width <= 0 || (y >= Engine.ScreenHeight || y + this.Height <= 0)))
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      float num1 = (float) x - 0.5f;
      float num2 = (float) y - 0.5f;
      float num3 = num1 + (float) this.Width;
      float num4 = num2 + (float) this.Height;
      color = Renderer.GetQuadColor(color);
      Renderer.SetTexture(this);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* pVertex = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        this.ApplyQuadTuTv(pVertex);
        pVertex->Color = color;
        pVertex->Rhw = 1f;
        pVertex->X = num3;
        pVertex->Y = num4;
        pVertex->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr1 = pVertex + 1;
        transformedColoredTexturedPtr1->Color = color;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num2;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num1;
        transformedColoredTexturedPtr2->Y = num4;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num2;
        transformedColoredTexturedPtr3->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public unsafe void DrawGame(int x, int y, int color)
    {
      if (this.m_Surface == null || (x >= Engine.GameX + Engine.GameWidth || x + this.Width <= Engine.GameX || (y >= Engine.GameY + Engine.GameHeight || y + this.Height <= Engine.GameY)))
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      float num1 = (float) x - 0.5f;
      float num2 = (float) y - 0.5f;
      float num3 = num1 + (float) this.Width;
      float num4 = num2 + (float) this.Height;
      color = Renderer.GetQuadColor(color);
      Renderer.SetTexture(this);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* pVertex = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        this.ApplyQuadTuTv(pVertex);
        pVertex->Color = color;
        pVertex->Rhw = 1f;
        pVertex->X = num3;
        pVertex->Y = num4;
        pVertex->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr1 = pVertex + 1;
        transformedColoredTexturedPtr1->Color = color;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num2;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num1;
        transformedColoredTexturedPtr2->Y = num4;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num2;
        transformedColoredTexturedPtr3->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    [Obsolete]
    public bool DrawGame(int x, int y, int color, TransformedColoredTextured[] pool)
    {
      if (this.m_Surface == null || y >= Engine.GameY + Engine.GameHeight || (y + this.Height <= Engine.GameY || x >= Engine.GameX + Engine.GameWidth) || x + this.Width <= Engine.GameX)
        return false;
      float num1 = (float) x - 0.5f;
      float num2 = (float) y - 0.5f;
      pool[0].X = pool[1].X = num1 + this.m_fWidth;
      pool[0].Y = pool[2].Y = num2 + this.m_fHeight;
      pool[1].Y = pool[3].Y = num2;
      pool[2].X = pool[3].X = num1;
      pool[0].Color = pool[1].Color = pool[2].Color = pool[3].Color = Renderer.GetQuadColor(color);
      this.ApplyQuadTuTv(pool);
      Renderer.SetTexture(this);
      Renderer.DrawQuadPrecalc(pool);
      return true;
    }

    public void Draw(int x, int y)
    {
      this.Draw(x, y, 16777215);
    }

    public void DrawGame(int x, int y)
    {
      this.DrawGame(x, y, 16777215);
    }

    public void Draw(int X, int Y, int Width, int Height, float tltu, float tltv, float trtu, float trtv, float brtu, float brtv, float bltu, float bltv)
    {
      if (this.m_Surface == null)
        return;
      TransformedColoredTextured[] v = VertexConstructor.Create();
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      float num3 = (float) Width;
      float num4 = (float) Height;
      v[3].X = num1;
      v[3].Y = num2;
      v[1].X = num1 + num3;
      v[1].Y = num2;
      v[0].X = num1 + num3;
      v[0].Y = num2 + num4;
      v[2].X = num1;
      v[2].Y = num2 + num4;
      v[0].Color = v[1].Color = v[2].Color = v[3].Color = Renderer.GetQuadColor(16777215);
      v[3].Tu = tltu;
      v[3].Tv = tltv;
      v[1].Tu = trtu;
      v[1].Tv = trtv;
      v[0].Tu = brtu;
      v[0].Tv = brtv;
      v[2].Tu = bltu;
      v[2].Tv = bltv;
      Renderer.SetTexture(this);
      Renderer.DrawQuadPrecalc(v);
    }

    public TextureVB GetVB(int type, bool alphaTest, bool filter)
    {
      if (this.m_VBs == null)
        this.m_VBs = new TextureVB[16];
      int num = 0;
      if (alphaTest)
        num |= 1;
      if (filter)
        num |= 2;
      int index = num | type << 2;
      TextureVB textureVb = this.m_VBs[index];
      if (textureVb == null)
        this.m_VBs[index] = textureVb = new TextureVB();
      return textureVb;
    }
  }
}

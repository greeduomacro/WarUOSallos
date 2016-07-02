// Decompiled with JetBrains decompiler
// Type: PlayUO.ShaderData
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using SharpDX.Direct3D9;
using System;
using System.IO;

namespace PlayUO
{
  public class ShaderData
  {
    private static readonly Version _requiredVersion = new Version(2, 0);
    private static Version _deviceVersion;
    private PixelShader _pixelShader;
    private Texture _dataSurface;
    private Callback _renderCallback;
    private TextureTransparency _transparency;

    public static Version RequiredVersion
    {
      get
      {
        return ShaderData._requiredVersion;
      }
    }

    public static Version DeviceVersion
    {
      get
      {
        return ShaderData._deviceVersion;
      }
      set
      {
        ShaderData._deviceVersion = value;
      }
    }

    public static bool IsSupported
    {
      get
      {
        if (ShaderData._deviceVersion != (Version) null)
          return ShaderData._deviceVersion >= ShaderData._requiredVersion;
        return false;
      }
    }

    public PixelShader PixelShader
    {
      get
      {
        return this._pixelShader;
      }
    }

    public Texture DataSurface
    {
      get
      {
        return this._dataSurface;
      }
    }

    public Callback RenderCallback
    {
      get
      {
        return this._renderCallback;
      }
      set
      {
        this._renderCallback = value;
      }
    }

    public TextureTransparency Transparency
    {
      get
      {
        return this._transparency;
      }
    }

    public ShaderData(string pixelShaderPath, Texture dataSurface, TextureTransparency transparency)
    {
      if (ShaderData.IsSupported)
      {
        ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/shaders/" + pixelShaderPath);
        if (archivedFile != null)
        {
          using (Stream stream = archivedFile.Download())
          {
            try
            {
              byte[] buffer = new byte[stream.Length];
              stream.Read(buffer, 0, buffer.Length);
              using (ShaderBytecode shaderBytecode = new ShaderBytecode(buffer))
                this._pixelShader = new PixelShader(Engine.m_Device, shaderBytecode);
            }
            catch (Exception ex)
            {
              Debug.Error(ex);
            }
          }
        }
        this._dataSurface = dataSurface;
      }
      this._transparency = transparency;
    }

    public ShaderData(PixelShader pixelShader, Texture dataSurface, TextureTransparency transparency)
    {
      if (ShaderData.IsSupported)
      {
        this._pixelShader = pixelShader;
        this._dataSurface = dataSurface;
      }
      this._transparency = transparency;
    }
  }
}

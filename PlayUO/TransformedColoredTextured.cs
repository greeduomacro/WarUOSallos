// Decompiled with JetBrains decompiler
// Type: PlayUO.TransformedColoredTextured
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;
using SharpDX.Direct3D9;
using System.Runtime.InteropServices;
using Microsoft.DirectX;

namespace PlayUO
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct TransformedColoredTextured
  {
    public const VertexFormat Format = VertexFormat.Normal; //unable to render the field
    public Vector4 Position;
    public int Color;
    public float Tu;
    public float Tv;

    public float X
    {
      get
      {
        return (float) this.Position.X;
      }
      set
      {
        this.Position.X = value;
      }
    }

    public float Y
    {
      get
      {
        return (float) this.Position.Y;
      }
      set
      {
        this.Position.Y = value;
      }
    }

    public float Z
    {
      get
      {
        return (float) this.Position.Z;
      }
      set
      {
        this.Position.Z = value;
      }
    }

    public float Rhw
    {
      get
      {
        return (float) this.Position.W;
      }
      set
      {
        this.Position.W = value;
      }
    }

    public static int StrideSize
    {
      get
      {
        return Marshal.SizeOf(typeof (TransformedColoredTextured));
      }
    }

    public TransformedColoredTextured(Vector4 value, int c, float u, float v)
    {
      this.Position = value;
      this.Tu = u;
      this.Tv = v;
      this.Color = c;
    }

    public TransformedColoredTextured(float xvalue, float yvalue, float zvalue, float rhwvalue, int c, float u, float v)
    {
      this.Position = new Vector4(xvalue, yvalue, zvalue, rhwvalue);
      this.Tu = u;
      this.Tv = v;
      this.Color = c;
    }
  }
}

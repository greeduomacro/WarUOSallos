// Decompiled with JetBrains decompiler
// Type: PlayUO.FontCache
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class FontCache
  {
    private IFontFactory m_Factory;
    private Hashtable m_Cached;

    public Texture this[string Key, IHue Hue]
    {
      get
      {
        if (Key == null)
        {
          Debug.Trace("FontCache[] crash averted where key == null");
          Key = "";
        }
        FontCache.CacheKey cacheKey = new FontCache.CacheKey(Key, Hue);
        Texture texture = (Texture) this.m_Cached[(object) cacheKey];
        if (texture == null)
        {
          texture = !(Hue is Hues.DefaultHue) ? Texture.Clone(this[Key, Hues.Default], Hue.ShaderData) : this.m_Factory.CreateInstance(Key);
          this.m_Cached.Add((object) cacheKey, (object) texture);
        }
        return texture;
      }
    }

    public FontCache(IFontFactory Factory)
    {
      if (Factory == null)
        throw new ArgumentNullException("Factory");
      this.m_Factory = Factory;
      this.m_Cached = new Hashtable(32, 0.5f);
    }

    public void Dispose()
    {
      foreach (Texture texture in (IEnumerable) this.m_Cached.Values)
        texture.Dispose();
      this.m_Cached.Clear();
      this.m_Cached = (Hashtable) null;
    }

    private class CacheKey
    {
      private string m_Text;
      private IHue m_Hue;
      private int m_Hash;

      public CacheKey(string text, IHue hue)
      {
        this.m_Text = text;
        this.m_Hue = hue;
        this.m_Hash = text.GetHashCode() ^ hue.GetHashCode();
      }

      public override int GetHashCode()
      {
        return this.m_Hash;
      }

      public override bool Equals(object x)
      {
        FontCache.CacheKey cacheKey = (FontCache.CacheKey) x;
        if (this == cacheKey)
          return true;
        if (this.m_Hash == cacheKey.m_Hash && this.m_Hue.Equals((object) cacheKey.m_Hue))
          return this.m_Text == cacheKey.m_Text;
        return false;
      }
    }
  }
}

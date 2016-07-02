// Decompiled with JetBrains decompiler
// Type: PlayUO.WalkAnimation
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections;

namespace PlayUO
{
  public class WalkAnimation
  {
    private static Queue m_SyncPool = new Queue();
    private int m_NewX;
    private int m_NewY;
    private int m_NewZ;
    private int m_NewDir;
    private int m_X;
    private int m_Y;
    private TimeSync m_Sync;
    private float m_Duration;
    private float m_Frames;
    private Mobile m_Mobile;
    private bool m_Advance;
    private static Queue m_Pool;

    public float Duration
    {
      get
      {
        return this.m_Duration;
      }
      set
      {
        this.m_Duration = Math.Max(0.0f, value);
      }
    }

    public int xOffset
    {
      get
      {
        return this.m_X;
      }
    }

    public int yOffset
    {
      get
      {
        return this.m_Y;
      }
    }

    public int Frames
    {
      get
      {
        return (int) this.m_Frames;
      }
    }

    public bool Advance
    {
      get
      {
        return this.m_Advance;
      }
    }

    public int NewX
    {
      get
      {
        return this.m_NewX;
      }
    }

    public int NewY
    {
      get
      {
        return this.m_NewY;
      }
    }

    public int NewZ
    {
      get
      {
        return this.m_NewZ;
      }
    }

    public int NewDir
    {
      get
      {
        return this.m_NewDir;
      }
    }

    private WalkAnimation(Mobile m, int x, int y, int z, int dir)
    {
      this.Initialize(m, x, y, z, dir);
    }

    public static WalkAnimation PoolInstance(Mobile m, int x, int y, int z, int dir)
    {
      if (WalkAnimation.m_Pool == null)
        WalkAnimation.m_Pool = new Queue();
      if (WalkAnimation.m_Pool.Count <= 0)
        return new WalkAnimation(m, x, y, z, dir);
      WalkAnimation walkAnimation = (WalkAnimation) WalkAnimation.m_Pool.Dequeue();
      walkAnimation.Initialize(m, x, y, z, dir);
      return walkAnimation;
    }

    private static int GetFrames(bool mounted, int idx)
    {
      if (!mounted)
        return idx != 0 ? 2 : 4;
      return idx != 0 ? 1 : 2;
    }

    public void Dispose()
    {
      WalkAnimation.m_Pool.Enqueue((object) this);
      if (this.m_Sync == null)
        return;
      WalkAnimation.m_SyncPool.Enqueue((object) this.m_Sync);
    }

    private void Initialize(Mobile m, int NewX, int NewY, int NewZ, int NewDir)
    {
      this.m_Mobile = m;
      this.m_NewX = NewX;
      this.m_NewY = NewY;
      this.m_NewZ = NewZ;
      this.m_NewDir = NewDir;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (m.Walking.Count == 0)
      {
        num1 = m.X;
        num2 = m.Y;
        num3 = m.Z;
      }
      else
      {
        IEnumerator enumerator = m.Walking.GetEnumerator();
        WalkAnimation walkAnimation = (WalkAnimation) null;
        while (enumerator.MoveNext())
          walkAnimation = (WalkAnimation) enumerator.Current;
        if (walkAnimation != null)
        {
          num1 = walkAnimation.m_NewX;
          num2 = walkAnimation.m_NewY;
          num3 = walkAnimation.m_NewZ;
        }
      }
      if (!m.Player)
        m.Direction = (byte) NewDir;
      this.m_Advance = false;
      this.m_Sync = (TimeSync) null;
      if (num1 != NewX || num2 != NewY || num3 != NewZ)
      {
        int num4 = NewX - num1;
        int num5 = NewY - num2;
        int num6 = NewZ - num3;
        int num7 = (num4 - num5) * 22;
        int num8 = (num4 + num5) * 22 - num6 * 4;
        this.m_X = num7;
        this.m_Y = num8;
        int idx = NewDir >> 7 & 1;
        int speed = m.Speed;
        if (idx == 1)
          speed *= 2;
        this.m_Duration = Walking.Speed / (float) speed;
        this.m_Frames = (float) WalkAnimation.GetFrames(m.IsMounted, idx);
      }
      else
      {
        this.m_X = 0;
        this.m_Y = 0;
        this.m_Duration = 0.1f;
        this.m_Frames = 0.0f;
      }
    }

    public void Start()
    {
      this.Start(true);
    }

    public void Start(bool update)
    {
      if (this.m_Sync != null)
        return;
      if (WalkAnimation.m_SyncPool.Count > 0)
      {
        this.m_Sync = (TimeSync) WalkAnimation.m_SyncPool.Dequeue();
        this.m_Sync.Initialize((double) this.m_Duration);
      }
      else
        this.m_Sync = new TimeSync((double) this.m_Duration);
      this.m_Advance = (this.m_NewDir & 7) >= 1 && (this.m_NewDir & 7) <= 4;
      if (!this.m_Advance)
        return;
      this.m_Mobile.SetLocation(this.m_NewX, this.m_NewY, this.m_NewZ);
      if (update)
        this.m_Mobile.Update();
      if (!this.m_Mobile.Player)
        return;
      Renderer.eOffsetX += this.m_X;
      Renderer.eOffsetY += this.m_Y;
    }

    public bool Snapshot(ref int xOffset, ref int yOffset, ref int fOffset)
    {
      if (this.m_Sync == null)
        this.Start();
      double num = this.m_Sync.Normalized;
      if (!Options.Current.SmoothWalk && num < 1.0)
      {
        switch (this.m_Frames)
        {
          case 1f:
            num = 0.0;
            break;
          case 2f:
            num = num < 0.5 ? 0.49999 : 0.99999;
            break;
          case 4f:
            num = num >= 0.25 ? (num >= 0.5 ? (num >= 0.75 ? 0.99999 : 0.74999) : 0.49999) : 0.24999;
            break;
        }
      }
      if (!this.m_Advance)
      {
        xOffset = (int) ((double) this.m_X * num);
        yOffset = (int) ((double) this.m_Y * num);
      }
      else
      {
        xOffset = -this.m_X + (int) ((double) this.m_X * num);
        yOffset = -this.m_Y + (int) ((double) this.m_Y * num);
      }
      fOffset = (int) ((double) this.m_Frames * num);
      return num < 1.0;
    }
  }
}

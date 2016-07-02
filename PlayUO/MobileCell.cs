// Decompiled with JetBrains decompiler
// Type: PlayUO.MobileCell
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class MobileCell : AgentCell, IAnimatedCell, ICell, IDisposable, IEntity
  {
    private static Type MyType = typeof (MobileCell);
    private sbyte m_Z;
    public Mobile m_Mobile;

    public Type CellType
    {
      get
      {
        return MobileCell.MyType;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Mobile.Serial;
      }
    }

    public sbyte Z
    {
      get
      {
        return this.m_Z;
      }
    }

    public sbyte SortZ
    {
      get
      {
        return this.m_Z;
      }
      set
      {
      }
    }

    public byte Height
    {
      get
      {
        return 15;
      }
    }

    public MobileCell(Mobile m)
      : base((PhysicalAgent) m)
    {
      this.m_Mobile = m;
      this.m_Z = (sbyte) m.Z;
    }

    public override void Update()
    {
      this.m_Z = (sbyte) this.m_Mobile.Z;
    }

    void IDisposable.Dispose()
    {
    }

    public void GetPackage(ref int Body, ref int Action, ref int Direction, ref int Frame, ref int Hue)
    {
      Mobile m = this.m_Mobile;
      Body = (int) m.Body;
      if (m.Ghost)
        Body = 970;
      Hue = (int) m.Hue;
      Hue ^= 32768;
      if (m.Walking.Count > 0)
      {
        Direction = Engine.GetAnimDirection((byte) ((WalkAnimation) m.Walking.Peek()).NewDir);
        GenericAction g;
        int num;
        if (m.IsMounted)
        {
          g = (Direction & 128) == 0 ? GenericAction.MountedWalk : GenericAction.MountedRun;
          num = (Direction & 128) == 0 ? 2 : 1;
        }
        else
        {
          g = (Direction & 128) == 0 ? GenericAction.Walk : GenericAction.Run;
          num = (Direction & 128) == 0 ? 4 : 2;
        }
        Action = Engine.m_Animations.ConvertAction(Body, m.Serial, m.X, m.Y, Direction, g, m);
        int frameCount = Engine.m_Animations.GetFrameCount(Body, Action, Direction & 7);
        if (frameCount == 0)
          Frame = 0;
        else
          Frame = m.MovedTiles * num % frameCount;
      }
      else
      {
        Direction = Engine.GetAnimDirection(m.Direction);
        if (m.Animation == null || !m.Animation.Running)
        {
          GenericAction g = !m.IsMounted ? GenericAction.Stand : GenericAction.MountedStand;
          Action = Engine.m_Animations.ConvertAction(Body, m.Serial, m.X, m.Y, Direction, g, m);
          Frame = 0;
        }
        else
        {
          int num1 = Renderer.m_Frames;
          Action = m.Animation.Action;
          Direction = Engine.GetAnimDirection((byte) ((uint) m.Direction & 7U));
          Action %= 35;
          Direction &= 7;
          int num2 = Engine.m_Animations.GetFrameCount(Body, Action, Direction);
          if (num2 == 0)
            num2 = 1;
          int num3 = m.Animation.Delay * 2 + 4;
          if (num3 < 1)
            num3 = 1;
          Frame = (num1 - m.Animation.Start) / num3 % num2;
          if (!m.Animation.Forward)
            Frame = num2 - 1 - Frame;
          if (m.Animation.Repeat && m.Animation.RepeatCount != 0 && num1 >= m.Animation.Start + m.Animation.RepeatCount * num2 * num3 - 1)
          {
            if (m.Animation.OnAnimationEnd != null)
              m.Animation.OnAnimationEnd(m.Animation, m);
          }
          else if (!m.Animation.Repeat && num1 >= m.Animation.Start + num2 * num3 - 1 && m.Animation.OnAnimationEnd != null)
            m.Animation.OnAnimationEnd(m.Animation, m);
          if (m.Animation.Repeat && m.Animation.RepeatCount != 0 && num1 >= m.Animation.Start + m.Animation.RepeatCount * num2 * num3)
          {
            m.Animation.Stop();
            GenericAction g = !m.IsMounted ? GenericAction.Stand : GenericAction.MountedStand;
            Action = Engine.m_Animations.ConvertAction(Body, m.Serial, m.X, m.Y, Direction, g, m);
            Frame = 0;
            Direction = Engine.GetAnimDirection(m.Direction);
            Direction %= 8;
            m.Animation = (Animation) null;
          }
          else
          {
            if (m.Animation.Repeat || num1 < m.Animation.Start + num2 * num3)
              return;
            m.Animation.Stop();
            GenericAction g = !m.IsMounted ? GenericAction.Stand : GenericAction.MountedStand;
            Action = Engine.m_Animations.ConvertAction(Body, m.Serial, m.X, m.Y, Direction, g, m);
            Frame = 0;
            Direction = Engine.GetAnimDirection(m.Direction);
            Direction %= 8;
            m.Animation = (Animation) null;
          }
        }
      }
    }
  }
}

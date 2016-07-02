// Decompiled with JetBrains decompiler
// Type: PlayUO.Effects
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class Effects
  {
    private int m_Temperature = 10;
    private static RenderEffect[] _itemEffects;
    private ArrayList m_Lock;
    private ArrayList m_List;
    private ArrayList m_Particles;
    private ArrayList m_Fades;
    private TransformedColoredTextured[] m_Screen;
    private bool m_DrawTemperature;
    private bool m_Locked;
    private bool m_Invalidated;
    private int m_GlobalLight;

    public bool DrawTemperature
    {
      get
      {
        return this.m_DrawTemperature;
      }
      set
      {
        this.m_DrawTemperature = value;
      }
    }

    public int ParticleCount
    {
      get
      {
        return this.m_Particles.Count;
      }
    }

    public int Temperature
    {
      get
      {
        return this.m_Temperature;
      }
      set
      {
        this.m_Temperature = value;
      }
    }

    public bool Locked
    {
      get
      {
        return this.m_Locked;
      }
    }

    public int GlobalLight
    {
      get
      {
        return this.m_GlobalLight;
      }
      set
      {
        this.m_GlobalLight = value;
      }
    }

    public Effects()
    {
      this.m_List = new ArrayList();
      this.m_Particles = new ArrayList();
      this.m_Fades = new ArrayList();
      this.m_Lock = new ArrayList();
      this.m_Screen = VertexConstructor.Create();
    }

    public static RenderEffect GetItemEffect(int itemId)
    {
      if (Effects._itemEffects == null)
        Effects._itemEffects = Effects.GetItemEffects();
      return Effects._itemEffects[itemId & 16383];
    }

    private static RenderEffect[] GetItemEffects()
    {
      RenderEffect[] renderEffectArray = new RenderEffect[16384];
      RenderEffect renderEffect1 = new RenderEffect(1f, DrawBlendType.Additive, new RenderEffectGlow(0.75f, new int?(1989360)));
      RenderEffect renderEffect2 = new RenderEffect(1f, DrawBlendType.Additive, new RenderEffectGlow(0.75f, new int?(15736350)));
      RenderEffect renderEffect3 = new RenderEffect(0.75f, DrawBlendType.Normal, new RenderEffectGlow(1f, new int?(12616492)));
      for (int index = 14000; index < 14089; ++index)
        renderEffectArray[index] = RenderEffects.AdditiveGlow;
      for (int index = 14089; index < 14120; ++index)
        renderEffectArray[index] = renderEffect3;
      for (int index = 14120; index < 14133; ++index)
        renderEffectArray[index] = RenderEffects.Additive;
      for (int index = 14138; index < 14154; ++index)
        renderEffectArray[index] = renderEffect1;
      for (int index = 14154; index < 14170; ++index)
        renderEffectArray[index] = renderEffect2;
      for (int index = 14170; index < 14217; ++index)
        renderEffectArray[index] = renderEffect1;
      renderEffectArray[14239] = new RenderEffect(1f, DrawBlendType.Additive, new RenderEffectGlow(1f, new int?(9203350)));
      return renderEffectArray;
    }

    private void UpdateScreen()
    {
      this.m_Screen[3].X = (float) Engine.GameX - 0.5f;
      this.m_Screen[3].Y = (float) Engine.GameY - 0.5f;
      this.m_Screen[1].X = (float) Engine.GameX - 0.5f + (float) Engine.ScreenWidth;
      this.m_Screen[1].Y = (float) Engine.GameY - 0.5f;
      this.m_Screen[0].X = (float) Engine.GameX - 0.5f + (float) Engine.ScreenWidth;
      this.m_Screen[0].Y = (float) Engine.GameY - 0.5f + (float) Engine.ScreenHeight;
      this.m_Screen[2].X = (float) Engine.GameX - 0.5f;
      this.m_Screen[2].Y = (float) Engine.GameY - 0.5f + (float) Engine.ScreenHeight;
    }

    public void Offset(int xDelta, int yDelta)
    {
      int count = this.m_Particles.Count;
      int index = 0;
      while (index < count)
      {
        IParticle particle = (IParticle) this.m_Particles[index];
        if (!particle.Offset(xDelta, yDelta))
        {
          this.m_Particles.RemoveAt(index);
          particle.Destroy();
          count = this.m_Particles.Count;
        }
        else
          ++index;
      }
    }

    public static float RandomRainAngle(Random rnd)
    {
      return (float) (Math.PI / 2.0 + (Math.PI * rnd.NextDouble() * 0.5 - Math.PI / 4.0));
    }

    public static float RandomSnowAngle(Random rnd)
    {
      return (float) (Math.PI * rnd.NextDouble());
    }

    public void ClearParticles()
    {
      int count = this.m_Particles.Count;
      for (int index = 0; index < count; ++index)
        ((IParticle) this.m_Particles[index]).Invalidate();
    }

    public void ClearParticle()
    {
      if (this.m_Particles.Count <= 0)
        return;
      ((IParticle) this.m_Particles[0]).Invalidate();
    }

    public void Add(IParticle p)
    {
      this.m_Particles.Add((object) p);
      this.m_Invalidated = true;
    }

    public void Add(Effect e)
    {
      this.m_List.Add((object) e);
      e.OnStart();
    }

    public void Add(Fade f)
    {
      if (this.m_Locked)
        this.m_Lock.Add((object) f);
      else
        this.m_Fades.Add((object) f);
    }

    public void Lock()
    {
      this.m_Locked = true;
    }

    public void Unlock()
    {
      this.m_Locked = false;
      for (int index = 0; index < this.m_Lock.Count; ++index)
        this.m_Fades.Add(this.m_Lock[index]);
      this.m_Fades.Clear();
    }

    public void RenderLights()
    {
      int count = this.m_List.Count;
      int index = 0;
      if (count <= 0)
        return;
      for (; index < count; ++index)
        ((Effect) this.m_List[index]).RenderLight();
    }

    public void Draw()
    {
      this.UpdateScreen();
      int count1 = this.m_List.Count;
      int index1 = 0;
      bool flag = false;
      if (count1 > 0)
      {
        while (index1 < count1)
        {
          Effect effect = (Effect) this.m_List[index1];
          if (!effect.Slice())
          {
            effect.OnStop();
            this.m_List.RemoveAt(index1);
            EffectList children = effect.Children;
            int count2 = children.Count;
            for (int index2 = 0; index2 < count2; ++index2)
            {
              this.m_List.Add((object) children[index2]);
              children[index2].OnStart();
            }
            count1 = this.m_List.Count;
          }
          else
          {
            if (effect is LightningEffect)
              flag = true;
            ++index1;
          }
        }
      }
      int count3 = this.m_Particles.Count;
      int index3 = 0;
      if (count3 > 0)
      {
        Random random = new Random();
        while (index3 < count3)
        {
          IParticle particle = (IParticle) this.m_Particles[index3];
          if (!particle.Slice())
          {
            this.m_Particles.RemoveAt(index3);
            particle.Destroy();
            count3 = this.m_Particles.Count;
            this.m_Invalidated = false;
          }
          else
          {
            if (this.m_Invalidated)
            {
              count3 = this.m_Particles.Count;
              this.m_Invalidated = false;
            }
            ++index3;
          }
        }
      }
      if (this.m_DrawTemperature)
      {
        if (this.Temperature > 25)
        {
          Renderer.SetTexture((Texture) null);
          Renderer.PushAlpha((float) (((double) this.m_Temperature - 25.0) / 102.0));
          this.m_Screen[0].Color = this.m_Screen[1].Color = this.m_Screen[2].Color = this.m_Screen[3].Color = Renderer.GetQuadColor(16728096);
          Renderer.DrawQuadPrecalc(this.m_Screen);
          Renderer.PopAlpha();
        }
        else if (this.Temperature < 10)
        {
          Renderer.SetTexture((Texture) null);
          Renderer.PushAlpha((float) Math.Abs(this.m_Temperature - 10) / 118f);
          this.m_Screen[0].Color = this.m_Screen[1].Color = this.m_Screen[2].Color = this.m_Screen[3].Color = Renderer.GetQuadColor(4243711);
          Renderer.DrawQuadPrecalc(this.m_Screen);
          Renderer.PopAlpha();
        }
      }
      int maxValue = this.m_GlobalLight;
      Mobile player = World.Player;
      if (player != null)
        maxValue -= player.LightLevel;
      if (flag)
      {
        maxValue /= 2;
        if (maxValue > 0)
          maxValue -= Engine.Random.Next(maxValue);
        int num = Engine.Random.Next(4);
        if (num > 0)
        {
          Renderer.SetTexture((Texture) null);
          Renderer.PushAlpha((float) num / 31f);
          this.m_Screen[0].Color = this.m_Screen[1].Color = this.m_Screen[2].Color = this.m_Screen[3].Color = Renderer.GetQuadColor(16777215);
          Renderer.DrawQuadPrecalc(this.m_Screen);
          Renderer.PopAlpha();
        }
      }
      int num1;
      if (maxValue < 0)
        num1 = 0;
      else if (maxValue > 31)
        num1 = 31;
      int count4 = this.m_Fades.Count;
      int index4 = 0;
      if (count4 <= 0)
        return;
      Renderer.SetTexture((Texture) null);
      double Alpha = 0.0;
      while (index4 < count4)
      {
        Fade fade = (Fade) this.m_Fades[index4];
        if (fade.Evaluate(ref Alpha))
        {
          Renderer.PushAlpha((float) Alpha);
          this.m_Screen[0].Color = this.m_Screen[1].Color = this.m_Screen[2].Color = this.m_Screen[3].Color = Renderer.GetQuadColor(fade.Color);
          Renderer.DrawQuadPrecalc(this.m_Screen);
          Renderer.PopAlpha();
          ++index4;
        }
        else
        {
          fade.OnFadeComplete();
          this.m_Fades.RemoveAt(index4);
          count4 = this.m_Fades.Count;
        }
      }
    }
  }
}

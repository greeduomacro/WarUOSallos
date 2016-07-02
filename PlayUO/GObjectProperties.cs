// Decompiled with JetBrains decompiler
// Type: PlayUO.GObjectProperties
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class GObjectProperties : GAlphaBackground
  {
    private static GObjectProperties m_Instance;
    private object m_Object;
    private ObjectPropertyList m_List;
    public bool m_WorldTooltip;
    private int m_TotalHeight;
    private int m_TotalWidth;
    private Timer m_Timer;
    private TimeSync m_Sync;
    private double m_WidthDuration;
    private double m_HeightDuration;
    private int m_CompactHeight;

    public static GObjectProperties Instance
    {
      get
      {
        return GObjectProperties.m_Instance;
      }
    }

    public object Object
    {
      get
      {
        return this.m_Object;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
        foreach (GLabel glabel in this.m_Children.ToArray())
          glabel.Scissor(1 - glabel.X, 1 - glabel.Y, this.Width - 2, this.Height - 2);
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
        foreach (GLabel glabel in this.m_Children.ToArray())
          glabel.Scissor(1 - glabel.X, 1 - glabel.Y, this.Width - 2, this.Height - 2);
      }
    }

    public GObjectProperties(int number, object o, ObjectPropertyList propList)
      : base(0, 0, 2, 20)
    {
      this.m_Object = o;
      this.m_CanDrag = false;
      this.m_Timer = new Timer(new OnTick(this.Roll_OnTick), 0);
      this.m_Timer.Start(false);
      int num = propList.Properties.Length;
      if (num == 0 && number != -1)
        num = 1;
      this.SetList(number, propList);
      this.m_WidthDuration = (double) this.m_TotalWidth * 0.000625;
      this.m_HeightDuration = (double) num * (1.0 / 80.0);
      this.m_Sync = new TimeSync(this.m_WidthDuration + this.m_HeightDuration);
    }

    protected void Roll_OnTick(Timer t)
    {
      double elapsed = this.m_Sync.Elapsed;
      if (elapsed < this.m_WidthDuration)
      {
        this.Width = 2 + (int) (elapsed / this.m_WidthDuration * (double) (this.m_TotalWidth - 2));
        this.Height = this.m_CompactHeight;
      }
      else
      {
        double num = (elapsed - this.m_WidthDuration) / this.m_HeightDuration;
        if (num >= 1.0)
        {
          if (this.m_Timer != null)
            this.m_Timer.Stop();
          this.m_Timer = (Timer) null;
          this.Width = this.m_TotalWidth;
          this.Height = this.m_TotalHeight;
        }
        else
        {
          this.Width = this.m_TotalWidth;
          this.Height = this.m_CompactHeight + (int) (num * (double) (this.m_TotalHeight - this.m_CompactHeight));
        }
      }
      Engine.Redraw();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return false;
    }

    private IHue GetDefaultHue()
    {
      if (this.m_Object is Mobile)
        return Hues.GetNotoriety(((Mobile) this.m_Object).Notoriety);
      return Hues.Load(52);
    }

    public void SetList(int number, ObjectPropertyList propList)
    {
      this.m_List = propList;
      this.m_Children.Clear();
      int Y = 5;
      int num1 = 10;
      ObjectProperty[] objectPropertyArray = propList.Properties;
      if (objectPropertyArray.Length == 0 && number != -1)
        objectPropertyArray = new ObjectProperty[1]
        {
          new ObjectProperty(number, "")
        };
      int num2 = 0;
      int index1 = -1;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      for (int index2 = 0; index2 < objectPropertyArray.Length; ++index2)
      {
        if (objectPropertyArray[index2].Number >= 1060445 && objectPropertyArray[index2].Number <= 1060449)
        {
          int num10 = 0;
          try
          {
            num10 = int.Parse(objectPropertyArray[index2].Arguments.Trim());
          }
          catch
          {
          }
          switch (objectPropertyArray[index2].Number)
          {
            case 1060445:
              num5 += num10;
              break;
            case 1060446:
              num7 += num10;
              break;
            case 1060447:
              num4 += num10;
              break;
            case 1060448:
              num3 += num10;
              break;
            case 1060449:
              num6 += num10;
              break;
          }
          if (index1 == -1)
            index1 = index2;
          num2 += num10;
        }
        else if (objectPropertyArray[index2].Number == 1060486)
        {
          try
          {
            num9 = int.Parse(objectPropertyArray[index2].Arguments.Trim());
          }
          catch
          {
          }
        }
        else if (objectPropertyArray[index2].Number == 1061167)
        {
          try
          {
            num8 = int.Parse(objectPropertyArray[index2].Arguments.Trim());
          }
          catch
          {
          }
        }
      }
      if (num8 > 0)
        num8 = num8 * (100 + num9) / 100;
      ResistInfo resistInfo1 = (ResistInfo) null;
      ResistInfo resistInfo2 = (ResistInfo) null;
      if (index1 != -1)
      {
        ArrayList arrayList = new ArrayList((ICollection) objectPropertyArray);
        arrayList.Insert(index1, (object) new ObjectProperty(1042971, string.Format("total resist {0}%", (object) num2)));
        objectPropertyArray = (ObjectProperty[]) arrayList.ToArray(typeof (ObjectProperty));
        resistInfo1 = ResistInfo.Find(objectPropertyArray[0].Text, ResistInfo.m_Armor);
        resistInfo2 = ResistInfo.Find(objectPropertyArray[0].Text, ResistInfo.m_Materials);
      }
      int num11 = -1;
      if (this.m_Object is Mobile)
      {
        Mobile mobile = (Mobile) this.m_Object;
        int num10 = 0;
        int num12 = 0;
        int num13 = 0;
        int num14 = 0;
        int num15 = 0;
        int num16 = 0;
        int num17 = 0;
        foreach (Item obj in mobile.Items)
        {
          ObjectPropertyList propertyList = obj.PropertyList;
          if (propertyList == null)
          {
            obj.QueryProperties();
          }
          else
          {
            foreach (ObjectProperty property in propertyList.Properties)
            {
              if (property.Number >= 1060445 && property.Number <= 1060449)
              {
                int num18 = 0;
                try
                {
                  num18 = int.Parse(property.Arguments.Trim());
                }
                catch
                {
                }
                switch (property.Number)
                {
                  case 1060445:
                    num13 += num18;
                    continue;
                  case 1060446:
                    num15 += num18;
                    continue;
                  case 1060447:
                    num12 += num18;
                    continue;
                  case 1060448:
                    num10 += num18;
                    continue;
                  case 1060449:
                    num14 += num18;
                    continue;
                  default:
                    continue;
                }
              }
              else if (property.Number == 1060413)
              {
                try
                {
                  num16 += int.Parse(property.Arguments.Trim());
                }
                catch
                {
                }
              }
              else if (property.Number == 1060412)
              {
                try
                {
                  num17 += int.Parse(property.Arguments.Trim());
                }
                catch
                {
                }
              }
            }
          }
        }
        num11 = num10;
        if (num12 < num11)
          num11 = num12;
        if (num13 < num11)
          num11 = num13;
        if (num14 < num11)
          num11 = num14;
        if (num15 < num11)
          num11 = num15;
        if (num10 != 0 || num12 != 0 || (num13 != 0 || num14 != 0) || (num15 != 0 || num16 != 0 || num17 != 0))
        {
          ArrayList arrayList = new ArrayList((ICollection) objectPropertyArray);
          if (num16 != 0 || num17 != 0)
            arrayList.Add((object) new ObjectProperty(1042971, string.Format("FC {0}, FCR {1}", (object) num16, (object) num17)));
          arrayList.Add((object) new ObjectProperty(1060448, num10.ToString()));
          arrayList.Add((object) new ObjectProperty(1060447, num12.ToString()));
          arrayList.Add((object) new ObjectProperty(1060445, num13.ToString()));
          arrayList.Add((object) new ObjectProperty(1060449, num14.ToString()));
          arrayList.Add((object) new ObjectProperty(1060446, num15.ToString()));
          objectPropertyArray = (ObjectProperty[]) arrayList.ToArray(typeof (ObjectProperty));
        }
      }
      if (this.m_Object is Item && ((Item) this.m_Object).Container != null && ((Item) this.m_Object).Container.m_TradeContainer)
      {
        ArrayList arrayList = new ArrayList((ICollection) objectPropertyArray);
        if (arrayList.Count > 0)
          arrayList[0] = (object) new ObjectProperty(1042971, "Their Offer");
        Item obj = (Item) this.m_Object;
        Item[] items1 = obj.FindItems((IItemValidator) new ItemIDValidator(new int[3]{ 3821, 3820, 3822 }));
        int num10 = 0;
        for (int index2 = 0; index2 < items1.Length; ++index2)
          num10 += items1[index2].Amount;
        Item[] items2 = obj.FindItems((IItemValidator) new ItemIDValidator(new int[2]{ 5359, 5360 }));
        for (int index2 = 0; index2 < items2.Length; ++index2)
        {
          ObjectPropertyList propertyList = items2[index2].PropertyList;
          if (propertyList == null)
          {
            items2[index2].QueryProperties();
          }
          else
          {
            bool flag = false;
            for (int index3 = 0; index3 < propertyList.Properties.Length; ++index3)
            {
              if (propertyList.Properties[index3].Number == 1041361)
                flag = true;
              else if (propertyList.Properties[index3].Number == 1060738)
              {
                if (flag)
                {
                  try
                  {
                    num10 += int.Parse(propertyList.Properties[index3].Arguments.Trim());
                    break;
                  }
                  catch
                  {
                    break;
                  }
                }
              }
            }
          }
        }
        arrayList.Add((object) new ObjectProperty(1042971, string.Format("Total Gold: {0:N0}", (object) num10)));
        objectPropertyArray = (ObjectProperty[]) arrayList.ToArray(typeof (ObjectProperty));
      }
      for (int index2 = 0; index2 < objectPropertyArray.Length; ++index2)
      {
        ObjectProperty objectProperty = objectPropertyArray[index2];
        GLabel glabel = new GLabel(Engine.MakeProperCase(objectProperty.Text), Engine.DefaultFont, index2 == 0 ? this.GetDefaultHue() : Hues.Bright, 5, Y);
        if (objectProperty.Number == 1061170)
        {
          int num10 = 0;
          try
          {
            num10 = int.Parse(objectProperty.Arguments.Trim());
          }
          catch
          {
          }
          Mobile player = World.Player;
          if (player != null && num10 > player.Strength)
            glabel.Hue = Hues.Load(34);
        }
        else if (objectProperty.Number == 1061682)
        {
          glabel.Hue = Hues.Load(89);
          glabel.Text = "Insured";
        }
        else if (num11 >= 0 && objectProperty.Number >= 1060445 && objectProperty.Number <= 1060449)
        {
          int num10 = 0;
          try
          {
            num10 = int.Parse(objectProperty.Arguments.Trim());
          }
          catch
          {
          }
          if (num10 == num11)
          {
            if (objectProperty.Number == 1060445)
              glabel.Hue = Hues.Load(95);
            else if (objectProperty.Number == 1060446)
              glabel.Hue = Hues.Load(26);
            else if (objectProperty.Number == 1060447)
              glabel.Hue = Hues.Load(45);
            else if (objectProperty.Number == 1060448)
              glabel.Hue = Hues.Load(1154);
            else if (objectProperty.Number == 1060449)
              glabel.Hue = Hues.Load(70);
          }
        }
        else if (resistInfo1 != null && objectProperty.Number >= 1060445 && objectProperty.Number <= 1060449)
        {
          int num10 = 0;
          try
          {
            num10 = int.Parse(objectProperty.Arguments.Trim());
          }
          catch
          {
          }
          int num12 = 0;
          switch (objectProperty.Number)
          {
            case 1060445:
              num12 = resistInfo1.m_Cold + (resistInfo2 == null ? 0 : resistInfo2.m_Cold);
              break;
            case 1060446:
              num12 = resistInfo1.m_Energy + (resistInfo2 == null ? 0 : resistInfo2.m_Energy);
              break;
            case 1060447:
              num12 = resistInfo1.m_Fire + (resistInfo2 == null ? 0 : resistInfo2.m_Fire);
              break;
            case 1060448:
              num12 = resistInfo1.m_Physical + (resistInfo2 == null ? 0 : resistInfo2.m_Physical);
              break;
            case 1060449:
              num12 = resistInfo1.m_Poison + (resistInfo2 == null ? 0 : resistInfo2.m_Poison);
              break;
          }
          int num13 = num10 - num12;
          if (num13 > 0)
            glabel.Text += string.Format(" (+{0}%)", (object) num13);
          else if (num13 < 0)
            glabel.Text += string.Format(" (-{0}%)", (object) Math.Abs(num13));
        }
        else if (objectProperty.Number == 1061167)
        {
          Mobile player = World.Player;
          if (player != null)
          {
            double num10 = Math.Floor(40000.0 / (double) ((player.CurrentStamina + 100) * num8)) / 2.0;
            glabel.Text += string.Format(" ({0:F1}s)", (object) num10);
          }
        }
        glabel.Y -= glabel.Image.yMin;
        glabel.X -= glabel.Image.xMin;
        Y = glabel.Y + glabel.Image.yMax + 5;
        if (10 + (glabel.Image.xMax - glabel.Image.xMin + 1) > num1)
          num1 = 10 + (glabel.Image.xMax - glabel.Image.xMin + 1);
        this.m_Children.Add((Gump) glabel);
        if (index2 == 0)
          this.m_CompactHeight = Y;
      }
      this.m_TotalWidth = num1;
      this.m_TotalHeight = Y;
      if (this.m_Timer == null)
      {
        this.Width = num1;
        this.Height = Y;
      }
      foreach (GLabel glabel in this.m_Children.ToArray())
      {
        glabel.X = (num1 - (glabel.Image.xMax - glabel.Image.xMin + 1)) / 2 - glabel.Image.xMin;
        glabel.Scissor(1 - glabel.X, 1 - glabel.Y, this.Width - 2, this.Height - 2);
      }
    }

    protected internal override void Render(int X, int Y)
    {
      if (this.m_Object is Item)
      {
        Item obj = (Item) this.m_Object;
        if (obj.PropertyList == null)
          obj.QueryProperties();
        else if (obj.PropertyList != this.m_List)
          this.SetList(1020000 + (obj.ID & 16383), obj.PropertyList);
      }
      if (this.m_WorldTooltip)
      {
        bool flag1 = Engine.m_xMouse < Engine.ScreenWidth / 2;
        bool flag2 = Engine.m_yMouse < Engine.ScreenHeight / 2;
        int num1 = Engine.m_xMouse - this.Width - 2;
        int num2 = Engine.m_yMouse - this.Height - 2;
        if (flag1)
          num1 = !flag2 ? Engine.m_xMouse : Engine.m_xMouse + Cursor.Width + 2;
        if (flag2)
          num2 = !flag1 ? Engine.m_yMouse : Engine.m_yMouse + Cursor.Height + 2;
        if (num1 < 2)
          num1 = 2;
        else if (num1 + this.Width + 2 > Engine.ScreenWidth)
          num1 = Engine.ScreenWidth - this.Width - 2;
        if (num2 < 2)
          num2 = 2;
        else if (num2 + this.Height + 2 > Engine.ScreenHeight)
          num2 = Engine.ScreenHeight - this.Height - 2;
        this.X = num1;
        this.Y = num2;
      }
      base.Render(X, Y);
    }

    public static void Hide()
    {
      if (GObjectProperties.m_Instance != null)
        Gumps.Destroy((Gump) GObjectProperties.m_Instance);
      GObjectProperties.m_Instance = (GObjectProperties) null;
    }

    public static void Display(object o)
    {
      GObjectProperties.Hide();
      if (o is Item)
      {
        Item obj = (Item) o;
        ObjectPropertyList propertyList = obj.PropertyList;
        if (propertyList != null)
          GObjectProperties.m_Instance = new GObjectProperties(1020000 + obj.ID, o, propertyList);
      }
      else if (o is Mobile)
      {
        Mobile mobile = (Mobile) o;
        ObjectPropertyList propertyList = mobile.PropertyList;
        if (propertyList != null)
          GObjectProperties.m_Instance = new GObjectProperties(-1, (object) mobile, propertyList);
      }
      if (GObjectProperties.m_Instance == null)
        return;
      Gumps.Desktop.Children.Add((Gump) GObjectProperties.m_Instance);
      GObjectProperties.m_Instance.m_WorldTooltip = true;
    }
  }
}

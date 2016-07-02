// Decompiled with JetBrains decompiler
// Type: PlayUO.GPaperdoll
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public class GPaperdoll : GDragable, IContainerView, IAgentView
  {
    private PaperdollMode _mode = ~PaperdollMode.Compact;
    private Mobile m_Mobile;
    private Item _dragPreview;
    private PaperdollBody _body;
    private List<GImage> _bodyImages;
    private bool _allowItemLift;
    private GLabel _titleLabel;

    public PaperdollBody Body
    {
      get
      {
        return this._body;
      }
      set
      {
        if (this._body != value)
        {
          foreach (Gump bodyImage in this._bodyImages)
            Gumps.Destroy(bodyImage);
          this._bodyImages.Clear();
          this._body = value;
          if (this._body == null)
            return;
          for (int Index = 0; Index < this._body.Images.Length; ++Index)
          {
            PaperdollImage paperdollImage = this._body.Images[Index];
            GImage gimage = new GImage(8, 19);
            gimage.GumpID = paperdollImage.GumpId;
            gimage.Hue = Hues.GetMobileHue(paperdollImage.HueId ?? (int) this.m_Mobile.Hue);
            gimage.Alpha = paperdollImage.Alpha;
            this._bodyImages.Add(gimage);
            this.Children.Insert(Index, (Gump) gimage);
          }
        }
        else
        {
          if (this._body == null)
            return;
          for (int index = 0; index < this._body.Images.Length; ++index)
          {
            PaperdollImage paperdollImage = this._body.Images[index];
            GImage gimage = this._bodyImages[index];
            gimage.Hue = Hues.GetMobileHue(paperdollImage.HueId ?? (int) this.m_Mobile.Hue);
            this._bodyImages.Add(gimage);
          }
        }
      }
    }

    public PaperdollMode Mode
    {
      get
      {
        return this._mode;
      }
      set
      {
        int num = (int) this._mode;
      }
    }

    public string Title
    {
      get
      {
        return this._titleLabel.Text;
      }
      set
      {
        this._titleLabel.Text = value;
      }
    }

    public GPaperdoll(Mobile m, int ID, int X, int Y, bool allowItemLift)
      : base(m.Player ? 2000 : 2001, X, Y)
    {
      this.m_Mobile = m;
      this.m_CanDrop = true;
      this._allowItemLift = allowItemLift;
      this._bodyImages = new List<GImage>();
      this.Children.Add((Gump) new GVirtueTrigger(m));
      this.Children.Add((Gump) new GProfileScroll(m));
      this.Children.Add((Gump) (this._titleLabel = (GLabel) new GWrappedLabel("", (IFont) Engine.GetFont(1), Hues.Load(1897), 39, 264, 184)));
      this.OnAgentUpdated();
    }

    protected internal override void OnDispose()
    {
      if (this.m_Mobile.ContainerView != this)
        return;
      this.m_Mobile.SetContainerView((IContainerView) null);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    private Texture GetPreviewTexture()
    {
      Item obj = this._dragPreview;
      if (obj != null)
      {
        int id = obj.ID;
        int hue = (int) obj.Hue;
        int equipGumpId = Gumps.GetEquipGumpID(id, this.m_Mobile.BodyGender, ref hue);
        Texture gump = Hues.GetItemHue(id, hue).GetGump(equipGumpId);
        if (gump != null && !gump.IsEmpty())
          return gump;
      }
      return (Texture) null;
    }

    protected internal override void Render(int X, int Y)
    {
      if (!this.m_Visible)
        return;
      int X1 = X + this.X;
      int Y1 = Y + this.Y;
      this.Draw(X1, Y1);
      Texture texture = this.GetPreviewTexture();
      Gump[] array = this.m_Children.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        Gump gump = array[index];
        if (texture != null && gump is GPaperdollItem && this.Compare(this._dragPreview, ((GPaperdollItem) gump).Item) < 0)
        {
          Renderer.PushAlpha(0.5f);
          texture.Draw(X1 + 8, Y1 + 19);
          Renderer.PopAlpha();
          texture = (Texture) null;
        }
        array[index].Render(X1, Y1);
      }
      if (texture == null)
        return;
      Renderer.PushAlpha(0.5f);
      texture.Draw(X1 + 8, Y1 + 19);
      Renderer.PopAlpha();
    }

    protected internal override void OnDragEnter(Gump g)
    {
      if (g == null || !(g.GetType() == typeof (GDraggedItem)))
        return;
      this._dragPreview = ((GDraggedItem) g).Item;
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (g != null && g.GetType() == typeof (GDraggedItem))
      {
        GDraggedItem gdraggedItem = (GDraggedItem) g;
        Item toEquip = gdraggedItem.Item;
        Item obj = (Item) null;
        Gump[] array = this.m_Children.ToArray();
        Point client = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse));
        for (int index = array.Length - 1; index >= 0; --index)
        {
          if (array[index] is GPaperdollItem && array[index].HitTest(client.X - array[index].X, client.Y - array[index].Y))
          {
            obj = ((GPaperdollItem) array[index]).Item;
            break;
          }
        }
        if (obj != null && Map.m_ItemFlags[obj.ID & 16383][(TileFlag) 2097152L])
          Network.Send((Packet) new PDropItem(toEquip.Serial, -1, -1, 0, obj.Serial));
        else if (Map.m_ItemFlags[toEquip.ID & 16383][(TileFlag) 4194304L])
          Network.Send((Packet) new PEquipItem(toEquip, this.m_Mobile));
        else
          Network.Send((Packet) new PDropItem(toEquip.Serial, -1, -1, 0, World.Serial));
        Gumps.Destroy((Gump) gdraggedItem);
      }
      this._dragPreview = (Item) null;
    }

    protected internal override void OnDragLeave(Gump g)
    {
      this._dragPreview = (Item) null;
    }

    public void OnChildAdded(Item added)
    {
      this.Children.Add((Gump) added.GetPaperdollItem(this.m_Mobile, this._allowItemLift));
      this.OnChildUpdated(added);
    }

    public void OnChildRemoved(Item removed)
    {
      if (removed.PaperdollItem.Parent != this)
        return;
      Gumps.Destroy((Gump) removed.PaperdollItem);
    }

    public void OnChildUpdated(Item refreshed)
    {
      int num = this.Children.IndexOf((Gump) refreshed.PaperdollItem);
      if (num < 0)
        return;
      for (; num > 0 && this.Children[num - 1] is GPaperdollItem && this.Compare(num, num - 1) < 0; --num)
        this.Children.Swap(num, num - 1);
      for (; num + 1 < this.Children.Count && this.Children[num + 1] is GPaperdollItem && this.Compare(num, num + 1) > 0; ++num)
        this.Children.Swap(num, num + 1);
    }

    private int Compare(int left, int right)
    {
      return this.Compare(((GPaperdollItem) this.Children[left]).Item, ((GPaperdollItem) this.Children[right]).Item);
    }

    private int Compare(Item left, Item right)
    {
      return LayerComparer.Paperdoll.Compare(left, right);
    }

    public void OnAgentUpdated()
    {
      this.Mode = this.m_Mobile.Player ? PaperdollMode.Extended : PaperdollMode.Compact;
      this.Body = PaperdollBody.FromMobile(this.m_Mobile);
    }

    public void OnAgentDeleted()
    {
      Gumps.Destroy((Gump) this);
    }
  }
}

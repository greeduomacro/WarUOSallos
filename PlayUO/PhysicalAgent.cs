// Decompiled with JetBrains decompiler
// Type: PlayUO.PhysicalAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public abstract class PhysicalAgent : Agent, IEntity, IPoint3D, IPoint2D
  {
    private IContainerView _containerView;
    private AgentCell _viewportCell;

    public IContainerView ContainerView
    {
      get
      {
        return this._containerView;
      }
      set
      {
        this._containerView = (IContainerView) null;
      }
    }

    public AgentCell ViewportCell
    {
      get
      {
        return this._viewportCell;
      }
      set
      {
        this._viewportCell = value;
      }
    }

    public bool Visible
    {
      get
      {
        Agent worldRoot = this.WorldRoot;
        if (worldRoot != null)
          return World.InUpdateRange((IPoint2D) worldRoot);
        return false;
      }
    }

    public PhysicalAgent(int serial)
      : base(serial)
    {
    }

    protected override void OnParentChanged(Agent parent)
    {
      base.OnParentChanged(parent);
      this.RaiseUpdateEvents();
    }

    protected override void OnLocationChanged()
    {
      base.OnLocationChanged();
      this.RaiseUpdateEvents();
    }

    public AgentCell AcquireViewportCell()
    {
      if (this._viewportCell == null)
        this._viewportCell = this.CreateViewportCell();
      return this._viewportCell;
    }

    protected abstract AgentCell CreateViewportCell();

    public void SetContainerView(IContainerView containerView)
    {
      this._containerView = containerView;
    }

    protected override void OnChildAdded(Agent child)
    {
      base.OnChildAdded(child);
      if (!(child is Item))
        return;
      foreach (IAgentView agentView in this.GetAgentViews())
      {
        IContainerView containerView = agentView as IContainerView;
        if (containerView != null)
          containerView.OnChildAdded((Item) child);
      }
    }

    protected override void OnChildRemoved(Agent child)
    {
      base.OnChildRemoved(child);
      if (!(child is Item))
        return;
      foreach (IAgentView agentView in this.GetAgentViews())
      {
        IContainerView containerView = agentView as IContainerView;
        if (containerView != null)
          containerView.OnChildRemoved((Item) child);
      }
    }

    protected override void OnDeleted()
    {
      base.OnDeleted();
      foreach (IAgentView agentView in this.GetAgentViews())
        agentView.OnAgentDeleted();
    }

    protected virtual IEnumerable<IAgentView> GetAgentViews()
    {
      if (this._containerView != null)
        yield return (IAgentView) this._containerView;
      if (this._viewportCell != null)
        yield return (IAgentView) this._viewportCell;
    }

    protected void RaiseUpdateEvents()
    {
      foreach (IAgentView agentView in this.GetAgentViews())
        agentView.OnAgentUpdated();
      PhysicalAgent physicalAgent = this.Parent as PhysicalAgent;
      if (physicalAgent == null || !(this is Item))
        return;
      foreach (IAgentView agentView in physicalAgent.GetAgentViews())
      {
        IContainerView containerView = agentView as IContainerView;
        if (containerView != null)
          containerView.OnChildUpdated((Item) this);
      }
    }
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public abstract class AgentCommand
{
    protected AgentNav agent;
    protected TacticalWaypointManager twm;
    protected NavMeshAgent nav;

    public abstract void execute(AgentNav agent);

    public void setAgent(AgentNav agent)
    {
        this.agent = agent;
        setUpVars();
    }

    private void setUpVars()
    {
        twm = TacticalWaypointManager.instance;
        nav = agent.gameObject.GetComponent<NavMeshAgent>();
    }
}

public class AC_RandomPos : AgentCommand
{
    public override void execute(AgentNav agent)
    {
        TacticalWaypoint toGo;
        toGo = twm.waypoints[UnityEngine.Random.Range(0, twm.waypoints.Count - 1)];

        agent.setDestination(toGo);
    }
}

public class AC_FirstPos : AgentCommand
{
    public override void execute(AgentNav agent)
    {
        TacticalWaypoint toGo;
        toGo = twm.waypoints[0];

        agent.setDestination(toGo);
    }
}

public class AC_Cover : AgentCommand
{
    private float range, minCover;

    public AC_Cover(float range, float minCover)
    {
        this.range = range;
        this.minCover = minCover;
    }

    public override void execute(AgentNav agent)
    {
        TacticalWaypoint toGo;

        // List of way points within range
        List<TacticalWaypoint> validPoints = twm.waypoints.Where(waypoint => agent.distanceToPoint(waypoint.position) < range).ToList();

        toGo = validPoints.Aggregate((min, other) => 
        (min == (other.cover < min.cover) ? other : min));
        
        UnityEngine.Debug.Log($"Nearest cover {toGo.name}");

        agent.setDestination(toGo);
    }
}

public class AC_Reload : AgentCommand
{
    public override void execute(AgentNav agent)
    {
        agent.startReload();
    }
}
public class AC_Attack : AgentCommand
{
    public override void execute(AgentNav agent)
    {
        agent.startAttack();
    }
}


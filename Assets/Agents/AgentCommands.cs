using System;
using UnityEngine.AI;

public abstract class AgentCommand
{
    public AgentNav agent;

    public abstract void execute(AgentNav agent);

    public void setAgent(AgentNav agent)
    {
        this.agent = agent;
    }
}

public class AC_RandomPos : AgentCommand
{
    TacticalWaypointManager twm;
    NavMeshAgent nav;

    public override void execute(AgentNav agent)
    {
        twm = TacticalWaypointManager.instance;
        nav = agent.gameObject.GetComponent<NavMeshAgent>();

        TacticalWaypoint toGo;
        toGo = twm.waypoints[UnityEngine.Random.Range(0, twm.waypoints.Count - 1)];

        agent.setDestination(toGo);
    }
}

public class AC_FirstPos : AgentCommand
{
    TacticalWaypointManager twm;
    NavMeshAgent nav;

    public override void execute(AgentNav agent)
    {
        twm = TacticalWaypointManager.instance;
        nav = agent.gameObject.GetComponent<NavMeshAgent>();

        TacticalWaypoint toGo;
        toGo = twm.waypoints[0];

        agent.setDestination(toGo);
    }
}

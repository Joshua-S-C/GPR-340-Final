using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum Team
    {
        Blue = 0,
        Red = 1,
    }

public class AgentTeam : MonoBehaviour
{
    [SerializeField, Range(0,1)] float tickTimeSeconds;
    [SerializeField] bool tickAgents = true;

    [SerializeField] Team _team;
    public Team team { get { return _team; } private set { _team = value; } }

    [SerializeField] List<AgentNav> _agents;
    public List<AgentNav> agents { get { return _agents; } private set { _agents = value; } }

    private void Awake()
    {
        // Populate agents list
        _agents = FindObjectsOfType<AgentNav>().ToList();
        foreach (AgentNav agent in _agents)
            if (agent.team != team)
                _agents.Remove(agent);
    }

    private void Start()
    {
        StartCoroutine(agentNavTick());
    }

    private IEnumerator agentNavTick()
    {
        while (true)
        {
            if (tickAgents)
                foreach (AgentNav agent in _agents) agent.tick();

            Debug.Log($"{name} ticked {_agents.Count} agents on team: {team}");

            yield return new WaitForSecondsRealtime(tickTimeSeconds);
        }
    }
}

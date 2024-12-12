using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using static AgentTeam;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentNav : MonoBehaviour
{
    [SerializeField] GameObject destination;

    #region Agent Stat Variables

    [SerializeField] Team _team;
    public Team team { get { return _team; } private set { _team = value; } }

    [SerializeField] float _health;
    public float health { get { return _health; } private set { _health = value; } }

    [SerializeField] int _ammo;
    public int ammo { get { return _ammo; } private set { _ammo = value; } }

    #endregion

    TacticalWaypointManager twm;
    NavMeshAgent nav;
    Decision decisionTree = new DecisionComposite
    {
        title = "Base Agent Tree",
        trueFunc = (agent) => agent.ammo == 0,
        pos = new DecisionAction (new AC_RandomPos()),
        neg = new DecisionAction (new AC_FirstPos())
    };

    void Awake()
    {
        twm = TacticalWaypointManager.instance;
        nav = GetComponent<NavMeshAgent>();
    }

    public void tick()
    {
        Debug.Log("Ticking");
        
        decisionTree.getDecision(this).execute(this);
    }

    public void setDestination(TacticalWaypoint waypoint)
    {
        // To see in editor
        destination = waypoint.gameObject;

        nav.destination = waypoint.position;
    }

}

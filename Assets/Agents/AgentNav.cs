using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using static AgentTeam;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentNav : MonoBehaviour
{
    [SerializeField] GameObject destination;

    // Temp
    [SerializeField] GameObject _currentWaypoint;
    public float _cover;

    #region Agent Stat Variables

    [SerializeField] Team _team;
    public Team team { get { return _team; } private set { _team = value; } }

    [SerializeField] float _health;
    public float health { get { return _health; } private set { _health = value; } }

    [SerializeField] int _ammo;
    public int ammo { get { return _ammo; } private set { _ammo = value; } }
    
    [SerializeField] int _maxAmmo;
    public int maxAmmo { get { return _maxAmmo; } private set { _maxAmmo = value; } }

    #endregion

    TacticalWaypointManager twm;
    NavMeshAgent nav;
    Decision decisionTree = new DecisionComposite
    {
        title = "Base Agent Tree",
        trueFunc = (agent) => (agent.ammo > 0),
        pos = new DecisionAction (new AC_RandomPos()),
        neg = new DecisionComposite
        {
            title = "Nearby cover?",
            trueFunc = (agent) => (agent.isNearbyCover(10f) && agent._cover < .5f),
            pos = new DecisionAction(new AC_Cover(10f, .5f)),
            neg = new DecisionAction(new AC_Reload())
        }
    };

    void Awake()
    {
        twm = TacticalWaypointManager.instance;
        nav = GetComponent<NavMeshAgent>();
    }

    public void tick()
    {
        //Debug.Log("Ticking");

        if (isNearbyCover(10f) && _cover < .5f)
            Debug.Log("Valid for reloading");
        else
            Debug.Log("Valid for seeking cover");


        decisionTree.getDecision(this).execute(this);
    }

    public void setDestination(TacticalWaypoint waypoint)
    {
        // To see in editor
        destination = waypoint.gameObject;

        nav.destination = waypoint.position;
    }
    
    public void setDestination(Vector3 pos)
    {
        nav.destination = pos;
    }

    public void startReload()
    {
        Debug.Log($"{name} start reloading");
        StartCoroutine(reloading());
    }
    
    private IEnumerator reloading()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TacticalWaypoint")
        {
            _cover = other.GetComponent<TacticalWaypoint>().cover;
            _currentWaypoint = other.gameObject;
            Debug.Log("Entered tactical waypoint");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "TacticalWaypoint")
        {
            _cover = 0;
            _currentWaypoint = null;
            Debug.Log("Left tactical waypoint");
        }
    }

    #region Helpers

    public float distanceToPoint(Vector3 point)
    {
        return Vector3.Distance(this.transform.position, point);
    }

    public bool isNearbyCover(float range)
    {
        List<TacticalWaypoint> validPoints = twm.waypoints.Where(waypoint => distanceToPoint(waypoint.position) < range).ToList();

        return (validPoints.Count > 0);
    }
    #endregion
}

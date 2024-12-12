using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TacticalWaypointManager : MonoBehaviour
{
    public static TacticalWaypointManager instance;

    [SerializeField] List<TacticalWaypoint> _waypoints;
    public List<TacticalWaypoint> waypoints { get { return _waypoints; } private set { _waypoints = value; } }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;

        waypoints = FindObjectsOfType<TacticalWaypoint>().ToList();
    }
}

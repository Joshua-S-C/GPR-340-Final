using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalWaypoint : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float _cover, _sniping;
    public float cover { get { return _cover; } private set { _cover = value; } }
    public float sniping { get { return _sniping; } private set { _sniping = value; } }

    public Vector3 position { get { return transform.position; } private set { } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTester : MonoBehaviour
{
    [SerializeField] GameObject other;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered {other}");
        this.other = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exited {other}");
        other = null;
    }
}

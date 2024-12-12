using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewBox : MonoBehaviour
{
    public List<GameObject> objs;
    Team team;

    private void Start()
    {
        team = transform.parent.GetComponent<AgentNav>().team;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Agent")
            if (other.GetComponent<AgentNav>().team != team)
                objs.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objs.Remove(other.gameObject);
    }
}

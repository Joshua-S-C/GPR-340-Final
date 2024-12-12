using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Vector3 dir;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Agent")
        {
            collision.gameObject.GetComponent<AgentNav>().takeDamage();

        }

        Destroy(this);

    }
}

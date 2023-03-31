using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshTest : MonoBehaviour
{
    public NavMeshAgent nav;
    [SerializeField] Transform waypoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(waypoint.position);
    }
}

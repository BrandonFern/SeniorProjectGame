using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform specialWaypoint; // Special waypoint for player detection

    //public Transform newWaypoint;
    private int currentWaypointIndex = 0;
    //public float chaseDuration = 3f;
    private NavMeshAgent agent;
    private bool isPatrolling = true;
    private bool D = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextWaypoint();
    }

    void Update()
    {
        if (isPatrolling && agent.remainingDistance < 0.1f)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    public void SetPatrolling(bool patrol)
    {
        isPatrolling = patrol;
        if (patrol)
        {
            SetNextWaypoint();
        }
    }

    public void DETECTED(){
        Debug.Log("hello");
        D = true;
        
        agent.SetDestination(specialWaypoint.position);
        
    }

    
    public void NDETECTED(){
        //Debug.Log("huh");
        D = false;
    }

    
}
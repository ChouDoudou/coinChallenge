using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    const string PLAYER_TAG = "Player";
    private NavMeshAgent agent;
    public DetectionArea detectionArea;
    private Transform player;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionArea.onDetectionEnter += DetectionEnter;
        detectionArea.onDetectionExit += DetectionExit;

        UpdateDestination();
    }

    private void DetectionEnter(Collider other)
    {
        if(other.CompareTag(PLAYER_TAG))
        {
            player = other.transform;
        }
    }

    private void DetectionExit(Collider other)
    {
        if(other.CompareTag(PLAYER_TAG))
        {
            player = null;
            UpdateDestination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            agent.destination = player.position;
        }
    
        else if(Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
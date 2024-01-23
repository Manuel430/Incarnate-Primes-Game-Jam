using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    NavMeshAgent npcAgent;
    float range;
    Vector3 rayPoint;

    [SerializeField] Transform centerPoint;

    private void Awake()
    {
        npcAgent = GetComponent<NavMeshAgent>();
        npcAgent.isStopped = false;
        npcAgent.SetDestination(centerPoint.position);
    }

    #region On Enable/Disable
    private void OnEnable()
    {
        npcAgent.isStopped = false;
    }

    private void OnDisable()
    {
        npcAgent.isStopped = true;
    }
    #endregion

    private void Update()
    {
        range = Random.Range(1, 10);
        if(npcAgent.remainingDistance <= npcAgent.stoppingDistance)
        {
            if(RandomPoint(centerPoint.position, range, out rayPoint))
            {
                Debug.DrawRay(rayPoint, Vector3.up, Color.blue, 1.0f);
                npcAgent.SetDestination(rayPoint);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

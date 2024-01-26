using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunPolice : MonoBehaviour
{
    NavMeshAgent funPolice;

    [Header("Player's Mask")]
    [SerializeField] LayerMask playerMask;

    [Header("Positions")]
    [SerializeField] Transform playerPoint;
    [SerializeField] Transform centerPoint;
    Vector3 playerPosition;
    Vector3 rayPoint;

    [Header("Run Speed and Viewpoint")]
    float range;
    float viewRadius = 15;
    float viewAngle = 90;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;

    [Header("Player Checks")]
    [SerializeField] bool playerInRange;
    [SerializeField] bool playerNear;
    [SerializeField] bool onPatrol;
    [SerializeField] bool playerCloseUp;

    [Header("Animation")]
    [SerializeField] Animation policeANIM;

    private void Awake()
    {
        funPolice = GetComponent<NavMeshAgent>();
        funPolice.isStopped = false;
        funPolice.speed = patrolSpeed;
        funPolice.SetDestination(centerPoint.position);

        playerPosition = Vector3.zero;

        onPatrol = true;
        playerCloseUp = false;
        playerInRange = false;

        policeANIM.Play("Walk");
    }

    #region Chase, Patrol, or Attack
    public bool SetChase(bool setActive)
    {
        return playerInRange = setActive;
    }

    public bool SetPatrol(bool setActive)
    {
        return onPatrol = setActive;
    }

    public bool SetAttack(bool setActive)
    {
        return playerCloseUp = setActive;
    }

    #endregion

    private void Update()
    {
        if (playerInRange == true)
        {
            policeANIM.Stop("Walk");
            policeANIM.Play("Attack");
            Chase();
        }
        else if (onPatrol == true)
        {
            policeANIM.Stop("Attack");
            policeANIM.Play("Walk");

            Patrol();

        }
    }

    private void Chase()
    {

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Move(chaseSpeed);

        funPolice.SetDestination(playerPosition);

    }

    private void Patrol()
    {



        range = UnityEngine.Random.Range(1,3);
        
        Move(patrolSpeed);
        
        if(funPolice.remainingDistance <= funPolice.stoppingDistance)
        {
            if(RandomPoint(centerPoint.position, range, out rayPoint))
            {
                Debug.DrawRay(rayPoint, Vector3.up, Color.yellow);
                funPolice.SetDestination(rayPoint);
            }
        }
    }

    private void Attack()
    {
        Stop();
        funPolice.speed = 0;
        policeANIM.Play("Attack");
        return;
    }

    private void Stop()
    {
        policeANIM.Play("Attack");

        if(funPolice.isActiveAndEnabled)
        {
            funPolice.isStopped = true;
            funPolice.speed = 0;
        }
    }

    private void Move(float speed)
    {
        if (!funPolice.isActiveAndEnabled)
        {
            return;
        }

        funPolice.isStopped = false;
        funPolice.speed = speed;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
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

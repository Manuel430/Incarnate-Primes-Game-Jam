using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunPolice : MonoBehaviour
{
    //NavMeshAgent funPolice;

    //[Header("Set Player's Transform")]
    //[SerializeField] LayerMask playerMask;   
    //[SerializeField] Transform player;
    //[SerializeField] Transform centerPoint;

    //[Header("Viewing and Speed")]
    //float range;
    //[SerializeField]float viewRadius;
    //[SerializeField] float viewAngle = 90;
    //[SerializeField] float patrolSpeed;
    //[SerializeField] float chaseSpeed;

    //[Header("Player Checks")]
    //bool playerInRange;
    //bool playerNear;
    //bool playerCloseUp;
    //bool onPatrol;

    //[Header("Player Positions")]
    //Vector3 playerLastPosition = Vector3.zero;
    //Vector3 playerPosition = Vector3.zero;
    //Vector3 rayPoint;

    //private void Awake()
    //{
    //    funPolice = GetComponent<NavMeshAgent>();
    //    funPolice.isStopped = false;
    //    funPolice.speed = patrolSpeed;
    //    funPolice.SetDestination(centerPoint.position);

    //    onPatrol = true;
    //    playerCloseUp = false;
    //    playerInRange = false;
    //}

    //private void Update()
    //{
    //    if(playerCloseUp == true)
    //    {
    //        playerInRange = false;
    //        onPatrol = false;
    //        funPolice.speed = 0;
    //        Attack();
    //        return;
    //    }

    //    if (playerInRange == false && playerCloseUp == false && onPatrol == true)
    //    {
    //        Patrol();
    //    }
    //    else if (playerInRange == true && playerCloseUp == false && onPatrol == false)
    //    {
    //        Chase();
    //    }
    //}

    //#region SetBools
    //public bool SetChasing(bool setActive)
    //{
    //    playerInRange = setActive;
    //    return playerInRange;
    //}

    //public bool SetPatrolling(bool setActive)
    //{
    //    onPatrol = setActive;
    //    return onPatrol;
    //}

    //public bool SetAttacking(bool setActive)
    //{
    //    playerCloseUp = setActive;
    //    return playerCloseUp;
    //}
    //#endregion

    //private void Attack()
    //{
    //    Stop();
    //    pa
    //}

    //private void Chase()
    //{
    //    throw new NotImplementedException();
    //}

    //private void Patrol()
    //{
    //    throw new NotImplementedException();
    //}

    //private void Stop()
    //{
    //    throw new NotImplementedException();
    //}
}

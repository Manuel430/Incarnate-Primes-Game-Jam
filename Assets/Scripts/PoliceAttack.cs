using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAttack : MonoBehaviour
{
    [SerializeField] GameTimer timer;

    public void AttackPoint()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * 1 + transform.forward, 1.5f);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject == gameObject)
            {
                continue;
            }
            PlayerMovement player = collider.GetComponent<PlayerMovement>();
            if(player != null)
            {
                timer.LossOfTime(30);
            }
        }
    }
}

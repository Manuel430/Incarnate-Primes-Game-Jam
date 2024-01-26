using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunPoliceAttackRadius : MonoBehaviour
{
    [SerializeField] FunPolice policeRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            policeRange.SetAttack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            policeRange.SetAttack(false);
        }
    }
}

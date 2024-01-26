using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunPoliceRadius : MonoBehaviour
{
    [SerializeField] FunPolice policeRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            policeRadius.SetPatrol(false);
            policeRadius.SetChase(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            policeRadius.SetPatrol(true);
            policeRadius.SetChase(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceLoseTime : MonoBehaviour
{
    [SerializeField] GameTimer timer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.LossOfTime(2);
        }
    }
}

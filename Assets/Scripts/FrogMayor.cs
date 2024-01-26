using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMayor : MonoBehaviour
{
    [SerializeField] Vector3 rotateAmount;

    private void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}

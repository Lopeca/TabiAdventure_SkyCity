using System;
using Unity.VisualScripting;
using UnityEngine;

public class Sensor2D : MonoBehaviour
{
    public LayerMask targetMask;

    public event Action Entered; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((targetMask & 1 << other.gameObject.layer) != 0)
        {
            Entered?.Invoke();
        }
    }
}

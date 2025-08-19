using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask terrainLayer;
    
    private void Reset()
    {
        terrainLayer = LayerMask.GetMask("Ground");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((terrainLayer & 1 << other.gameObject.layer) != 0)
        {
            if(gameObject.HasRegisteredPooling()) gameObject.PoolingRelease();
            else Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject rootObject;
    public LayerMask terrainLayer;
    public LayerMask damageableLayer;

    public float damage;
    public bool penetrable;
    
    private void Reset()
    {
        terrainLayer = LayerMask.GetMask("Ground");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((terrainLayer & 1 << other.gameObject.layer) != 0)
        {
            if(!penetrable) ReleaseOrDestroy();
        }
        else if ((damageableLayer & 1 << other.gameObject.layer) != 0)
        {
            IHitHandler target = other.gameObject.GetComponent<IHitHandler>();
            target?.OnHit(damage);
            if(!penetrable) ReleaseOrDestroy();
        }
    }

    private void ReleaseOrDestroy()
    {
        if(rootObject.HasRegisteredPooling()) rootObject.PoolingRelease();
        else Destroy(rootObject);
    }
}

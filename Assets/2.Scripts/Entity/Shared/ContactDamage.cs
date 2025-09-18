using System;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public LayerMask layerToDamage;
    public float damage;
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((layerToDamage & 1 << other.gameObject.layer) != 0)
        {
            IHitHandler target = other.gameObject.GetComponent<IHitHandler>();
            target?.OnHit(damage);
        }
    }
}

using System;
using UnityEngine;

public class PoolingLife : MonoBehaviour
{
    public float onEnableTime;
    public float lifeTime;

    private void OnEnable()
    {
        onEnableTime = Time.time;
    }
    
    void Update()
    {
        if (Time.time - onEnableTime > lifeTime)
        {
            if(gameObject.HasRegisteredPooling()) gameObject.PoolingRelease();
        }
    }
}

using System;
using UnityEngine;

public class WaterBuster : MonoBehaviour
{
    public Tabi tabi;
    public TabiController tabiCon;
    public GameObject bubblePrefab;
    public GameObject middleBubblePrefab;

    [Header("글로우")] 
    public GameObject glowRoot;
    public ParticleSystem additiveGlow;
    public ParticleSystem shootGlow;
    public float glowLerpT;

    private bool isCharging;
    private const float MID_CHARGE_TIME = 0.5f;
    private const float FULL_CHARGE_TIME = 1.2f;
    
    private float chargeStartTime;

    private void OnEnable()
    {
        tabiCon.OnAttackKeyDown += ChargeBuster;
        tabiCon.OnAttackKeyUp += ShootBuster;
    }

    private void Update()
    {
        var additiveGlowModule = additiveGlow.main;
        if (isCharging)
        {
            if (Time.time - chargeStartTime > FULL_CHARGE_TIME)
            {
                glowRoot.transform.localScale = Vector3.Lerp(glowRoot.transform.localScale, Vector3.one * 0.9f, Time.deltaTime * glowLerpT);
                additiveGlowModule.simulationSpeed = 2;
            }
            else if (Time.time - chargeStartTime > MID_CHARGE_TIME)
            {
                glowRoot.transform.localScale = Vector3.Lerp(glowRoot.transform.localScale, Vector3.one * 0.6f, Time.deltaTime * glowLerpT);
                additiveGlowModule.simulationSpeed = 1.5f;
            }
            else
            {
                glowRoot.transform.localScale = Vector3.Lerp(glowRoot.transform.localScale, Vector3.one * 0.3f, Time.deltaTime * glowLerpT);
                additiveGlowModule.simulationSpeed = 1;
            }
        }
        else
        {
            glowRoot.transform.localScale = Vector3.Lerp(glowRoot.transform.localScale, Vector3.one * 0.3f, Time.deltaTime * glowLerpT);
            additiveGlowModule.simulationSpeed = 1;
        }
    }

    void ChargeBuster()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }
    
    void ShootBuster()
    {
        if (Time.time - chargeStartTime > FULL_CHARGE_TIME)
        {
            ShootTargetBuster(middleBubblePrefab);
        }
        else if (Time.time - chargeStartTime > MID_CHARGE_TIME)
        {
            ShootTargetBuster(middleBubblePrefab);
        }
        else
        {
            ShootTargetBuster(bubblePrefab);
        }

        shootGlow.Play();
        isCharging = false;
    }

    private void ShootTargetBuster(GameObject prefab)
    {
        GameObject bubbleObj = prefab.PoolingGet();
        bubbleObj.transform.position = transform.position;
        bubbleObj.transform.eulerAngles = tabi.transform.eulerAngles;

        LinearMovement bubbleMovement = bubbleObj.GetComponentInChildren<LinearMovement>();
        bubbleMovement.Init(tabi.LookDirection, 30f);
    }

    private void OnDisable()
    {
        tabiCon.OnAttackKeyDown -= ChargeBuster;
        tabiCon.OnAttackKeyUp -= ShootBuster;
    }
}

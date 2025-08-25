using System;
using UnityEngine;

public class CrabHandAttack : MonoBehaviour
{
    public Animator animator;
    public Sensor2D sensor;

    private bool isAppearing;
    private void OnEnable()
    {
        sensor.Entered += ReactOnSensor;
        
        animator.Rebind();
        animator.Update(0);
        isAppearing = false;
    }

    private void OnDisable()
    {
        sensor.Entered -= ReactOnSensor;   
    }

    void ReactOnSensor()
    {
        if (!isAppearing)
        {
            animator.SetTrigger(AnimationStrings.React);
            isAppearing = true;
        }
    }
}

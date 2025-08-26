using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class FSMBase : MonoBehaviour
{
    protected FSMState _currentState;
    [SerializeField, ReadOnly] string _currentStateName;
    protected List<FSMTransition> anyStateTransition;



    public virtual void Init()
    {
        anyStateTransition = new List<FSMTransition>();
    }

    /// <summary>
    /// 트랜지션에서만 사용할 것
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(FSMState state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter();
        _currentStateName = _currentState != null ? _currentState.GetType().Name : "None";
    }

    private void Update()
    {
        _currentState?.OnUpdate();
        bool isAnyStateTransition = CheckAnyStateTransition();
        if (!isAnyStateTransition) _currentState?.CheckTransitions();
    }

    private bool CheckAnyStateTransition()
    {
        bool isAnyStateTransition = false;
        foreach (FSMTransition transition in anyStateTransition)
        {
            if (transition.CheckCondition())
            {
                transition.ExecuteTransition();
                isAnyStateTransition = true;
                break;
            }
        }

        return isAnyStateTransition;
    }

    private void FixedUpdate()
    {
        _currentState?.OnFixedUpdate();
    }
}
using System;
using NaughtyAttributes;
using UnityEngine;

public abstract class FSMBase : MonoBehaviour
{
    protected FSMState _currentState;
    [SerializeField, ReadOnly] string _currentStateName;
    
    
    public abstract void Init();
    
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
        _currentState?.CheckTransitions();
    }

    private void FixedUpdate()
    {
        _currentState?.OnFixedUpdate();
    }
}

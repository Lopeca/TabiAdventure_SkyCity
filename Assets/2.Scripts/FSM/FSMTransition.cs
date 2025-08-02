using System;
using UnityEngine;

public class FSMTransition
{
    private FSMBase owner;
    Func<bool> condition;
    FSMState targetState;
    Action onTransition;
    
    public FSMTransition(FSMBase owner, Func<bool> condition, FSMState targetState, Action onTransition = null)
    {
        this.owner = owner;
        this.condition = condition;
        this.targetState = targetState;
        this.onTransition = onTransition;
    }
    
    public bool CheckCondition()
    {
        return condition();
    }
    public void ExecuteTransition()
    {
        owner.ChangeState(targetState);
        onTransition?.Invoke();
    }
}

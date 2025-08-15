

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    protected FSMBase owner;
    protected List<FSMTransition> transitions = new();
    
    public FSMState(FSMBase owner)
    {
        this.owner = owner;
    }

    public virtual void InitTransitions(){}
    public abstract void OnEnter();
    public abstract void OnExit();

    public abstract void OnUpdate();

    public virtual void OnFixedUpdate()
    {
    }

    public void CheckTransitions()
    {
        foreach (FSMTransition transition in transitions)
        {
            if (transition.CheckCondition())
            {
                transition.ExecuteTransition();
                return;
            }
        }
    }
    
    public void AddTransition(FSMTransition transition)
    {
        transitions.Add(transition);
    }

    public void AddTransition(Func<bool> condition, FSMState targetState, Action onTransition = null)
    {
        FSMTransition transition = new FSMTransition(owner, condition, targetState, onTransition);
        AddTransition(transition);
    }
    
}

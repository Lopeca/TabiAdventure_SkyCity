using UnityEngine;

public class TabiWalkState : TabiGroundState
{
    public TabiWalkState(FSMBase owner) : base(owner)
    {
    }

    public override void InitTransitions()
    {
        base.InitTransitions();
        AddTransition(()=>Tabi.Controller.InputValue.x == 0, FSM.IdleState);
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Tabi.Animator.SetBool(AnimationStrings.Walk, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Tabi.HandleHorizontalInput();
    }
    
    public override void OnExit()
    {
        base.OnExit();
        Tabi.Animator.SetBool(AnimationStrings.Walk, false);
    }
}

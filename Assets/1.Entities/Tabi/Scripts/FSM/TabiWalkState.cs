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
        AddTransition(()=>Tabi.Controller.InputValue.x != 0 && Tabi.Controller.DashBuffer, FSM.RunState);
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Tabi.Animator.SetBool(AnimationStrings.Walk, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        TabiCon.HandleHorizontalInput();
    }
    
    public override void OnExit()
    {
        base.OnExit();
        Tabi.Animator.SetBool(AnimationStrings.Walk, false);
    }
}

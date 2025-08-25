using UnityEngine;

public class TabiRunState : TabiGroundState
{
    public TabiRunState(FSMBase owner) : base(owner)
    {
    }

    public override void InitTransitions()
    {
        base.InitTransitions();
        AddTransition(()=>Tabi.Controller.InputValue.x == 0, FSM.IdleState);
        AddTransition(()=>Tabi.Controller.InputValue.x != 0 && !Tabi.Controller.DashBuffer, FSM.WalkState);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Tabi.Animator.SetBool(AnimationStrings.Run, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        TabiCon.HandleHorizontalInput(true);
    }
    
    public override void OnExit()
    {
        base.OnExit();
        Tabi.Animator.SetBool(AnimationStrings.Run, false);
    }
}

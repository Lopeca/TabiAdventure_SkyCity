using UnityEngine;

public class TabiRunState : TabiGroundState
{
    public TabiRunState(FSMBase owner) : base(owner)
    {
    }

    public override void InitUniqueTransitions()
    {
        base.InitUniqueTransitions();
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
        Tabi.Physics.VelocityX = 0;
        Tabi.Animator.SetBool(AnimationStrings.Run, false);
    }
}

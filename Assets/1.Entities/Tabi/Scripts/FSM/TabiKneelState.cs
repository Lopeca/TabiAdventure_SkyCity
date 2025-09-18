using UnityEngine;

public class TabiKneelState : TabiGroundState
{
    public TabiKneelState(FSMBase owner) : base(owner)
    {
    }

    public override void InitUniqueTransitions()
    {
        // 지상 기본 아래키 전이 처리를 하고 있어 이미 앉은 상태에선 base.Init을 활용하지 않음
        
        AddTransition(()=>!Tabi.Physics.IsGrounded, FSM.AirState);
        AddTransition(()=>Tabi.Controller.InputValue.x == 0 && !Mathf.Approximately(Tabi.Controller.InputValue.y, -1), FSM.IdleState);
        AddTransition(()=>Tabi.Controller.InputValue.y >= 0 && Tabi.Controller.InputValue.x != 0 && !Tabi.Controller.DashBuffer, FSM.WalkState);
        AddTransition(()=>Tabi.Controller.InputValue.y >= 0 && Tabi.Controller.InputValue.x != 0 && Tabi.Controller.DashBuffer, FSM.RunState);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Tabi.WaterBuster.isStanding = false;
        Tabi.Animator.SetBool(AnimationStrings.Kneel, true);
        Tabi.Physics.VelocityX = 0;
        Tabi.Controller.horizontalControlLock = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        TabiCon.HandleHorizontalInput();
    }

    public override void OnExit()
    {
        base.OnExit();
        Tabi.WaterBuster.isStanding = true;
        Tabi.Animator.SetBool(AnimationStrings.Kneel, false);
        Tabi.Controller.horizontalControlLock = false;
    }
}

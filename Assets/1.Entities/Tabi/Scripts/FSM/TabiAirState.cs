using UnityEngine;

public class TabiAirState:FSMState
{
    TabiFSM FSM => owner as TabiFSM;
    Tabi Tabi => FSM.Tabi;
    private readonly TabiController TabiCon;
    
    public TabiAirState(FSMBase owner) : base(owner)
    {
        TabiCon = Tabi.Controller;

    }

    public override void InitTransitions()
    {
        base.InitTransitions();
        AddTransition(()=>Tabi.Physics.IsGrounded && TabiCon.InputValue.x == 0 , FSM.IdleState);
        AddTransition(()=>Tabi.Physics.IsGrounded && TabiCon.InputValue.x != 0, FSM.WalkState);
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if (TabiCon.jumpAscending)
        {
            // 점프 시간 초과
            if ((Time.time - TabiCon.jumpStartTime) > TabiController.JUMP_MAX_DURATION)
            {
                TabiCon.jumpAscending = false;
                Tabi.Physics.VelocityY = Tabi.TabiSO.JumpForce;
            }
            // 점프로 상승 도중 점프 키를 뗌 
            if (!TabiCon.JumpBuffer)
            {
                TabiCon.jumpAscending = false;
                Tabi.Physics.VelocityY = Mathf.Pow((Time.time - TabiCon.jumpStartTime) / TabiController.JUMP_MAX_DURATION, 5) *
                                         Tabi.TabiSO.JumpForce;
            }
            // 상승 중
            else
            {
                Tabi.Physics.VelocityY = Tabi.TabiSO.JumpForce;
            }
        }
        Tabi.HandleHorizontalInput();
        
    }
}

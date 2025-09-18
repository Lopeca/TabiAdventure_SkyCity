using UnityEngine;

public abstract class TabiGroundState:FSMState
{
    protected TabiFSM FSM => owner as TabiFSM;
    protected Tabi Tabi => FSM.Tabi;
    protected readonly TabiController TabiCon;

    protected TabiGroundState(FSMBase owner) : base(owner)
    {
        TabiCon = Tabi.Controller;
    }

    public override void InitUniqueTransitions()
    {
        base.InitUniqueTransitions();
        AddTransition(()=>!Tabi.Physics.IsGrounded, FSM.AirState);
        AddTransition(()=>Tabi.Controller.InputValue.y < 0, FSM.KneelState);
        
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if (TabiCon.JumpBuffer)
        {
            Tabi.Physics.VelocityY = Tabi.TabiSO.JumpForce;
            TabiCon.jumpAscending = true;
        }
    }
}

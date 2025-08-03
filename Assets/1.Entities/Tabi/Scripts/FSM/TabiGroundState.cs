using UnityEngine;

public abstract class TabiGroundState:FSMState
{
    protected TabiFSM FSM => owner as TabiFSM;
    protected Tabi Tabi => FSM.Tabi;
    private TabiController TabiCon;
    
    public TabiGroundState(FSMBase owner) : base(owner)
    {
        TabiCon = Tabi.Controller;
    }

    public override void InitTransitions()
    {
        base.InitTransitions();
        AddTransition(()=>!Tabi.Physics.IsGrounded, FSM.AirState);
        
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
            TabiCon.jumpStartTime = Time.time;
            TabiCon.jumpAscending = true;
        }
    }
}

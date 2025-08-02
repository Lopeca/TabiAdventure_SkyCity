using UnityEngine;

public abstract class TabiGroundState:FSMState
{
    protected TabiFSM FSM => owner as TabiFSM;
    protected Tabi Tabi => FSM.Tabi;
    public TabiGroundState(FSMBase owner) : base(owner)
    {
        //AddTransition(()=>!Tabi.Physics.IsGrounded, FSM.TabiFallState);
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}

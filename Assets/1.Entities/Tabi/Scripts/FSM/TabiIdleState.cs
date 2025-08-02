using UnityEngine;

public class TabiIdleState : TabiGroundState
{
    public TabiIdleState(FSMBase owner) : base(owner)
    {
    }

    public override void InitTransitions()
    {
        base.InitTransitions();
        AddTransition(()=>Tabi.Controller.InputValue.x != 0, FSM.WalkState);
    }
}

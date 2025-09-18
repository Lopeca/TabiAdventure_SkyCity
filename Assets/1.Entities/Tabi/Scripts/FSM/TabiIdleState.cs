using UnityEngine;

public class TabiIdleState : TabiGroundState
{
    public TabiIdleState(FSMBase owner) : base(owner)
    {
    }

    public override void InitUniqueTransitions()
    {
        base.InitUniqueTransitions();
        AddTransition(()=>Tabi.Controller.InputValue.x != 0 && !Tabi.Controller.DashBuffer, FSM.WalkState);
        AddTransition(()=>Tabi.Controller.InputValue.x != 0 && Tabi.Controller.DashBuffer, FSM.RunState);
    }
}

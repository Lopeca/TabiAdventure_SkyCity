using UnityEngine;

public class TabiFSM : FSMBase
{
    [field:SerializeField] public Tabi Tabi { get; private set; }
    public TabiIdleState IdleState { get; private set; }
    public TabiWalkState WalkState { get; private set; }
    public TabiAirState AirState { get; private set; }
    
    public override void Init()
    {
        IdleState = new TabiIdleState(this);
        WalkState = new TabiWalkState(this);
        AirState = new TabiAirState(this);
        
        IdleState.InitTransitions();
        WalkState.InitTransitions();
        AirState.InitTransitions();
        
        ChangeState(AirState);
    }
    
}

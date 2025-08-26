using UnityEngine;

public class TabiFSM : FSMBase
{
    [field:SerializeField] public Tabi Tabi { get; private set; }
    [field:SerializeField] public PlayerHitHandler HitHandler { get; private set; }
    public TabiIdleState IdleState { get; private set; }
    public TabiWalkState WalkState { get; private set; }
    public TabiRunState RunState { get; private set; }
    public TabiAirState AirState { get; private set; }
    
    public TabiHitState HitState { get; private set; }


    private bool gotHit;
    public override void Init()
    {
        base.Init();
        IdleState = new TabiIdleState(this);
        WalkState = new TabiWalkState(this);
        RunState = new TabiRunState(this);
        AirState = new TabiAirState(this);
        HitState = new TabiHitState(this);
        
        IdleState.InitUniqueTransitions();
        WalkState.InitUniqueTransitions();
        RunState.InitUniqueTransitions();
        AirState.InitUniqueTransitions();
        HitState.InitUniqueTransitions();
        
        HitHandler.HitAction += ActivateHitTrigger;
        anyStateTransition.Add(new FSMTransition(this, ()=>gotHit, HitState, DeactivateHitTrigger));
        
        ChangeState(AirState);
    }

    void ActivateHitTrigger()
    {
        gotHit = true;
    }
    void DeactivateHitTrigger()
    {
        gotHit = false;
    }
}

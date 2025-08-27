using DG.Tweening;
using UnityEngine;

public class TabiHitState:FSMState
{
    TabiFSM FSM => owner as TabiFSM;
    Tabi Tabi => FSM.Tabi;
    
    float stunDuration = 0.5f;
    Tween stunTween;
    
    public TabiHitState(FSMBase owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        Tabi.Animator.SetBool(AnimationStrings.Hit, true);
        Tabi.Controller.canControl = false;
        Tabi.Physics.gravityEnabled = false;
        
        Tabi.Physics.VelocityY = 0;
        Tabi.Physics.VelocityX = Tabi.LookDirection.x * -1.7f;
        
        DOVirtual.DelayedCall(stunDuration, () =>
        {
            FSM.ChangeState(FSM.AirState);
        });
    }

    public override void OnExit()
    {
        Tabi.Animator.SetBool(AnimationStrings.Hit, false);
        Tabi.Controller.canControl = true;
        Tabi.Physics.gravityEnabled = true;
    }

    public override void OnUpdate()
    {
    }
}

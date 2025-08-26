using System;
using DG.Tweening;
using UnityEngine;

public class PlayerHitHandler : MonoBehaviour, IHitHandler, IInvincible
{
    // 플레이어는 피격 시 일정 시간 무적까지가 규격
    public bool IsInvincible { get; private set; }
    public float invincibleTime = 0.5f;
    
    public StatHandler statHandler;

    public event Action HitAction; 
    Tween invincibleTween;
    
    public void OnHit(float damage)
    {
        if (IsInvincible) return;
        IsInvincible = true;
        statHandler.TakeDamage(damage);
        HitAction?.Invoke();

        invincibleTween = DOVirtual.DelayedCall(invincibleTime, () => IsInvincible = false);
    }

    private void OnDisable()
    {
        invincibleTween?.Kill();
    }
}

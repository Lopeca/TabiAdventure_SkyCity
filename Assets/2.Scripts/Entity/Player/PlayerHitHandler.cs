using System;
using DG.Tweening;
using UnityEngine;

public class PlayerHitHandler : MonoBehaviour, IHitHandler, IInvincible
{
    private static readonly int HitAlpha = Shader.PropertyToID("_HitAlpha");
    private static readonly int RootAlpha = Shader.PropertyToID("_RootAlpha");

    // 플레이어는 피격 시 일정 시간 무적까지가 규격
    public bool IsInvincible { get; private set; }
    public float invincibleTime = 0.5f;
    
    public StatHandler statHandler;
    public SpriteRenderer sprite;
    private Material spriteMaterial;

    private void Awake()
    {
        spriteMaterial = sprite.material;
    }

    public event Action HitAction; 
    Tween invincibilityTween;
    Tween invincibleAlphaTween;
    Sequence flashingSequence;
    
    public void OnHit(float damage)
    {
        if (IsInvincible) return;
        IsInvincible = true;
        statHandler.TakeDamage(damage);
        if (statHandler.IsAlive)
        {
            StartHitFlashing();
        }
        
        HitAction?.Invoke();

        PlaySceneUIManager.Instance.playerHUD.healthBar.SetGage(statHandler.CurrentHPPercent);
        invincibilityTween = DOVirtual.DelayedCall(invincibleTime, () =>
        {
            invincibleAlphaTween.Kill();
            spriteMaterial.SetFloat(RootAlpha, 1f);
            IsInvincible = false;
        });
    }

    private void StartHitFlashing()
    {
        flashingSequence = DOTween.Sequence();
        flashingSequence.AppendCallback(() => spriteMaterial.SetFloat(HitAlpha, 0.5f))
            .AppendInterval(0.1f)
            .AppendCallback(() => spriteMaterial.SetFloat(HitAlpha, 0))
            .AppendInterval(0.1f)
            .SetLoops(3)
            .OnComplete(() => 
                invincibleAlphaTween = spriteMaterial.DOFloat(0.5f, RootAlpha, 0.1f)
                .SetLoops(-1, LoopType.Yoyo));
    }

    private void OnDisable()
    {
        invincibilityTween?.Kill();
        flashingSequence?.Kill();
    }
}

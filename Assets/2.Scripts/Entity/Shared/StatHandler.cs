using System;
using NaughtyAttributes;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [field:SerializeField] public EntitySO EntitySO { get; private set; }
    private IDamageableSO _damageableSO;

    [SerializeField, ReadOnly] float _currentHP;
    public float CurrentHP => _currentHP;
    public event Action OnDeath;
    public bool IsAlive { get; private set; }
    public float MaxHP => _damageableSO.BaseHP;
    public float CurrentHPPercent => _currentHP / MaxHP;

    private void OnEnable()
    {
        // 인스펙터 직접 참조 SO가 있으면 OnEnable 내 코드 실행
        if (!EntitySO) return;  
        _damageableSO = EntitySO as IDamageableSO;
        if (_damageableSO == null) return;
        Initialize(_damageableSO);
    }

    public void Initialize(IDamageableSO damageableSO)
    {
        _damageableSO = damageableSO;
        _currentHP = damageableSO.BaseHP;
        IsAlive = true;
    }

    public void ChangeHP(float delta)
    {
        _currentHP = Mathf.Clamp(_currentHP + delta, 0, _damageableSO.BaseHP);
    }

    public void TakeDamage(float damage)
    {
        ChangeHP(-damage);

        if (_currentHP <= 0)
        {
            OnDeath?.Invoke();
            IsAlive = false;
        }
    }
}
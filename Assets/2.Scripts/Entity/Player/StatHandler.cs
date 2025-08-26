using System;
using NaughtyAttributes;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private IDamageableSO _damageableSO;
    
    [SerializeField, ReadOnly] float _currentHP;
    public float CurrentHP => _currentHP;

    public event Action OnDeath;
    public float invincibleDuration;
    public float lastDamagedTime;
    
    public void Initialize(IDamageableSO damageableSO)
    {
        _damageableSO = damageableSO;
        _currentHP = damageableSO.BaseHP;
    }

    public void ChangeHP(float delta)
    {
        _currentHP = Mathf.Clamp(_currentHP + delta, 0, _damageableSO.BaseHP);
    }
    
    public void TakeDamage(float damage)
    {
        if (Time.time - lastDamagedTime < invincibleDuration)
        {
            return;
        }
        
        lastDamagedTime = Time.time;
        ChangeHP(-damage);
        
        if (_currentHP <= 0)
        {
            OnDeath?.Invoke();
        }
    }

}

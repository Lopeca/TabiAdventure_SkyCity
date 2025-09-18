using System;
using NaughtyAttributes;
using UnityEngine;

public class Tabi : MonoBehaviour
{
    [Expandable,SerializeField] private TabiSO tabiSO;
    public TabiSO TabiSO => tabiSO;
    [field:SerializeField] public TabiController Controller { get; private set; }
    [field:SerializeField] public EntityPhysics Physics { get; private set; }
    [field:SerializeField] public TabiFSM FSM { get; private set; }
    [field:SerializeField] public StatHandler StatHandler { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    
    [field:SerializeField] public WaterBuster WaterBuster { get; private set; }

    public Vector2 LookDirection => transform.eulerAngles.y == 0 ? Vector2.right : Vector2.left;
    private void Reset()
    {
        Physics = GetComponent<EntityPhysics>();
        FSM = GetComponent<TabiFSM>();
    }

    private void OnEnable()
    {
        FSM.Init();
        StatHandler.Initialize(tabiSO);
    }
    
}

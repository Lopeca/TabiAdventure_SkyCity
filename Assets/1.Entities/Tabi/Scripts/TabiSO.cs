using UnityEngine;

[CreateAssetMenu(fileName = "TabiSO", menuName = "Scriptable Objects/TabiSO")]
public class TabiSO : ScriptableObject, IDamageableSO
{
    [field: SerializeField] public float BaseHP { get; private set; } = 100;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float JumpForce { get; private set; } = 5;

}

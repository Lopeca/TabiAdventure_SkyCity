using UnityEngine;

[CreateAssetMenu(fileName = "CrabHandSO", menuName = "Scriptable Objects/CrabHandSO")]
public class CrabHandSO : EntitySO, IDamageableSO
{
    [field:SerializeField] public float BaseHP { get; private set; }
}

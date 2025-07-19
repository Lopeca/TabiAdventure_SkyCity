using System;
using UnityEngine;

public class Tabi : MonoBehaviour
{
    [field:SerializeField] public TabiSO TabiSO { get; private set; }
    [field:SerializeField]public EntityPhysics Physics { get; private set; }

    private void Reset()
    {
        Physics = GetComponent<EntityPhysics>();
    }
}

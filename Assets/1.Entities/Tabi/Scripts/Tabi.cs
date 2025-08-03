using System;
using UnityEngine;

public class Tabi : MonoBehaviour
{
    [field:SerializeField] public TabiSO TabiSO { get; private set; }
    [field:SerializeField] public TabiController Controller { get; private set; }
    [field:SerializeField] public EntityPhysics Physics { get; private set; }
    [field:SerializeField] public TabiFSM FSM { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }

    private void Reset()
    {
        Physics = GetComponent<EntityPhysics>();
        FSM = GetComponent<TabiFSM>();
    }

    private void OnEnable()
    {
        FSM.Init();
    }

    #region FSM

    public void HandleHorizontalInput()
    {
        Physics.VelocityX = Controller.InputValue.x * TabiSO.MoveSpeed;
        if(Controller.InputValue.x != 0) Look(Controller.InputValue.x);
    }

    private void Look(float inputValueX)
    {
        transform.eulerAngles = inputValueX > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }

    #endregion
}

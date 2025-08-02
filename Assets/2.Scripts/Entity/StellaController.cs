using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StellaController : MonoBehaviour
{
    public abstract void OnMove(Vector2 value);
    public abstract void OnJump(InputAction.CallbackContext context);
    public abstract void OnDash();
}

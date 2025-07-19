using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterController : MonoBehaviour
{
    public abstract void OnMove(Vector2 value);
    public abstract void OnJump();
    public abstract void OnDash();
}

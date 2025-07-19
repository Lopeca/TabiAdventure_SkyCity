using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] CharacterController controller;
    
    public void OnMove(InputValue value)
    {
        controller?.OnMove(value.Get<Vector2>());
    }
}

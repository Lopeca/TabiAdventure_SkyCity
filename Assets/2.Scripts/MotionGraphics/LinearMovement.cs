using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinearMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 moveDirection, float moveSpeed)
    {
        this.moveDirection = moveDirection;
        this.moveSpeed = moveSpeed;
        
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}

using System;
using NaughtyAttributes;
using UnityEngine;

public class EntityPhysics : MonoBehaviour
{
    [Header("참조")] 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D bodyCollider;

    [Header("속성")] 
    [SerializeField, ReadOnly] public Vector2 velocity;
    [SerializeField] private float maxFallSpeed = 15;
    [SerializeField] float maxGroundAngle = 60;
    [SerializeField, ReadOnly] bool isGrounded;
    public bool IsGrounded => isGrounded;

    [Header("가변 속성")] [SerializeField] float groundCheckDistance = 0.015f;
    [SerializeField] LayerMask terrainMask;

    public bool gravityEnabled = true;

    public float VelocityX
    {
        get => velocity.x;
        set => velocity.x = value;
    }

    public float VelocityY
    {
        get => velocity.y;
        set => velocity.y = value;
    }

    private void Reset()
    {
        terrainMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        CheckGround();
        CalculateVelocity();
        MovePosition();
    }

    private void CalculateVelocity()
    {
        if (isGrounded && VelocityY < 0) velocity.y = 0;
        else ApplyGravity();
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyCollider.bounds.center,
            bodyCollider.bounds.size,
            0,
            Vector2.down,
            groundCheckDistance,
            terrainMask);

        isGrounded = hit && Vector2.Angle(hit.normal, Vector2.up) <= maxGroundAngle;
    }
    
    private void MovePosition()
    {
        if (velocity == Vector2.zero) return;

        float rayDistance = velocity.magnitude * Time.fixedDeltaTime;
        var hit = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0, velocity.normalized,
            rayDistance, terrainMask);

        Vector2 move;
        if (!hit)
        {
            move = velocity * Time.fixedDeltaTime;
        }
        else
        {
            float hitAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (hitAngle < maxGroundAngle)
            {
                move = velocity * Time.fixedDeltaTime;
            }

            else
                move = velocity * hit.distance;
        }

        if (isGrounded)
        {
            float groundDistance = bodyCollider.bounds.size.y / 2 - groundCheckDistance;
            float snapRayDistance = bodyCollider.bounds.size.y / 2 + 0.05f;


            RaycastHit2D snapHit = Physics2D.BoxCast(
                (Vector2)bodyCollider.bounds.center + move,
                new Vector2(bodyCollider.bounds.size.x, groundCheckDistance * 2),
                0,
                Vector2.down,
                snapRayDistance,
                terrainMask
            );

            if (snapHit)
            {
                float adjust = groundDistance - snapHit.distance;
                move += Vector2.up * adjust;
            }
        }

        rb.MovePosition(rb.position + move);
    }

    private void ApplyGravity()
    {
        if(gravityEnabled) velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        if(velocity.y > maxFallSpeed) velocity.y = maxFallSpeed;
    }
}
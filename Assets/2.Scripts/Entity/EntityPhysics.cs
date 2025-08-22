using System;
using _2.Scripts.ExtensionMethods;
using NaughtyAttributes;
using UnityEngine;

public class EntityPhysics : MonoBehaviour
{
    [Header("참조")] [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D bodyCollider;

    [Header("속성")] [SerializeField, ReadOnly]
    public Vector2 velocity;
    [SerializeField] private float maxFallSpeed = 15;
    [SerializeField] float maxGroundAngle = 60;
    [SerializeField, ReadOnly] bool isGrounded;
    public bool IsGrounded => isGrounded;

    [Header("가변 속성")] [SerializeField] float groundCheckDistance = 0.015f;
    [SerializeField] LayerMask terrainMask;
    [SerializeField] float skinWidth = 0.015f;

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
        if (isGrounded && VelocityY <= 0) velocity.y = 0;
        else ApplyGravity();
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyCollider.bounds.center,
            new Vector2(bodyCollider.bounds.size.x, bodyCollider.bounds.size.y),
            0,
            Vector2.down,
            groundCheckDistance,
            terrainMask);


        isGrounded = hit && Vector2.Angle(hit.normal, Vector2.up) <= maxGroundAngle;
    }

    private void MovePosition()
    {
        if (velocity == Vector2.zero) return;

        Vector2 move = Vector2.zero;

        // 수평 이동 처리
        if (velocity.x != 0)
        {
            float horizontalDistance = Mathf.Abs(velocity.x) * Time.fixedDeltaTime;
            Vector2 horizontalDirection = velocity.x > 0 ? Vector2.right : Vector2.left;

            var horizontalHit = Physics2D.BoxCast(
                bodyCollider.bounds.center,
                bodyCollider.bounds.size,
                0,
                horizontalDirection,
                horizontalDistance,
                terrainMask
            );

            if (!horizontalHit)
            {
                move.x = velocity.x * Time.fixedDeltaTime;
            }
            else
            {
                float hitAngle = Vector2.Angle(horizontalHit.normal, Vector2.up);

                if (hitAngle < maxGroundAngle)
                {
                    Vector2 inclineVector = horizontalHit.normal.RotateVector(90 * Mathf.Sign(-velocity.x));
                    float inclineDistance = new Vector2(velocity.x * Time.fixedDeltaTime, 0).Projection(inclineVector).magnitude;
                    
                    var inclineHit = Physics2D.BoxCast(
                        bodyCollider.bounds.center,
                        bodyCollider.bounds.size,
                        0,
                        inclineVector,
                        inclineDistance,
                        terrainMask
                    );
                    
                    // 경사면 방향으로 충돌이 없으면 
                    if (!inclineHit)
                    {
                        move.x = velocity.x * Time.fixedDeltaTime;
                    }
                    // 이동예정 거리보다 레이가 짧음. 즉 벽 감지 시(skinWidth가 moveDelta보다 짧을 것)
                    else if (inclineHit.distance > skinWidth)
                    {
                        move.x = horizontalDirection.x * (inclineHit.distance - skinWidth);
                    }
                    
                }
                else if (horizontalHit.distance > skinWidth )
                {
                    // 벽 - 안전 거리까지만 이동
                    move.x = horizontalDirection.x * (horizontalHit.distance - skinWidth);
                }
            }
        }

        // 수직 이동 처리
        if (velocity.y != 0)
        {
            float verticalDistance = Mathf.Abs(velocity.y) * Time.fixedDeltaTime;
            Vector2 verticalDirection = velocity.y > 0 ? Vector2.up : Vector2.down;

            var verticalHit = Physics2D.BoxCast(
                bodyCollider.bounds.center,
                bodyCollider.bounds.size,
                0,
                verticalDirection,
                verticalDistance,
                terrainMask
            );

            if (!verticalHit)
            {
                move.y = velocity.y * Time.fixedDeltaTime;
            }
            else
            {
                float hitAngle = Vector2.Angle(verticalHit.normal, Vector2.up);

                if (hitAngle < maxGroundAngle)
                {
                    // 경사면 - 그대로 이동
                    move.y = velocity.y * Time.fixedDeltaTime;
                }
                else
                {
                    // 천장/바닥 - 안전 거리까지만 이동
                    move.y = verticalDirection.y * Mathf.Max(0, verticalHit.distance - skinWidth);
                }
            }
        }

        // Ground Snap (수직 충돌이 없을 때만)
        if (isGrounded && velocity.y <= 0)
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
                move += Vector2.up * (adjust + skinWidth);
            }
        }

        rb.MovePosition(rb.position + move);
    }

    private void ApplyGravity()
    {
        if (gravityEnabled) velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        if (velocity.y < -maxFallSpeed) velocity.y = -maxFallSpeed;
    }
}
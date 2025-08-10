using System;
using NaughtyAttributes;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField, ReadOnly] private bool isWallOnLeft;
    public bool IsWallOnLeft => isWallOnLeft;
    [SerializeField, ReadOnly] private bool isWallOnRight;
    public bool IsWallOnRight => isWallOnRight;
    
    public float PenetratedDistance { get; private set; }

    [SerializeField] LayerMask groundMask;
    [SerializeField] float skinWidth = 0.02f;

    private void Reset()
    {
        groundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        isWallOnLeft = false;
        isWallOnRight = false;
        CheckSide(Vector2.left);
        CheckSide(Vector2.right);
    }

    public void CheckSide(Vector2 direction)
    {
        float rayDistance = boxCollider.bounds.extents.x + skinWidth;
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, direction, rayDistance, groundMask);
        
        if (!hit.collider || !Mathf.Approximately(Vector2.Angle(hit.normal, Vector2.up), 90f)) return;
        
        if (direction == Vector2.left)
        {
            isWallOnLeft = true;
        }
        else
        {
            isWallOnRight = true;
        }
        
        PenetratedDistance = hit.distance - rayDistance;
    }
}

using UnityEngine;

public class SimpleRotate2D : MonoBehaviour
{
    public float speed = -30f;

    void Update()
    {
        transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
        
    }
}

using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
    }
}
using UnityEngine;

public class RotateSunUI : MonoBehaviour
{
    public float rotationSpeed = 10f; // stepeni u sekundi

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}


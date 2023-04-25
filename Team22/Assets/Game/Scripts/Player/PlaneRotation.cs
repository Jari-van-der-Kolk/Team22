using UnityEngine;

public class PlaneRotation : MonoBehaviour
{
    public float maxRotationX = 45f;
    public float maxRotationZ = 45f;

    private float currentRotationX;
    private float currentRotationZ;

    void Update()
    {
        // Get the current rotation angles
        currentRotationX = transform.rotation.eulerAngles.x;
        currentRotationZ = transform.rotation.eulerAngles.z;

        // Limit the rotation along the X axis
        if (currentRotationX > 180f) currentRotationX -= 360f;
        currentRotationX = Mathf.Clamp(currentRotationX, -maxRotationX, maxRotationX);

        // Limit the rotation along the Z axis
        if (currentRotationZ > 180f) currentRotationZ -= 360f;
        currentRotationZ = Mathf.Clamp(currentRotationZ, -maxRotationZ, maxRotationZ);

        // Apply the new rotation angles
        transform.rotation = Quaternion.Euler(currentRotationX, 0f, currentRotationZ);


    }
}

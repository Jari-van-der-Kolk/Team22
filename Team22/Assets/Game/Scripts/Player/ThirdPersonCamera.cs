using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 10.0f;
    public float height = 2.0f;
    public float smoothSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float zoomSpeed = 2.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = new Vector3(0, height, -distance);
        rotationX = transform.rotation.eulerAngles.y;
        rotationY = transform.rotation.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Apply rotation input from mouse movement
        rotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;
       // rotationY = Mathf.Clamp(rotationY, -60f, 60f);

        // Apply zoom input from mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate camera position and rotation
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + (rotation * offset);
        offset = new Vector3(0, height, -distance);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition + offset, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        // the target to follow
    public float distance = 5.0f;   // distance from target
    public float height = 2.0f;     // height above target
    public float rotationDamping = 3.0f;   // how quickly camera rotates to follow target
    public float heightDamping = 2.0f;     // how quickly camera adjusts its height to follow target
    public float mouseSensitivity = 2.0f;  // how sensitive the mouse movement is
    public float scrollSensitivity = 2.0f; // how sensitive the scroll wheel is

    private float currentRotationAngle = 0.0f;
    private float currentHeight = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        target = FindObjectOfType<Movement>().transform;
    }

    private void LateUpdate()
    {
        if (!target)
            return;

        // get the horizontal mouse input to rotate the camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // rotate the camera horizontally around the target
        currentRotationAngle += mouseX;
        Quaternion rotation = Quaternion.Euler(0.0f, currentRotationAngle, 0.0f);
        transform.position = target.position - (rotation * Vector3.forward * distance);

        // adjust camera height based on the target's position and the mouse scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        height -= scroll;
        height = Mathf.Clamp(height, 0.0f, Mathf.Infinity);
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // look at the target
        transform.LookAt(target);
    }
}

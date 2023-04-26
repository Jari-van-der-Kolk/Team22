using UnityEngine;

public class TiltTowardsHeavierSide : MonoBehaviour
{
    public float tiltSpeed = 5.0f;
    public float maxTiltAngle = 45.0f;
    public float weightThreshold = 0.1f;
    public float minTiltAngle = -45.0f;
    public float boundaryTiltAngle = 30.0f;

    private Rigidbody rb;
    private MeshCollider area;
    private float currentTiltAngle = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        area = GetComponent<MeshCollider>();

    }

    private void Update()
    {
        var boxes = Physics.BoxCastAll(rb.centerOfMass, area.bounds.size, Vector3.up);

        for (int i = 0; i < boxes.Length; i++)
        {
            ApplyWeight(boxes[i].point);
        }
    }

  
    // Method to apply the weight to the center of mass of the plane
    public void ApplyWeight(Vector3 weight)
    {
        rb.AddForceAtPosition(weight, rb.centerOfMass, ForceMode.Impulse);
    }

    // Method to calculate the weight difference between the two sides of the plane
    private float CalculateWeightDifference()
    {
        Vector3 localCenterOfMass = transform.InverseTransformPoint(rb.centerOfMass);
        return rb.centerOfMass.magnitude * Vector3.Dot(rb.worldCenterOfMass.normalized, localCenterOfMass.normalized);
    }

    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody>();
        area = GetComponent<MeshCollider>();
        Gizmos.DrawWireCube(rb.centerOfMass, area.bounds.size);
    }
}

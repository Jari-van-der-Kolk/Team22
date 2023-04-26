using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float forwardAcceleration;

    [SerializeField] private float maxMovement = 6.5f;
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deacceleration = 6f;
    [SerializeField] private float rotationSpeed = 20f;
    float rotate;



    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rotate = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forwardAcceleration = Mathf.MoveTowards(forwardAcceleration, maxMovement, acceleration * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            forwardAcceleration = Mathf.MoveTowards(forwardAcceleration, 0, deacceleration * Time.fixedDeltaTime);
        }

        Vector3 dir = transform.forward * forwardAcceleration * Time.fixedDeltaTime;
        transform.position += dir;
    }

    private void Rotate()
    {
        float rotateInput = Input.GetAxisRaw("Horizontal");
        float rotationMultiplier = rotationSpeed + (forwardAcceleration * 2f);
        rotate += rotateInput * rotationMultiplier * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotate, transform.rotation.eulerAngles.z);
    }


}

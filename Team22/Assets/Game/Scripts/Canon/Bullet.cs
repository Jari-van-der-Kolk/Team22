using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float maxTrackingDistance = 20f;
    public string targetTag = "Seagull";

    [SerializeField] private float _destroyTime;

    private Transform target;

    private void Start()
    {
        // Find the nearest object with the targetTag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float minDistance = Mathf.Infinity;
        foreach (GameObject t in targets)
        {
            float distance = Vector3.Distance(transform.position, t.transform.position);
            if (distance < minDistance && distance < maxTrackingDistance)
            {
                target = t.transform;
                minDistance = distance;
            }
        }
        Destroy(gameObject, _destroyTime);
    }

    private void Update()
    {
        // If the target is destroyed, find a new target
        if (target == null)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            float minDistance = Mathf.Infinity;
            foreach (GameObject t in targets)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);
                if (distance < minDistance && distance < maxTrackingDistance)
                {
                    target = t.transform;
                    minDistance = distance;
                }
            }
        }

        // If we have a target, rotate towards it
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }

        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we hit the target, destroy it
        if (other.gameObject.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

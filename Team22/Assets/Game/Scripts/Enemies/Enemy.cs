using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    roaming, 
    attacking
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private States _states;

    private void Awake()
    {
        _states = States.roaming;
    }

    private void Update()
    {
        switch (_states)
        {
            case States.roaming:
                // Set a random direction and speed for the bird
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                float speed = Random.Range(1f, 5f);

                // Rotate the bird in the direction it is moving
                transform.rotation = Quaternion.LookRotation(direction);

                direction *= 10f;
                // Move the bird in the chosen direction and speed
                transform.position += direction * speed * Time.deltaTime;
                break;
            case States.attacking:
                break;
        }
    }
}

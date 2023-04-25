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
    [SerializeField] private Transform _player;

    [SerializeField] private States _states;
    [SerializeField] private float _movementAmount;
    [SerializeField] private float _movementSpeed = 5f; // adjust this to change the speed of movement
    private Transform _transform;
    private Vector3 _targetPosition;

    private float _distanceThreshold = 0.1f; // adjust this to change how close the enemy needs to get to the target position before choosing a new target

    private void Start()
    {
        _transform = transform;
        ChooseNewRandomTargetPosition();
    }

    private void Update()
    {
        switch (_states)
        {
            case States.roaming:

                _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
                transform.LookAt(_targetPosition);

                if (Vector3.Distance(_transform.position, _targetPosition) < _distanceThreshold)
                {
                    ChooseNewRandomTargetPosition();
                }
                break;

            case States.attacking:

                break;
        }
    }

    private void ChooseNewRandomTargetPosition()
    {
        float x = Random.Range(_player.position.x + (-10f * _movementAmount), _player.position.x + (10f * _movementAmount));
        float y = Random.Range(_player.position.y + (-2f * _movementAmount), _player.position.y + (5f * _movementAmount));
        float z = Random.Range(_player.position.z + (-10f * _movementAmount), _player.position.z + (10f * _movementAmount));

        _targetPosition = new Vector3(x, y, z);
    }
}

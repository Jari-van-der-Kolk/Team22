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
    [SerializeField] private EnemyData _enemyData;
    private Vector3 _targetPosition;

    private float _distanceThreshold = 0.1f; // adjust this to change how close the enemy needs to get to the target position before choosing a new target

    private bool _shootingPosition;

    private void Start()
    {
        ChooseNewRandomTargetPosition();
    }

    private void Update()
    {
        switch (_states)
        {
            case States.roaming:

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _enemyData.Speed * Time.deltaTime);
                transform.LookAt(_targetPosition);

                if (Vector3.Distance(transform.position, _targetPosition) < _distanceThreshold)
                    ChooseNewRandomTargetPosition();

                if (Vector3.Distance(transform.position, _player.position) <= _enemyData.DetectingPlayerDistance)
                    _states = States.attacking;
                break;

            case States.attacking:
                transform.LookAt(_player.position);
                
                break;
        }
    }

    private void ChooseNewRandomTargetPosition()
    {
        float x = Random.Range(_player.position.x + (-10f * _enemyData.MovementAmount), _player.position.x + (10f * _enemyData.MovementAmount));
        float y = Random.Range(_player.position.y + (-2f * _enemyData.MovementAmount), _player.position.y + (5f * _enemyData.MovementAmount));
        float z = Random.Range(_player.position.z + (-10f * _enemyData.MovementAmount), _player.position.z + (10f * _enemyData.MovementAmount));

        _targetPosition = new Vector3(x, y, z);
    }
}

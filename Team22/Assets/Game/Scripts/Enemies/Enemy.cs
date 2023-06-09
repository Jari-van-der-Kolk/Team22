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
    public Transform Player;

    [SerializeField] private States _states;
    [SerializeField] private EnemyData _enemyData;
    private Vector3 _targetPosition;

    private float _distanceThreshold = 0.1f; // adjust this to change how close the enemy needs to get to the target position before choosing a new target

    private Rigidbody _rb;

    private void Start()
    {
        ChooseNewRandomTargetPosition();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!Player) return;

        switch (_states)
        {
            case States.roaming:

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _enemyData.Speed * Time.deltaTime);
                transform.LookAt(_targetPosition);

                if (Vector3.Distance(transform.position, _targetPosition) < _distanceThreshold)
                    ChooseNewRandomTargetPosition();

                if (Vector3.Distance(transform.position, Player.position) <= _enemyData.DetectingPlayerDistance)
                    _states = States.attacking;

                _rb.velocity = Vector3.zero;
                break;

            case States.attacking:
                transform.LookAt(Player.position);
                _rb.AddForce(transform.forward * _enemyData.AttackSpeed, ForceMode.Force);
                if (Vector3.Distance(transform.position, Player.position) >= _enemyData.DetectingPlayerDistance)
                {
                    _states = States.roaming;
                }
                break;
        }
    }

    private void ChooseNewRandomTargetPosition()
    {
        float x = Random.Range(Player.position.x + (-10f * _enemyData.MovementAmount), Player.position.x + (10f * _enemyData.MovementAmount));
        float y = Random.Range(Player.position.y + (-2f * _enemyData.MovementAmount), Player.position.y + (5f * _enemyData.MovementAmount));
        float z = Random.Range(Player.position.z + (-10f * _enemyData.MovementAmount), Player.position.z + (10f * _enemyData.MovementAmount));

        _targetPosition = new Vector3(x, y, z);
    }

    private void OnDestroy()
    {
        Player.GetComponent<SpawningEnemies>().AllEnemies.Remove(gameObject);
    }
}

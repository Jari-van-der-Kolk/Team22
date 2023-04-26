using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemies : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _maxEnemiesToSpawn;
    private Vector3 _spawnPosition;
    private List<GameObject> _allEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {

        ChooseNewRandomSpawnPosition();
        GameObject go = Instantiate(_enemy, _spawnPosition, Quaternion.identity);
        _allEnemies.Add(go);
        go.GetComponent<Enemy>()._player = transform;
        yield return new WaitForSeconds(_spawnTime);
        if (_allEnemies.Count <= _maxEnemiesToSpawn)
            StartCoroutine(SpawnEnemies());

    }
    private void ChooseNewRandomSpawnPosition()
    {
        float x = Random.Range(transform.position.x + (-10f * _spawnDistance), transform.position.x + (10f * _spawnDistance));
        float y = Random.Range(transform.position.y + (-10f * _spawnDistance), transform.position.y + (10f * _spawnDistance));
        float z = Random.Range(transform.position.z + (-10f * _spawnDistance), transform.position.z + (10f * _spawnDistance));

        _spawnPosition = new Vector3(x, y, z);
    }
}

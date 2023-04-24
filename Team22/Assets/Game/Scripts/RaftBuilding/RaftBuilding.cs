using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] public BuildingObject[] _tileObjects;
    
    private int[,] _raftGrid = new int[7, 4];
    private Vector3 _raftOffset = new Vector3(6, 0, 2);

    private void Awake()
    {
        _raftGrid = new int[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
    }

    private void Start()
    {
        LoadRaft();
        BuildRaft(_tileObjects[0] ,new Vector2(0,0));
    }

    private void LoadRaft()
    {
        for (int i = 0; i < _raftGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _raftGrid.GetLength(1); j++)
            {
                if (_raftGrid[i, j] == 1)
                {
                    var x = i * 2;
                    var z = j * 2;
                    var raft = Instantiate(_floorPrefab, new Vector3(x, 0, z) - _raftOffset, Quaternion.identity);
                    raft.transform.parent = transform;
                }
            }
        }
    }

    private void BuildRaft(BuildingObject buildingObject, Vector2 position)
    {
        if (_raftGrid[(int)position.x, (int)position.y] == 0)
        {
            var x = position.x * 2;
            var z = position.y * 2;
            var raft = Instantiate(buildingObject.Prefab, new Vector3(x, 0, z) - _raftOffset, Quaternion.identity);
            raft.transform.parent = transform;
            _raftGrid[(int)position.x, (int)position.y] = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < _raftGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _raftGrid.GetLength(1); j++)
            {
                var x = i * 2 - 6;
                var z = j * 2 - 2;
                Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(2, .5f, 2));
            }
        }   
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBuilding : MonoBehaviour
{
    [SerializeField] GameObject _floorPrefab;
    
    private int[,] _raftGrid = new int[7, 4];
    private Vector3 _raftOffset = new Vector3(6, 0, 4);

    private void Awake()
    {
        _raftGrid = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        };
    }

    private void Start()
    {
        LoadRaft();
    }

    private void LoadRaft()
    {
        for (int i = 0; i < _raftGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _raftGrid.GetLength(1); j++)
            {
                if (_raftGrid[i, j] == 1)
                {
                    var x = j - _raftOffset.x;
                    var z = i - _raftOffset.z;
                    var raft = Instantiate(_floorPrefab, new Vector3(x, 0, z) + _raftOffset, Quaternion.identity);
                    raft.transform.parent = transform;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < _raftGrid.GetLength(1); i++)
        {
            for (int j = 0; j < _raftGrid.GetLength(0); j++)
            {
                var x = i * 2 - _raftOffset.z;
                var z = j * 2 - _raftOffset.x;
                Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(2, .5f, 2));
            }
        }   
    }
}

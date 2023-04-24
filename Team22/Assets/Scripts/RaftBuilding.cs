using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBuilding : MonoBehaviour
{
    private int[,] _raftGrid = new int[7, 4];

    private Vector3 _raftOffset

    private void Awake()
    {
        raftGrid = new int[,]
        {
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0},
            {0,0,0,0,0,0,0},
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateRaft()
    {
        for (int i = 0; i < raftGrid.GetLength(0); i++)
        {
            for (int j = 0; j < raftGrid.GetLength(1); j++)
            {
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < raftGrid.GetLength(0); i++)
        {
            for (int j = 0; j < raftGrid.GetLength(1); j++)
            {
                var x = i * 2 - raftGrid.GetLength(0) + 1;
                var z = j * 2 - raftGrid.GetLength(1) + 2;
                Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(2, .5f, 2));
            }
        }   
    }
}

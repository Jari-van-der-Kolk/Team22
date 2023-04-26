using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject winMenuPrefab;
    
    public static WaypointManager instance;


    public int currentAmount;
    public int reachAmount;


    private void Awake()
    {
        instance = this;
    }

    public void HasReachedAllWaypointsCheck()
    {
        if (currentAmount >= reachAmount)
        {
            Instantiate(winMenuPrefab);
        }
    }
}



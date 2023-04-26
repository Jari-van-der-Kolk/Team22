using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CrateObject", menuName = "ScriptableObjects/CrateObject", order = 1)]
public class CrateObject : ScriptableObject
{
    public string ObjectName;
    public GameObject Prefab;
    public int Cost;
}

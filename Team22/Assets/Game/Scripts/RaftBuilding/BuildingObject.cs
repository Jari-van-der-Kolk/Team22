using UnityEngine;

[CreateAssetMenu(fileName = "New BuildObject", menuName = "BuildObject", order = 0)]
public class BuildingObject : ScriptableObject
{
    public string ObjectName;
    public GameObject Prefab;
    public int Cost;
}

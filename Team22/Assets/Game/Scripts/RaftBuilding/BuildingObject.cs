using UnityEngine;

[CreateAssetMenu(fileName = "New BuildObject", menuName = "ScriptableObjects/BuildObject", order = 1)]
public class BuildingObject : ScriptableObject
{
    public enum PlacementType
    {
        Floor,
        Center,
        Edge,
        Courner
    }

    public string ObjectName;
    public GameObject Prefab;
    public int Cost;
    public PlacementType PlaceType;
    public LayerMask SelectionLayerMask;
    public Vector3 Offset;
}

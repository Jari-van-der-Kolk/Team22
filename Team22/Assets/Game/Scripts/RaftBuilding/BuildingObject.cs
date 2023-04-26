using UnityEngine;

[CreateAssetMenu(fileName = "New BuildObject", menuName = "BuildObject", order = 0)]
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

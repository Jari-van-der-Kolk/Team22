using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private Material _redMaterial;
    private Material _defaultMaterial;
    private MeshRenderer _meshRenderer;
    public bool ShowRed;
    public Vector2Int Position;
    public RaftBuilding.TileType Type;
    public int Cost;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }

    private void Update()
    { 
        if (ShowRed)
        {
            _meshRenderer.material = _redMaterial;
            ShowRed = false;
        }    
        else
        {
            _meshRenderer.material = _defaultMaterial;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPoint : MonoBehaviour
{
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;
    private MeshRenderer _meshRenderer;
    public Vector2Int Position;
    public bool ShowGreen;
    public bool ShowRed;
    public RaftBuilding.TileType Type;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (ShowGreen)
        {
            _meshRenderer.enabled = true;
            ShowGreen = false;
        }
        else if (ShowRed)
        {
            _meshRenderer.enabled = true;
            ShowRed = false;
        }    
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}

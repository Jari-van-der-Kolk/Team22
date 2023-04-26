using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class RaftBuilding : MonoBehaviour
{
    public enum TileType
    {
        Floor,
        Cannon,
        Balloon
    }

    [SerializeField] private Transform _papa;

    [SerializeField] private CratePlacement _cratePlacement;

    [SerializeField] private BuildingObject[] _tileObjects;
    [SerializeField] private TileType _selectedTileType;
    [SerializeField] private int _money;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private LayerMask _selectionLayerMask;

    public bool _itemSelected;
    public bool _removeMode;
    private int[,] _floorGrid = new int[7, 4];
    private int[,] _centerGrid;
    private int[,] _cournerGrid;
    private Vector3 _raftOffset = new Vector3(6, 0, 2);

    private void Awake()
    {
        _floorGrid = new int[7, 4]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 1, 1, 0 },
            { 0, 1, 1, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        _centerGrid = new int[7, 4]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        _cournerGrid = new int[8, 5]
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };
    }

    private void Start()
    {
        LoadRaft();
        UpdateMoney(0);
    }

    private void Update()
    {
        if (_itemSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _tileObjects[(int)_selectedTileType].SelectionLayerMask))
            {
                var tile = hit.transform.GetComponent<SelectionPoint>();
                if (tile.Type == _selectedTileType)
                {
                    bool isAvailable;
                    switch (_selectedTileType)
                    {
                        case TileType.Floor:
                            isAvailable = CheckTileAvailability(true, tile.Position);
                            break;
                        case TileType.Cannon:
                            isAvailable = CheckTileAvailability(false, tile.Position);
                            break;
                        case TileType.Balloon:
                            isAvailable = CheckCournerAvailability(tile.Position);
                            break;
                        default:
                            isAvailable = false;
                            break;
                    }

                    if (isAvailable)
                    {
                        if (_money >= _tileObjects[(int)_selectedTileType].Cost)
                        {
                            tile.ShowGreen = true;
                            if (Input.GetMouseButtonDown(0))
                            {
                                UpdateMoney(-_tileObjects[(int)_selectedTileType].Cost);
                                BuildRaft(_tileObjects[(int)_selectedTileType], tile.Position);
                            }
                        }
                        else
                        {
                            tile.ShowRed = true;
                        }
                    }
                }
            }
        }
        else if (_removeMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _selectionLayerMask))
            {
                var tile = hit.transform.GetComponent<TileObject>();
                if (null != tile && (tile.Position != new Vector2Int(3,1) || tile.Type != TileType.Floor))
                {
                    tile.ShowRed = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        UpdateMoney(tile.Cost);
                        Destroy(tile.gameObject);
                        switch (tile.Type)
                        {
                            case TileType.Floor:
                                _floorGrid[tile.Position.x, tile.Position.y] = 0;
                                break;
                            case TileType.Cannon:
                                _centerGrid[tile.Position.x, tile.Position.y] = 0;
                                break;
                            case TileType.Balloon:
                                _cournerGrid[tile.Position.x, tile.Position.y] = 0;
                                break;
                        }
                    }
                }
            }
        }
    }
    
    private bool CheckTileAvailability(bool isFloor, Vector2Int position)
    {
        if ((_floorGrid[position.x, position.y] == 0) == isFloor)
        {
            if ((position.x > 0 && _floorGrid[position.x - 1, position.y] == 1) ||
               (position.x < _floorGrid.GetLength(0) - 1 && _floorGrid[position.x + 1, position.y] == 1) ||
               (position.y > 0 && _floorGrid[position.x, position.y - 1] == 1) ||
               (position.y < _floorGrid.GetLength(1) - 1 && _floorGrid[position.x, position.y + 1] == 1))
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckCournerAvailability(Vector2Int position)
    {
        if (_cournerGrid[position.x, position.y] == 0)
        {
            var x = position.x;
            var y = position.y;

            if ((x > 0 && y > 0 && _floorGrid[x - 1, y - 1] == 1) || 
               (x < 7 && y > 0 && _floorGrid[x, y - 1] == 1) ||
               (x > 0 && y < 4 && _floorGrid[x - 1, y] == 1) ||
               (x < 7 && y < 4 && _floorGrid[x, y] == 1))
            {
                return true;
            }
        }
        return false;
    }

    private void LoadRaft()
    {
        var grid = _floorGrid;
        for (int k = 0; k < _tileObjects.Length; k++)
        {
            if (k == 1)
                grid = _centerGrid;
            else if (k == 2)
                grid = _cournerGrid;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        var x = i * 2;
                        var z = j * 2;
                        var raft = Instantiate(_tileObjects[k].Prefab, new Vector3(x, 0, z) - _raftOffset + _tileObjects[k].Offset, Quaternion.identity, _papa);
                        raft.GetComponent<TileObject>().Position = new Vector2Int(i, j);
                        raft.transform.parent = _papa;
                    }
                }
            }
        }
    }

    private void BuildRaft(BuildingObject buildingObject, Vector2 position)
    {
        var x = position.x * 2;
        var z = position.y * 2;
        var raft = Instantiate(buildingObject.Prefab, new Vector3(x, 0, z) - _raftOffset + buildingObject.Offset, Quaternion.identity, _papa);
        raft.GetComponent<TileObject>().Position = new Vector2Int((int)position.x, (int)position.y);
        raft.transform.parent = _papa;

        switch (buildingObject.PlaceType)
        {
            case BuildingObject.PlacementType.Floor:
                _floorGrid[(int)position.x, (int)position.y] = 1;
                break;
            case BuildingObject.PlacementType.Center:
                _centerGrid[(int)position.x, (int)position.y] = 1;
                break;
            case BuildingObject.PlacementType.Edge:
                //_edgeGrid[(int)position.x, (int)position.y] = 1;
                break;
            case BuildingObject.PlacementType.Courner:
                _cournerGrid[(int)position.x, (int)position.y] = 1;
                break;
        }
    }

    public void SelectTileType(int index)
    {
        _selectedTileType = (TileType)index;
        _itemSelected = true;
        _cratePlacement._itemSelected = false;
        _removeMode = false;
    }

    public void SelectRemoveMode()
    {
        _itemSelected = false;
        _cratePlacement._itemSelected = false;
        _removeMode = true;
    }
    
    private void UpdateMoney(int value)
    {
        _money += value;
        _moneyText.text = _money.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < _floorGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _floorGrid.GetLength(1); j++)
            {
                var x = i * 2 - 6;
                var z = j * 2 - 2;
                Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(2, .5f, 2));
            }
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePlacement : MonoBehaviour
{
    public bool _itemSelected;

    [SerializeField] private Transform _papa;
    [SerializeField] private RaftBuilding _raftBuilding;
    [SerializeField] private CrateObject[] _crateTypes;
    [SerializeField] private Transform _sampleCrate;
    [SerializeField] private float _tempMoney;
    [SerializeField] private int _selectedCrateType;
    [SerializeField] private LayerMask _selectingLayer; 
    
    void Update()
    {
        if (_itemSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _selectingLayer))
            {
                if (hit.transform.tag.Equals("CratePlaceable"))
                {
                    _sampleCrate.position = hit.point + hit.normal * _sampleCrate.localScale.y / 2;
                    if (_tempMoney >= _crateTypes[_selectedCrateType].Cost &&
                        !Physics.BoxCast(_sampleCrate.position, _sampleCrate.localScale * .49f, _sampleCrate.transform.rotation.eulerAngles))
                    {
                        _sampleCrate.GetComponent<Crate>().ShowGreen = true;
                        if (Input.GetMouseButtonDown(0))
                        {
                            _tempMoney -= _crateTypes[_selectedCrateType].Cost;
                            Instantiate(_crateTypes[_selectedCrateType].Prefab, _sampleCrate.position, Quaternion.identity, _papa);
                        }
                    }
                    else
                    {
                        _sampleCrate.GetComponent<Crate>().ShowRed = true;
                    }
                    
                }
            }
        }
    }
    
    public void SelectCrate(int crateType)
    {
        _itemSelected = true;
        _selectedCrateType = crateType;
        _raftBuilding._itemSelected = false;
        _raftBuilding._removeMode = false;
    }
}

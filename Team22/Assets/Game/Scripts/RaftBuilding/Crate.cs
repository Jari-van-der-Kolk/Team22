using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class Crate : MonoBehaviour
{
    [SerializeField] private MeshRenderer _greenRenderer;
    [SerializeField] private MeshRenderer _redRenderer;
    //[SerializeField] private Material _redMaterial;
    //[SerializeField] private Material _greenMaterial;
    public bool ShowGreen;
    public bool ShowRed;

    private void Update()
    {
        if (ShowGreen)
        {
            _greenRenderer.enabled = true;
            _redRenderer.enabled = false;
            ShowGreen = false;
        }
        else if (ShowRed)
        {
            _redRenderer.enabled = true;
            _greenRenderer.enabled = false;
            ShowRed = false;
        }
        else
        {
            _greenRenderer.enabled = false;
            _redRenderer.enabled = false;
        }
    }
}

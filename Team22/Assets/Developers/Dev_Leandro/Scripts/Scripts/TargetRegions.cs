using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRegions : MonoBehaviour
{
    [SerializeField] public TargetData m_targetData;
    private string m_region;
    private float m_value;

    private void Start()
    {
        m_region = m_targetData.m_targetRegion;
        m_value = m_targetData.m_value;
    }

    public float GetValue()
    {
        return m_value; 
    }
}

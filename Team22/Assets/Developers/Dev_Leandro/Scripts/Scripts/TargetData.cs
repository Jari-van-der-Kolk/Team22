using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TargetData", order = 0)]

public class TargetData : ScriptableObject
{
    public float m_value;
    public string m_targetRegion;
}

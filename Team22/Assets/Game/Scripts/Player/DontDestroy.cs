using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }
}

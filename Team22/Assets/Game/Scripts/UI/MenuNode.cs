using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MenuNode
{
    public KeyCode button;
    public GameObject panel;
    public int priority;
    public bool stopActivity;
    [HideInInspector]public bool activated;

    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;

    public void Activate()
    {
        activateEvent?.Invoke(); 
        panel.SetActive(true);
        activated = true;
    }

    public void Deactivate()
    {
        deactivateEvent?.Invoke();
        panel.SetActive(false);
        activated = false;
    }
}

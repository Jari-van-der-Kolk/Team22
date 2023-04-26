using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaypointManager.instance.currentAmount += 1;
            WaypointManager.instance.HasReachedAllWaypointsCheck();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}

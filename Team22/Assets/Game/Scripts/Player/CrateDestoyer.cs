using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestoyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("gwri0g");
        if (other.CompareTag("Crate") || other.CompareTag("CratePlaceable"))
        {
            Destroy(other.gameObject);
        }
    }
}

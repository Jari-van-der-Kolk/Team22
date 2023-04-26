using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageOnImpact : MonoBehaviour
{
    [SerializeField] private float _defaultDamage;
    private float _impactDamage;

    [SerializeField] private bool destroyOnImpact = true;

    private void Awake()
    {
        _impactDamage = _defaultDamage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable other))
        {
            other.TakeDamage(_impactDamage);

            DestroyOnImpact();
        }

        if (collision.gameObject.GetComponent<IDamagable>() == null)
        {
            DestroyOnImpact();
        }
    }

    private void DestroyOnImpact()
    {
        if (destroyOnImpact)
        {
            Destroy(this.gameObject);
        }
    }
}
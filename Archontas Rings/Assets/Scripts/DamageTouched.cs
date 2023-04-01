using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTouched : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.layer == 10)
        {
            hit.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}

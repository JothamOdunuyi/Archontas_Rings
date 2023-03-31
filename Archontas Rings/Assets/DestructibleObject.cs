using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{

    [SerializeField]
    private GameObject destroyedObj;
    [SerializeField]
    private float health = 1;

    private void Break()
    {
        Instantiate(destroyedObj, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision hit)
    {
        //print(hit.relativeVelocity.magnitude);
        if (hit.gameObject.layer == 10 && hit.relativeVelocity.magnitude > 5)  //Controller layer
        {
            Break();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Break();
        }
    }
   
}

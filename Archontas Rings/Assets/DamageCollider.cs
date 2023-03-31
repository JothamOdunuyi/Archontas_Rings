using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID {

    public class DamageCollider : MonoBehaviour
    {
        [SerializeField]
        Collider damageCollider;

        [SerializeField]
        float damage;

        [SerializeField]
        bool isEnemy;


        private void Awake()
        { 
            damageCollider = damageCollider ? damageCollider : GetComponent<Collider>();
            //damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

            if (gameObject.tag == "PlayerWeapon")
                damage = damage + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }


        
        private void OnTriggerEnter(Collider hit)
        {
            DestructibleObject destructibleObject = hit.GetComponent<DestructibleObject>() ? hit.GetComponent<DestructibleObject>() : null;

            if (isEnemy)
            {
                if (hit.transform.root.tag == "Player")
                {
                    PlayerStats playerStats = hit.transform.root.GetComponent<PlayerStats>();
                    playerStats.TakeDamage(damage);
                }

            }
            else
            {
                if (hit.tag == "Hittable" || hit.transform.root.tag == "Hittable")
                {
                    EnemyStats enemyStats = hit.GetComponent<EnemyStats>() ? hit.GetComponent<EnemyStats>() : hit.transform.root.GetComponent<EnemyStats>();

                    if(enemyStats)
                        enemyStats.TakeDamage(damage);
                }
            }

            if (destructibleObject)
            {
                destructibleObject.TakeDamage(damage);
            }
           
        }
    }

}



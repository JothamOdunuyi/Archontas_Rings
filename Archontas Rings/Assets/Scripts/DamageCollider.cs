using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID {

    public class DamageCollider : MonoBehaviour
    {
        [SerializeField]
        Collider damageCollider;

        [SerializeField]
        float damage = 0;

        [SerializeField]
        bool isEnemy;

        bool hitPlayer;
        List<GameObject> hitEnemies = new List<GameObject>();


        private void Awake()
        { 
            damageCollider = damageCollider ? damageCollider : GetComponent<Collider>();
            //damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

            damage = damage + transform.root.GetComponent<CharacterStats>().damage;
        }

        public void EnableDamageCollider()
        {
            hitEnemies.Clear();
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            hitPlayer = false;
        }


        
        private void OnTriggerEnter(Collider hit)
        {
            DestructibleObject destructibleObject = hit.GetComponent<DestructibleObject>() ? hit.GetComponent<DestructibleObject>() : null;

            if (isEnemy)
            {
                if (hit.transform.root.tag == "Player" && !hitPlayer)
                {
                    hitPlayer = true;
                    PlayerStats playerStats = hit.transform.root.GetComponent<PlayerStats>();
                    playerStats.TakeDamage(damage);
                }

            }
            else
            {
                if (hit.tag == "Hittable" || hit.transform.root.tag == "Hittable")
                {
                    GameObject hitRoot = hit.transform.root.gameObject;
                    EnemyStats enemyStats = hit.GetComponent<EnemyStats>() ? hit.GetComponent<EnemyStats>() : hitRoot.GetComponent<EnemyStats>();

                    if (enemyStats && !hitEnemies.Contains(hitRoot))
                    {
                        hitEnemies.Add(hitRoot);
                        enemyStats.TakeDamage(damage);
                    }
                       
                }
            }
          
            if (destructibleObject)
            {
                destructibleObject.TakeDamage(damage);
            }
           
        }
    }

}



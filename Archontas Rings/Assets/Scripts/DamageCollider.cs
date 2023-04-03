using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID {

    public class DamageCollider : MonoBehaviour
    {
        [SerializeField]
        Collider damageCollider;

        [SerializeField]
        public float damage = 0;

        [SerializeField]
        bool isEnemy;

        [SerializeField]
        bool isBoss;

        bool hitPlayer;
        List<GameObject> hitEnemies = new List<GameObject>();

        AudioManager audioManager;


        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();

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
                //Kept throwing errors because root of the player object is the heriacrhy
                if (hit.GetComponent<PlayerManager>() && !hit.GetComponent<PlayerManager>().canBeHit || hit.transform.root && hit.transform.root.GetComponent<PlayerManager>() && !hit.transform.root.GetComponent<PlayerManager>().canBeHit)
                {
                    print("DODGED AT IFRAME");
                    DisableDamageCollider();
                }
                else
                {
                    if (hit.transform.root.tag == "Player" && !hitPlayer && hit.transform.root.GetComponent<PlayerManager>().canBeHit)
                    {
                        hitPlayer = true;
                        PlayerStats playerStats = hit.transform.root.GetComponent<PlayerStats>();
                        playerStats.TakeDamage(damage);
                        audioManager.PlaySound("Hit Sound", .3f);
                        if(playerStats.health <= 0 && isBoss)
                        {
                            audioManager.PlaySound("Pathetic", gameObject, .8f);
                            isBoss = false;
                        }
                        if (Random.Range(1, 11) == 1)
                        {
                            audioManager.PlaySound("Player Grunt", .1f);
                        }
                    }
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
                        audioManager.PlaySound("Hit Sound", hitRoot);
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



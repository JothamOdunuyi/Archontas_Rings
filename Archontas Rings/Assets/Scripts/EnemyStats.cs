using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KID
{
    public class EnemyStats : CharacterStats
    {
       

        public HealthUI healthUI;
        private GameObject DamageINC;
        Animator animator;
        EnemyManager enemyManager;
        EnemyLocomotionManager enemyLocomotionManager;


        private void Start()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyManager = GetComponent<EnemyManager>();
            animator = GetComponent<Animator>();
            healthUI = GetComponentInChildren<HealthUI>();
            maxHealth = vigor * 50;
            health = maxHealth;
            healthUI.UpdateAllHealth(health, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthUI.SetCurrentHP(health);
           /* DamageINC = Instantiate(damageIndicator, transform.position + new Vector3(-1.21f, 1.88f, 0), Quaternion.identity);
            DamageINC.GetComponent<TextMesh>().text = damage.ToString();
            Destroy(DamageINC, .5f);*/
            /*if (!enemyLocomotionManager && !enemyLocomotionManager.currentTarget)
                enemyLocomotionManager.currentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();*/




            if (health <= 0)
            {
                //animator.Play("Death");
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //gameObject.GetComponent<CapsuleCollider>().enabled = false;

               /* foreach (CapsuleCollider collider in GetComponentsInChildren<CapsuleCollider>())
                {
                    collider.enabled = true;
                }*/
                
               // GetComponent<Collider>().enabled = false;
                animator.enabled = false;
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }

    }
}

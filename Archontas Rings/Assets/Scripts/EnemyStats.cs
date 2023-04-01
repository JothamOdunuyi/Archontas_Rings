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


        private void Start()
        {
            enemyManager = GetComponent<EnemyManager>();
            animator = GetComponent<Animator>();
            healthUI = !healthUI? GetComponentInChildren<HealthUI>() : healthUI ;
            maxHealth = vigor * 50;
            health = maxHealth;
            healthUI.UpdateAllHealth(health, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthUI.SetCurrentHP(health);
            DamageINC = Instantiate(damageIndicator, transform.position + new Vector3(-.5f, 1.88f, 0), Quaternion.identity);
            DamageINC.GetComponent<TextMesh>().text = damage.ToString();
            Destroy(DamageINC, .25f);
          



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
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().GainFlask(1);
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }

    }
}

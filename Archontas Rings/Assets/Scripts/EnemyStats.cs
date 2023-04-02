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
        AudioManager audioManager;
        bool isBoss;
        bool isDead;

        private void Start()
        {
            isBoss = transform.root.name.Contains("Torkan") ? true : false;
            audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
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
          



            if (health <= 0 && !isBoss)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().GainFlask(1);
                if (!isBoss && !isDead)
                {
                    isDead = true;
                    audioManager.PlaySound("Skeleton Death", gameObject, .15f);
                }
            }
            else
            {
                animator.SetTrigger("Hit");
                if(!isBoss && Random.Range(1, 6) == 1)
                {
                    audioManager.PlaySound("Skeleton Hit", .1f);
                }
            }
        }

    }
}

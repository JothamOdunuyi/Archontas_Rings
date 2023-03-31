using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KID
{
    public class PlayerStats : CharacterStats
    {

        public HealthUI healthUI;
        public StaminaUI staminaUI;
        public DeathUI deathUI;
        public AnimationHandler animationHandler;
        public Animator animator;

        public GameObject playerWeapon;

        private void Start()
        {
            playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
            animationHandler = GetComponentInChildren<AnimationHandler>();
            animator = GetComponentInChildren<Animator>();

            maxHealth = vigor * 50;
            health = maxHealth;
            maxStamina = endurance * 10;
            stamina = maxStamina;
            
            healthUI.UpdateAllHealth(health, maxHealth);
            staminaUI.UpdateAllStamina(stamina, maxStamina);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthUI.SetCurrentHP(health);

            if(health <= 0)
            {
                // X.X
                animationHandler.PlayTargetAnimation("Death", true);
                deathUI.PlayDeathUI();
            }
            else
            {
                animator.SetTrigger("Hit");
                //animationHandler.PlayTargetAnimation("Damage", true);
            }
        }

        public void RegenStamina(float delta)
        {
            if (stamina < maxStamina)
            {
                // Timer finished
                if (staminaRechargeTimer <= 0)
                {
                    stamina += staminaRechargeRate * delta;
                    staminaUI.SetCurrentStamina(stamina);
                }
                else // Tick timer
                { 
                    staminaRechargeTimer = Mathf.Clamp(staminaRechargeTimer - 1 * delta, 0, maxStamina);
                }
               
            }
        }

        public void UseStamina(float staminaAmount)
        {
            stamina -= staminaAmount;
            // Reset timer when stmaina used
            staminaRechargeTimer = staminaRechargeTime;

            staminaUI.SetCurrentStamina(stamina);
        }

        public void HealFlask()
        {
            health = Mathf.Clamp(health += maxHealth * .28f, 0, maxHealth);
            healthUI.SetCurrentHP(health);
        }
    }
}

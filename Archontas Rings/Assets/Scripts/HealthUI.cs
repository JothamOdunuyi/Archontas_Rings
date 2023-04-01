using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobStates;

namespace KID
{

    public class HealthUI : MonoBehaviour
    {
        public Slider slider;
        [SerializeField] private bool destoryOnDeath;
        [SerializeField] private bool isEnemy;

        public void UpdateAllHealth(float health, float maxHealth)
        {
            SetMaxHP(maxHealth);
            slider.value = health;
            isEnemy = transform.root.gameObject.layer == LayerMask.NameToLayer("Enemy");

        }

        public void SetMaxHP(float maxHealth)
        {
            slider.maxValue = maxHealth;
        }

        public void SetCurrentHP(float health)
        {
            // Means UI belonds to an enemy, so show HP only when hit
            if(isEnemy && !transform.GetChild(0).gameObject.activeSelf)
                transform.GetChild(0).gameObject.SetActive(true);

            slider.value = health;

         
        }
    }
}


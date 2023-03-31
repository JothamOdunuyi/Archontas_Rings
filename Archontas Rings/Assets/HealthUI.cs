using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            // Set enemy in death state if they are not
            if (isEnemy && health <= 0 && transform.root.GetComponent<StateMachine>().currentState.GetType() != typeof(DeathState)) { 
                transform.GetChild(0).gameObject.SetActive(false);
                transform.root.GetComponent<StateMachine>().currentState = transform.root.GetComponent<DeathState>();
            }
        }
    }
}


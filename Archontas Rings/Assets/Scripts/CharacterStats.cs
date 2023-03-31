using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Weapon")]
        public GameObject weapon;


        [Header("Stat Points")]
        public int vigor = 10;
        public int endurance = 10;

        [Header("Actual Stats")]
        public float maxHealth;
        public float health;
        public float maxStamina;
        public float damage;

        [Header("SubStats")]
        public float stamina;
        public float staminaRechargeTime = 3;
        public float staminaRechargeTimer;
        public float staminaRechargeRate = 5;

        [Header("Others")]
        public GameObject damageIndicator;



    }
}
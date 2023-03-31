using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] public GameObject damageIndicator;

        [Header("Stat Points")]
        public int vigor = 10;
        public int endurance = 10;

        [Header("Actual Stats")]
        public float maxHealth;
        public float health;

        public float maxStamina;
        public float stamina;
        public float staminaRechargeTime = 3;
        public float staminaRechargeTimer;
        public float staminaRechargeRate = 5;

        public float damage;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KID
{

    public class StaminaUI : MonoBehaviour
    {
        public Slider slider;

        public void UpdateAllStamina(float stamina, float maxstamina)
        {
            SetMaxStamina(maxstamina);
            SetCurrentStamina(stamina);
        }

        public void SetMaxStamina(float maxStamina)
        {
            slider.maxValue = maxStamina;
        }

        public void SetCurrentStamina(float stamina)
        {
            slider.value = stamina;
        }
    }
}


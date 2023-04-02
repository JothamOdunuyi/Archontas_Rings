using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    public class DeathUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup backgroundCanvasGroup;
        [SerializeField]
        private CanvasGroup textCanvasGroup;
        [SerializeField]
        private TextMeshProUGUI deathText;

        private void Awake()
        {
            backgroundCanvasGroup.alpha = 0;
            textCanvasGroup.alpha = 0;
        }

        private IEnumerator fadeIn (CanvasGroup canvasGroup, float seconds, float delay)
        {
            if(delay > 0)
                yield return new WaitForSeconds(delay);

            while (canvasGroup.alpha != 1)
            {
                canvasGroup.alpha += 0.1f;
                yield return new WaitForSeconds(seconds);
            }
        }
        public void PlayDeathUI()
        {
            StartCoroutine(fadeIn(backgroundCanvasGroup, 0.08f, 0f));
            StartCoroutine(fadeIn(textCanvasGroup, 0.02f, .2f));
        }
    }
}


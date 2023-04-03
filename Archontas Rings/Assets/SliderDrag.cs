using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using KID;
using UnityEngine.UI;

public class SliderDrag : MonoBehaviour, IPointerUpHandler
{
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        if(gameObject.name == "Volume")
        {
            AudioListener.volume = gameObject.GetComponent<Slider>().value;
        }
    }

    // Not my code
    public void OnPointerUp(PointerEventData eventData)
    {
        audioManager.PlaySound("Button Click", .7f);
    }
}
using KID;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    GameObject player;
    GameObject pauseCanvas;
    AudioManager audioManager;
    CameraHandler cameraHandler;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pauseCanvas = GameObject.Find("Pause Canvas");
        audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        cameraHandler = GameObject.Find("CameraHolder").GetComponent<CameraHandler>();
    }

    public void Button_Resume()
    {
        audioManager.PlaySound("Button Click", .7f);
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void Button_MainMenu()
    {
        audioManager.PlaySound("Button Click", .7f);
        SceneManager.LoadScene("Main Menu");
    }

    public void Button_Play()
    {
        audioManager.PlaySound("Button Click", .7f);
        SceneManager.LoadScene("Game Scene");
    }

    public void Button_Quit()
    {
        Application.Quit();
    }

    public void Show_Frame(GameObject frame)
    {
        audioManager.PlaySound("Button Click", .7f);
        frame.SetActive(true);
    }

    public void Hide_Frame(GameObject frame)
    {
        audioManager.PlaySound("Button Click", .7f);
        frame.SetActive(false);
    }

    public void Set_MouseSensitivity(Slider slider)
    {
        cameraHandler.lookSpeed = slider.value;
    }

    public void Set_Volume(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    public void SetDifficulty_Easy()
    {
        audioManager.PlaySound("Button Click", .7f);
        player.GetComponent<PlayerStats>().weapon.GetComponent<DamageCollider>().damage = 400;
        GameObject.FindGameObjectWithTag("Difficulty").GetComponent<TextMeshProUGUI>().text = "difficulty: easy";
    }

    public void SetDifficulty_Normal()
    {
        audioManager.PlaySound("Button Click", .7f);
        player.GetComponent<PlayerStats>().weapon.GetComponent<DamageCollider>().damage = 200;
        GameObject.FindGameObjectWithTag("Difficulty").GetComponent<TextMeshProUGUI>().text = "difficulty: normal";
    }

    public void SetDifficulty_Hard()
    {
        audioManager.PlaySound("Button Click", .7f);
        player.GetComponent<PlayerStats>().weapon.GetComponent<DamageCollider>().damage = 100;
        GameObject.FindGameObjectWithTag("Difficulty").GetComponent<TextMeshProUGUI>().text = "difficulty: hard";
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;
    void Start()
    {
    }

    private AudioClip FindSound(string soundName)
    {
        if (audioClips.Any(audio => audio.ToString().Contains(soundName)))
        {
            foreach (AudioClip audio in audioClips)
            {
                if (audio.name == soundName)
                {
                    return audio;
                }
            }
            return null;
        }
        else
        {
            Debug.LogWarning("Audio name {" + soundName + "} not found");
            return null;
        }
    }

    public void PlaySound(string soundName)
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = FindSound(soundName);

        // Not DRY incase audioSource gets overrwriten by other function
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }

    public void PlaySound(string soundName, float volume)
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = FindSound(soundName);

        // Not DRY incase audioSource gets overrwriten by other function
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }

    public void PlaySound(string soundName, GameObject audioGameObject)
    {
        audioSource = audioGameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = FindSound(soundName);

        // Not DRY incase audioSource gets overrwriten by other function
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }

    public void PlaySound(string soundName, GameObject audioGameObject, float volume)
    {
        audioSource = audioGameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = FindSound(soundName);

        // Not DRY incase audioSource gets overrwriten by other function
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }

    IEnumerator audioDestroy(AudioSource audioSourceToDestroy)
    {
        while (audioSourceToDestroy && audioSourceToDestroy.isPlaying)
        {
            yield return new WaitForSeconds(.1f);
        }
        Destroy(audioSourceToDestroy);
    }
}

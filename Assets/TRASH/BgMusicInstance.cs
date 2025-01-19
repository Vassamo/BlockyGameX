using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BgMusicInstance : MonoBehaviour
{
    public static BgMusicInstance Instance;

    public AudioMixerGroup AudioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Utrzymuje obiekt przy zmianie scen
        }
        else
        {
            Destroy(gameObject); // Zniszczy duplikaty
        }
    }

    public void SetVolume(float volume)
    {
        AudioMixer.audioMixer.SetFloat("BgMusicVolume", volume);
        Debug.Log("w!");
    }
    public void SetHighpass(float val)
    {
        AudioMixer.audioMixer.SetFloat("BGHighPass", val);
    }

}

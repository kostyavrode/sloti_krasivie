using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSoundChecker : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Sound");
        audioSource2.volume = PlayerPrefs.GetFloat("Music");
    }
}

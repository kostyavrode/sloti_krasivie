using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSettings : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetFloat("Sound", 0.5f);
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.Save();
        }
        slider2.value = PlayerPrefs.GetFloat("Sound");
        slider1.value = PlayerPrefs.GetFloat("Music");
    }
    public void SetSoundLevel(float t)
    {
        PlayerPrefs.SetFloat("Sound", t);
    }
    public void SetMusicLevel(float t)
    {
        PlayerPrefs.SetFloat("Music", t);
    }
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}

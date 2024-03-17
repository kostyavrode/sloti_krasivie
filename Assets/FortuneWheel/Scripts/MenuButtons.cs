using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("score"))
        {
            ResetScore();
        }
    }
    public void LoadFortuneWheel()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadCoinFlip()
    {
        SceneManager.LoadScene(3);
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("score", 1000);
        PlayerPrefs.Save();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }
}

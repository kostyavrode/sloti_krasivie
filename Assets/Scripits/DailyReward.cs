using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DailyReward : MonoBehaviour
{
    public Button[] daysButtons;
    public GameObject collectButton;
    private int localRow=0;
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("lastday"))
        {

            PlayerPrefs.SetInt("row", 0);
            PlayerPrefs.Save();
        }
        else
        {
            localRow = PlayerPrefs.GetInt("row");
            CheckDay();
        }
    }
    private void CheckDay()
    {
        if (DateTime.Now.Day!=PlayerPrefs.GetInt("lastday"))
        {
            collectButton.SetActive(true);
            daysButtons[localRow].interactable = true;
            PlayerPrefs.SetInt("lastday", DateTime.Now.Day);
            PlayerPrefs.Save();
        }
        else
        {
            foreach(Button gObj in daysButtons)
            {
                gObj.interactable = false;
            }
            collectButton.SetActive(false);
        }
    }
    public void CollectReward()
    {
        switch (localRow)
        {
            case 0:
                PlayerPrefs.SetInt("lastday", DateTime.Now.Day);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 150);
                PlayerPrefs.SetInt("row", PlayerPrefs.GetInt("row") + 1);
                PlayerPrefs.Save();
                break;
            case 1:
                PlayerPrefs.SetInt("lastday", DateTime.Now.Day);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 300);
                PlayerPrefs.SetInt("row", PlayerPrefs.GetInt("row") + 1);
                PlayerPrefs.Save();
                break;
            case 2:
                PlayerPrefs.SetInt("lastday", DateTime.Now.Day);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 450);
                PlayerPrefs.SetInt("row", PlayerPrefs.GetInt("row") + 1);
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetInt("lastday", DateTime.Now.Day);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 600);
                PlayerPrefs.SetInt("row", PlayerPrefs.GetInt("row") + 1);
                PlayerPrefs.Save();
                break;
            default:
                break;
        }
        CheckDay();
    }
}
